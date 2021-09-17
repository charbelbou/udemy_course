import { Component } from "@angular/core";
import { JwtHelperService } from "@auth0/angular-jwt";
import { AuthService } from "@auth0/auth0-angular";
import { MyAuthService } from "../services/auth.service";

@Component({
  selector: "app-nav-menu",
  templateUrl: "./nav-menu.component.html",
  styleUrls: ["./nav-menu.component.css"],
})
export class NavMenuComponent {
  isExpanded = false;

  constructor(public auth: MyAuthService) {}

  collapse() {
    this.isExpanded = false;
  }
  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
