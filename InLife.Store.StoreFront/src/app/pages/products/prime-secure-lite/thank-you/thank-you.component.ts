import { environment } from '@environment';

import { Injectable, Injector, OnDestroy } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpResponse, HttpHeaders, HttpParams, HttpErrorResponse } from '@angular/common/http';
import { DomSanitizer } from '@angular/platform-browser';
import { getMatScrollStrategyAlreadyAttachedError } from '@angular/cdk/overlay/scroll/scroll-strategy';

import { NgxUiLoaderService } from 'ngx-ui-loader';
import { Observable, Subscription, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';

import * as moment from 'moment';
import { jsPDF } from 'jspdf';

import { ApiService, SessionStorageService } from '@app/services';
import { CONSTANTS } from '@app/services/constants';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { GeneralMessagePromptComponent } from '@app/shared/component/prompt-message/general-message-prompt.component';

@Injectable({ providedIn: 'root' })
@Component
({
	selector: 'app-thank-you',
	templateUrl: './thank-you.component.html',
	styleUrls: ['./thank-you.component.css', './thank-you.component.scss'],
	providers: []
})
export class ThankYouComponent implements OnInit, OnDestroy
{
	CONSTANTS = CONSTANTS;
	basicInformation: any;
	calculationInformation: any;
	healthCondition: any;
	beneficialInformation: any;
	personalInformation: any;
	declarationFormInformation: any;
	employmentFormInformation: any;
	identificationFormInformation: any;
	insuredIdentityDocumentImageData: string;
	extensionData: any;
	employment: any;
	identication: any;
	age: any;
	getinnerForm: any;
	pdfblogImage: any;
	totalCashBenefit: any;
	paymentAmount: string;
	landline: string;
	policyNo: string;
	health1: string;
	health2: string;
	health3: string;
	privacyFile: any;
	policy: any;
	dateNow: string;
	activatedRouteSubscription$: Subscription;
	txn = {} as any;

	constructor
	(
		private router: Router,
		private injector: Injector,
		public session: SessionStorageService,
		private http: HttpClient,
		private apiService: ApiService,
		private ngxService: NgxUiLoaderService,
		private sanitizer: DomSanitizer,
		private activatedRoute: ActivatedRoute,
		private dialog: MatDialog
	)
	{
		// const getQuoteFormData = JSON.parse(this.session.get("getQuoteForm") || "[]");
		// const getApplyFormData = JSON.parse(this.session.get("getApplyForm") || "[]");
		// const extension = JSON.parse(this.session.get("extensionData") || "[]");
		const getQuoteFormData = this.session.get("getQuoteForm") || "[]";
		const getApplyFormData = this.session.get("getApplyForm") || "[]";
		const extension = this.session.get("extensionData") || "[]";
		console.log(getQuoteFormData, getApplyFormData)
		this.getFile();
		this.basicInformation = getQuoteFormData.basicInformation;
		this.calculationInformation = getQuoteFormData.calculatePremium;
		//this.totalCashBenefit = this.calculationInformation.totalCashBenefit.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
		this.healthCondition = getQuoteFormData.healthCondition;
		this.beneficialInformation = getApplyFormData.beneficiaryDetails;
		this.declarationFormInformation = getApplyFormData.declarationForm;
		this.employmentFormInformation = getApplyFormData.employment;
		this.identificationFormInformation = getApplyFormData.identification;
		this.personalInformation = getApplyFormData.personalInformation;
		this.extensionData = extension;
		this.insuredIdentityDocumentImageData = this.session.get("insuredIdentityDocumentImageData");

		this.employment = getQuoteFormData.employment;
		this.identication = getQuoteFormData.identification;
		this.getinnerForm = this.session.get("getinnerForm");
		this.age = this.session.get("age");
	}

	ngOnInit(): void
	{
		this.dateNow = new Date().toLocaleString();

		this.activatedRouteSubscription$ = this.activatedRoute.queryParams.subscribe((params)=>{
			
			if(params['target'] === 'payment-callback') {
				this.ngxService.start();

				this.txn.MerchTxnRef = params['vpc_MerchTxnRef'];
				this.txn.OrderInfo = params['vpc_OrderInfo'];
				this.txn.Amount = params['vpc_Amount'] === '250000'? '2500.00' : '3000.00';
				this.txn.TransactionDate = this.formatDate(new Date());
				this.txn.txnResponseCode = params['vpc_TxnResponseCode'];
				this.txn.TxnResponseCodeDesc = CONSTANTS.PAYMENT_TRANSACTION_RESPONSE.find(x => x.id === params['vpc_TxnResponseCode']).name;
				this.txn.Message = params['vpc_Message'];
				this.txn.AcqResponseCode = params['vpc_AcqResponseCode'];
				this.txn.ReceiptNo = params['vpc_ReceiptNo'];
				this.txn.AuthorizeId = params['vpc_AuthorizeId'];
				this.txn.TransactionNo = params['vpc_TransactionNo'];
				this.txn.Card = params['vpc_Card'];
				this.txn.policy = params['policy'];
				
				if(this.txn.txnResponseCode === '0') { //Transaction Success

					let payload = {
						"MerchantTxnRef": this.txn.MerchTxnRef,
						"OrderInfo": this.txn.OrderInfo,
						"AmountPaid": this.txn.Amount,
						"TransactionDate": this.txn.TransactionDate,
						"TxnResponseCodeDesc": this.txn.TxnResponseCodeDesc,
						"TxnResponseCode": this.txn.txnResponseCode,
						"PaymentServerMessage": this.txn.Message,
						"AcqResponseCode": this.txn.AcqResponseCode,
						"ReceiptNo": this.txn.ReceiptNo,
						"AuthorizationID": this.txn.AuthorizeId,
						"OrderID": this.txn.TransactionNo,
						"CardType": this.txn.Card,
						"PolicyNo": this.txn.policy
					};

					let headers: HttpHeaders = new HttpHeaders();
					headers = headers.append('Content-Type', 'application/json');
					headers = headers.append('Ocp-Apim-Subscription-Key', environment.primeCareApi.subscriptionKey);
			
					let options =
					{
						headers: headers,
						params: new HttpParams()
					};
			
					let body = JSON.stringify(payload);

					let endpoint = environment.primeCareApi.host  + environment.primeCareApi.savePaymentEndpoint;
					
					this.http
					.post(endpoint, body, options)
					.pipe(
						retry(1),
					)
					.subscribe((data: any) =>
					{
						console.log('success save');
						console.log(data);

						let isSuccess = data;
						this.ngxService.stopAll();
						if (this.isNullOrWhiteSpace(isSuccess))
						{
							const dialogRef = this.dialog.open(GeneralMessagePromptComponent, {
								width: '300px',
								data: {
									message: `We apologize things don't appear to be working at the moment. Please try again.`
								}
							});
						}
					}, (error) =>{
						console.log('error');
						console.log(error);
						this.ngxService.stopAll();
						const dialogRef = this.dialog.open(GeneralMessagePromptComponent, {
							width: '300px',
							data: {
								message: `We apologize things don't appear to be working at the moment. Please try reload the page.`
							}
						});
					});
				}
				else {//Failed - return to Plan Summary
					this.ngxService.stopAll();
					this.router.navigate(['prime-secure-lite/pay']);
				}
			}
		});
	}

	ngOnDestroy() {
		if (this.activatedRouteSubscription$) this.activatedRouteSubscription$.unsubscribe();
	}

	getFile()
	{
		var url = "/Home/GetFiles";
		this.apiService.sendGetRequest(url).subscribe((responseBody) =>
		{
			this.privacyFile = "data:application/pdf;base64," + responseBody[0].primeCareFile;
			this.policy = "data:application/pdf;base64," + responseBody[1].primeCareFile;
		});

	}

	sanitize(url: string)
	{
		return this.sanitizer.bypassSecurityTrustUrl(url);
	}

	isNullOrWhiteSpace(value: string)
	{
		if (typeof value === 'undefined' || value == null)
			return true;

		return value.replace(/\s/g, '').length < 1;
	}
	
	formatDate(dateString: any): string
	{
		let date = moment(dateString);
		let formatted = date.format('MM/DD/YYYY');
		return formatted;
	}

	getReferenceData(list: any[], item: any)
	{
		let id = Number(item.value);

		// If 0 return null
		if (id === 0)
			return null;

		// Return match
		for (var i = 0; i < list.length; i++)
			if (Number(list[i].id) === id)
				return list[i];

		// If no match return null
		return null;
	}

	getReferenceDataName(list: any[], item: any): string
	{
		let match = this.getReferenceData(list, item);

		if (match === null)
			return null;

		return String(match.name).toUpperCase();
	}

	getReferenceDataId(list: any[], item: any): number | null
	{
		let match = this.getReferenceData(list, item);

		if (match === null)
			return null;

		return Number(match.id);
	}

	getReferenceDataById(list: any[], id: any): any
	{
		let refId: number = Number(id);

		// If 0 return null
		if (refId === 0)
			return null;

		// Return match
		for (var i = 0; i < list.length; i++)
			if (Number(list[i].id) === refId)
				return list[i];

		// If no match return null
		return null;
	}

}
