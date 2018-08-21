import { Customer } from './customer';
export interface PlanningApp {
    id: number; 
    customer: Customer;
    name: string;
    businessDate: string;
    planningStatus: string;
    currentStateStatus: string;
    currentState: string;
    expectedStateCompletionDate: string; 
    nextState: string; 
    completionDate: string; 
    generator: string;
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

  export interface PlanningAppGenerator {
    customerId: number; 
    stateInitialiserId: number;
    name: string;
  }

