import { BrowserModule } from "@angular/platform-browser";
import { ErrorHandler, NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { RouterModule } from "@angular/router";
import { ToastyModule } from "ng2-toasty";
import * as Raven from "raven-js";

import { AppComponent } from "./app.component";
import { NavMenuComponent } from "./nav-menu/nav-menu.component";
import { HomeComponent } from "./home/home.component";
import { CounterComponent } from "./counter/counter.component";
import { FetchDataComponent } from "./fetch-data/fetch-data.component";
import { VehicleFormComponent } from "./vehicle-form/vehicle-form.component";
import { VehicleService } from "./services/vehicle.service";
import { AppErrorHandler } from "./app.error-handler";
import { VehicleListComponent } from "./vehicle-list/vehicle-list.component";
import { PaginationComponent } from "./pagination/pagination.component";
import { AngularFontAwesomeModule } from "angular-font-awesome";

// Sentry.io configuration
Raven.config(
  "https://e557de37fe5543f8ad7102cff11f91a5@o998250.ingest.sentry.io/5956847"
).install();

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    VehicleFormComponent,
    VehicleListComponent,
    PaginationComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: "ng-cli-universal" }),
    HttpClientModule,
    FormsModule,
    AngularFontAwesomeModule,
    ToastyModule.forRoot(),
    RouterModule.forRoot([
      { path: "", component: HomeComponent, pathMatch: "full" },
      // Added this route for VehicleFormComponent
      // Two different routes for vehicle page
      // Depending on whether the page is for a new vehicle, or to update an existing one.
      { path: "vehicles/new", component: VehicleFormComponent },
      { path: "vehicles/:id", component: VehicleFormComponent },
      // New Route for viewing vehicles
      { path: "vehicles", component: VehicleListComponent },
      { path: "counter", component: CounterComponent },
      { path: "fetch-data", component: FetchDataComponent },
    ]),
  ],
  providers: [
    // Configures the app to use AppErrorHandler instead of ErrorHandler class.
    { provide: ErrorHandler, useClass: AppErrorHandler },
    VehicleService,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
