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
		private payService_API: PayService
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

	getFile()
	{
		var url = "/Home/GetFiles";
		this.apiService.sendGetRequest(url).subscribe((responseBody) =>
		{
			this.privacyFile = "data:application/pdf;base64," + responseBody[0].primeCareFile;
			this.policy = "data:application/pdf;base64," + responseBody[1].primeCareFile;
		});

	}

	backToPlanSummary() {
		this.router.navigate(['/group/plan-summary']);
	}

	sanitize(url: string)
	{
		return this.sanitizer.bypassSecurityTrustUrl(url);
	}


	sendData()
	{
		if (this.isNullOrWhiteSpace(this.policyNo))
			this.createApplication();
		else
			this.callPaymentUrl();
	}

	createApplication()
	{
		var arrData =
		{
			"PlanCode": "AH0017",
			"PlanName": "Prime Care",

			"PaymentMode": this.calculationInformation.paymentMode == "Monthly" ? 1 : 12,
			"FaceAmount": parseFloat(this.calculationInformation.totalCashBenefit),

			"OwnerIsInsured": 1,
			"OwnerRelationToInsuredId": 24, // Same Person

			"OwnerPrefixId": this.nullIfZero(this.basicInformation.prefix),
			"OwnerSuffixId": this.nullIfZero(this.basicInformation.suffix),
			"OwnerFirstName": this.basicInformation.fname,
			"OwnerLastName": this.basicInformation.lname,
			"OwnerMiddleName": this.basicInformation.mname,

			"OwnerBirthday": this.formatDate(this.calculationInformation.dateofbirth),
			"OwnerGenderId": this.nullIfZero(this.calculationInformation.gender),

			"OwnerEmailAddress": this.basicInformation.email,
			"OwnerResidencePhoneNumber": this.basicInformation.landline,
			"OwnerMobileNo": this.basicInformation.mobile,

			"InsuredPrefixId": this.nullIfZero(this.basicInformation.prefix),
			"InsuredSuffixId": this.nullIfZero(this.basicInformation.suffix),
			"InsuredFirstName": this.basicInformation.fname,
			"InsuredMiddleName": this.basicInformation.mname,
			"InsuredLastName": this.basicInformation.lname,

			"InsuredBirthday": this.formatDate(this.calculationInformation.dateofbirth),
			"InsuredGenderId": this.nullIfZero(this.calculationInformation.gender),

			"InsuredEmailAddress": this.basicInformation.email,
			"InsuredResidencePhoneNumber": this.basicInformation.landline,
			"InsuredMobileNo": this.basicInformation.mobile,

			"InsuredResidenceAddress1": this.personalInformation.street,
			"InsuredResidenceAddress2": this.personalInformation.village,
			//"InsuredResidenceAddress3": "", // No form equivalent
			"InsuredResidenceMunicipalityId": this.nullIfZero(this.personalInformation.municipality),
			"InsuredResidenceProvinceId": this.nullIfZero(this.personalInformation.province),
			"InsuredResidenceZipCode": this.personalInformation.zipCode,

			"InsuredCitizenshipId": this.nullIfZero(this.personalInformation.nationality),
			"InsuredCivilStatusId": this.nullIfZero(this.personalInformation.civilStatus),

			"InsuredPrimaryOccupationCompanyName": this.employmentFormInformation.company,
			"InsuredPrimaryOccupationId": this.nullIfZero(this.employmentFormInformation.occupation),

			"InsuredPrimaryOccupationMonthlyIncome": parseFloat(this.employmentFormInformation.monthlyIncome),
			"InsuredFundSourceId": this.nullIfZero(this.employmentFormInformation.fundSource),

			"InsuredPreferredMailingAddress": "Home",
			"InsuredPrimaryOccupationAddress1": this.personalInformation.street,
			"InsuredPrimaryOccupationAddress2": this.personalInformation.village,
			//"InsuredPrimaryOccupationAddress3": "",
			"InsuredPrimaryOccupationZipCode": this.personalInformation.zipCode,
			"InsuredPrimaryOccupationProvinceId": this.nullIfZero(this.personalInformation.province),

			"InsuredPrimaryOccupationMunicipalityId": this.nullIfZero(this.personalInformation.municipality),
			//"InsuredOfficePhoneNumber": 1234567,

			"Health1": this.parseYesNo(this.healthCondition.healthCondition1),
			"Health2": this.parseYesNo(this.healthCondition.healthCondition2),
			"Health3": this.parseYesNo(this.healthCondition.healthCondition3),

			"Fatca1": this.declarationFormInformation.uslawpersion,
			"Fatca2": this.declarationFormInformation.usnotlaw,

			"Question1": this.declarationFormInformation.changeexstinginsurance,
			"Question2": this.declarationFormInformation.premiumpaid,

			// "PolicyDeliveryOption": this.basicInformation.PolicyDeliveryOption, // No form equivalent

			"Beneficiary":
				[{
					"SuffixId": this.nullIfZero(this.beneficialInformation.suffix),
					"FirstName": this.beneficialInformation.fname,
					"MiddleName": this.beneficialInformation.mname,
					"LastName": this.beneficialInformation.lname,

					"AddressType": 0,
					"Address1": this.beneficialInformation.insuredStreet,
					"Address2": this.beneficialInformation.insuredVillage,
					//"Address3": "", // No form equivalent
					"ProvinceId": this.nullIfZero(this.beneficialInformation.insuredProvince),
					"MunicipalityId": this.nullIfZero(this.beneficialInformation.insuredMunicipality),
					"ZipCode": this.beneficialInformation.insuredZipCode,
					"CountryId": this.beneficialInformation.insuredNationality,

					"LandLineNumber": this.beneficialInformation.insuredLandline,
					"MobileNumber": this.beneficialInformation.insuredMobile,
					"CivilStatusId": this.nullIfZero(this.beneficialInformation.insuredCivilStatus),
					"GenderId": this.nullIfZero(this.beneficialInformation.insuredGender),
					"Birthday": this.formatDate(this.beneficialInformation.insuredDateofbirth),
					"RelationToInsuredId": this.nullIfZero(this.beneficialInformation.relation),
					"Priority": this.beneficialInformation.designation,
					"Right": this.beneficialInformation.type
				}],

			"ExistingOtherInsurance": this.extensionData,

			// Info by Harold Poblete - 2020.05.09 1:18PM - MS Teams
			// InsuredIdTypeId =  (SSS. TIN. GSIS) ID Type -- see the mapping in the updated document
			// InsuredTinNo = ID number from selected id in InsuredIdTypeId
			// InsuredOtherIdNoType = (Free Text / not ID)
			// InsuredOtherIdNo =  ID number from selected id in InsuredOtherIdNoType

			// "InsuredOtherIdNoType": this.identificationFormInformation.legalIdType,
			// "InsuredSpouseOtherIdNo": this.identificationFormInformation.LegalIdNumber,
			// "InsuredTinNo": this.identificationFormInformation.secondaryLegalIdNumber,

			"InsuredOtherIdNoType": this.identificationFormInformation.legalIdType,
			"InsuredOtherIdNo": this.identificationFormInformation.LegalIdNumber,

			"InsuredIdTypeId": this.nullIfZero(this.identificationFormInformation.secondaryLegalIdType),
			"InsuredTinNo": this.identificationFormInformation.secondaryLegalIdNumber,

			"InsuredValidIdImage": this.insuredIdentityDocumentImageData,
			"OwnerValidIdImage": this.insuredIdentityDocumentImageData,

			"AgentCode": this.basicInformation.acode,
			"RefFirstName": this.basicInformation.afname,
			"RefLastName": this.basicInformation.alname
		}

		let headers: HttpHeaders = new HttpHeaders();
		headers = headers.append('Content-Type', 'application/json');
		headers = headers.append('Ocp-Apim-Subscription-Key', environment.primeCareApi.subscriptionKey);

		let options =
		{
			headers: headers,
			params: new HttpParams()
		};

		let body = JSON.stringify(arrData);
		let endpoint = environment.primeCareApi.host + environment.primeCareApi.createApplicationEndpoint;

		this.ngxService.start();

		// LOG FOR DEBUGGING
		//console.log(`Posting to ${endpoint}`);
		this.session.set('CreateApplication', arrData);
		//return;

		this.http
			.post(endpoint, body, options)
			.pipe(
				retry(1),
				catchError((error: HttpErrorResponse) =>
				{
					this.ngxService.stop();
					let errorMessage = '';
					if (error.error instanceof ErrorEvent)
					{
						// client-side error
						errorMessage = `Error: ${error.error.message}`;
					}
					else
					{
						// server-side error
						errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
					}

					window.alert(errorMessage);
					return throwError(errorMessage);
				})
			)
			.subscribe(data =>
			{
				// TODO: Start a new ngxService with a different message
				this.ngxService.stop();

				this.policyNo = <string>data;

				// LOG FOR DEBUGGING
				this.session.set('policyNo', this.policyNo);
				this.session.set('amount', this.paymentAmount);
				//console.log(`Policy Number: ${this.policyNo}`);
				//console.log(`Amount: ${this.paymentAmount}`);

				// Special case for PrimeCare API
				// It returns STATUS OK 200 and an empty string for the policy number when there's an internal error in the API
				// Show an error message if policy number is empty
				if (this.isNullOrWhiteSpace(this.policyNo))
				{
					window.alert(`We apologize things don't appear to be working at the moment. Please try again later.`);
					this.ngxService.stop();
				}
				else
				{
					this.callPaymentUrl();
				}
			});
	}

	callPaymentUrl()
	{
		this.ngxService.start();

		// TODO: Change to OrderId from API
		// Numbers only
		//let refNo = moment().format('YYYYMMDDHHmmssSSS');
		let refNo = this.session.get('refNo');
		let policyNo = this.policyNo;
		let amount = String(this.paymentAmount).replace(/\D/g, '');

		let endpoint = environment.paymentGatewayEndpoint;
		let returnUrl = `${window.location.protocol}//${window.location.hostname}/redirect.html?target=payment-callback%26policy=${policyNo}`;
		let targetUrl = `${endpoint}?RefNo=${refNo}&Amount=${amount}&RetURL=${returnUrl}`;

		// LOG FOR DEBUGGING
		//console.log(`Redirecting to the payment gateway:`);
		//console.log(targetUrl);
		this.session.set('PaymentGatewayURL', targetUrl);
		//window.alert(`This is for debugging purpose only. You can now check the console logs before we redirect you to the payment gateway.`);

		window.location.href = targetUrl;
	}

	handleError(error: any)
	{
		let errorMessage = '';
		if (error.error instanceof ErrorEvent)
		{
			// client-side error
			errorMessage = `Error: ${error.error.message}`;
		}
		else
		{
			// server-side error
			errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
		}
		window.alert(errorMessage);
		return throwError(errorMessage);
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
