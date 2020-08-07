import { Injectable } from '@angular/core';
import {
  HttpClient,
  HttpHeaders,
  HttpRequest,
  HttpEventType,
  HttpInterceptor,
  HttpResponse,
  HttpHandler,
  HttpEvent,
  HttpErrorResponse,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { MyConfig } from './../../myConfig';

@Injectable({
  providedIn: 'root',
})
export class HttpRqstService {
  baseurl = MyConfig.baseurl;

  // Http Headers
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };
  constructor(private http: HttpClient) {}

  // POST
  post(endPointUrl, data): Observable<any> {
    return this.http.post<any>(
      this.baseurl + endPointUrl,
      JSON.stringify(data),
      this.httpOptions
    );
  }

  public upload(endPointUrl, formData, token) {
    return this.http.post<any>(this.baseurl + endPointUrl, formData, {
      headers: new HttpHeaders({
        token: token,
      }),
      reportProgress: true,
      observe: 'events',
    });
  }

  // GET
  absoluteGet(endPointUrl): Observable<any> {
    return this.http.get<any>(endPointUrl);
  }

  // GET
  get(endPointUrl): Observable<any> {
    return this.http.get<any>(this.baseurl + endPointUrl);
  }

  // PUT
  put(endPointUrl, data): Observable<any> {
    return this.http.put<any>(
      this.baseurl + endPointUrl,
      JSON.stringify(data),
      this.httpOptions
    );
  }

  // DELETE
  delete(endPointUrl) {
    return this.http.delete<any>(this.baseurl + endPointUrl, this.httpOptions);
  }
}
