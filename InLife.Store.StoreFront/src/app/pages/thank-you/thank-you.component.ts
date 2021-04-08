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
	styleUrls: ['./thank-you.component.scss']
})
export class ThankYouComponent implements OnInit
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
	gender: any;
	activatedRouteSubscription$: Subscription;
	txn = {} as any;

	constructor
	(
		private session: SessionStorageService,
		private route: ActivatedRoute,
		private http: HttpClient,
		private facebookPixelService: FacebookPixelService
	)
	{
		const getQuoteFormData = this.session.get("getQuoteForm") || "[]";
		const getApplyFormData = this.session.get("getApplyForm") || "[]";
		const extension = this.session.get("extensionData") || "[]";
		const paymentResponse = this.session.get("PaymentResponse") || "[]";
		this.basicInformation = getQuoteFormData.basicInformation;
		this.calculationInformation = getQuoteFormData.calculatePremium;
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
		this.gender = this.calculationInformation.gender == 7 ? "Female" : "Male";
		this.mapResponse(paymentResponse);
		
		this.resetForm();
	}

	resetForm()
	{
		this.session.clear();
	}

	mapResponse(params) {
		this.txn.MerchTxnRef = params.MerchantTxnRef;
		this.txn.OrderInfo = params.OrderInfo;
		this.txn.Amount = params.AmountPaid;
		this.txn.TransactionDate = params.TransactionDate;
		this.txn.txnResponseCode = params.TxnResponseCode;
		this.txn.TxnResponseCodeDesc = params.TxnResponseCodeDesc;
		this.txn.Message = params.PaymentServerMessage;
		this.txn.AcqResponseCode = params.AcqResponseCode;
		this.txn.ReceiptNo = params.ReceiptNo;
		this.txn.AuthorizeId = params.AuthorizationID;
		this.txn.TransactionNo = params.OrderID;
		this.txn.Card = params.CardType;
		this.txn.policy = params.PolicyNo;
	}

	ngOnInit(): void
	{
		this.facebookPixelService.track('Purchase');

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

	formatDate(dateString: any): string
	{
		let date = moment(dateString);
		let formatted = date.format('MM/DD/YYYY');
		return formatted;
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
