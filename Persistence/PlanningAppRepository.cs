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
        public PlanningAppRepository(VegaDbContext vegaDbContext, IOptionsSnapshot<StateStatusSettings> options)
        {
            this.vegaDbContext = vegaDbContext;
            stateStatusSettings = options.Value;
        }

        public StateStatusSettings stateStatusSettings { get; }

        public void Add(PlanningApp planningApp)
        {
            var stateInitialiser = vegaDbContext.StateInitialisers
                            .Where(s => s.Id == planningApp.StateInitialiserId)
                                .Include(t => t.States)
                                .SingleOrDefault();

            //Important to keep order of states as they can be added and removed - 
            //EF core cant do Include(t => t.States.Orderby)
            var orderedStates = stateInitialiser.States.OrderBy(o => o.OrderId); 
            var initialStatus = vegaDbContext.StateStatus.Where(s => s.Name == stateStatusSettings.STATE_ON_TIME).SingleOrDefault();
            var initialStatusList  = vegaDbContext.StateStatus.ToList();
            //TODO!!!!!! Move out of repository!!!!
            planningApp.GeneratePlanningStates(orderedStates, initialStatusList);
            vegaDbContext.Add(planningApp);   
        }

        public async Task<PlanningApp> GetPlanningApp(int id, bool includeRelated = true)
        {
            if(!includeRelated) {
                return await vegaDbContext.PlanningApps.FindAsync(id);
            }
            else {
                return await vegaDbContext.PlanningApps
                                .Where(s => s.Id == id)
                                    .Include(b => b.CurrentPlanningStatus) 
                                    .Include(t => t.PlanningAppStates)
                                        .ThenInclude(s => s.state) 
                                    .Include(t => t.PlanningAppStates)
                                        .ThenInclude(a => a.StateStatus) 
                                    .SingleOrDefaultAsync();

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
                                .AsQueryable();

            //TODO Later!!!! Use IQuerable for Customer lookup

            if(queryObj.PlanningAppType==null) {
                queryObj.PlanningAppType = StatusList.AppInProgress;
            }

            var planningStatusSelectorMap = new Dictionary<string, Expression<Func<PlanningApp, bool>>>()
            {
                [StatusList.AppInProgress] = pa => pa.CurrentPlanningStatus.Name == StatusList.AppInProgress,
                [StatusList.AppArchived] = pa => pa.CurrentPlanningStatus.Name == StatusList.AppArchived,
                [StatusList.AppTerminated] = pa => pa.CurrentPlanningStatus.Name == StatusList.AppTerminated
            };

            var queryList = query.Where(planningStatusSelectorMap[queryObj.PlanningAppType]).ToList();

            if(queryObj.PlanningAppType == StatusList.AppInProgress) {
                List<String> statusList = new List<String>();
                if(queryObj.PlanningAppStatusType == null)
                    statusList.AddRange(new List<String> { StatusList.Overdue, StatusList.Due, StatusList.OnTime} );
                else 
                    statusList.Add(queryObj.PlanningAppStatusType);

                List<PlanningApp> planningAppList = new List<PlanningApp>();
                foreach ( var status in statusList) {
                    planningAppList.AddRange(queryList.Where(p => p.Current().DynamicStateStatus() == status)
                                                    .OrderBy(p => p.Current().DueByDate)
                                                    .ToList());
                }
                query = planningAppList.AsQueryable();
            }
            else 
                query = queryList.AsQueryable();
            

            result.TotalItems =  query.Count();
            query = query.ApplyPaging(queryObj);

            //result.Items = await query.ToListAsync();
            result.Items = query.ToList();
            return result;
        }

        //private List<PlanningApp> buildPlanningAppList()
        public PlanningApp UpdatePlanningApp(PlanningApp planningApp)
        {
            vegaDbContext.Update(planningApp);

            return planningApp;
        }
    }
}