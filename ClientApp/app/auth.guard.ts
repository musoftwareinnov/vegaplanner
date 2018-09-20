// auth.guard.ts
import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';
import { UserService } from './shared/services/user.service';

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(private user: UserService,private router: Router) {}

  canActivate() {

    if(!this.user.isLoggedIn())
    {
       console.info("AuthGuard: Component Activation Unsuccessfull, user not logged in ")
       this.router.navigate(['/login']);
       return false;
    }

    console.info("AuthGuard: Component Activation successfull ")
    return true;
  }

  getUserWebToken() {
      this.user.getUwt();
  }
}