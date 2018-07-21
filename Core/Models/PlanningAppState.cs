using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using vega.Core.Models.Generic;
using vega.Core.Models.States;

namespace vega.Core.Models
{
    public class PlanningAppState
    {
        public readonly string OnTime =  "OnTime";
        public readonly string Due =  "Due";
        public readonly string Overdue =  "Overdue";
        public readonly string Complete =  "Complete";
        public readonly string Overran =  "Overran";
        public readonly string Error =  "Error";

        public int Id { get; set; }
        public int StateInitialiserStateId { get; set; }
        public StateInitialiserState state { get; set; }
        public DateTime DueByDate { get; set; }

        public DateTime? CompletionDate { get; set; }

        public int StateStatusId { get; set; }
        public StateStatus StateStatus { get; set; }
   
        public bool CurrentState { get; set; }
        public StateStatusSettings Options { get; }

        /* Helper Methods  */
        public void CloseOutState(DateTime completionDate, List<StateStatus> stateStatusList) 
        {
            CurrentState = false;
            CompletionDate = completionDate;

            if(CompletionDate > DueByDate)
                StateStatus = stateStatusList.Where(s => s.Name == Overran).SingleOrDefault();
            else 
                StateStatus = stateStatusList.Where(s => s.Name == Complete).SingleOrDefault();
        }

        //REFACTOR THIS CODE !!!!
        public void ReOpenState(DateTime completionDate, List<StateStatus> stateStatusList) 
        {
            CurrentState = true;
            CompletionDate = null;

            if(DueByDate >= completionDate)
                  StateStatus = stateStatusList.Where(s => s.Name == OnTime).SingleOrDefault();
            else 
                  StateStatus = stateStatusList.Where(s => s.Name == Overdue).SingleOrDefault();
        }

        public void ReSetState(DateTime completionDate, List<StateStatus> stateStatusList) 
        {
            CurrentState = false;
            CompletionDate = null;

            if(DueByDate >= completionDate)
                  StateStatus = stateStatusList.Where(s => s.Name == OnTime).SingleOrDefault();
            else 
                  StateStatus = stateStatusList.Where(s => s.Name == Overdue).SingleOrDefault();
        }


    }
}