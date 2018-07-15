using System.Collections.Generic;
using System.Collections.ObjectModel;
using vega.Core.Models.Generic;
using vega.Core.Models.States;

namespace vega.Core.Models
{
    public class PlanningApp : IdNameProperty
    {

        public int CustomerId { get; set; }
        
        public int StateInitialiserId { get; set; }

        public StateInitialiser StateInitialiser { get; set; }

        // public int CurrentStateId { get; set; }
        // public StateInitialiserState CurrentState { get; set; }

        // public int NextStateId { get; set; }
        // public StateInitialiserState NextState { get; set; }


        public ICollection<PlanningAppState> PlanningAppStates { get; set; }

        public PlanningApp()
        {
            PlanningAppStates = new Collection<PlanningAppState>();

        }
    }
}