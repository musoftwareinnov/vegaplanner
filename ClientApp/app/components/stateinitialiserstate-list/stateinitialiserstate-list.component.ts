import { StateInitialiser } from './../../models/stateinitialiser';
import { Component, OnInit } from '@angular/core';
import { Customer } from '../../models/customer';
import { CustomerService } from '../../services/customer.service';
import { ActivatedRoute, Router } from '../../../../node_modules/@angular/router';
import { ToastyService } from '../../../../node_modules/ng2-toasty';
import { StateInitialiserService } from '../../services/stateinitialiser.service';
import { AuthGuard } from '../../auth.guard';

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
    private stateInitialiserService: StateInitialiserService,
    private authGuard:AuthGuard) { 
        authGuard.canActivate();
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
}

