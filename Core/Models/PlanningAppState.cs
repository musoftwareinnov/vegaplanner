using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using vega.Core.Models.Generic;
using vega.Core.Models.States;
using vega.Core.Utils;
using vega.Extensions.DateTime;

namespace vega.Core.Models
{
    public class PlanningAppState
    {

        public int Id { get; set; }
        public int StateInitialiserStateId { get; set; }
        public StateInitialiserState state { get; set; }
        public DateTime DueByDate { get; set; }

        public DateTime? CompletionDate { get; set; }

        public int StateStatusId { get; set; }
        public StateStatus StateStatus { get; set; }
   
        public bool CurrentState { get; set; }
        public StateStatusSettings Options { get; }

   
        public PlanningAppState()
        {
             
        }
        /* Helper Methods  */
        public int CompleteState(DateTime completionDate, List<StateStatus> stateStatusList) 
        {
            CurrentState = false;
            CompletionDate = completionDate;

            if(CompletionDate > DueByDate)
                StateStatus = stateStatusList.Where(s => s.Name == StatusList.Overran).SingleOrDefault();
            else 
                StateStatus = stateStatusList.Where(s => s.Name == StatusList.Complete).SingleOrDefault();

            return DateTime.Compare(CompletionDate.Value, DueByDate); 
        }
   
        public string DynamicStateStatus() {
            var alertDate = DueByDate.AddBusinessDays(state.AlertToCompletionTime * -1);
            
            var CurrentDate = CurrentDateSingleton.setDate(DateTime.Now).getCurrentDate();

            if(CompletionDate == null  )
            {
                if(CurrentDate > DueByDate)
                    return StatusList.Overdue;
                else if (CurrentDate >= alertDate && CurrentDate <= DueByDate)
                    return StatusList.Due;          
                else   
                    return StatusList.OnTime;
            }
            return StateStatus.Name;
        }
    }
}