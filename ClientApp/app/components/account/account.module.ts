import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule }  from '@angular/forms';

//Maybe add later!!!
// import { SharedModule }   from '../shared/modules/shared.module';
// import { EmailValidator } from '../directives/email.validator.directive';
// import { RegistrationFormComponent } from './registration-form/registration-form.component';
// import { FacebookLoginComponent } from './facebook-login/facebook-login.component';

import { UserService }  from '../../shared/services/user.service';
import { routing }  from './account.routing';
import { LoginFormComponent } from './login-form/login-form.component';



@NgModule({
  imports: [
    CommonModule,FormsModule,routing
  ],
  declarations: [LoginFormComponent],
  providers:    [ UserService ]
})
export class AccountModule { }