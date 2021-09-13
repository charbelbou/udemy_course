import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { SaveVehicle } from "../models/vehicle";

@Injectable({
  providedIn: "root",
})
// Vehicle Services
export class VehicleService {
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
    return this.http.post("/api/vehicles", vehicle);
  }

  // Getting vehicle through id
  getVehicle(id) {
    return this.http.get("/api/vehicles/" + id);
  }

  // Updating vehicle using object and id
  update(vehicle: SaveVehicle) {
    return this.http.put("api/vehicles/" + vehicle.id, vehicle);
  }

  // Deleting vehicle using id.
  delete(id) {
    return this.http.delete("/api/vehicles" + id);
  }
}
