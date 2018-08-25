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
 
        public IList<PlanningAppState> PlanningAppStates { get; set; }

        public ICollection<Drawing> Drawings { get; set; }

        public PlanningApp()
        {
            PlanningAppStates = new List<PlanningAppState>();
            Drawings = new Collection<Drawing>();
        }

        public PlanningApp GeneratePlanningStates(List<StateInitialiserState> stateInitialisers, IEnumerable<StateStatus> stateStatus) 
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
        
            return this;
        }

       public PlanningApp InsertNewPlanningState(StateInitialiserState newStateInitialiser, IEnumerable<StateStatus> stateStatus) 
        {

            if(!Completed()) {
                var currentState = Current();
                if(newStateInitialiser.OrderId > currentState.state.OrderId) {

                    //Remove states after current state
                    var states = this.PlanningAppStates.ToList();

                    PlanningAppState newState = new PlanningAppState();
                    newState.state = newStateInitialiser;
                    newState.CurrentState = false;
                    newState.StateStatus = stateStatus.Where(s => s.Name == StatusList.OnTime).SingleOrDefault();
                    newState.CompletionDate = null;
                    newState.DueByDate = DateTime.Now;
                    PlanningAppStates.Add(newState);
                    generateDueByDates();
                }
            }
            return this;
        }

        public void RemovePlanningState(StateInitialiserState stateInitialiserState) 
        {
            if(!Completed()) {
                var currentState = Current();
                if(stateInitialiserState.OrderId > currentState.state.OrderId) {
                        var planningStateToRemove = this.PlanningAppStates.Where(s => s.state.Id == stateInitialiserState.Id).SingleOrDefault();
                        if(planningStateToRemove != null) {
                            this.PlanningAppStates.Remove(planningStateToRemove);
                            generateDueByDates();
                        }
                }
            }  
        }   

        public void generateDueByDates() 
        {
            if(!Completed()) {

                //Important - put states in order before processing!!
                PlanningAppStates = PlanningAppStates.OrderBy(s => s.state.OrderId).ToList();
                var prevState = new PlanningAppState();
                var currState = Current();
                var resetCurrent = Current();

                while(currState != null) {
                    if(!isFirstState(currState)) {
                        prevState = SeekPrev();
                        currState.AggregateDueByDate(prevState);
                    }
                    else 
                        currState.SetDueByDate();
                    
                    currState = Next(currState);
                }               
                //Set original state 
                SetCurrent(resetCurrent);
            }
        }
 
  

        public void NextState(List<StateStatus> statusList)
        {

            if(!Completed()) {
                PlanningAppStates = PlanningAppStates.OrderBy(s => s.state.OrderId).ToList();

                var currentDate = CurrentDateSingleton.setDate(DateTime.Now).getCurrentDate();

                var prevState = Current(); //Store reference to current state

                if(!isLastState(prevState)) {
                        SeekNext().CurrentState = true;   //move to next state
                }  
                prevState.CompleteState(currentDate, statusList ); //Close out previouse state
                //If Overran then roll all future completion dates by business days overdue
                if(currentDate > prevState.DueByDate) {   
                    var daysDiff = prevState.DueByDate.GetBusinessDays(currentDate, new List<DateTime>());           
                    RollForwardDueByDates(daysDiff, prevState);  
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
                    var prevState = SeekPrev();
                    //daysDiff is used to subtract the future DueByStates by days overrun, basically resetting
                    var daysDiff = prevState.DueByDate.GetBusinessDays(prevState.CompletionDate.Value, new List<DateTime>());
                    rewindState();

                    if(daysDiff > 0)  {
                        var statesToUpdate = RollbackDueByDates(daysDiff, current);   
                    }   
                }
            }
        }

        public void Terminate(List<StateStatus> statusList)
        {   
            CurrentPlanningStatus = statusList.Where(p => p.Name == StatusList.AppTerminated).SingleOrDefault();
        }
     
        private void rewindState()
        {
            var current = Current(); //Make a copy
 
            //Make previous state active
            SeekPrev().CompletionDate = null;
            SeekPrev().CurrentState = true;
  
            //set current non active
            current.CurrentState = false;
        }   
        private void RollForwardDueByDates(int daysDiff, PlanningAppState prevState)
        {
            if(!Completed()) {
                PlanningAppStates
                        .Where(s => s.DueByDate > prevState.DueByDate)
                        .Select(c => {c.DueByDate = c.DueByDate.AddBusinessDays(daysDiff); return c;})
                        .ToList();  
            }
        }
        private List<PlanningAppState> RollbackDueByDates(int daysDiff, PlanningAppState current)
        {
            return PlanningAppStates
                    .Where(s => s.DueByDate > current.DueByDate)
                    .Select(c => {c.DueByDate = c.DueByDate.AddBusinessDays(-daysDiff); return c;})
                    .ToList();  
        }

        //****************/
        //Helper functions

        public void SetCurrent(PlanningAppState planningAppState) {

            foreach ( var state  in PlanningAppStates) {
                state.CurrentState = false;
            }
            PlanningAppStates[PlanningAppStates.IndexOf(planningAppState)].CurrentState = true;
        }

        public PlanningAppState Next(PlanningAppState planningAppState)
        {       
            PlanningAppState nextState = new PlanningAppState();
            if(!isLastState(planningAppState)) {
                nextState = PlanningAppStates[PlanningAppStates.IndexOf(planningAppState)+1];
                planningAppState.CurrentState = false;
                nextState.CurrentState = true;
                return nextState;
            }
            else    
                return null;
        }
        public PlanningAppState SeekNext()
        {       
            if(!Completed() && !isLastState(Current()))
                return PlanningAppStates[PlanningAppStates.IndexOf(Current())+1]; 
            else    
                return null;
        }

        public PlanningAppState SeekPrev()
        {   
            if(!Completed() && !isFirstState(Current()))
                return PlanningAppStates[PlanningAppStates.IndexOf(Current())-1]; 
            else    
                return null;
        }

        public bool Completed()
        { 
             return PlanningAppStates.Where(p => p.CurrentState == true).Count() == 0;
        }

        public PlanningAppState Current()
        {

                return PlanningAppStates.Where(s => s.CurrentState == true).SingleOrDefault();
        }

        public DateTime MinDueByDate() {
            if(SeekPrev() != null)
                return SeekPrev().DueByDate;
            else   
                return CurrentDateSingleton.setDate(DateTime.Now).getCurrentDate();
            
        }
        public DateTime CompletionDate() {
            if(!Completed())
                return SeekLastState().DueByDate;
            else   
                return SeekLastState().CompletionDate.Value;
        }

        private bool isLastState(PlanningAppState planningAppState)
        {
                return PlanningAppStates.Count() == (PlanningAppStates.IndexOf(planningAppState) + 1);
        }

        private bool isFirstState(PlanningAppState planningAppState)
        {
                return PlanningAppStates.IndexOf(planningAppState) == 0;
        }

        private PlanningAppState SeekLastState()
        {
                return PlanningAppStates.Count() > 0 ? PlanningAppStates[PlanningAppStates.Count() - 1] : null;
                
        }

        private PlanningAppState FirstState(PlanningAppState planningAppState)
        {
                return PlanningAppStates.Count() > 0 ? PlanningAppStates[0] : null;
        }


    }
}