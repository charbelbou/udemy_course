import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { map } from "rxjs/operators";

@Injectable({ providedIn: "root" })
export class PhotoService {
  constructor(private http: HttpClient) {}

  // Upload photo using photo file and vehicle id
  upload(vehicleId, photo) {
    var formData = new FormData();
    formData.append("file", photo);
    return this.http.post(`/api/vehicles/${vehicleId}/photos`, formData, {
      reportProgress: true,
    });
  }
  // Get photos using vehicle ID
  getPhotos(vehicleId) {
    return this.http.get(`/api/vehicles/${vehicleId}/photos`);
  }
}
