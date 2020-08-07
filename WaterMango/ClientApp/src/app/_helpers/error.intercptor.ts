import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

//import { AuthenticationService } from '../services/authentication.service';
import { CommonService } from './common';
import { Router, RouterStateSnapshot } from '@angular/router';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(
    private common: CommonService,
    // private authenticationService: AuthenticationService,
    private router: Router
  ) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      catchError((err) => {
        if (err.status === 401) {
          // auto logout if 401 response returned from api
          // this.authenticationService.logout();
          this.router.navigate(['/login/' + err.status], {
            queryParams: { returnUrl: request.url },
          });
          //location.reload(true);
        } else if (err.status === 500) {
          if (err.error)
            this.common.notificationError('Error ' + err.status, err.error);
          else
            this.common.notificationError(
              'Server Error',
              'Unknown Error occurred.'
            );
        } else if (err.status === 403) {
          this.common.notificationError(
            'Error ' + err.status,
            'The current user is not authorized to perform this operation.'
          );
        } else if (err.status === 404) {
          this.common.notificationInfo('', 'No data found.');
        } else if (err.status === 400) {
          let error = '';
          for (
            var i = 0;
            i < Object.getOwnPropertyNames(err.error.errors).length;
            ++i
          ) {
            error =
              error +
              err.error.errors[
                Object.getOwnPropertyNames(err.error.errors)[i]
              ][0] +
              '<br/>';
          }
          this.common.notificationError('Validation Error', error);
        } else {
          this.common.notificationError(
            'Server Error ' + err.status,
            err.error
          );
          console.log(err);
        }

        return throwError(err);
      })
    );
  }
}
