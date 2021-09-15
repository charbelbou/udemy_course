import { ErrorHandler, Inject, NgZone, isDevMode } from "@angular/core";
import { ToastyService } from "ng2-toasty";
import * as Raven from "raven-js";

export class AppErrorHandler implements ErrorHandler {
  // This class is instantiated before we import ToastyModule,
  // So we need to inject it manually
  constructor(
    private ngZone: NgZone,
    @Inject(ToastyService) private toastyService: ToastyService
  ) {}

  handleError(error: any): void {
    // Runs toastyService error in an angular zone
    this.ngZone.run(() => {
      this.toastyService.error({
        title: "Error",
        msg: "An unexpected error happened.",
        theme: "bootstrap",
        showClose: true,
        timeout: 5000,
      });
    });

    if (!isDevMode()) {
      Raven.captureException(error.originalError | error);
    } else {
      throw error;
    }
  }
}
