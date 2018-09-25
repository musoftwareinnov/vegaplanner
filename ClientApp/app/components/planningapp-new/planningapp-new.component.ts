import { PlanningApp } from './../../models/planningapp';
import { Component, OnInit } from '@angular/core';
import { CustomerSelect, Customer } from '../../models/customer';
import { StateGeneratorSelect } from '../../models/state-generator';
import { PlanningAppGenerator } from '../../models/planningapp';
import { PlanningAppService } from '../../services/planningapp.service';
import { ToastyService } from 'ng2-toasty';

import { ActivatedRoute, Router } from '@angular/router';
import { CustomerService } from '../../services/customer.service';
import { StateInitialiserService } from '../../services/stateinitialiser.service';
import { AuthGuard } from '../../auth.guard';

@Component({
  selector: 'app-planningapp-new',
  templateUrl: './planningapp-new.component.html',
  styleUrls: ['./planningapp-new.component.css']
})
export class PlanningAppNewComponent implements OnInit {
  query: any = {
    pageSize: 0
  };
  customerSelect:any[] = [];
  stateGeneratorSelect:any[] = [];


  generator: PlanningAppGenerator = {
    customerId: 0,
    stateInitialiserId: 0,
    name: '',
    developer: {
      companyName: "",
      firstName: "",
      lastName: "",
      fullName: "",
      emailAddress: "",
      telephoneMobile:"",
      telephoneWork:""
    },
    developmentAddress: {
      CompanyName: "",
      addressLine1: "",
      addressLine2: "",
      postcode: "",
      geoLocation: "",
    }

  }

  constructor(private PlanningAppService: PlanningAppService,
              private toastyService: ToastyService,
              private customerService: CustomerService,
              private stateInitialiserService: StateInitialiserService,
              private router: Router,
              private authGuard:AuthGuard) {
                  authGuard.canActivate();
               }

  ngOnInit() {

    //TODO: Join merge for performance???
    this.customerService.getCustomers(this.query)
      .subscribe(customerSelect => this.customerSelect = customerSelect);

    this.stateInitialiserService.getStateInitialiserList(this.query)
      .subscribe(stateGeneratorSelect => this.stateGeneratorSelect = stateGeneratorSelect);

  }

  onGeneratorChange(){

  }
  
  submit() {  
    console.warn(this.generator)
    this.PlanningAppService.generatePlanningApp(this.generator).subscribe(
    planningApp => {
      this.toastyService.success({
        title: 'Success', 
        msg: 'Planning Application was sucessfully created.',
        theme: 'bootstrap',
        showClose: true,
        timeout: 5000
      })
  
      this.router.navigate(['/planningapps/', planningApp.id])
    });
  }
}
