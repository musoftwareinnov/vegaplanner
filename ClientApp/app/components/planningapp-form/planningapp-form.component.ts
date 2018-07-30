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
    customerId: 0,
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
    //                               });
    
    //                                }
    // route.params.subscribe(
    //                       p => {
    //                         this.planningApp.id = +p['id'] || 0;
    //                         this.planningAppService.getPlanningApp(this.planningApp.id)
    //                         .subscribe(
    //                           v => this.planningApp = v,
    //                           err => {
    //                             if (err.status == 404) {
    //                               this.router.navigate(['/planningapps']);
    //                               return; 
    //                             }
    //                         });
    //                       });
    //}


    
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
            msg: 'Application moved to next state : ' + this.planningApp.nextState,
            theme: 'bootstrap',
            showClose: true,
            timeout: 5000
          })     
          this.router.navigate(['/planningapps/', this.planningApp.id])
          //this.router.navigate(['/planningapps/26']);
        });
  }



  //   var sources = [  
  //     // this.vehicleService.getMakes(),
  //     // this.vehicleService.getFeatures(),
  //   ];

  //   if (this.planningApp.id)
  //   {
  //     console.warn("plamnning App");
  //     console.warn(this.planningApp.id);
  //     sources.push(this.planningAppService.getPlanningApp(this.planningApp.id));

  //   }
  //   Observable.forkJoin(sources).subscribe(data => {
  //     // this.makes = data[0];
  //     // this.features = data[1];

  //     if (this.planningApp.id) {
  //       this.setPlanningApp(data[2]);
  //       this.populateModels();
  //     }
  //   }, err => {
  //     if (err.status == 404)
  //       this.router.navigate(['/home']);
  //   });
  //   // this.vehicleService.getMakes().subscribe(makes =>  this.makes = makes);
  //   // this.vehicleService.getFeatures().subscribe(features => this.features = features)
  // }

  // private setPlanningApp(v:PlanningApp) {
  //   this.planningApp.id = v.id;
  //   this.planningApp.customerId = v.customerId
  //   this.planningApp.name = v.name
  //   // this.planningApp.businessDate = v.businessDate
  //   // this.planningApp.planningStatus = v.planningStatus
  //   // this.planningApp.currentStateStatus = v.currentStateStatus
  //   // this.planningApp.currentState = v.currentState
  //   // this.planningApp.expectedStateCompletionDate = v.expectedStateCompletionDate
  //   // this.planningApp.nextState = v.nextState
  //   // this.planningApp.completionDate = v.completionDate
  //   console.warn(v.name);
  //}

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

