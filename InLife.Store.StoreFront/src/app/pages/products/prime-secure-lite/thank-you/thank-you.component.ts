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
import { GeneralMessagePromptComponent } from '@app/shared/component/general-message-prompt/general-message-prompt.component';

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
		const paymentResponse = this.session.get("PaymentResponse") || "[]";
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

		this.mapResponse(paymentResponse);
	}
	

	ngOnInit(): void
	{
		this.dateNow = new Date().toLocaleString();
	}

	ngOnDestroy() {
		if (this.activatedRouteSubscription$) this.activatedRouteSubscription$.unsubscribe();
	}

	mapResponse(params) {
		this.txn.MerchTxnRef = params['vpc_MerchTxnRef'];
		this.txn.OrderInfo = params['vpc_OrderInfo'];
		this.txn.Amount = params['vpc_Amount'];
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
