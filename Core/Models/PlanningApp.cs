using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using vega.Core.Models.Generic;
using vega.Core.Models.States;
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
            DateTime today = DateTime.Now.Date; //TODO Get From Settings

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
                    newState.DueByDate = today.AddBusinessDays(stateInialiser.CompletionTime);

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
                if(CurrentStateCompletionDate != null)
                {
                    var current = Current(); 
                    if(!isLastState(current)) {
                            Next().CurrentState = true;   //move to next state
                    }                   
                    current.CloseOutState(CurrentStateCompletionDate, statusList );                   
                    //If Overran then roll all future dates
                }
            }
        }

        public void PrevState(List<StateStatus> statusList)
        {
            
            var current = Current();
            if(current != null)   //Cannot Role back a completed application
            {
                if(!isFirstState(current))
                {
                    Prev().ReOpenState(CurrentStateCompletionDate, statusList);
                    current.ReSetState(CurrentStateCompletionDate, statusList);
                }
            }
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
            return LastState().DueByDate;
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