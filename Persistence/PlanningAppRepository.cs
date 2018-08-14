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

            //var stateInitialiser = await stateInitialiserRepository.GetStateInitialiser(planningApp.StateInitialiserId, includeDeleted: false);

            // var stateInitialiser = vegaDbContext.StateInitialisers
            //                 .Where(s => s.Id == planningApp.StateInitialiserId)
            //                     .Include(t => t.States)
            //                     .SingleOrDefault();

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
                                    .SingleOrDefault();

                //sort planing states using 
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

            var planningStatusSelectorMap = new Dictionary<string, Expression<Func<PlanningApp, bool>>>()
            {
                [StatusList.AppInProgress] = pa => pa.CurrentPlanningStatus.Name == StatusList.AppInProgress,
                [StatusList.AppArchived] = pa => pa.CurrentPlanningStatus.Name == StatusList.AppArchived,
                [StatusList.AppTerminated] = pa => pa.CurrentPlanningStatus.Name == StatusList.AppTerminated
            };

            if(queryObj.PlanningAppType == StatusList.AppInProgress) {
                var queryList = query.Where(planningStatusSelectorMap[queryObj.PlanningAppType]).ToList();

                List<String> statusList = new List<String>();

                if(queryObj.StateStatus > 0) {
                        var stateStatus = stateStatusRepository.GetStateStatus(queryObj.StateStatus);
                        statusList.Add(stateStatus.Name);
                }
                else if(queryObj.PlanningAppStatusType == null)
                    statusList.AddRange(new List<String> { StatusList.Overdue, StatusList.Due, StatusList.OnTime} );
                else 
                    statusList.Add(queryObj.PlanningAppStatusType);

                //Generate the list
                List<PlanningApp> planningAppList = new List<PlanningApp>();
                foreach ( var status in statusList) {
                    planningAppList.AddRange(queryList.Where(p => p.Current().DynamicStateStatus() == status)
                                                    .OrderBy(p => p.Current().DueByDate)
                                                    .ToList());
                }
                query = planningAppList.AsQueryable();
            }
            else 
                {
                if(queryObj.PlanningAppType != "All") 
                    query = query.AsQueryable().Where(p => p.CurrentPlanningStatus.Name == queryObj.PlanningAppType);
                }

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

        //private List<PlanningApp> buildPlanningAppList()
        public PlanningApp UpdatePlanningApp(PlanningApp planningApp)
        {
            vegaDbContext.Update(planningApp);

            return planningApp;
        }
    }
}