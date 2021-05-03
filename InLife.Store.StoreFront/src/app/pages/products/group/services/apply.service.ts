import { SessionStorageService } from '@app/services';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, map } from 'rxjs/operators';
import { ApiBaseService } from '../../../../services/api-base.service';

@Injectable()
export class ApplyService extends ApiBaseService
{
	constructor(private http: HttpClient,session: SessionStorageService)
	{
        super(session);
	}

    // Upload file
    uploadRequirement(url: any, data: any, fileType: any, fileName: any) {
      let newHeaders = this.headerWithSession();
      newHeaders = newHeaders.append('Content-Type', fileType);
      newHeaders = newHeaders.append('Filename', fileName);

      return this.http.put(url, data, {headers: newHeaders})
      .pipe(
        map((response) => <any>response),
        catchError(this.handleError)
      );
    }
    
}
