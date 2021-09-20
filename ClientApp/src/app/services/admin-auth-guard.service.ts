import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { MyAuthGuard } from "./auth-guard.service";
import { MyAuthService } from "./auth.service";

// Used to block accountless users from accessing admin page
// Implements CanActivate
@Injectable({ providedIn: "root" })
export class AdminAuthGuard extends MyAuthGuard {
  constructor(myAuth: MyAuthService, private router: Router) {
    super(myAuth);
  }

  // If not authenticated then return false
  // and redirect to login page

  // If authenticated, check if admin
  // If not admin, redirect to home
  canActivate() {
    var isAuthenticated = super.canActivate();
    if (isAuthenticated) {
      if (!this.myAuth.isInRole("Admin")) {
        this.router.navigate(["/"]);
      }
      return this.myAuth.isInRole("Admin");
    } else {
      return false;
    }
  }
}
