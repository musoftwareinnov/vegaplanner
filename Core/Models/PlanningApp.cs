using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        // public StateInitialiserState CurrentState { get; set; }

        // public int NextStateId { get; set; }
        // public StateInitialiserState NextState { get; set; }


        public IList<PlanningAppState> PlanningAppStates { get; set; }

        public PlanningApp()
        {
            PlanningAppStates = new List<PlanningAppState>();

        }

        public void GeneratePlanningStates(StateInitialiser stateInitialiser, StateStatus initialStatus) 
        {
            DateTime today = DateTime.Now; //TODO Get From Settings

            foreach(var stateInialiser in stateInitialiser.States) {
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
    }
}