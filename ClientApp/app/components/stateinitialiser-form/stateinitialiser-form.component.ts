import { StateInitialiserState } from './../../models/stateinitialiserstate';
import { Component, OnInit } from '@angular/core';
import { StateInitialiserStateService } from '../../services/stateinitialiserstate.service';
import { ActivatedRoute, Router } from '../../../../node_modules/@angular/router';
import { ToastyService } from '../../../../node_modules/ng2-toasty';
import { StateInitialiser } from '../../models/stateinitialiser';
import { StateInitialiserService } from '../../services/stateinitialiser.service';

@Component({
  selector: 'app-stateinitialiser-form',
  templateUrl: './stateinitialiser-form.component.html',
  styleUrls: ['./stateinitialiser-form.component.css']
})
export class StateInitialiserFormComponent implements OnInit {
  stateInitialiser: StateInitialiser = {
      id: 0, 
      name: "",
      description: "",
      states: []
    };

  orderId: any = 0;
  stateInitialiserId: any = 0;

  sub: any = "";
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private toastyService: ToastyService,
    private stateInitialiserService: StateInitialiserService) { 
    }

  ngOnInit() {

  }

  submit() {
    var result$;
    
    console.warn(this.stateInitialiser);
    result$ = this.stateInitialiserService.create(this.stateInitialiser); 
  
    result$.subscribe(

      stateInitialiser => {
      this.toastyService.success({
        title: 'Success', 
        msg: 'Generator was sucessfully saved.',
        theme: 'bootstrap',
        showClose: true,
        timeout: 5000
      })
  
      this.router.navigate(['/stateinitialisers']);
    });
  }
}

