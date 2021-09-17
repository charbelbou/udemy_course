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

// Used to block accountless users from accessing admin page
// Implements CanActivate
@Injectable({ providedIn: "root" })
export class MyAuthGuard implements CanActivate {
  constructor(public myAuth: MyAuthService) {}

  // If authenticated then return true
  // else redirect to login page and return false
  canActivate() {
    console.log(this.myAuth.Authenticated);
    if (this.myAuth.Authenticated) {
      return true;
    }

    this.myAuth.loginWithRedirect();
    return false;
  }
}
