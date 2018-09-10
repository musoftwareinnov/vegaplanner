import { StateRule } from "./StateRule";

export interface PlanningAppState {
    id: number; 
    stateName: string;
    dueByDate: string;
    dateCompleted: string;
    stateStatus: string;
    currentState: boolean;
    minDueByDate: string;
    dueByDateEditable: boolean;
    isCustomDuration: boolean;
    stateRules: StateRule[];
    notes: string;
}