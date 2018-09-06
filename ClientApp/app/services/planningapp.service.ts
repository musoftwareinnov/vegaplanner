import { PlanningApp, ChangePlanningAppState, PlanningAppGenerator, SavePlanningNotes } from '../models/planningapp';
import { PlanningAppSummary } from '../models/planningappsummary';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import 'rxjs/add/operator/map';
import { StateAction } from '../constants';

@Injectable()
export class PlanningAppService {

  private readonly planningappsEndpoint = '/api/planningapps';
  constructor(private http: HttpClient) { }

  getPlanningAppSummary(filter:any) {
    return this.http.get<any>(this.planningappsEndpoint + '?' + this.toQueryString(filter))
  }

  getPlanningApp(id:any) {
    return this.http.get<PlanningApp>(this.planningappsEndpoint + '/' + id)
  }

  nextState(changePlanningAppState: ChangePlanningAppState) {
    changePlanningAppState.method = StateAction.Next;  //move to next state
    return this.http.put(this.planningappsEndpoint + '/' + changePlanningAppState.id, changePlanningAppState)
  }

  terminate(changePlanningAppState: ChangePlanningAppState) {
    changePlanningAppState.method = StateAction.Terminate; 
    return this.http.put(this.planningappsEndpoint + '/' + changePlanningAppState.id, changePlanningAppState)
  }

  saveNotes(planningNotes: SavePlanningNotes) {
    return this.http.put(this.planningappsEndpoint + '/' + planningNotes.id, planningNotes)
  }

  saveDevelopmentDetails(planningApp: PlanningApp) {
    return this.http.put(this.planningappsEndpoint + '/' + planningApp.id, planningApp)
  }
  
  generatePlanningApp(planningAppGenerator:PlanningAppGenerator) {
    console.warn(planningAppGenerator);
    return this.http.post<PlanningApp>(this.planningappsEndpoint, planningAppGenerator)
  }

  toQueryString(obj:any) {
      var parts = [];
      for (var property in obj) {
        var value = obj[property];
        if( value != null && value != undefined)
          parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value))
      }

      return parts.join('&');
  }
}