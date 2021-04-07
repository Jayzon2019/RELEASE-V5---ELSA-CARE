import { environment } from '@environment';

import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, NavigationStart } from '@angular/router';
import { HttpClient, HttpResponse, HttpHeaders, HttpParams } from '@angular/common/http';

import { Observable, throwError } from 'rxjs';
import { catchError, finalize } from 'rxjs/operators';

import { NgxUiLoaderService } from 'ngx-ui-loader';
import * as moment from 'moment';

import { SessionStorageService } from '@app/services';
import { CONSTANTS } from '@app/services/constants';

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
	hasErrorSavingPayment: boolean = false;
	params: any;
	message: string = "Processing please wait....";
	constructor
	(
		private ngxService: NgxUiLoaderService,
		private routerlink: Router,
		private route: ActivatedRoute,
		public session: SessionStorageService,
		private http: HttpClient
	)
	{
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
		this.route.queryParams.subscribe(params =>
		{
			this.params = params;
			setTimeout(() => { //  Wait 10s before execute save payment or redirect
				if (params.vpc_TxnResponseCode === 0 || 
					params.vpc_TxnResponseCode === "0") {
					this.savePayment(this.params);
				} else {
					this.routerlink.navigate(['payment-failed', params.vpc_TxnResponseCode]);
				}
			}, 10000);
			
		});
	}


	savePayment(params) {

		let headers: HttpHeaders = new HttpHeaders();
		headers = headers.append('Content-Type', 'application/json');
		headers = headers.append('Ocp-Apim-Subscription-Key', environment.primeCareApi.subscriptionKey);

		let options =
		{
			headers: headers,
			params: new HttpParams()
		};
		var arr = {
			"PolicyNo": params.policy,
			"MerchantTxnRef": params.vpc_MerchTxnRef,
			"OrderInfo": params.vpc_OrderInfo,
			"AmountPaid": params.vpc_Amount,
			"TransactionDate": this.formatDate(new Date()),
			"TxnResponseCodeDesc": this.getTransactionCodeDescription(params.vpc_TxnResponseCode),
			"TxnResponseCode": params.vpc_TxnResponseCode,
			"PaymentServerMessage": params.vpc_Message,
			"AcqResponseCode": params.vpc_AcqResponseCode,
			"ReceiptNo": params.vpc_ReceiptNo,
			"AuthorizationID": params.vpc_AuthorizeId,
			"OrderID": params.vpc_TransactionNo,
			"CardType": params.vpc_Card,
		}
		let errorMsg = "We apologize things don't appear to be working at the moment. Please try again.";
		let body = JSON.stringify(arr);
		let endpoint = environment.primeCareApi.host + environment.primeCareApi.savePaymentEndpoint;
		this.http
			.post(endpoint, body, options)
			.pipe(finalize(() => this.ngxService.stopAll()))
			.subscribe(data =>
			{
				if(data || data === "true") {
					this.session.set('PaymentResponse', arr);
					this.routerlink.navigate(['prime-secure-lite/thank-you']);
				} else {
					this.message = errorMsg;
					this.hasErrorSavingPayment = true;
				}
				
			}, (error) => {
				this.message = errorMsg;
				this.hasErrorSavingPayment = true;
			});
	}

	tryAgain() {
		this.message = "Processing please wait....";
		this.hasErrorSavingPayment = false;
		this.savePayment(this.params);
	}

	formatDate(dateString: any): string
	{
		let date = moment(dateString);
		let formatted = date.format('MM/DD/YYYY');
		return formatted;
	}
	getTransactionCodeDescription(id: string)
	{
		const list = CONSTANTS.PAYMENT_TRANSACTION_RESPONSE;

		// Return match
		for (var i = 0; i < list.length; i++)
			if (list[i].id === id)
				return list[i].name;

		// If no match return null
		return null;
	}
}
