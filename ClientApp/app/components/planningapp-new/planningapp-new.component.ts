import { PlanningApp } from './../../models/planningapp';
import { Component, OnInit } from '@angular/core';
import { CustomerSelect } from '../../models/customer';
import { StateGeneratorSelect } from '../../models/state-generator';
import { PlanningAppGenerator } from '../../models/planningapp';
import { PlanningAppService } from '../../services/planningapp.service';
import { ToastyService } from 'ng2-toasty';

import { ActivatedRoute, Router } from '@angular/router';
import { CustomerService } from '../../services/customer.service';
import { StateInitialiserService } from '../../services/stateinitialiser.service';

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

  planningAppGenerator: PlanningAppGenerator = {
    customerId: 0,
    stateInitialiserId: 0,
    name: ''
  };

  planningApp: PlanningApp = {
    id: 0,
    customer: {
      id: 0, 
      firstName: "",
      lastName: "",
      address1: "",
      address2: "",
      postcode: "",
      emailAddress: "",
      telephoneHome: "",
      telephoneMobile:"",
      notes: "",
    },
    name: "",
    businessDate: "",
    planningStatus:  "",
    currentStateStatus: "",
    currentState:  "",
    expectedStateCompletionDate:  "",
    nextState:  "",
    councilPlanningAppId: "",
    completionDate:  "",
    generator: "",
    notes: "",
    planningAppStates: [],
    method: 1
  };

  constructor(private PlanningAppService: PlanningAppService,
              private toastyService: ToastyService,
              private customerService: CustomerService,
              private stateInitialiserService: StateInitialiserService,
              private router: Router) { }

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
    console.warn(this.planningAppGenerator)
    this.PlanningAppService.generatePlanningApp(this.planningAppGenerator).subscribe(
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
