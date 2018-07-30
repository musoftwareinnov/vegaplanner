export interface PlanningAppSummary {
    id: number; 
    customerId: number;
    name: string;
    planningStatus: string;
    currentStateStatus: string;
    currentState: string;
    expectedStateCompletionDate: string; 
    nextState: string; 
    completionDate: string; 
  }