import { SessionStorageService } from '@app/services';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, map } from 'rxjs/operators';
import { ApiBaseService } from '../../../../services/api-base.service';

@Injectable()
export class QuoteService extends ApiBaseService
{

	constructor(private http: HttpClient,session: SessionStorageService)
	{
        super(session);
	}

    // submit quote
    addQuote(data: any) {
        
        return this.http.post(`${this.baseURL()}/group/applications`, data)
        .pipe(
          map((response) => <any>response),
          catchError(this.handleError)
        );
    }

    updateQuote(data: any, refCode: any) {
      return this.http.patch(`${this.baseURL()}/group/applications/${refCode}`, data, { headers: this.headerWithSession()})
      .pipe(
        map((response) => <any>response),
        catchError(this.handleError)
      );
    }


    
}
