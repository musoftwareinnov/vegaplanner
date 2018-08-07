import { StateInitialiser } from './../../models/stateinitialiser';
import { Component, OnInit } from '@angular/core';
import { Customer } from '../../models/customer';
import { CustomerService } from '../../services/customer.service';
import { ActivatedRoute, Router } from '../../../../node_modules/@angular/router';
import { ToastyService } from '../../../../node_modules/ng2-toasty';
import { StateInitialiserService } from '../../services/stateinitialiser.service';

@Component({
  selector: 'app-stateinitialiserstate-list',
  templateUrl: './stateinitialiserstate-list.component.html',
  styleUrls: ['./stateinitialiserstate-list.component.css']
})
export class StateInitialiserStateListComponent implements OnInit {


  stateInitialiser: StateInitialiser = {
      id: 0, 
      name: "",
      description: "",
      states: [] 
    };
    
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private toastyService: ToastyService,
    private stateInitialiserService: StateInitialiserService) { 

    route.params.subscribe(p => { this.stateInitialiser.id = +p['id'] || 0})
    }

  ngOnInit() {

    console.warn(this.stateInitialiser);
    if (this.stateInitialiser.id)
        this.stateInitialiserService.getStateInitialiser(this.stateInitialiser.id)
        .subscribe(
          v => this.stateInitialiser = v,
          err => {
            if (err.status == 404) {
              this.router.navigate(['/stateinitialisers']);
              return; 
            }
        });
  }

  // submit() {

  //   console.warn("Submit -> "  + this.customer.id);
  //   var result$ = (this.customer.id) ? this.customerService.update(this.customer) : this.customerService.create(this.customer); 


  //   result$.subscribe(

  //     customer => {
  //     this.toastyService.success({
  //       title: 'Success', 
  //       msg: 'Customer was sucessfully saved.',
  //       theme: 'bootstrap',
  //       showClose: true,
  //       timeout: 5000
  //     })
  
  //     // this.router.navigate(['/customers/', customer.id])
  //     this.router.navigate(['/customers'])
  //   });
  // }
}

