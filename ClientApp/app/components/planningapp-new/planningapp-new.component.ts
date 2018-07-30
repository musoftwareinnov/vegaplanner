import { PlanningApp } from './../../models/planningapp';
import { Component, OnInit } from '@angular/core';
import { CustomerSelect } from '../../models/customer';
import { StateGeneratorSelect } from '../../models/state-generator';
import { PlanningAppGenerator } from '../../models/planningapp';
import { PlanningAppService } from '../../services/planningapp.service';
import { ToastyService } from 'ng2-toasty';

import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-planningapp-new',
  templateUrl: './planningapp-new.component.html',
  styleUrls: ['./planningapp-new.component.css']
})
export class PlanningAppNewComponent implements OnInit {
  planningAppGenerator: PlanningAppGenerator = {
    customerId: 0,
    stateInitialiserId: 0,
    name: ''
  };
  customerSelect: CustomerSelect[] =  [ {
        id: 1,
        customerNameLong: 'Test Customer',
        postcode: 'TR4 9PF'
      },
      {
        id: 2,
        customerNameLong: 'Test Customer 2',
        postcode: 'BR1 3DE'
      }
    ];

  stateGeneratorSelect: StateGeneratorSelect[] = [
    {
      id: 1,
      name: 'General State Generator'
    },
    {
      id: 2,
      name: 'Add Building Regs Generator'
    },
  ]

  constructor(private PlanningAppService: PlanningAppService,
              private toastyService: ToastyService,
              private router: Router) { }

  ngOnInit() {

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
