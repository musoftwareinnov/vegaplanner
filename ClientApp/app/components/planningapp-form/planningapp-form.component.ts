import { Customer } from './../../models/customer';
import * as _ from 'underscore';
import { ProgressService } from '../../services/progress.service';
import { ChangePlanningAppState, PlanningApp } from '../../models/planningapp';
import { INITIAL_CONFIG } from '@angular/platform-server';
import { PlanningAppService } from '../../services/planningapp.service';
import { PhotoService } from '../../services/photo.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ViewChild, ElementRef, NgZone } from '@angular/core';
import 'rxjs/add/Observable/forkJoin';
import { Observable } from 'rxjs/Observable';
import { ToastyService } from 'ng2-toasty';

@Component({
  selector: 'app-planingapp-form',
  templateUrl: './planningapp-form.component.html',
  styleUrls: ['./planningapp-form.component.css']
})
export class PlanningAppFormComponent implements OnInit {
  @ViewChild('fileInput') fileInput: ElementRef | undefined;

  vehicleId: number = 0; 
  photos: any[] = [];
  progress: any;

  savePlanningApp: ChangePlanningAppState = {
    id: 0,
    method: 1   //default to next state
  };

  planningApp: PlanningApp = {
    id: 0,
    // customerId: 0,
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
    completionDate:  "",
    planningAppStates: [],
    method: 1
  };

  constructor(
    private route: ActivatedRoute,
    private zone: NgZone,
    private router: Router,
    private toastyService: ToastyService,
    private progressService: ProgressService,
    private photoServices: PhotoService,
    private planningAppService: PlanningAppService) { 

    route.params.subscribe(p => { this.planningApp.id = +p['id'] || 0}); }
    
  ngOnInit() {
    //this.photoServices.getPhotos(this.planningApp.id)
    this.photoServices.getPhotos(1)
      .subscribe(photos => this.photos = photos);

    this.planningAppService.getPlanningApp(this.planningApp.id)
    .subscribe(
      v => this.planningApp = v,
      err => {
        if (err.status == 404) {
          this.router.navigate(['/planningapps']);
          return; 
        }
    });
  }

  submit() {
    //var result$ = (this.planningApp.id) ? this.planningAppService.update(this.vehicle) : this.vehicleService.create(this.vehicle); 

    this.savePlanningApp.method = 1;
    this.savePlanningApp.id = this.planningApp.id;
    console.warn( this.savePlanningApp);
    var result$ = this.planningAppService.nextState(this.savePlanningApp )
    result$.subscribe(
        planningApp => {
          this.toastyService.success({
            title: 'Success', 
            msg: 'New State : ' + this.planningApp.nextState,
            theme: 'bootstrap',
            showClose: true,
            timeout: 5000
          })     
          this.router.navigate(['/planningapps/', this.planningApp.id])
          //this.router.navigate(['/planningapps/26']);
        });
  }

  onMakeChange() {
    // this.populateModels();
    // delete this.vehicle.modelId;
  }

  private populateModels() {
    // var selectedMake = this.makes.find(m => m.id == this.vehicle.makeId);
    // this.models = selectedMake ? selectedMake.models : [];
  }

  onFeatureToggle(featureId: any, $event: any) {
    // if($event.target.checked)
    //   this.vehicle.features.push(featureId);
    // else {
    //   var index = this.vehicle.features.indexOf(featureId);
    //   this.vehicle.features.splice(index, 1);
    // }
  
  }
  
  delete() {
    // if (confirm("Are you sure?")) {
    //   this.planningAppService.delete(this.vehicle.id)
    //     .subscribe(x => { this.router.navigate(['/home'])});
    // }
  }

  uploadPhoto() {

    if(this.fileInput) {
      var nativeElement: HTMLInputElement = this.fileInput.nativeElement;

      if(nativeElement.files != null)
      {
        console.log(this.fileInput)
        this.progressService.startTracking()
          .subscribe(progress => {
            console.log(progress);
            this.zone.run(() => {
              this.progress = progress;
            });
          },
          undefined,
          () => { this.progress = null });

        //this.photoServices.upload(this.planningApp.id, nativeElement.files[0])
        this.photoServices.upload(1, nativeElement.files[0])
          .subscribe(photo => {
              this.photos.push(photo)
          });
      }
    }
  }
}

