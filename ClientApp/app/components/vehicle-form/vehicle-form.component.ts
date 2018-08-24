import * as _ from 'underscore';
import { SaveVehicle, Vehicle } from '../../models/vehicle';
import { INITIAL_CONFIG } from '@angular/platform-server';
import { VehicleService } from '../../services/vehicle.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
//import 'rxjs/add/Observable/forkJoin';
import { Observable } from 'rxjs/Observable';
import { ToastyService } from 'ng2-toasty';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {
  makes:any[] = [];
  models:any[] = [];
  features:any[] = [];
  vehicle: SaveVehicle = {
    id: 0,
    makeId: 0,
    modelId: 0,
    isRegistered: false,
    features: [],
    contact: {
      name: '',
      email: '',
      phone: '',
    }
  };

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private toastyService: ToastyService,
    private vehicleService: VehicleService) { 

      route.params.subscribe(p => { this.vehicle.id = +p['id'] || 0})
    }

  ngOnInit() {
    var sources = [  
      this.vehicleService.getMakes(),
      this.vehicleService.getFeatures(),
    ];

    if (this.vehicle.id)
      sources.push(this.vehicleService.getVehicle(this.vehicle.id));

    // Observable.forkJoin(sources).subscribe(data => {
    //   this.makes = data[0];
    //   this.features = data[1];

    //   if (this.vehicle.id) {
    //     this.setVehicle(data[2]);
    //     this.populateModels();
    //   }
    // }, err => {
    //   if (err.status == 404)
    //     this.router.navigate(['/home']);
    // });
  }

  private setVehicle(v:Vehicle) {
    this.vehicle.id = v.id;
    this.vehicle.makeId = v.make.id;
    this.vehicle.modelId = v.model.id;
    this.vehicle.isRegistered = v.isRegistered;
    this.vehicle.contact = v.contact;
    this.vehicle.features = _.pluck(v.features, 'id');
  }

  onMakeChange() {
    this.populateModels();
    delete this.vehicle.modelId;
  }

  private populateModels() {
    var selectedMake = this.makes.find(m => m.id == this.vehicle.makeId);
    this.models = selectedMake ? selectedMake.models : [];
  }

  onFeatureToggle(featureId: any, $event: any) {
    if($event.target.checked)
      this.vehicle.features.push(featureId);
    else {
      var index = this.vehicle.features.indexOf(featureId);
      this.vehicle.features.splice(index, 1);
    }
  
  }

  submit() {
    var result$ = (this.vehicle.id) ? this.vehicleService.update(this.vehicle) : this.vehicleService.create(this.vehicle); 
    result$.subscribe(
      // err => {
      //     this.toastyService.error({
      //     title: 'Error', 
      //     msg: 'Vehicle was not saved.',
      //     theme: 'bootstrap',
      //     showClose: true,
      //     timeout: 5000
      // })
      // },
    vehicle => {
      this.toastyService.success({
        title: 'Success', 
        msg: 'Vehicle was sucessfully saved.',
        theme: 'bootstrap',
        showClose: true,
        timeout: 5000
      })
  
      this.router.navigate(['/vehicles/', this.vehicle.id])
    });
  }
  
  delete() {
    if (confirm("Are you sure?")) {
      this.vehicleService.delete(this.vehicle.id)
        .subscribe(x => { this.router.navigate(['/home'])});
    }
  }

  
}
