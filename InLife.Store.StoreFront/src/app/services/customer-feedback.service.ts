import { SessionStorageService } from '@app/services';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { StorageType } from '@app/services/storage-types.enum';
import { environment } from '@environment';
import { ApiBaseService } from '@app/services/api-base.service';

@Injectable()
export class CustomerFeedBackService extends ApiBaseService
{
	public headers_object: any;
	public sidebar_value = new Subject();
	public token: any = '';
	constructor(private http: HttpClient, session: SessionStorageService)
	{
      super(session);
	}

    // Upload file
    customerFeedBack(refCode: string, data: any) {
        return this.http.put(`${this.baseURL()}/group/applications/${refCode}/feedback`, data, {headers: this.headerWithSession()})
        .pipe(
          map((response) => <any>response),
          catchError(this.handleError)
        );
    }
    
}
