import { SessionStorageService } from './../../services/session-storage.service';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from '@environment';
import { takeUntil, finalize, map, catchError, switchMap } from 'rxjs/operators';
import { ActivatedRoute } from '@angular/router';
import { ApiBaseService } from '@app/services/api-base.service';

@Component({
	selector: 'affiliate',
    template: '',
})
export class AffiliateComponent extends ApiBaseService implements OnInit
{

    target: string;

	constructor(private http: HttpClient, session: SessionStorageService, private route: ActivatedRoute) { 
        super(session);
        this.route.queryParams
            .pipe(
                map(params => {
                    const productPages =
                    {
                        'prime-care':        'prime-care',
                        'prime-secure':      'prime-secure',
                        'prime-secure-lite': 'prime-secure-lite'
                    };
                    console.log(params);
                    const code = params.affcode ?? params.code ?? params.c;
                    const product = params.product ?? params.prod ?? params.p;
            
                    const targetPage = productPages[product] ? productPages[product] : '/';
                    const path = window.location.pathname;
                    const origin = window.location.origin;


                    let endpoint = environment.appApi.host + `/affiliates/${code}`;
                    this.target = targetPage;

                    debugger;

                    return endpoint;
                }),
                switchMap((endpoint) => this.getAffiliate(endpoint))
            )
            .subscribe(resp => {
                session.set('affiliate', resp);
                window.location.href = this.target
            }, error => {
                session.remove('affiliate');
            });

    }

    public getAffiliate(endpoint: any) {
        let headers: HttpHeaders = new HttpHeaders();
            headers = headers.append('Content-Type', 'application/json');
    
            let options =
            {
                headers: headers,
                params: new HttpParams()
            };
    
            return this.http
            .get(endpoint, options)
            .pipe(
                map((response) => <any>response),
                catchError(this.handleError)
              );
    } 

	ngOnInit(): void
	{
	}
}
