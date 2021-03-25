import { environment } from './../../../../../environments/environment';
import { SessionStorageService } from '@app/services';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { ApiBaseService } from './api-base.service';

@Injectable()
export class ApplicationStatusService extends ApiBaseService
{
	constructor(private http: HttpClient, session: SessionStorageService)
	{
    super(session);
	}
    //Get application summary
    public getApplicationSummary(refCode: any) {
      return this.http.get(`${this.baseURL()}/group/applications/${refCode}/summary`, { headers: this.headerWithSession()})
      .pipe(
        map((response) => <any>response),
        catchError(this.handleError)
      );
    } 

    // Request Session
    public getRequestSession(refCode: any, otpCode: any) {
        return this.http.get(`${this.baseURL()}/group/applications/${refCode}/request-session?otp=${otpCode}`)
        .pipe(
          map((response) => <any>response),
          catchError(this.handleError)
        );
    }

    // Request application status
    public getApplicationStatus(refCode: any) {

        return this.http.get(`${this.baseURL()}/group/applications/${refCode}/status`, { headers: this.headerWithSession()})
        .pipe(
          map((response) => <any>response),
          catchError(this.handleError)
        );
    }

    // Cancel Application
    public cancelApplication(refCode: any, data: any) {
      
      return this.http.put(`${this.baseURL()}/group/applications/${refCode}/cancel`, data, { headers: this.headerWithSession()})
        .pipe(
          map((response) => <any>response),
          catchError(this.handleError)
        );
    }

    // OTP Message
    public sendOTPRequest(refCode: any, data:any =''): Observable<any> {
      return this.http.put(`${this.baseURL()}/group/applications/${refCode}/request-otp`, data)
      .pipe(
        map((response) => <any>response),
        catchError(this.handleError)
      );
  }
}
