import { Component, OnInit } from '@angular/core';
import { Customer } from '../../models/customer';
import { CustomerService } from '../../services/customer.service';
import { ActivatedRoute, Router } from '../../../../node_modules/@angular/router';
import { ToastyService } from '../../../../node_modules/ng2-toasty';

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
      address1: "",
      address2: "",
      postcode: "",
      emailAddress: "",
      telephoneHome: "",
      telephoneMobile:"",
      notes: "",
    };
    
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private toastyService: ToastyService,
    private customerService: CustomerService) { 

      // route.params.subscribe(p => { this.customer.id = +p['id'] || 0})
    }

  ngOnInit() {
    console.warn(this.customer);
    // if (this.customer.id)
    //     this.customerService.getCustomer(this.customer.id);
  }

}
