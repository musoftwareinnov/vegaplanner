import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { HttpJwtService } from '../shared/services/httpJwt.service';

@Injectable()
export class DrawingService {

  private httpHeaders = new HttpHeaders;

  constructor(private http: HttpClient, private httpJwtService:HttpJwtService) { }

  upload(planningAppId = 0, drawing: any) {
    var formData = new FormData();
    formData.append('file', drawing )
    
    return this.http.post(`/api/planningapps/${planningAppId}/drawings`, formData  )
  }

  getDrawings(planningAppId: number) {
    return this.httpJwtService.get<any>(`/api/planningapps/${planningAppId}/drawings`)
  }
}