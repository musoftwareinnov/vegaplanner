import { PlanningApp } from "./planningapp";

export interface Customer {
    id: number; 
    firstName: string;
    lastName: string;
    address1: string;
    address2: string;
    postcode: string;
    emailAddress: string; 
    telephoneHome: string; 
    telephoneMobile: string; 
    notes: string;
    planningApplications: PlanningApp[];
}

export interface CustomerSelect {
    id: number; 
    customerNameLong: string;
    postcode: string;
}
