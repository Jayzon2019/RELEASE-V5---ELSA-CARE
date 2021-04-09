import { StorageType } from '@app/services/storage-types.enum';
import { environment } from '@environment';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpClient, HttpResponse, HttpHeaders, HttpParams } from '@angular/common/http';

import { Observable, Subscription, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

import * as moment from 'moment';

import { SessionStorageService, FacebookPixelService } from '@app/services';
import { CONSTANTS } from '@app/services/constants';


@Component
({
	selector: 'app-thank-you',
	templateUrl: './thank-you.component.html',
	styleUrls: ['./thank-you.component.css', './thank-you.component.scss']
})
export class ThankYouComponent implements OnInit
{
	plan: string = '';
	isPrimeCare: boolean = false;
	isPrimeSecure: boolean = false;
	isPrimeSecureLite: boolean = false;

	constructor
	(
		private session: SessionStorageService,
		private router: Router,
		private route: ActivatedRoute,
		private http: HttpClient,
		private facebookPixelService: FacebookPixelService
	)
	{

	}

	resetForm()
	{
		this.session.remove(StorageType.POLICYNO);
		this.session.remove('age');
		this.session.remove('getinnerForm');
		this.session.remove('getQuoteForm');
		this.session.remove('UnderWritingStatus');
		this.session.remove(StorageType.QUOTE_INTERNAL_DATA);
		this.session.remove(StorageType.QUOTE_EXTERNAL_DATA);
		this.session.remove(StorageType.ACQUIRED_PLAN);
	}

	backToHome() {
		this.resetForm();
		this.router.navigate(['/']);
	}

	ngOnInit(): void
	{
		const planAcquired = this.session.get(StorageType.ACQUIRED_PLAN);

		if(planAcquired.plan === 'PrimeSecureLite') {
			this.isPrimeSecureLite = true;
		} else if(planAcquired.plan === 'PrimeCare') {
			this.isPrimeCare = true;
		} else {
			this.isPrimeSecure = true;
		}
		this.plan = (planAcquired.plan == 'PrimeSecureLite') ? 
					"Prime Secure Lite" : (planAcquired.plan == 'PrimeCare') ? 
					"Prime Care" : "Prime Secure";
		this.facebookPixelService.track('Purchase');
	}

	

}
