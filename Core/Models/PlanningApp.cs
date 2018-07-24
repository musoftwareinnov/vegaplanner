using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using vega.Core.Models.Generic;
using vega.Core.Models.States;
using vega.Core.Utils;
using vega.Extensions.DateTime;

namespace vega.Core.Models
{
    public class PlanningApp : IdNameProperty
    {

        public int CustomerId { get; set; }
        
        public int StateInitialiserId { get; set; }

        public StateInitialiser StateInitialiser { get; set; }

        // public int CurrentStateId { get; set; }
        // public PlanningAppState CurrentPlanningState { get; set; }

        // public int NextStateId { get; set; }
        // public PlanningAppState NextPlanningState { get; set; }
        public DateTime CurrentStateCompletionDate { get; set; }

        public IList<PlanningAppState> PlanningAppStates { get; set; }

        public PlanningApp()
        {
            PlanningAppStates = new List<PlanningAppState>();

        }

        public void GeneratePlanningStates(IOrderedEnumerable<StateInitialiserState> stateInitialisers, StateStatus initialStatus) 
        {
            var currentDate = CurrentDateSingleton.setDate(DateTime.Now).getCurrentDate();

            foreach(var stateInialiser in stateInitialisers) {
                PlanningAppState newState = new PlanningAppState();
                newState.state = stateInialiser;

                PlanningAppState prevState;
                var stateCount = PlanningAppStates.Count;
                if(stateCount > 0) {
                    prevState = PlanningAppStates[stateCount-1];
                    newState.DueByDate =  prevState.DueByDate.AddBusinessDays(stateInialiser.CompletionTime);
                }
                else
                    newState.DueByDate = currentDate.AddBusinessDays(stateInialiser.CompletionTime);

                newState.StateStatus = initialStatus;
                PlanningAppStates.Add(newState);
            }
            //set first state to current state
            if(PlanningAppStates.Count > 0)
                 PlanningAppStates[0].CurrentState = true;
        }

        public void NextState(List<StateStatus> statusList)
        {

            if(!Completed()) {

                var currentDate = CurrentDateSingleton.setDate(DateTime.Now).getCurrentDate();
                var current = Current(); 
                if(!isLastState(current)) {
                        Next().CurrentState = true;   //move to next state
                }                   
                current.CompleteState(currentDate, statusList );  
                var daysDiff = current.DueByDate.GetBusinessDays(currentDate, new List<DateTime>());

                if(daysDiff > 0) {              
                    //If Overran then roll all future completion dates by business days overdue
                    var statesToUpdate = UpdateDueByDates(daysDiff);  
                }
                
            }
        }

        //Currently Not used = business input required
        public void PrevState(List<StateStatus> statusList)
        {
            
            var current = Current();
            if(current != null)   //Cannot Role back a completed application
            {
                if(!isFirstState(current))
                {
                    var prevState = Prev();
                    //daysDiff is used to reset the future DueByStates to initial value
                    var daysDiff = prevState.DueByDate.GetBusinessDays(prevState.CompletionDate.Value, new List<DateTime>());

                    Prev().ReOpenState(CurrentStateCompletionDate, statusList);  //IMPROVE ON THIS!!!!
                    current.CloseState(CurrentStateCompletionDate, statusList);

                    if(daysDiff > 0)  {
                        //negate daysDiff to recalculate dueByDa
                        var statesToUpdate = UpdateDueByDates(-daysDiff);      
                    }

                }
            }
        }


        //NOT WORKING use completion date
        private List<PlanningAppState> UpdateDueByDates(int daysDiff)
        {
            return PlanningAppStates
                    .Where(s => s.DueByDate >= Current().DueByDate)
                    .Select(c => {c.DueByDate = c.DueByDate.AddBusinessDays(daysDiff); return c;})
                    .ToList();  
        }

        public PlanningAppState Next()
        {       
            if(!Completed() && !isLastState(Current()))
                return PlanningAppStates[PlanningAppStates.IndexOf(Current())+1]; 
            else    
                return null;
        }

        private PlanningAppState Prev()
        {   
                return PlanningAppStates[PlanningAppStates.IndexOf(Current())-1]; 
        }

        public bool Completed()
        { 
             return PlanningAppStates.Where(p => p.CurrentState == true).Count() == 0;
        }

        public PlanningAppState Current()
        {
                return PlanningAppStates.Where(s => s.CurrentState == true).SingleOrDefault();
        }

        public string PlanningStatus() {
            return Completed() ? "Completed" : "In Progress";  //TODO take from database
        }

        public DateTime CompletionDate() {
            if(!Completed())
                return LastState().DueByDate;
            else   
                return LastState().CompletionDate.Value;
        }

        private bool isLastState(PlanningAppState planningAppState)
        {
                return PlanningAppStates.Count() == (PlanningAppStates.IndexOf(planningAppState) + 1);
        }

        private bool isFirstState(PlanningAppState planningAppState)
        {
                return PlanningAppStates.IndexOf(planningAppState) == 0;
        }

        private PlanningAppState LastState()
        {
                return PlanningAppStates.Count() > 0 ? PlanningAppStates[PlanningAppStates.Count() - 1] : null;
                
        }

        private PlanningAppState FirstState(PlanningAppState planningAppState)
        {
                return PlanningAppStates.Count() > 0 ? PlanningAppStates[0] : null;
        }


    }
}