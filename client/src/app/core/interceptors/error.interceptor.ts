import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';

@Injectable()

//implements http interceptors
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router, private toastr: ToastrService) {}

    // catches error that come back in our http request
  intercept(req: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(req).pipe(
      catchError(error => {
        if (error) {
          if (error.status === 400) {
            if (error.error.errors) {         //if validation error throw back to component. if just 400 error serve toastr
              throw error.error;
            } else {
              this.toastr.error(error.error.message, error.error.statusCode);
            }
          }

          if (error.status === 401) {
            this.toastr.error(error.error.message, error.error.statusCode);
          }

          if (error.status === 404) {
            this.router.navigateByUrl('/not-found');
          }

          if (error.status === 500) {
            const navigationExtras: NavigationExtras = {state: {error: error.error}};
            this.router.navigateByUrl('/server-error', navigationExtras)
          }
        }
        return throwError(error);
      })
    );
  }
}
