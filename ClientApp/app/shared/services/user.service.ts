import { Injectable, Inject } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { UserRegistration } from '../models/user.registration.interface';
import { ConfigService } from '../utils/config.service';
import {BaseService} from "./base.service";
import { Observable } from 'rxjs/Rx';
import { BehaviorSubject } from 'rxjs/Rx'; 
import {LocalStorageService, SessionStorageService} from 'ngx-webstorage';
import {LOCAL_STORAGE, WebStorageService} from 'angular-webstorage-service';



// Add the RxJS Observable operators we need in this app.
import '../../rxjs-operators';
import { HttpHeaders } from '@angular/common/http';

@Injectable()

export class UserService extends BaseService {

  baseUrl: string = '';
  // Observable navItem source
  private _authNavStatusSource = new BehaviorSubject<boolean>(false);
  private _authNavUserNameSource = new BehaviorSubject<string>("No User");
  // Observable navItem stream
  authNavStatus$ = this._authNavStatusSource.asObservable();
  authNavUser$ = this._authNavUserNameSource.asObservable();  //used to display user name in Nav Menu

  private loggedIn = false;

  constructor(private http: Http, private configService: ConfigService, 
                                  private localSt:LocalStorageService, 
                                  )  {
    super();

    // ?? not sure if this the best way to broadcast the status but seems to resolve issue on page refresh where auth status is lost in
    // header component resulting in authed user nav links disappearing despite the fact user is still logged in
    this._authNavStatusSource.next(this.loggedIn);
    this.baseUrl = configService.getApiURI();
  }

  register(email: string, password: string, firstName: string, lastName: string,location: string): Observable<UserRegistration> {
    let body = JSON.stringify({ email, password, firstName, lastName,location });
    let headers = new Headers({ 'Content-Type': 'application/json' });
    let options = new RequestOptions({ headers: headers });

    return this.http.post(this.baseUrl + "/accounts", body, options)
      .map(res => true)
      .catch(this.handleError);
  }  

   login(userName:string, password:string) {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');

    return this.http
      .post(
      this.baseUrl + '/auth/login',
      JSON.stringify({ userName, password }),{ headers }
      )
      .map(res => res.json())
      .map(res => {
        if (typeof window !== 'undefined') {
          localStorage.setItem('authToken', res.authToken);
          if(res.authToken )
            console.log("UserService Login succeeded: webtoken obtained for " + userName);
       }
        this.loggedIn = true;
        this._authNavStatusSource.next(true);
        this._authNavUserNameSource.next(res.userName);
        return true;
      })
      .catch(this.handleError);
  }

  logout() {
    if (typeof window !== 'undefined') {
      localStorage.removeItem('authToken')
      console.log("UserService Logout succeeded");
    }
    this.loggedIn = false;
    this._authNavStatusSource.next(false);
    this._authNavUserNameSource.next("");
  }

  isLoggedIn() {
    return this.loggedIn;
  }

  getUwt() {
    var httpHeaders = new HttpHeaders;
    if(this.isLoggedIn()) {
      //console.log("UserService getUserWebTokenHeader:" + localStorage.getItem('authToken'));
      if (typeof window !== 'undefined') {
        //console.log("UserService getting webtoken:" + localStorage.getItem('authToken'));
        var webToken = localStorage.getItem('authToken');
        var httpHeaders = new HttpHeaders(
          {
            'Content-Type': 'application/json',
            'Authorization':`Bearer ${webToken}`
          });
      }
    }
    return httpHeaders;
  }
}