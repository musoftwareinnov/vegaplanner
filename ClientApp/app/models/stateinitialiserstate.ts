import { StateRule } from "./StateRule";

export interface StateInitialiserState { 
    id: number;
    name: string;
    orderId: number;
    completionTime: number;
    alertToCompletionTime: number;
    stateInitialiserId: number;
    stateRules: StateRule[];
}