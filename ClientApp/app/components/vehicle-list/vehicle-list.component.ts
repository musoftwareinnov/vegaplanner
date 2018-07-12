import { VehicleService } from './../../services/vehicle.service';
import { Component, OnInit } from '@angular/core';
import { Vehicle, KeyValuePair } from './../../models/vehicle';

@Component({
  templateUrl: './vehicle-list.component.html'
})
export class VehicleListComponent implements OnInit {
  //vehicles: Vehicle[] = [];
  private readonly PAGE_SIZE = 10; 
  queryResult: any = {};
  makes: any[] = [];
  models: KeyValuePair[] = [];
  query: any = {
    pageSize: this.PAGE_SIZE
  };

  constructor(private VehicleService: VehicleService) { }

  ngOnInit() {
    this.VehicleService.getMakes()
      .subscribe(makes => this.makes = makes);

      // this.VehicleService.getModels()
      // .subscribe(models => this.models = models);

    this.populateVehicles();
    //this.populateModels();
  }

  private populateVehicles() {
    this.VehicleService.getVehicles(this.query)
      .subscribe(result => this.queryResult = result);
  }

  private populateModels() {
    var selectedMake = this.makes.find(m => m.id == this.query.makeId);
    this.models = selectedMake ? selectedMake.models : [];
  }

  onPageChange(page:any) {
    this.query.page = page; 
    this.populateVehicles();
  }

  onFilterChange() {
    this.query.page = 1; 
    this.populateVehicles();
  }

  resetFilter() {
    this.query = {
      page: 1,
      pageSize: this.PAGE_SIZE
    };
    this.populateVehicles();
  }

  sortBy(columnName:any) {
    if(this.query.sortBy === columnName) {
      this.query.isSortAscending = !this.query.isSortAscending ;
    }
    else {
      this.query.sortBy = columnName;
      this.query.isSortAscending = true;
    }
    this.populateVehicles();
  }
}
