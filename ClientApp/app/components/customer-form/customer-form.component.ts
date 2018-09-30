import { PlanningApp } from './../../models/planningapp';
import { Component, OnInit } from '@angular/core';
import { Customer } from '../../models/customer';
import { CustomerService } from '../../services/customer.service';
import { ActivatedRoute, Router } from '../../../../node_modules/@angular/router';
import { ToastyService } from '../../../../node_modules/ng2-toasty';
import { AuthGuard } from '../../auth.guard';

@Component({
  selector: 'app-customer-form',
  templateUrl: './customer-form.component.html',
  styleUrls: ['./customer-form.component.css']
})
export class CustomerFormComponent implements OnInit {
  customer: Customer = {
      id: 0, 
      firstName: "",
      lastName: "",
      addressLine1: "",
      addressLine2: "",
      postcode: "",
      emailAddress: "",
      telephoneHome: "",
      telephoneMobile:"",
      telephoneWork:"",
      notes: "",
    };

  errors: string = "";
    
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private toastyService: ToastyService,
    private customerService: CustomerService,
    private authGuard:AuthGuard) {

      authGuard.canActivate();
      route.params.subscribe(p => { this.customer.id = +p['id'] || 0})
    }

  ngOnInit() {
    
    if (this.customer.id)
        this.customerService.getCustomer(this.customer.id)
        .subscribe(
          customer => this.customer = customer,
          err => {
            if (err.status == 404) {
              this.router.navigate(['/customers']);
              return; 
            }
        });
  }

  submit() {

    console.warn("Submit -> "  + this.customer.id);
    var result$ = (this.customer.id) ? this.customerService.update(this.customer) : this.customerService.create(this.customer); 
    //this.errors='';

    result$.subscribe(

      customer => {
      this.toastyService.success({
        title: 'Success',  
        msg: 'Customer was sucessfully saved.',
        theme: 'bootstrap',
        showClose: true,
        timeout: 5000
      }),
      // this.router.navigate(['/customers/', customer.id])
      this.router.navigate(['/customers'])
      },
      error => this.errors = "Name already taken");
  }
}
