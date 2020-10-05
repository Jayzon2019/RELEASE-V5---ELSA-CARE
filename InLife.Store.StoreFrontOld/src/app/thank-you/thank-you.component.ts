import { environment } from '.././../environments/environment';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpClient, HttpResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { LocalStorageService, SessionStorageService, LocalStorage, SessionStorage } from 'angular-web-storage';
import { FacebookPixelService } from '../services/facebook-pixel.service';
import * as moment from 'moment';

@Component
({
	selector: 'app-thank-you',
	templateUrl: './thank-you.component.html',
	styleUrls: ['./thank-you.component.scss']
})
export class ThankYouComponent implements OnInit
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
	policyNo: string;
	transactionDate: any;
	transactionReference: any;
	transactionNo: any;
	amount: any;
	paymentResponse: any;
	paymentAmount: string;
	gender: string;
	receiptNumber: any;

	constructor
	(
		public session: SessionStorageService,
		private route: ActivatedRoute,
		private http: HttpClient,
		public facebookPixelService: FacebookPixelService
	)
	{

		const getQuoteFormData = JSON.parse(this.session.get("getQuoteForm") || "[]");
		const getApplyFormData = JSON.parse(this.session.get("getApplyForm") || "[]");
		const paymentResponse = this.session.get("PaymentResponse");

		this.transactionReference = paymentResponse.MerchantTxnRef;
		this.transactionDate = paymentResponse.TransactionDate;
		this.receiptNumber = paymentResponse.ReceiptNo;

		this.basicInformation = getQuoteFormData.basicInformation;
		this.calculationInformation = getQuoteFormData.calculatePremium;

		this.beneficialInformation = getApplyFormData.beneficiaryDetails;
		this.personalInformation = getApplyFormData.personalInformation;
		this.employment = getApplyFormData.employment;
		this.identication = getApplyFormData.identification;
		this.getinnerForm = this.session.get("getinnerForm");
		this.age = this.session.get("age");
		this.policyNo = this.session.get('policyNo');
		this.paymentAmount = paymentResponse.AmountPaid;
		this.gender = this.calculationInformation.gender == 7 ? "Female" : "Male";
		this.resetForm();
	}

	resetForm() {
		// this.session.set("getQuoteForm", "");
		// this.session.set("getApplyForm", "");
		// this.session.set("extensionData", "");
		// this.session.set("insuredIdentityDocumentImageData", "");
		// this.session.set("getinnerForm", "");
		// this.session.set("age", "");
		// this.session.set("PostQuote", "");
		// this.session.set("insuredIdentityDocumentImagePreview", "");
		// this.session.set("CreateApplication", "");
		this.session.clear();
	}

	ngOnInit(): void
	{
		this.facebookPixelService.track('Purchase');

		// let headers: HttpHeaders = new HttpHeaders();
		// headers = headers.append('Content-Type', 'application/json');
		// headers = headers.append('Ocp-Apim-Subscription-Key', environment.primeCareApi.subscriptionKey);

		// let options =
		// {
		// 	headers: headers,
		// 	params: new HttpParams()
		// };

		// this.route.queryParams.subscribe(params => {
		// 	let arr =
		// 	{
		// 		"PolicyNo": this.policyNo,
		// 		"MerchantTxnRef": params.vpc_MerchTxnRef,
		// 		"OrderInfo": params.vpc_OrderInfo,
		// 		"AmountPaid": params.vpc_Amount,
		// 		"TransactionDate": new Date(),
		// 		"TxnResponseCodeDesc": params.vpc_AcqAVSRespCode,
		// 		"TxnResponseCode": params.vpc_TxnResponseCode,
		// 		"PaymentServerMessage": params.vpc_Message,
		// 		"AcqResponseCode": params.vpc_AcqResponseCode,
		// 		"ReceiptNo": params.vpc_ReceiptNo,
		// 		"AuthorizationID": params.vpc_AuthorizeId,
		// 		"OrderID": params.vpc_TransactionNo,
		// 		"CardType": params.vpc_Card
		// 	}

		// 	let body = JSON.stringify(arr);
		// 	let endpoint = environment.primeCareApi.host + environment.primeCareApi.savePaymentEndpoint;

		// 	this.transactionReference = arr["OrderID"];
		// 	this.transactionNo = arr["ReceiptNo"];
		// 	this.amount = arr["AmountPaid"];
		// 	let date = moment();
		// 	let formatted = date.format('MM/DD/YYYY');
		// 	this.transactionDate = formatted;

		// 	console.log(arr);
		// 	this.session.set('SavePayment', arr);

		// 	this.http
		// 		.post(endpoint, body, options)
		// 		.pipe(catchError(this.handleError))
		// 		.subscribe(data => {
		// 			console.log(data);
		// 		});
		// });
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

}
