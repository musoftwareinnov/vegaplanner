import { StateRule } from "./StateRule";
import { CustomField } from "./CustomField";

export interface StateInitialiserState { 
    id: number;
    name: string;
    orderId: number;
    completionTime: number;
    alertToCompletionTime: number;
    stateInitialiserId: number;
    canDelete:boolean;
    stateInitialiserStateCustomFields: CustomField[];
}