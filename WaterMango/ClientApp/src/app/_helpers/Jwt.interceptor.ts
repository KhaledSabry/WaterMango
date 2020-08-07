import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable } from 'rxjs';

//import { AuthenticationService } from '../services/authentication.service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  //Interceptor to add authorization header with jwt token if available
  // constructor(private authenticationService: AuthenticationService) { }

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    let currentUser = null; // this.authenticationService.currentUserValue;
    //Note : since we don't have the users this Interceptor is useless
    if (currentUser && currentUser.authToken) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${currentUser.authToken}`,
        },
      });
    }

    return next.handle(request);
  }
}
