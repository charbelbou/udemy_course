import { Component, OnInit } from "@angular/core";
import { of } from "rxjs";
import { KeyValuePair, Vehicle } from "../models/vehicle";
import { VehicleService } from "../services/vehicle.service";

@Component({
  selector: "app-vehicle-list",
  templateUrl: "./vehicle-list.component.html",
  styleUrls: ["./vehicle-list.component.css"],
})
export class VehicleListComponent implements OnInit {
  private readonly PAGE_SIZE = 3;

  queryResult: any = {};
  makes;
  models;
  query: any = {
    pageSize: this.PAGE_SIZE,
  };

  // Each column for displaying the vehicles
  // has boolean which represents whether it's sortable or not.
  columns = [
    { title: "Id" },
    { title: "Make", key: "make", isSortable: true },
    { title: "Model", key: "model", isSortable: true },
    { title: "Contact Name", key: "contactName", isSortable: true },
    {},
  ];

  constructor(private VehicleService: VehicleService) {}

  ngOnInit() {
    // Get Makes and subscribe
    this.VehicleService.getMakes().subscribe(
      (makes: KeyValuePair[]) => (this.makes = makes)
    );
    // Populate the vehicles
    this.populateVehicles();
  }

  private populateVehicles() {
    // Gets vehicles with this.query
    this.VehicleService.getVehicles(this.query).subscribe((result) => {
      this.queryResult = result;
    });
  }

  onFilterChange($event) {
    if ($event.target.id == "make") {
      if (this.query.makeId) {
        var selectedMake = this.makes.find((m) => m.id == this.query.makeId);
        this.models = selectedMake.models;
      } else {
        this.query = {
          pageSize: this.PAGE_SIZE,
        };
      }
    }
    // If filter changes, needs to return back to page 1 and repopulate vehicles
    this.query.page = 1;
    this.populateVehicles();
  }

  resetFilter() {
    // If filter is reset, return to page 1 and reset page size to the default value
    // then repopulate vehicles
    this.query = {
      page: 1,
      pageSize: this.PAGE_SIZE,
    };
    this.populateVehicles();
  }

  sortBy(columnName) {
    // Sorting by columnName

    // If what we're clicking by is already selected, just flip the ascending to descending or vice versa
    if (this.query.sortBy == columnName) {
      this.query.isSortAscending = !this.query.isSortAscending;
    }

    // If what we're clicking isn't selcted, assign the new sort by, and assign isSortAscending to true
    else {
      this.query.sortBy = columnName;
      this.query.isSortAscending = true;
    }
    // Repopulate vehicles
    this.populateVehicles();
  }

  onPageChange(page) {
    // Changing page, assign page to query.page, and repopulate vehicles.
    this.query.page = page;
    this.populateVehicles();
  }
}
