using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace vega.Controllers.Resources
{
    public class PlanningAppResource
    {
        public int CustomerId { get; set; }
        //public String StateName { get; set; }
        // public DateTime DueByDate { get; set; }
        // public DateTime? DateCompleted { get; set; }
        // public String StateStatus { get; set; }
        public string CurrentStateStatus { get; set; }
        public string CurrentState { get; set; }
        public string NextState { get; set; }

        // public ICollection<KeyValuePairResource> states { get; set; }
        // public PlanningAppResource()
        // {
        //      = new Collection<KeyValuePairResource>();
        // }

        public ICollection<PlanningAppStateResource> PlanningAppStates { get; set; }

        public PlanningAppResource()
        {
            PlanningAppStates = new Collection<PlanningAppStateResource>();
        }
    }
}