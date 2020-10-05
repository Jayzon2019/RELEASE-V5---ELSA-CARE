import { environment } from '.././../environments/environment';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpClient, HttpResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { LocalStorageService, SessionStorageService, LocalStorage, SessionStorage } from 'angular-web-storage';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import * as moment from 'moment';
import { CONSTANTS } from '../services/constants';

@Component
({
	selector: 'app-payment-callback',
	templateUrl: './payment-callback.component.html',
	styleUrls: ['./payment-callback.component.scss']
})
export class PaymentCallbackComponent implements OnInit
{
	basicInformation: any;
	calculationInformation: any;
	title: any;
	beneficialInformation: any;
	personalInformation: any;
	employment: any;
	identication: any;
	age: any;
	getinnerForm: any;

	constructor
	(
		private ngxService: NgxUiLoaderService,
		private routerlink: Router,
		private route: ActivatedRoute,
		public session: SessionStorageService,
		private http: HttpClient
	)
	{
		this.ngxService.start();
		const getQuoteFormData = JSON.parse(localStorage.getItem("getQuoteForm") || "[]");
		const getApplyFormData = JSON.parse(localStorage.getItem("getApplyForm") || "[]");
		this.basicInformation = getQuoteFormData.basicInformation;
		this.calculationInformation = getQuoteFormData.calculatePremium;

		this.beneficialInformation = getApplyFormData.beneficiaryDetails;
		this.personalInformation = getApplyFormData.personalInformation;
		this.employment = getApplyFormData.employment;
		this.identication = getApplyFormData.identification;
		this.getinnerForm = this.session.get("getinnerForm");
		this.age = this.session.get("age");
	}

	ngOnInit(): void
	{
		let headers: HttpHeaders = new HttpHeaders();
		headers = headers.append('Content-Type', 'application/json');
		headers = headers.append('Ocp-Apim-Subscription-Key', environment.primeCareApi.subscriptionKey);

		let options =
		{
			headers: headers,
			params: new HttpParams()
		};

		this.route.queryParams.subscribe(params =>
		{
			var arr =
			{
				"PolicyNo": params.policy,
				"MerchantTxnRef": params.vpc_MerchTxnRef,
				"OrderInfo": params.vpc_OrderInfo,
				"AmountPaid": params.vpc_Amount,
				"TransactionDate": new Date(),
				"TxnResponseCodeDesc": this.getTransactionCodeDescription(params.vpc_TxnResponseCode),
				"TxnResponseCode": params.vpc_TxnResponseCode,
				"PaymentServerMessage": params.vpc_Message,
				"AcqResponseCode": params.vpc_AcqResponseCode,
				"ReceiptNo": params.vpc_ReceiptNo,
				"AuthorizationID": params.vpc_AuthorizeId,
				"OrderID": params.vpc_TransactionNo,
				"CardType": params.vpc_Card,
			}

			let body = JSON.stringify(arr);
			let endpoint = environment.primeCareApi.host + environment.primeCareApi.savePaymentEndpoint;

			//console.log(arr);
			this.session.set('PaymentResponse', arr);

			this.http
				.post(endpoint, body, options)
				.pipe(catchError(this.handleError))
				.subscribe(data =>
				{
					this.ngxService.stop();

					if (params.vpc_TxnResponseCode == 0)
					{
						this.routerlink.navigate(['thank-you']);
					}
					else
					{
						this.routerlink.navigate(['payment-failed', params.vpc_TxnResponseCode]);
					}
				});
		});
	}

	handleError(error: any)
	{
		let errMsg =
			(error.message) ? error.messagess
				: error.status ? `${error.status} - ${error.statusText}`
					: 'API error';

		console.error(errMsg);
		return throwError(error);
	}

	getTransactionCodeDescription(id: string)
	{
		const list = CONSTANTS.PAYMENT_TRANSACTION_RESPONSE;

		// Return match
		for(var i = 0; i < list.length; i++)
			if(list[i].id === id)
				return list[i].name;

		// If no match return null
		return null;
	}
}
