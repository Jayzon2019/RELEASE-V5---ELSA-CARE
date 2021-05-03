import { Injectable } from '@angular/core';
import { Resolve } from '@angular/router';
import { ApiService, SessionStorageService } from '@app/services';
import { Observable } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { ApiBaseService } from '../services/api-base.service';

@Injectable()
export class PrimeCareSliderResolver extends ApiBaseService implements Resolve<Observable<null>>  {

    constructor(private apiService: ApiService, session: SessionStorageService) { 
        super(session);
    }

    resolve(): Observable<any> | Promise<any> | Observable<null> {
        let url = "/PrimeHero/GetPrimeHeroSliders";
        return this.apiService.sendGetRequest(url)
        .pipe(
            map((response) => <any>response),
            catchError(this.handleError)
          );
    }
}
