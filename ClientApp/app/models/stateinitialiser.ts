import { StateInitialiserState } from "./stateinitialiserstate";

export interface StateInitialiser { 
    id: number;
    name: string;
    description: string;
    states: StateInitialiserState[];
}