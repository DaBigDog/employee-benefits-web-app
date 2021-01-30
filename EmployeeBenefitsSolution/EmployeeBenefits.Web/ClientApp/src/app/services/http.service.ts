import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders,  HttpErrorResponse } from "@angular/common/http";

import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  private jsonHeader: HttpHeaders = new HttpHeaders().set('Content-Type', 'application/json');


  constructor(private httpClient: HttpClient) {

  }


  public sendGetRequest(url: string): Observable<any> {
    return this.httpClient.get(url)
      .pipe(catchError(this.handleError));
  }

  public sendDeleteRequest(url: string): Observable<any> {
    return this.httpClient.delete(url)
      .pipe(catchError(this.handleError));
  }

  public sendPostRequest(url: string, data: any): Observable<any> {
    return this.httpClient.post(url, JSON.stringify(data), { headers: this.jsonHeader })
      .pipe(catchError(this.handleError));
  }

  public sendPutRequest(url: string, data: any): Observable<any> {
    return this.httpClient.put(url, JSON.stringify(data), { headers: this.jsonHeader })
      .pipe(catchError(this.handleError));
  }

  /*** We need some type of system to better alert the user and improve this! ***/
  private handleError(error: HttpErrorResponse): Observable<any> {
    let errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;

    console.log(errorMessage);
    return throwError(errorMessage);
  }

}
