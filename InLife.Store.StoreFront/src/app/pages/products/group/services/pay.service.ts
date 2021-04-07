import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, map } from 'rxjs/operators';
import { ApiBaseService } from '../../../../services/api-base.service';
import { SessionStorageService } from '@app/services';

@Injectable()
export class PayService extends ApiBaseService
{
	constructor(private http: HttpClient,session: SessionStorageService)
	{
        super(session);
	}

    // Upload file
    payment(refCode:string, data: any) {
        return this.http.put(`${this.baseURL()}/group/applications/${refCode}/payment`, data, {headers :this.headerWithSession()})
        .pipe(
          map((response) => <any>response),
          catchError(this.handleError)
        );
    }
    
}
