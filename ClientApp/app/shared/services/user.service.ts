import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { UserRegistration } from '../models/user.registration.interface';
import { ConfigService } from '../utils/config.service';
import {BaseService} from "./base.service";
import { Observable } from 'rxjs/Rx';
import { BehaviorSubject } from 'rxjs/Rx'; 
import {LocalStorageService, SessionStorageService} from 'ngx-webstorage';

// Add the RxJS Observable operators we need in this app.
import '../../rxjs-operators';

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

  constructor(private http: Http, private configService: ConfigService, private localSt:LocalStorageService, private sessionSt:SessionStorageService) {
    super();
  
    // localSt.store('auth_token', "fffff");
    // localSt.clear('auth_token');
    this.loggedIn = !!localSt.retrieve('authToken');

    console.log("logged In:" + this.loggedIn);
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
        this.localSt.store('authToken', res.authToken);
        this.loggedIn = true;
        this._authNavStatusSource.next(true);
        this._authNavUserNameSource.next(res.userName);
        return true;
      })
      .catch(this.handleError);
  }

  logout() {
    this.localSt.clear('authToken')
    this.loggedIn = false;
    this._authNavStatusSource.next(false);
    this._authNavUserNameSource.next("");
  }

  isLoggedIn() {
    return this.loggedIn;
  }
}