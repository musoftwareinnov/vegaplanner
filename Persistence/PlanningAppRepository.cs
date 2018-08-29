using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vega.Core.Models;
using vega.Core;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System;
using vega.Extensions;
using vega.Core.Models.States;
using Microsoft.Extensions.Options;

namespace vega.Persistence
{
    public class PlanningAppRepository : IPlanningAppRepository
    {
        private readonly VegaDbContext vegaDbContext;
        private readonly IStateInitialiserRepository stateInitialiserRepository;


        Dictionary<string, Expression<Func<PlanningApp, bool>>> planningStatusSelectorMap = new Dictionary<string, Expression<Func<PlanningApp, bool>>>()
            {
                [StatusList.AppInProgress] = pa => pa.CurrentPlanningStatus.Name == StatusList.AppInProgress,
                [StatusList.AppArchived] = pa => pa.CurrentPlanningStatus.Name == StatusList.AppArchived,
                [StatusList.AppTerminated] = pa => pa.CurrentPlanningStatus.Name == StatusList.AppTerminated,
                [StatusList.Complete] = pa => pa.CurrentPlanningStatus.Name == StatusList.Complete
            };

        Dictionary<string, Func<PlanningApp, bool>> stateStatusSelectorMap = new Dictionary<string, Func<PlanningApp, bool>>()
            {
                [StatusList.Overdue] = pa => pa.Current().DynamicStateStatus() == StatusList.Overdue,
                [StatusList.Due] = pa => pa.Current().DynamicStateStatus() == StatusList.Due,
                [StatusList.OnTime] = pa => pa.Current().DynamicStateStatus() == StatusList.OnTime,
            };

        public PlanningAppRepository(VegaDbContext vegaDbContext, 
                                     IStateStatusRepository stateStatusRepository,
                                     IStateInitialiserRepository stateInitialiserRepository,
                                     IOptionsSnapshot<StateStatusSettings> options)
        {
            this.vegaDbContext = vegaDbContext;
            this.stateStatusRepository = stateStatusRepository;
            this.stateInitialiserRepository = stateInitialiserRepository;
            stateStatusSettings = options.Value;
        }

        public StateStatusSettings stateStatusSettings { get; }

        private IStateStatusRepository stateStatusRepository { get; set; }
 
        public void Add(PlanningApp planningApp, StateInitialiser stateInitialiser)
        {

            var initialStatus = vegaDbContext.StateStatus.Where(s => s.Name == stateStatusSettings.STATE_ON_TIME).SingleOrDefault();
            var initialStatusList  = vegaDbContext.StateStatus.ToList();
            
            planningApp = planningApp.GeneratePlanningStates(stateInitialiser.States, initialStatusList);
            vegaDbContext.Add(planningApp);   
        }

        public async Task<PlanningApp> GetPlanningApp(int id, bool includeRelated = true)
        {
            if(!includeRelated) {
                return await vegaDbContext.PlanningApps.FindAsync(id);
            }  
            else {
                var sortStates =  vegaDbContext.PlanningApps
                                .Where(s => s.Id == id)
                                    .Include(b => b.CurrentPlanningStatus)
                                    .Include(t => t.PlanningAppStates)
                                        .ThenInclude(s => s.state) 
                                    .Include(t => t.PlanningAppStates)
                                        .ThenInclude(a => a.StateStatus)
                                    .Include(c => c.Customer)
                                    .Include(g => g.StateInitialiser)
                                    .SingleOrDefault();

                //sort planing states using 
                if(sortStates != null)
                    sortStates.PlanningAppStates = sortStates.PlanningAppStates.OrderBy(o => o.state.OrderId).ToList();

                return sortStates;
            }            
        }

        public QueryResult<PlanningApp> GetPlanningApps(PlanningAppQuery queryObj)
        {
            var result = new QueryResult<PlanningApp>();
            var resList = new List<PlanningApp>();

            var query =  vegaDbContext.PlanningApps
                                .Include(b => b.CurrentPlanningStatus) 
                                .Include(t => t.PlanningAppStates)
                                    .ThenInclude(a => a.StateStatus)
                                .Include(t => t.PlanningAppStates)
                                    .ThenInclude(s => s.state)
                                .Include(c => c.Customer)
                                .AsQueryable();

            if(queryObj.CustomerId > 0)
                query = query.Where(c => c.Customer.Id == queryObj.CustomerId);
 
            if(queryObj.PlanningAppType==null) {
                queryObj.PlanningAppType = StatusList.AppInProgress;
            }

            //Build up list of planning apps
            List<PlanningApp> planningAppSelectList = new List<PlanningApp>();

            var statusList = stateStatusRepository.GetStateStatusListGroup(queryObj.PlanningAppType);

            if(statusList.First().GroupType == StatusList.AppInProgress) {
                var appsInProgress = query.Where(pa => pa.CurrentPlanningStatus.Name == StatusList.AppInProgress).ToList();
                foreach(var status in statusList) { 
                    planningAppSelectList.AddRange(appsInProgress.Where(pa => pa.Current().DynamicStateStatus() == status.Name)
                                         .OrderBy(o => o.Current().DueByDate));
                }
            }
            else {
                //Get a list off all apps that are not in progress - ie, Completed/Archived/Terminated
                foreach(var status in statusList) { 
                        planningAppSelectList.AddRange(query.Where(pa => pa.CurrentPlanningStatus.Name == status.Name)
                                         .OrderByDescending(o => o.Id));
                }
            }


            // if(queryObj.PlanningAppType == StatusList.AppInProgress) {
            //     planningAppSelectList = generateInProgressList(query);  
            // }
            // else if(stateStatusSelectorMap.ContainsKey(queryObj.PlanningAppType)) {
            //         var inProgress = query.Where(planningStatusSelectorMap[StatusList.AppInProgress]).ToList();
            //         planningAppSelectList.AddRange(inProgress.Where(stateStatusSelectorMap[queryObj.PlanningAppType]).OrderBy(p => p.Current().DueByDate).ToList());
            //     }
            // else if (planningStatusSelectorMap.ContainsKey(queryObj.PlanningAppType)){
            //         query = query.Where(planningStatusSelectorMap[queryObj.PlanningAppType]);
            //         planningAppSelectList.AddRange( query.Where(planningStatusSelectorMap[queryObj.PlanningAppType]).OrderByDescending(p => p.Id));
            //     }
            // else if(queryObj.PlanningAppType == "All") {
            //         planningAppSelectList = query.OrderByDescending(p => p.Id).ToList();
            // }  

            query = planningAppSelectList.AsQueryable();
            result.TotalItems =  query.Count();
            query = query.ApplyPaging(queryObj); 
            result.Items = query.ToList();
            return result;
        }

        public List<PlanningApp> GetPlanningAppsUsingGenerator(int generatorId, bool inProgress = true)
        {
            return  vegaDbContext.PlanningApps
                                .Where(p => p.StateInitialiserId == generatorId && p.CurrentPlanningStatus.Name == "InProgress")
                                .Include(b => b.CurrentPlanningStatus) 
                                .Include(t => t.PlanningAppStates)
                                    .ThenInclude(a => a.StateStatus)
                                .Include(t => t.PlanningAppStates)
                                    .ThenInclude(s => s.state)
                                .ToList();
        }

        public PlanningApp UpdatePlanningApp(PlanningApp planningApp)
        {
            vegaDbContext.Update(planningApp);

            return planningApp;
        }
    }
}