import {
  HttpEvent,
  HttpEventType,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ok } from "assert";
import { Observable, Subject } from "rxjs";
import { tap } from "rxjs/operators";

@Injectable({ providedIn: "root" })
export class ProgressService {
  // Subject Objects for the progress
  private uploadProgress: Subject<any>;

  startTracking() {
    this.uploadProgress = new Subject();
    return this.uploadProgress;
  }

  notify(progress) {
    if (this.uploadProgress) {
      this.uploadProgress.next(progress);
    }
  }

  endTracking() {
    if (this.uploadProgress) {
      this.uploadProgress.complete();
    }
  }

  constructor() {}
}

@Injectable({ providedIn: "root" })
// Impelements HttpInterceptor
export class BrowserXhrWithProgress implements HttpInterceptor {
  // Injects ProgressService
  constructor(private ProgressSerivce: ProgressService) {}

  // Implement intercept function
  // req and next parameters
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    if (req.reportProgress) {
      // only intercept when the request is configured to report its progress
      return next.handle(req).pipe(
        tap((event: HttpEvent<any>) => {
          if (event.type === HttpEventType.UploadProgress) {
            // Get the updated progress values and pass them to the uploadProgress subject
            // Pass the total bytes and the percentage
            this.ProgressSerivce.notify({
              total: event.total,
              percentage: Math.round((event.loaded / event.total) * 100),
            });
          }
          if (event.type == HttpEventType.Response) {
            this.ProgressSerivce.endTracking();
          }
        })
      );
    } else {
      // else just pass to next handler
      return next.handle(req);
    }
  }
}
