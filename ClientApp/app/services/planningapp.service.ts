import { Headers } from '@angular/http';
import { PlanningApp, ChangePlanningAppState, PlanningAppGenerator, SavePlanningNotes } from '../models/planningapp';
import { PlanningAppSummary } from '../models/planningappsummary';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import 'rxjs/add/operator/map';
import { StateAction } from '../constants';
import { UserService } from '../shared/services/user.service';
import { HttpJwtService } from '../shared/services/httpJwt.service';


@Injectable()
export class PlanningAppService {

  private readonly planningappsEndpoint = '/api/planningapps';

  private httpHeaders = new HttpHeaders;

  constructor(private http: HttpClient, 
              private userService:UserService,
              private httpJwtService:HttpJwtService) { }

  getPlanningAppSummary(filter:any) {
    return this.httpJwtService.get<PlanningApp>(this.planningappsEndpoint + '?' + this.toQueryString(filter));
  }

  getPlanningApp(id:any) {
    return this.httpJwtService.get<PlanningApp>(this.planningappsEndpoint + '/' + id)
  }

  nextState(changePlanningAppState: ChangePlanningAppState) {
    changePlanningAppState.method = StateAction.Next;  //move to next state
    return this.httpJwtService.put<ChangePlanningAppState>(this.planningappsEndpoint + '/' + changePlanningAppState.id, changePlanningAppState)
  }

  terminate(changePlanningAppState: ChangePlanningAppState) {
    changePlanningAppState.method = StateAction.Terminate; 
    return this.httpJwtService.put<ChangePlanningAppState>(this.planningappsEndpoint + '/' + changePlanningAppState.id, changePlanningAppState)
  }

  saveNotes(planningNotes: SavePlanningNotes) {
    return this.httpJwtService.put<SavePlanningNotes>(this.planningappsEndpoint + '/' + planningNotes.id, planningNotes)
  }

  saveDevelopmentDetails(planningApp: PlanningApp) {
    return this.httpJwtService.put<PlanningApp>(this.planningappsEndpoint + '/' + planningApp.id, planningApp)
  }
  
  generatePlanningApp(planningAppGenerator:PlanningAppGenerator) {
    return this.httpJwtService.post<PlanningApp>(this.planningappsEndpoint, planningAppGenerator)
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