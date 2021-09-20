import { Injectable } from "@angular/core";
import {
  ActivatedRouteSnapshot,
  CanActivate,
  RouterStateSnapshot,
  UrlTree,
} from "@angular/router";
import { AuthService } from "@auth0/auth0-angular";
import { Observable } from "rxjs";
import { MyAuthService } from "./auth.service";

// Used to block accountless users from accessing admin page/creating vehicles/...
// Implements CanActivate
@Injectable({ providedIn: "root" })
export class MyAuthGuard implements CanActivate {
  constructor(protected myAuth: MyAuthService) {}

  // If authenticated then return true
  // else redirect to login page and return false
  canActivate() {
    if (this.myAuth.authenticated()) {
      return true;
    }

    this.myAuth.loginWithRedirect();
    return false;
  }
}
