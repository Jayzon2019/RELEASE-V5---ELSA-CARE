import { SessionStorageService } from '@app/services';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, map } from 'rxjs/operators';
import { ApiBaseService } from '../../services/api-base.service';
import { environment } from '@environment';

@Injectable()
export class PSLiteService extends ApiBaseService
{
	constructor(private http: HttpClient, session: SessionStorageService)
	{
        super(session);
	}

  saveQuoteInternalAPI(data: any) {
    let headers: HttpHeaders = new HttpHeaders();
		headers = headers.append('Content-Type', 'application/json');
    return this.http.post(`${this.baseURLPayment()}${environment.appApi}`+`/prime-secure-lite`, data, {headers: headers})
    .pipe(
      map((response) => <any>response),
      catchError(this.handleError)
    );
  }

  createUnderWritingStatus(data: any) {
    let newHeaders = this.headerPaymentGateWay();
    return this.http.post(`${this.baseURLPayment()}${environment.primeCareApi.createQuoteEndpoint}`, data, {headers: newHeaders})
    .pipe(
      map((response) => <any>response),
      catchError(this.handleError)
    );
  }

  requestPolicyNo(data: any) {
    let newHeaders = this.headerPaymentGateWay();
    return this.http.post(`${this.baseURLPayment()}${environment.primeCareApi.createApplicationEndpoint}`, data, {headers: newHeaders})
    .pipe(
      map((response) => <any>response),
      catchError(this.handleError)
    );
  }

  savePayment(data: any) {
    let newHeaders = this.headerPaymentGateWay();
    return this.http.post(`${this.baseURLPayment()}${environment.primeCareApi.savePaymentEndpoint}`, data, {headers: newHeaders})
    .pipe(
      map((response) => <any>response),
      catchError(this.handleError)
    );
  }
    
}
