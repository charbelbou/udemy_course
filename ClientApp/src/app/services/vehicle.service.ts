import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

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

  create(vehicle) {
    return this.http.post("/api/vehicles", vehicle);
  }
}
