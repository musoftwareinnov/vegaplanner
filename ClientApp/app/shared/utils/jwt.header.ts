import { UserService } from './../services/user.service';
import { Injectable } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';
 
@Injectable()
export class JwtHeader {

    httpHeaders: HttpHeaders;
 
    constructor(private user:UserService) {
        this.httpHeaders= user.getUwt();
    }
 
     getJwtHeader() {
         return this.httpHeaders;
     }    
}