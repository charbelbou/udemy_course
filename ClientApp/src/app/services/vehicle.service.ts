import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { SaveVehicle } from "../models/vehicle";

@Injectable({
  providedIn: "root",
})
// Vehicle Services
export class VehicleService {
  // Refactoring the endpoint string to make it easier
  private readonly vehiclesEndpoint = "/api/vehicles/";

  // Inject HttpClient in constructor to access API
  constructor(private http: HttpClient) {}

  // Function to get all makes.
  getMakes() {
    // Uses http to get makes through it's API endpoint ('api/makes')
    return this.http.get("api/makes");
  }
  // Function to get all features.
  getFeatures() {
    // Uses http to get features through it's API endpoint ('api/features')
    return this.http.get("api/features");
  }

  // Creating Vehicle
  create(vehicle) {
    return this.http.post(this.vehiclesEndpoint, vehicle);
  }

  // Getting vehicle through id
  getVehicle(id) {
    return this.http.get(this.vehiclesEndpoint + id);
  }

  // Updating vehicle using object and id
  update(vehicle: SaveVehicle) {
    return this.http.put(this.vehiclesEndpoint + vehicle.id, vehicle);
  }

  // Deleting vehicle using id.
  delete(id) {
    return this.http.delete(this.vehiclesEndpoint + id);
  }
  // Getting vehicles with filter
  // Run the filter through .toQueryString to parse it
  getVehicles(filter) {
    return this.http.get(
      this.vehiclesEndpoint + "?" + this.toQueryString(filter)
    );
  }
  // Converts the object to query string
  toQueryString(obj) {
    var parts = [];
    for (var property in obj) {
      var value = obj[property];
      if (value != null && value != undefined) {
        parts.push(
          encodeURIComponent(property) + "=" + encodeURIComponent(value)
        );
      }
    }
    return parts.join("&");
  }
}
