import { PSLiteService } from './../../../../shared/services/pslite.servce';
import { environment } from '@environment';

import { Injectable, Injector } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpResponse, HttpHeaders, HttpParams, HttpErrorResponse } from '@angular/common/http';
import { DomSanitizer } from '@angular/platform-browser';
import { getMatScrollStrategyAlreadyAttachedError } from '@angular/cdk/overlay/scroll/scroll-strategy';

import { NgxUiLoaderService } from 'ngx-ui-loader';
import { Observable, Subject, throwError } from 'rxjs';
import { retry, catchError, map, switchMap, takeUntil, finalize, filter } from 'rxjs/operators';

import * as moment from 'moment';
import { jsPDF } from 'jspdf';

import { ApiService, FacebookPixelService, SessionStorageService } from '@app/services';
import { CONSTANTS } from '@app/services/constants';
import { Router } from '@angular/router';
import { StorageType } from '@app/services/storage-types.enum';
import { UtilitiesService } from '@app/shared/services/utilities.service';

@Injectable({ providedIn: 'root' })

@Component
({
	templateUrl: './pay.component.html',
	styleUrls: ['./pay.component.css', './pay.component.scss']
})

export class PayComponent implements OnInit
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
	destroy$ = new Subject();

	constructor
	(
		private router: Router,
		private injector: Injector,
		public session: SessionStorageService,
		private http: HttpClient,
		private apiService: ApiService,
		private ngxService: NgxUiLoaderService,
		private sanitizer: DomSanitizer,
		private psLiteService_API: PSLiteService,
		private util: UtilitiesService,
		private facebookPixelService: FacebookPixelService,
	)
	{
		// const getQuoteFormData = JSON.parse(this.session.get("getQuoteForm") || "[]");
		// const getApplyFormData = JSON.parse(this.session.get("getApplyForm") || "[]");
		// const extension = JSON.parse(this.session.get("extensionData") || "[]");
		const getQuoteFormData = this.session.get("getQuoteForm") || "[]";
		const getApplyFormData = this.session.get("getApplyForm") || "[]";
		const extension = this.session.get("extensionData") || "[]";
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
		this.paymentAmount = this.getinnerForm.annual;
		//image convert to pdf
		// this.pdfblogImage = this.getinnerForm.pdfbasestring;
		// this.landline = this.basicInformation.landline ? "+63" + this.basicInformation.landline : "";
		this.policyNo = this.session.get(StorageType.POLICYNO);

	}

	ngOnInit(): void
	{
		this.facebookPixelService.track('ViewContent');
		this.facebookPixelService.track('PlanSummary');
		var arr =
		{
			"totalCashBenefit": this.calculationInformation.totalCashBenefit,
			"gender": this.calculationInformation.gender,
			"dateofbirth": this.calculationInformation.dateofbirth,
			"paymentMode": this.calculationInformation.paymentMode,
			"prefix": this.basicInformation.prefix,
			"fname": this.basicInformation.fname,
			"mname": this.basicInformation.mname,
			"lname": this.basicInformation.lname,
			"suffix": this.basicInformation.suffix,
			"email": this.basicInformation.email,
			"landline": this.basicInformation.landline,
			"mobile": this.basicInformation.mobile,
			"country": this.basicInformation.country,
			"province": this.basicInformation.province,
			"municipality": this.basicInformation.municipality,
			"primeCare": this.basicInformation.primeCare
		}
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


	createApplication()
	{
		this.ngxService.start();
		if(!this.policyNo) {
			const quoteExternalData = JSON.stringify(this.session.get(StorageType.QUOTE_EXTERNAL_DATA));
			const quoteInternalData = JSON.stringify(this.session.get(StorageType.QUOTE_INTERNAL_DATA));
			const applyData = this.session.get(StorageType.APPLY_DATA);
			const underws = this.session.get('UnderWritingStatus');

			

			let errorMsg =  `We apologize things don't appear to be working at the moment. Please try again.`;

			if(underws) {
				this.psLiteService_API.requestPolicyNo(JSON.stringify(applyData))
					.pipe(takeUntil(this.destroy$))
					.subscribe(policyNo => {
						if (this.isNullOrWhiteSpace(policyNo)) {
							//prompt user to try again
							this.ngxService.stopAll();
							this.util.ShowGeneralMessagePrompt({message: errorMsg});
						} else {
							this.facebookPixelService.track('CompleteRegistration');
							this.facebookPixelService.track('SubmitApplication');
							this.session.set(StorageType.POLICYNO, policyNo);
							this.session.set(StorageType.ACQUIRED_PLAN, {plan: 'PrimeSecureLite', variant: ''});
							this.policyNo = policyNo;
							this.callPaymentUrl();
						}
					}, (error) => {
						this.ngxService.stopAll();
						this.util.ShowGeneralMessagePrompt({message: errorMsg});
					})
			}

			// COMMENTED LAST APRIL 8, 2021
			// if(underws) { // If in the first request of requestPolicyNo returns nothing then rerequest it again
			// 	this.psLiteService_API.requestPolicyNo(JSON.stringify(applyData))
			// 		.pipe(takeUntil(this.destroy$))
			// 		.subscribe(policyNo => {
			// 			if (this.isNullOrWhiteSpace(policyNo)) {
			// 				//prompt user to try again
			// 				this.ngxService.stopAll();
			// 				this.util.ShowGeneralMessagePrompt({message: errorMsg});
			// 			} else {
			// 				this.session.set(StorageType.POLICYNO, policyNo);
			// 				this.policyNo = policyNo;
			// 				this.callPaymentUrl();
			// 			}
			// 		}, (error) => {
			// 			this.ngxService.stopAll();
			// 			this.util.ShowGeneralMessagePrompt({message: errorMsg});
			// 		})
			// } else { // Create underwritingstatus and request Policy No
			// 	this.psLiteService_API.createUnderWritingStatus(quoteExternalData)
			// 	.pipe(
			// 		map((data: any) => {
			// 			if(data.underwritingStatus === 'CLEAN_CASE') {
			// 				this.session.set('refNo', '1357246812'.concat(Math.floor(Math.random() * 100001).toString()));
			// 				this.session.set('UnderWritingStatus', data)
			// 				applyData.ProposalId = data.proposalId;
			// 				this.session.set(StorageType.APPLY_DATA, applyData);
			// 			} else {
			// 				this.destroy$.next(true);
			// 				this.router.navigate(['prime-secure-lite/ineligible']);
			// 			}
			// 			return data;
			// 		}),
			// 		filter((data: any) => data.underwritingStatus === 'CLEAN_CASE'),
			// 		switchMap((data) => this.psLiteService_API.requestPolicyNo(JSON.stringify(applyData))),
			// 		takeUntil(this.destroy$)
			// 	)
			// 	.subscribe(policyNo => {
			// 		if (this.isNullOrWhiteSpace(policyNo)) {
			// 			//prompt user to try again
			// 			this.ngxService.stopAll();
			// 			this.util.ShowGeneralMessagePrompt({message: errorMsg});
			// 		} else {
			// 			this.session.set(StorageType.POLICYNO, policyNo);
			// 			this.session.set(StorageType.ACQUIRED_PLAN, {plan: 'PrimeSecureLite', variant: ''});
			// 			this.policyNo = policyNo;
			// 			this.callPaymentUrl();
			// 		}
			// 	}, (error) => {
			// 		this.ngxService.stopAll();
			// 		this.util.ShowGeneralMessagePrompt({message: errorMsg});

			// 	})
			// }

			
		} else {
			this.callPaymentUrl(); // Recall payment after unsuccesful transaction
		}
	}


	callPaymentUrl()
	{
		// TODO: Change to OrderId from API
		// Numbers only
		//let refNo = moment().format('YYYYMMDDHHmmssSSS');
		this.facebookPixelService.track('InitiateCheckout');
		let refNo = this.session.get('refNo');
		let policyNo = this.policyNo;
		let amount = this.paymentAmount.replace(/,/g, '') + '00';
		let endpoint = environment.paymentGatewayEndpoint;
		let returnUrl = `${window.location.protocol}//${window.location.host}/payment-callback?policy=${policyNo}`;
		let targetUrl = `${endpoint}?RefNo=${refNo}&Amount=${amount}&RetURL=${returnUrl}`;

		// LOG FOR DEBUGGING
		//console.log(`Redirecting to the payment gateway:`);
		//console.log(targetUrl);
		this.session.set('PaymentGatewayURL', targetUrl);
		//window.alert(`This is for debugging purpose only. You can now check the console logs before we redirect you to the payment gateway.`);
		window.location.href = targetUrl;
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
