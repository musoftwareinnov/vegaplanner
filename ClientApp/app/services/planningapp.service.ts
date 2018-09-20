import { Headers } from '@angular/http';
import { PlanningApp, ChangePlanningAppState, PlanningAppGenerator, SavePlanningNotes } from '../models/planningapp';
import { PlanningAppSummary } from '../models/planningappsummary';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import 'rxjs/add/operator/map';
import { StateAction } from '../constants';
import { JwtHeader } from '../shared/utils/jwt.header';
import { UserService } from '../shared/services/user.service';

@Injectable()
export class PlanningAppService {

  private readonly planningappsEndpoint = '/api/planningapps';

  private httpHeaders = new HttpHeaders;

  constructor(private http: HttpClient, private jwtHeader:JwtHeader, private userService:UserService) { }

  getPlanningAppSummary(filter:any) {
    return this.http.get<any>(this.planningappsEndpoint + '?' + this.toQueryString(filter), { headers: this.userService.getUwt() });
  }

  getPlanningApp(id:any) {
    //  return this.restDecorService.get<PlanningApp>(this.planningappsEndpoint + '/' + id)
    return this.http.get<PlanningApp>(this.planningappsEndpoint + '/' + id, { headers: this.userService.getUwt() });
  }

  nextState(changePlanningAppState: ChangePlanningAppState) {
    changePlanningAppState.method = StateAction.Next;  //move to next state
    return this.http.put(this.planningappsEndpoint + '/' + changePlanningAppState.id, changePlanningAppState, { headers: this.userService.getUwt() })
  }

  terminate(changePlanningAppState: ChangePlanningAppState) {
    changePlanningAppState.method = StateAction.Terminate; 
    return this.http.put(this.planningappsEndpoint + '/' + changePlanningAppState.id, changePlanningAppState, { headers: this.userService.getUwt() })
  }

  saveNotes(planningNotes: SavePlanningNotes) {
    return this.http.put(this.planningappsEndpoint + '/' + planningNotes.id, planningNotes, { headers: this.userService.getUwt()})
  }

  saveDevelopmentDetails(planningApp: PlanningApp) {
    return this.http.put(this.planningappsEndpoint + '/' + planningApp.id, planningApp, { headers: this.userService.getUwt() })
  }
  
  generatePlanningApp(planningAppGenerator:PlanningAppGenerator) {
    console.warn(planningAppGenerator);
    return this.http.post<PlanningApp>(this.planningappsEndpoint, planningAppGenerator, { headers: this.userService.getUwt() })
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