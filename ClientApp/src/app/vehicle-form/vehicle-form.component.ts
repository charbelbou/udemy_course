import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { ToastyService } from "ng2-toasty";
import { Observable } from "rxjs";
import { VehicleService } from "../services/vehicle.service";
import "rxjs/add/observable/forkJoin";
import { SaveVehicle, Vehicle } from "../models/vehicle";
import * as _ from "underscore";

@Component({
  selector: "app-vehicle-form",
  templateUrl: "./vehicle-form.component.html",
  styleUrls: ["./vehicle-form.component.css"],
})
export class VehicleFormComponent implements OnInit {
  // Initializing variables for the makes, models, features, and the selected vehicle.
  makes: any[];
  models: any[];
  vehicle: SaveVehicle = {
    id: 0,
    makeId: 0,
    modelId: 0,
    isRegistered: false,
    features: [],
    contact: {
      name: "",
      email: "",
      phone: "",
    },
  };
  features: any[];
  // Inject ActivatedRoute, Router, VehicleService, and ToastyService
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private VehicleService: VehicleService,
    private ToastySerivce: ToastyService
  ) {
    // route used to subscribe to url parameters
    // assign the "id" parameter to vehicle.id
    route.params.subscribe((p) => {
      this.vehicle.id = +p["id"] || 0;
    });
  }

  // Is triggered when component is rendered
  ngOnInit(): void {
    // Collection of sources (to get makes and features)
    var sources = [
      this.VehicleService.getMakes(),
      this.VehicleService.getFeatures(),
    ];

    // Pushes 'getVehicle' as a source, only if id is given in url. (Update form)
    if (this.vehicle.id) {
      sources.push(this.VehicleService.getVehicle(this.vehicle.id));
    }

    // ForkJoin all the sources and subscribe
    // Assign data[0] to makes, assign data[1] to features
    // If id isn't 0 (passed from url), set vehicle and popluateModels.
    Observable.forkJoin(sources).subscribe(
      ([makes, features, vehicle]: any[]) => {
        this.makes = makes;
        this.features = features;
        if (this.vehicle.id) {
          this.setVehicle(vehicle);
          this.populateModels();
        }
      },
      // If invalid ID, send back to home
      (err) => {
        if (err.status == 404) {
          this.router.navigate(["/"]);
        }
      }
    );
  }

  // Populates the 'vehicle' object with the object passed as a parameter
  // Used to populate form if updating vehicle object.
  private setVehicle(v: Vehicle) {
    this.vehicle.id = v.id;
    this.vehicle.makeId = v.make.id;
    this.vehicle.modelId = v.model.id;
    this.vehicle.isRegistered = v.isRegistered;
    this.vehicle.contact = v.contact;
    // Pluck features IDs, rather than the object themselves
    this.vehicle.features = _.pluck(v.features, "id");
  }

  // Executed when 'Make' selection changes
  // If make is changed, we need to populate the models again
  onMakeChange() {
    this.populateModels();
    delete this.vehicle.modelId;
  }

  private populateModels() {
    // Finds the selected make
    var selectedMake = this.makes.find((m) => m.id == this.vehicle.makeId);
    // If selectedMake exists, assigns the list of models to models,
    // otherwise, assigns empty list.
    this.models = selectedMake ? selectedMake.models : [];
  }

  // Triggered when feature is selected/deselected
  onFeatureToggle(featureId, $event) {
    // If feature was selected, push it's id to list of features
    if ($event.target.checked) {
      this.vehicle.features.push(featureId);
    }
    // If feature was deselected, remove it's id.
    else {
      var index = this.vehicle.features.indexOf(featureId);
      this.vehicle.features.splice(index, 1);
    }
  }

  // Triggered when form is submitted
  submit() {
    // If form is used to update a vehicle (if id isnt 0), call VehicleService.update on the vehicle object
    // and subscribe
    // If Id isn't 0, update pre existing vehicle
    // If successful, trigger a toastyservice notification

    var result$ = this.vehicle.id
      ? this.VehicleService.update(this.vehicle)
      : this.VehicleService.create(this.vehicle);
    result$.subscribe((vehicle: Vehicle) => {
      this.ToastySerivce.success({
        title: "Success",
        msg: "The vehicle was sucessfully updated",
        theme: "bootstrap",
        showClose: true,
        timeout: 5000,
      });
      // Navigate to vehicle page
      this.router.navigate(["/vehicles/", vehicle.id]);
    });
  }

  // Delete vehicle and navigate back to home.
  delete() {
    if (confirm("Are you sure?")) {
      this.VehicleService.delete(this.vehicle.id).subscribe((x) => {
        this.router.navigate(["/"]);
      });
    }
  }
}
