import { ProgressService } from '../../services/progress.service';
import { PhotoService } from '../../services/photo.service';
import { ToastyService } from 'ng2-toasty';
import { VehicleService } from '../../services/vehicle.service';
import { Component, OnInit, ViewChild, ElementRef, NgZone } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { elementDef } from '@angular/core/src/view';

@Component({
  templateUrl: 'view-vehicle.component.html'
})
export class ViewVehicleComponent implements OnInit {
  @ViewChild('fileInput') fileInput: ElementRef | undefined;
  vehicle: any;
  vehicleId: number = 0; 
  photos: any[] = [];
  progress: any;

  constructor(
    private zone: NgZone,
    private route: ActivatedRoute, 
    private router: Router,
    private toasty: ToastyService,
    private progressService: ProgressService,
    private photoServices: PhotoService,
    private vehicleService: VehicleService) { 

    route.params.subscribe(p => {
      this.vehicleId = +p['id'];
      if (isNaN(this.vehicleId) || this.vehicleId <= 0) {
        router.navigate(['/vehicles']);
        return; 
      }
    });
  }

  ngOnInit() { 

    this.photoServices.getPhotos(this.vehicleId)
      .subscribe(photos => this.photos = photos);

    this.vehicleService.getVehicle(this.vehicleId)
      .subscribe(
        v => this.vehicle = v,
        err => {
          if (err.status == 404) {
            this.router.navigate(['/vehicles']);
            return; 
          }
        });
  }

  delete() {
    if (confirm("Are you sure?")) {
      this.vehicleService.delete(this.vehicle.id)
        .subscribe(x => {
          this.router.navigate(['/vehicles']);
        });
    }
  }

  //To test on chrome browser set 'No Throttling' in DevTools -> network
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

        this.photoServices.upload(this.vehicleId, nativeElement.files[0])
          .subscribe(photo => {
              this.photos.push(photo)
          });
      }
    }
  }
}