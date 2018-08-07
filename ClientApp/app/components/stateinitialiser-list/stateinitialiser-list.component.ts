import { Component, OnInit } from '@angular/core';
import { StateInitialiserService } from '../../services/stateinitialiser.service';

@Component({
  selector: 'app-stateinitialiser-list',
  templateUrl: './stateinitialiser-list.component.html',
  styleUrls: ['./stateinitialiser-list.component.css']
})
export class StateInitialiserListComponent implements OnInit {
  private readonly PAGE_SIZE = 10; 
  queryResult: any = {};
  query: any = {
    pageSize: this.PAGE_SIZE
  };

  constructor(private stateInitialiserService: StateInitialiserService) { }

  ngOnInit() {
    this.populateStateInitialisers();
  }

  private populateStateInitialisers() {
    this.stateInitialiserService.getStateInitialiserList(this.query)
      .subscribe(result => this.queryResult = result);
  }

  onPageChange(page:any) {
    this.query.page = page; 
    this.populateStateInitialisers();
  }

  onFilterChange() {
    this.query.page = 1; 
    this.populateStateInitialisers();
  }
}
