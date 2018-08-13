import { StateInitialiserState } from './../../models/stateinitialiserstate';
import { Component, OnInit } from '@angular/core';
import { StateInitialiserStateService } from '../../services/stateinitialiserstate.service';
import { ActivatedRoute, Router } from '../../../../node_modules/@angular/router';
import { ToastyService } from '../../../../node_modules/ng2-toasty';

@Component({
  selector: 'app-stateinitialiserstate-form',
  templateUrl: './stateinitialiserstate-form.component.html',
  styleUrls: ['./stateinitialiserstate-form.component.css']
})
export class StateInitialiserStateFormComponent implements OnInit {
  stateInitialiserState: StateInitialiserState = {
      id: 0, 
      name: "",
      orderId: 0,
      completionTime: 2,
      alertToCompletionTime: 1,
      stateInitialiserId: 0
    };

  orderId: any = 0;
  stateInitialiserId: any = 0;

  sub: any = "";
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private toastyService: ToastyService,
    private stateInitialiserStateService: StateInitialiserStateService) { 

    route.params.subscribe(p => { 
                                  this.stateInitialiserState.id = +p['id'] || 0 });
    }

  ngOnInit() {
    this.sub = this.route
      .queryParams
      .subscribe(params => {
        this.orderId = +params['orderId'] || 0;
        this.stateInitialiserId = +params['initialiserId'] || 0;
      });
    
    if (this.stateInitialiserState.id)
   
        this.stateInitialiserStateService.getStateInitialiserState(this.stateInitialiserState.id)
        .subscribe(
          v => this.stateInitialiserState = v,
          err => {
            if (err.status == 404) {
              this.router.navigate(['/stateinitialiserstates']);
              return; 
            }
        });
  }

  submit() {
    var result$;
    
    if (this.stateInitialiserState.id) 
      result$ = this.stateInitialiserStateService.update(this.stateInitialiserState) 
    else {
      this.stateInitialiserState.orderId = this.orderId;
      this.stateInitialiserState.stateInitialiserId = this.stateInitialiserId;
      console.warn(this.stateInitialiserState);
      result$ = this.stateInitialiserStateService.create(this.stateInitialiserState); 
    }

    result$.subscribe(

      stateInitialiserState => {
      this.toastyService.success({
        title: 'Success', 
        msg: 'State was sucessfully saved.',
        theme: 'bootstrap',
        showClose: true,
        timeout: 5000
      })
  
      //this.router.navigate(['/stateinitialisers/', this.stateInitialiserState.id])
      this.router.navigate(['/stateinitialisers']);
    });
  }

  delete() {
    if (confirm("Are you sure?")) {
      this.stateInitialiserStateService.delete(this.stateInitialiserState.id)
        .subscribe(x => { this.router.navigate(['/stateinitialisers'])});
    }
  }
}
