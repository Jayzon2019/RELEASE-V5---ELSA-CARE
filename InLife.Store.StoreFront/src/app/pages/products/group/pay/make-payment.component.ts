import { environment } from '@environment';
import { Injectable, Injector, OnDestroy } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpResponse, HttpHeaders, HttpParams, HttpErrorResponse } from '@angular/common/http';
import { DomSanitizer } from '@angular/platform-browser';
import { getMatScrollStrategyAlreadyAttachedError } from '@angular/cdk/overlay/scroll/scroll-strategy';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { Observable, Subject, throwError } from 'rxjs';
import { retry, catchError, takeUntil } from 'rxjs/operators';

import * as moment from 'moment';
import { jsPDF } from 'jspdf';

import { ApiService, SessionStorageService } from '@app/services';
import { CONSTANTS } from '@app/services/constants';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { PayService } from '../services/pay.service';
import { StorageType } from '@app/services/storage-types.enum';
import { UtilitiesService } from '@app/shared/services/utilities.service';

@Injectable({ providedIn: 'root' })

@Component
({
	selector: 'app-make-payment',
	templateUrl: './make-payment.component.html',
	styleUrls: ['./make-payment.component.css']
})

export class MakePaymentComponent implements OnInit, OnDestroy
{
	destroy$ = new Subject();
	firstpage:any=true;
	secondpage:any=false;
	thirdpage:any=false;
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
	errorMsg: string;
	privacyFile: any;
	policy: any;
	getApplyForm: FormGroup;
	hasImage: boolean = false;
	showMsg: boolean = false;
	hasError: boolean = false;
	paymentMode:number;
	totalPremium: number = 0;
	annualPremium: number = 0;
	insuranceCoverage: number = 0;
	constructor
	(
		private injector: Injector,
		public session: SessionStorageService,
		private http: HttpClient,
		private apiService: ApiService,
		private ngxService: NgxUiLoaderService,
		private sanitizer: DomSanitizer,
		private formBuilder: FormBuilder,
		private router: Router,
		private payService_API: PayService,
		private util: UtilitiesService
	)
	{
		this.getApplyForm = this.formBuilder.group({
			TotalNumberOfMembers: new FormControl(["", Validators.required]),
			PlanType: new FormControl(["", Validators.required]),
			CompanyName: new FormControl(["", Validators.required]),
			CompanyLandLineNo: new FormControl(["", Validators.required]),
			CompanyMobileNo: new FormControl(["", Validators.required]),
			StreetNumer: new FormControl(["", Validators.required]),
			VillageName: new FormControl(["", Validators.required]),
			Barangaya: new FormControl(["", Validators.required]),
			Region: new FormControl(["", Validators.required]),
			City: new FormControl(["", Validators.required]),
			ZipCode: new FormControl(["", Validators.required]),
			AuthPrefixName: new FormControl(["", Validators.required]),
			AuthFristName: new FormControl(["", Validators.required]),
			AuthMiddleName: new FormControl(["", Validators.required]),
			AuthLastName: new FormControl(["", Validators.required]),
			AuthSuffixName: new FormControl(["", Validators.required]),
			AuthEamilId: new FormControl(["", Validators.required]),
			AuthMobileNumber: new FormControl(["", Validators.required]),
			AuthLandlineNo: new FormControl(["", Validators.required]),
			Status: new FormControl(["", Validators.required]),
		});
	}

	ngOnInit(): void
	{
		this.ngxService.stopAll();
		const getQuoteFormData = this.session.get("selectedGroupPlanData") || "[]";
		this.totalPremium = getQuoteFormData.totalPremium | 0;
		this.annualPremium = getQuoteFormData.annualPremium | 0;
		this.insuranceCoverage = getQuoteFormData.insuranceCoverage | 0;
	}

	ngOnDestroy() {
		this.destroy$.next(true);
		this.destroy$.unsubscribe();
	}

	onSubmit(){
		this.ngxService.start();
		this.hasError = false;
		let refCode = this.session.get(StorageType.ACCESS_DATA).referenceCode;
		let data = { paymentMethod: this.paymentMode == 1 ? 'BankTransfer' : 'OTC' };
		this.payService_API.payment(refCode, data)
			.pipe(
				takeUntil(this.destroy$)
			)
			.subscribe(data => {
				this.ngxService.stopAll();
				this.session.set("PaymentResponse", {
					MerchantTxnRef:Math.floor(Math.random() * (1000000000 - 1 + 1)) + 1, 
					TransactionDate: Date.now(),
					RerefenceCode: refCode,
					PaymentType: this.paymentMode == 1 ? 'Bank Transfer':'Over-the-Counter'
				});
				this.router.navigate(['/group/thank-you']);
			}, error => {
				this.ngxService.stopAll();
				this.hasError = true;
				this.errorMsg = error.message;
			});
	}

	backToPlanSummary() {
		this.router.navigate(['/group/plan-summary']);
	}


	formatDate(dateString: any): string
	{
		let date = moment(dateString);
		let formatted = date.format('MM/DD/YYYY');
		return formatted;
	}

	getData(list: any[], item: any)
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
		// let match = this.getReferenceData(list, item);
		let match = {
			name: '',
			id: ''
		}
		if (match === null)
			return null;

		return String(match.name).toUpperCase();
	}

	getReferenceDataId(list: any[], item: any): number | null
	{
		// let match = this.getReferenceData(list, item);
		let match = {
			name: '',
			id: ''
		}
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

	nullIfZero(id: any): number | null
	{
		let value = Number(id);

		return (value === 0)
			? null
			: value;
	}

	isNullOrWhiteSpace(value: string)
	{
		if (typeof value === 'undefined' || value == null)
			return true;

		return value.replace(/\s/g, '').length < 1;
	}

	parseYesNo(value: any): string
	{
		value = String(value).toUpperCase();
		return value == 'YES'
			? '1'
			: '0';
	}

	// Convert base64 image data to base64 pdf data
	convertImageToPdf(w: number, h: number, imgData: string): string
	{
		const doc = new jsPDF
			({
				orientation: (w > h) ? 'l' : 'p',
				unit: 'px',
				format: [w, h]
			});

		// TODO: Fix the image scaling issue when adding the image to the document
		//doc.addImage(base64ImgString, 0, 0, img.width, img.height);
		doc.addImage(imgData, 'JPEG', 0, 0, w, h, undefined, 'FAST');
		//doc.addImage(img, 'JPEG', 0, 0, img.width, img.height, undefined, 'FAST');

		// Output to base64 string and strip to DATA only
		const base64PdfString = (doc.output('datauristring') as string).split(',')[1];

		//console.log(base64PdfString);

		return base64PdfString;
	}

	openNewWindow(url: string) {
		this.util.openNewWindow(url);
	}
	bank(){
		console.log('bankwork');
		this.thirdpage=false;
		this.firstpage=false;
		this.secondpage=true;

	}
	counter(){
		console.log('counterwork');
		
		this.secondpage=false;
		this.thirdpage=true;
	}

}
