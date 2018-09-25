import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { UserService } from './user.service';

// Wrapper class for passing Web Tokens to server

@Injectable()
export class HttpJwtService {

  private httpHeaders = new HttpHeaders;

  constructor(private http: HttpClient, private userService:UserService) { }

  get<T>(endpoint: string) {
    return this.http.get<T>(endpoint, { headers: this.userService.getUwt() });
  }

  put<T>(url: string, body: any) {
    return this.http.put<T>(url, body, { headers: this.userService.getUwt() })
  }

  post<T>(url: string, body: any) {
    return this.http.post<T>(url, body, { headers: this.userService.getUwt() })
  }

  delete<T>(endpoint: string) {
    return this.http.delete(endpoint, { headers: this.userService.getUwt() });
  }
}