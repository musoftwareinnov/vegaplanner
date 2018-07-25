using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using vega.Core.Models;

namespace vega.Core
{
    public interface IPlanningAppRepository
    {
        void Add(PlanningApp planningApp);
        Task<PlanningApp> GetPlanningApp(int id, bool includeRelated = true);
        Task<IEnumerable<PlanningApp>> GetPlanningApps(bool includeRelated = true);
        PlanningApp UpdatePlanningApp(PlanningApp planningApp);
    }
}