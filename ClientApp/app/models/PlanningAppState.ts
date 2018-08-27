
export interface PlanningAppState {
    id: number; 
    stateName: string;
    dueByDate: string;
    dateCompleted: string;
    stateStatus: string;
    currentState: boolean;
    minDueByDate: string;
    dueByDateEditable: boolean;
    notes: string;
}