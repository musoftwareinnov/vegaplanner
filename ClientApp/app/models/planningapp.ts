import { Customer } from './customer';
import { Developer } from './developer';
import { Address } from './address';
export interface PlanningApp {
    id: number; 
    planningReferenceId: string;
    customer: Customer;
    developer: Developer;
    developmentAddress: Address;
    name: string;
    businessDate: string;
    planningStatus: string;
    currentStateStatus: string;
    currentState: string;
    expectedStateCompletionDate: string; 
    nextState: string; 
    councilPlanningAppId: string;
    completionDate: string; 
    generator: string;
    notes: string;
    planningAppStates: PlanningAppStates[];
    method: number;
  }

export interface PlanningAppStates {
    id: number; 
    stateName: string;
    dueByDate: string;
    dateCompleted: string;
    stateStatus: string;
    currentState: boolean;
  }

  export interface ChangePlanningAppState {
    id: number; 
    method: number;
  }

  export interface SavePlanningNotes {
    id: number; 
    notes: string;
  }

  export interface PlanningAppGenerator {
    customerId: number; 
    stateInitialiserId: number;
    name: string;
    developer: Developer;
    developmentAddress: Address;
  }

