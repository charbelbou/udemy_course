import { Component, OnInit } from "@angular/core";
import { ToastyService } from "ng2-toasty";
import { VehicleService } from "../services/vehicle.service";

@Component({
  selector: "app-vehicle-form",
  templateUrl: "./vehicle-form.component.html",
  styleUrls: ["./vehicle-form.component.css"],
})
export class VehicleFormComponent implements OnInit {
  // Initializing variables for the makes, models, features, and the selected vehicle.
  makes: any[];
  models: any[];
  vehicle: any = {
    features: [],
    contact: {},
  };
  features: any[];
  // Inject VehicleService
  constructor(
    private VehicleService: VehicleService,
    private ToastySerivce: ToastyService
  ) {}

  // Is triggered when component is rendered
  ngOnInit(): void {
    // Using VehicleService's 'getMakes' function and subscribing to it,
    // Assign returned list of data to makes.
    this.VehicleService.getMakes().subscribe((makes: any[]) => {
      this.makes = makes;
    });
    // Using VehicleService's 'getFeatures' function and subscribing to it,
    // Assign returned list of data to features.
    this.VehicleService.getFeatures().subscribe((features: any[]) => {
      this.features = features;
    });
  }

  // Executed when 'Make' selection changes
  onMakeChange() {
    // Finds the selected make
    var selectedMake = this.makes.find((m) => m.id == this.vehicle.makeId);
    console.log(selectedMake);
    // If selectedMake exists, assigns the list of models to models,
    // otherwise, assigns empty list.
    this.models = selectedMake ? selectedMake.models : [];
    delete this.vehicle.modelId;
  }

  onFeatureToggle(featureId, $event) {
    if ($event.target.checked) {
      this.vehicle.features.push(featureId);
    } else {
      var index = this.vehicle.features.indexOf(featureId);
      this.vehicle.features.splice(index, 1);
    }
  }

  submit() {
    this.vehicle.isRegistered = this.vehicle.isRegistered == "true";
    this.VehicleService.create(this.vehicle).subscribe(
      (x) => console.log(x),
      (err) => {
        console.log(err);
        console.log("Weq");
        this.ToastySerivce.error({
          title: "Error",
          msg: "An unexpected error happened.",
          theme: "bootstrap",
          showClose: true,
          timeout: 5000,
        });
      }
    );
  }
}
