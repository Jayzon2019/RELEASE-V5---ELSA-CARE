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
                    const dir = path.substring(0, path.lastIndexOf('/'));
                    var base = 'https://access.insularlife.com.ph/AdvisorsPortal/rest/affiliates/info'; //prod
                    if(origin == 'https://uat-inlifestore.insularlife.com.ph' || origin == 'https://uat2-inlifestore.insularlife.com.ph') {
                        base = 'https://access-tst.insularlife.com.ph/AdvisorsPortal/rest/affiliates/info';
                    } else if (origin == 'https://dev-inlife-estore.azurewebsites.net' || origin == 'http://localhost:4200') {
                        base = 'https://access-tst.insularlife.com.ph/AdvisorsPortal/rest/affiliates/info';
                    }
            
                    var endpoint = `${base}/${code}`;
                    
                    this.target = targetPage;

                    return endpoint;
                }),
                switchMap((endpoint) => this.getAffiliate(endpoint)),
                finalize(() => window.location.href = this.target)
            )
            .subscribe(resp => {
                sessionStorage.setItem('affiliate', JSON.stringify(resp));
            }, error => {
                sessionStorage.removeItem('affiliate');
            });

    }

    public getAffiliate(endpoint: any) {
        let headers: HttpHeaders = new HttpHeaders();
            headers = headers.append('Content-Type', 'application/json');
            headers = headers.append('ClientID', environment.affiliate.clientID);
            headers = headers.append('ClientSecret', environment.affiliate.clientSecret);
    
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
