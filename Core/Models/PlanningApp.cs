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
        // public int PlanningAppStatusId { get; set; }
        // public PlanningAppStatus PlanningAppStatus { get; set; }
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }
        
        public int StateInitialiserId { get; set; }

        public StateInitialiser StateInitialiser { get; set; }

        public int CurrentPlanningStatusId { get; set; }
        public StateStatus CurrentPlanningStatus { get; set; }

        // public int NextStateId { get; set; }
        // public PlanningAppState NextPlanningState { get; set; }
        //public DateTime CurrentStateCompletionDate { get; set; }

        public IList<PlanningAppState> PlanningAppStates { get; set; }

        public PlanningApp()
        {
            PlanningAppStates = new List<PlanningAppState>();

        }

        public void GeneratePlanningStates(IOrderedEnumerable<StateInitialiserState> stateInitialisers, IEnumerable<StateStatus> stateStatus) 
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

                newState.StateStatus = stateStatus.Where(s => s.Name == StatusList.OnTime).SingleOrDefault();
                PlanningAppStates.Add(newState);
            }
            //set first state to current state
            if(PlanningAppStates.Count > 0)
                 PlanningAppStates[0].CurrentState = true;

            //Set overall Status to InProgress
            CurrentPlanningStatus = stateStatus.Where(s => s.Name == StatusList.AppInProgress).SingleOrDefault();
        
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
                //If Overran then roll all future completion dates by business days overdue
                if(currentDate > current.DueByDate) {   
                    var daysDiff = current.DueByDate.GetBusinessDays(currentDate, new List<DateTime>());           
                    RollForwardDueByDates(daysDiff);  
                }                  
            }
            if(Completed()) {
                CurrentPlanningStatus = statusList.Where(s => s.Name == StatusList.Complete).SingleOrDefault();
            }

        }

        public void PrevState(List<StateStatus> statusList)
        {   
            var current = Current();
            if(current != null)   //Cannot Role back a completed application
            {
                if(!isFirstState(current))
                {
                    var prevState = Prev();
                    //daysDiff is used to subtract the future DueByStates by days overrun, basically resetting
                    var daysDiff = prevState.DueByDate.GetBusinessDays(prevState.CompletionDate.Value, new List<DateTime>());
                    rewindState();

                    if(daysDiff > 0)  {
                        var statesToUpdate = RollbackDueByDates(daysDiff);   
                    }   
                }
            }
        }
     
        private void rewindState()
        {
            var current = Current(); //Make a copy

            //Make previous state active
            Prev().CompletionDate = null;
            Prev().CurrentState = true;

            //set current non active
            current.CurrentState = false;
        } 
        private void RollForwardDueByDates(int daysDiff)
        {
            if(!Completed()) {
                PlanningAppStates
                        .Where(s => s.DueByDate >= Current().DueByDate)
                        .Select(c => {c.DueByDate = c.DueByDate.AddBusinessDays(daysDiff); return c;})
                        .ToList();  
            }
        }
        private List<PlanningAppState> RollbackDueByDates(int daysDiff)
        {
            return PlanningAppStates
                    .Where(s => s.DueByDate > Current().DueByDate)
                    .Select(c => {c.DueByDate = c.DueByDate.AddBusinessDays(-daysDiff); return c;})
                    .ToList();  
        }

        //****************/
        //Helper functions
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