import { environment } from '@environment';
import { Injectable, Injector, ViewEncapsulation } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpResponse, HttpHeaders, HttpParams, HttpErrorResponse } from '@angular/common/http';
import { DomSanitizer } from '@angular/platform-browser';
import { getMatScrollStrategyAlreadyAttachedError } from '@angular/cdk/overlay/scroll/scroll-strategy';

import { NgxUiLoaderService } from 'ngx-ui-loader';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import * as moment from 'moment';
import { jsPDF } from 'jspdf';

import { ApiService, SessionStorageService, FacebookPixelService } from '@app/services';
import { CONSTANTS } from '@app/services/constants';
import { parse } from 'url';
import { StorageType } from '@app/services/storage-types.enum';

@Component
	({
		selector: 'app-thank-you',
		templateUrl: './thank-you.component.html',
		styleUrls: ['./thank-you.component.css'],
		encapsulation: ViewEncapsulation.None
	})
export class ThankYouComponent implements OnInit {
	CONSTANTS = CONSTANTS;
	companyName: string;
	region: string;
	regionTxt: string;
	city: string;
	cityTxt: string;
	address: string;
	country: string;
	countryTxt: string;
	cityZip: string;
	landline: string;
	mobileNumber: string;
	companyEmail: string;
	representativeName: string;
	representativeLandline: string;
	representativeMobileNumber: string;
	representativeEmail: string;
	productName: string;
	planNo: string;
	totalMembers: any;
	premiumPerHead: string;
	totalPremium: string;
	LifeCoverageperHead: string;
	ADD_TPDCoverageperHead: string;
	StudentsTeachersOtherBenefits: any;
	extensionData: string;
	groupPolicyNo: string;
	transactionDate: any;
	transactionReference: any;
	paymentResponse: any;
	paymentAmount: string;
	privacyFile: any;
	policy: any;
	authSuffixTxt: string;
	authPrefixTxt: string;
	selectedGroupPlanData: any;
	paymentData: any;
	totalStudents: any;
	totalTeachers: any;
	constructor
		(
			private router: Router,
			private injector: Injector,
			public session: SessionStorageService,
			private http: HttpClient,
			private apiService: ApiService,
			private ngxService: NgxUiLoaderService,
			private sanitizer: DomSanitizer,
			private facebookPixelService: FacebookPixelService
	) { 
		const getApplyGroupFormData = this.session.get(StorageType.POST_GROUP_QUOTE) || "[]";
		const getGroupQuoteFormData = this.session.get("selectedGroupPlanData") || "[]";
		const groupExtension = this.session.get("groupExtensionData") || "[]";
		const GroupPaymentResponse = this.session.get("PaymentResponse" || "[]");
		this.selectedGroupPlanData = getGroupQuoteFormData;
		if (GroupPaymentResponse) {
			this.transactionReference = GroupPaymentResponse.MerchantTxnRef || '';
			this.transactionDate = GroupPaymentResponse.TransactionDate || '';
			this.paymentAmount = GroupPaymentResponse.AmountPaid || '';
			this.paymentData = GroupPaymentResponse;
		}
		this.companyName = getApplyGroupFormData.CompanyName || '';

		this.address = getApplyGroupFormData.StreetNumer + " " + getApplyGroupFormData.VillageName;
		this.city = getApplyGroupFormData.City || "";
		var suffix = getApplyGroupFormData.AuthSuffixTxt || "";
		if (suffix == "Not Applicable") {
			suffix = "";
		}
		this.authSuffixTxt = suffix;
		this.authPrefixTxt = getApplyGroupFormData.AuthPrefixTxt || "";
		this.cityTxt = getApplyGroupFormData.CityTxt || "";
		this.region = getApplyGroupFormData.Region || "";
		this.regionTxt = getApplyGroupFormData.RegionTxt || "";
		this.country = getApplyGroupFormData.Barangaya || '';
		this.countryTxt = getApplyGroupFormData.Barangaya || '';
		this.cityZip = getApplyGroupFormData.RegionTxt + " " + (getApplyGroupFormData.CityTxt || '') + " " + getApplyGroupFormData.ZipCode;

		this.landline = getApplyGroupFormData.CompanyLandLineNo ? "+63 " + getApplyGroupFormData.CompanyLandLineNo : "";
		this.mobileNumber = getApplyGroupFormData.CompanyMobileNo ? "+63 " + getApplyGroupFormData.CompanyMobileNo : "";
		this.companyEmail = getApplyGroupFormData.AuthEamilId;
		this.representativeName = getApplyGroupFormData.AuthPrefixTxt + " " + getApplyGroupFormData.AuthFristName + " " + getApplyGroupFormData.AuthMiddleName + " " + getApplyGroupFormData.AuthLastName + " " + suffix;
		this.representativeLandline = getApplyGroupFormData.AuthLandlineNo ? "+63 " + getApplyGroupFormData.AuthLandlineNo : "";
		this.representativeMobileNumber = getApplyGroupFormData.AuthMobileNumber ? "+63 " + getApplyGroupFormData.AuthMobileNumber : "";
		this.representativeEmail = getApplyGroupFormData.AuthEamilId;
		this.productName = getApplyGroupFormData.ProductName;
		this.planNo = getApplyGroupFormData.SelectedPlan || '';
		this.totalMembers = getApplyGroupFormData.TotalNumberOfMembers;
		this.totalStudents = getApplyGroupFormData.TotalNumberOfStudents;
		this.totalTeachers = getApplyGroupFormData.TotalNumberOfTeachers;
		this.premiumPerHead = getGroupQuoteFormData.annualPremium;
		if (!this.totalMembers) {
			this.totalMembers = "0";
		}
		if (!this.premiumPerHead) {
			this.premiumPerHead = "0";
		}
		this.extensionData = groupExtension;
		this.totalPremium = getGroupQuoteFormData.totalPremium;
		this.LifeCoverageperHead = getGroupQuoteFormData.insuranceCoverage || '';
		this.ADD_TPDCoverageperHead = getGroupQuoteFormData.insuranceCoverage || '';
		this.groupPolicyNo = this.session.get("groupPolicyNo");

		if(this.selectedGroupPlanData.plan == '3') {
			this.StudentsTeachersOtherBenefits = this.session.get(StorageType.STUDENTS_TEACHERS_BENEFITS);
			console.log(this.StudentsTeachersOtherBenefits);
			this.ADD_TPDCoverageperHead = this.StudentsTeachersOtherBenefits.Life_Insurance_Accident;
		}
		
	}
	back() { 
		this.ngxService.stopAll();
		this.resetForm();
		this.router.navigate(['/']);
	}
	feedback() {
		this.ngxService.start();
		let refCode = this.session.get(StorageType.ACCESS_DATA).referenceCode;
		this.router.navigate(['/feedback', refCode]);
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

	ngOnInit(): void {
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

	handleError(error: any) {
		let errMsg =
			(error.message) ? error.messagess
				: error.status ? `${error.status} - ${error.statusText}`
					: 'API error';

		console.error(errMsg);
		return throwError(error);
	}

}
