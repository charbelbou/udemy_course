import { Injectable } from "@angular/core";
import { JwtHelperService } from "@auth0/angular-jwt";
import { AuthService } from "@auth0/auth0-angular";

@Injectable({ providedIn: "root" })
export class MyAuthService {
  roles: string[] = [];
  Profile: any;
  Authenticated: boolean;
  AccessToken: any;

  constructor(public auth: AuthService) {
    this.Profile = JSON.parse(localStorage.getItem("profile"));
    this.readRolesFromLocalStorage();

    // Check if user is authenticated or not
    this.auth.isAuthenticated$.subscribe((Authenticated) => {
      this.Authenticated = Authenticated;
    });

    // Get token
    this.auth.idTokenClaims$.subscribe((token) => {
      console.log(token);
      // If token exists, set item in local storage, then read from it
      if (token) {
        localStorage.setItem("token", token.__raw);

        this.readRolesFromLocalStorage();
      }
      // Assign token
      this.AccessToken = token;
    });
    // Get profile and save it to local storage
    // and assign profile
    this.auth.user$.subscribe((profile) => {
      localStorage.setItem("profile", JSON.stringify(profile));
      this.Profile = profile;
    });
  }

  // Checks if user is admin
  // Used in template
  isInRole(roleName) {
    return this.roles.indexOf(roleName) > -1;
  }

  // Read roles from storage
  private readRolesFromLocalStorage() {
    // Get token from storage
    var storedToken = localStorage.getItem("token");
    // If token exists, decode it and extract the roles
    if (storedToken) {
      var JwtHelper = new JwtHelperService();
      var decodedToken = JwtHelper.decodeToken(storedToken);
      this.roles = decodedToken["https://vega.com/roles"];
    }
  }

  // Used to login in with redirected link
  loginWithRedirect() {
    this.auth.loginWithRedirect();
  }

  // Logout and clear items from storage
  logout() {
    this.auth.logout();
    localStorage.removeItem("token");
    localStorage.removeItem("profile");
    this.roles = [];
    this.Profile = null;
  }
}
