import { BrowserModule } from "@angular/platform-browser";
import { ErrorHandler, NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { RouterModule } from "@angular/router";
import { ToastyModule } from "ng2-toasty";
import * as Raven from "raven-js";
import "bootstrap";

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
import { ViewVehicleComponent } from "./view-vehicle/view-vehicle.component";
import { PhotoService } from "./services/photo.service";
import { AuthModule } from "@auth0/auth0-angular";
import { ChartModule } from "angular2-chartjs";

import { AdminComponent } from "./admin/admin.component";
import { MyAuthService } from "./services/auth.service";
import { MyAuthGuard } from "./services/auth-guard.service";
import { AdminAuthGuard } from "./services/admin-auth-guard.service";
import { JwtHelperService, JWT_OPTIONS } from "@auth0/angular-jwt";

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
    ViewVehicleComponent,
    AdminComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: "ng-cli-universal" }),
    HttpClientModule,
    FormsModule,
    ChartModule,
    ToastyModule.forRoot(),
    AuthModule.forRoot({
      domain: "dev-r8lrb84i.us.auth0.com",
      clientId: "O1ro1EP2yBHaMT6WRUobovqQdadknSuq",
    }),
    RouterModule.forRoot([
      // Home route
      { path: "", component: HomeComponent, pathMatch: "full" },
      // Two different routes for vehicle page
      // Depending on whether the page is for a new vehicle, or to update an existing one.

      // New vehicle form, need to be signed in to be able to post vehicle
      {
        path: "vehicles/new",
        component: VehicleFormComponent,
        canActivate: [MyAuthGuard],
      },
      // Route to display vehicle information
      { path: "vehicles/:id", component: ViewVehicleComponent },

      // Route to edit vehicle information
      // Need to be signed in
      {
        path: "vehicles/edit/:id",
        component: VehicleFormComponent,
        canActivate: [MyAuthGuard],
      },

      // Vehicle list component route
      { path: "vehicles", component: VehicleListComponent },

      // Checks if user is logged in before redirecting to admin page
      // If not logged in, redirect to login page
      // If logged in but not an admin, redirects back to home
      // Uses AdminAuthGuard
      {
        path: "admin",
        component: AdminComponent,
        canActivate: [AdminAuthGuard],
      },
      { path: "counter", component: CounterComponent },
      { path: "fetch-data", component: FetchDataComponent },
    ]),
  ],
  providers: [
    // Configures the app to use AppErrorHandler instead of ErrorHandler class.
    { provide: ErrorHandler, useClass: AppErrorHandler },
    VehicleService,
    AdminAuthGuard,
    PhotoService,
    MyAuthGuard,
    MyAuthService,
    { provide: JWT_OPTIONS, useValue: JWT_OPTIONS },
    JwtHelperService,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
