import {
  Component,
  ElementRef,
  NgZone,
  OnInit,
  ViewChild,
} from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { ToastyService } from "ng2-toasty";
import { PhotoService } from "../services/photo.service";
import { ProgressService } from "../services/progress.service";
import { VehicleService } from "../services/vehicle.service";

@Component({
  selector: "app-view-vehicle",
  templateUrl: "./view-vehicle.component.html",
  styleUrls: ["./view-vehicle.component.css"],
})
export class ViewVehicleComponent implements OnInit {
  @ViewChild("fileInput", { static: false }) fileInput: ElementRef;
  vehicle: any;
  vehicleId: number;
  photos;
  progress: any;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private toasty: ToastyService,
    private photoService: PhotoService,
    private vehicleService: VehicleService,
    private progressService: ProgressService,
    private zone: NgZone
  ) {
    route.params.subscribe((p) => {
      this.vehicleId = +p["id"];
      if (isNaN(this.vehicleId) || this.vehicleId <= 0) {
        router.navigate(["/vehicles"]);
        return;
      }
    });
  }

  // Get photos and vehicle information
  ngOnInit() {
    this.photoService
      .getPhotos(this.vehicleId)
      .subscribe((photos) => (this.photos = photos));

    this.vehicleService.getVehicle(this.vehicleId).subscribe(
      (v) => (this.vehicle = v),
      (err) => {
        if (err.status == 404) {
          this.router.navigate(["/vehicles"]);
          return;
        }
      }
    );
  }

  delete() {
    if (confirm("Are you sure?")) {
      this.vehicleService.delete(this.vehicle.id).subscribe((x) => {
        this.router.navigate(["/vehicles"]);
      });
    }
  }

  // Upload photo
  uploadPhoto() {
    // Get photo from HTMLInputElement

    // Subscribe to uploadprogress subject
    this.progressService.startTracking().subscribe((progress) => {
      console.log(progress);
      this.zone.run(() => {
        this.progress = progress;
      });
    });
    var nativeElement: HTMLInputElement = this.fileInput.nativeElement;
    var file = nativeElement.files[0];
    nativeElement.value = "";
    // Upload image and push to array of photos
    this.photoService.upload(this.vehicleId, file).subscribe(
      (photo) => {
        this.photos.push(photo);
      },
      (err) => {
        this.toasty.error({
          title: "Error",
          msg: err.text,
          theme: "bootstrap",
          showClose: true,
          timeout: 5000,
        });
      }
    );
  }
}
