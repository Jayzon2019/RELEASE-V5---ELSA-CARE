import { environment } from '.././../environments/environment';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { LocalStorageService, SessionStorageService, LocalStorage, SessionStorage } from 'angular-web-storage';
import { HttpClient, HttpResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import * as moment from 'moment';


@Component
({
	selector: 'app-home',
	templateUrl: './default.component.html',
	styleUrls: ['./default.component.scss']
})
export class DefaultComponent implements OnInit
{
	constructor
	(
		public session: SessionStorageService,
		private route: ActivatedRoute,
		private router: Router,
		private http: HttpClient,
		private ngxService: NgxUiLoaderService
	)
	{
		this.route.queryParams.subscribe(params =>
		{
			//let target = params['redirect'];
			//console.log(target);
			//if(target)
			//{
			//	this.router.navigate(['/' + target]);
			//}
		});
	}

	ngOnInit(): void
	{
	}
}
