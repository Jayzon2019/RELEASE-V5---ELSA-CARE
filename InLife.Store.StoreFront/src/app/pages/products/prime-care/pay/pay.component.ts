import { UtilitiesService } from './../../../../shared/services/utilities.service';
import { Router } from '@angular/router';
import { environment } from '@environment';

import { Injectable, Injector } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpResponse, HttpHeaders, HttpParams, HttpErrorResponse } from '@angular/common/http';
import { DomSanitizer } from '@angular/platform-browser';
import { getMatScrollStrategyAlreadyAttachedError } from '@angular/cdk/overlay/scroll/scroll-strategy';

import { NgxUiLoaderService } from 'ngx-ui-loader';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';

import * as moment from 'moment';
import { jsPDF } from 'jspdf';

import { ApiService, FacebookPixelService, SessionStorageService } from '@app/services';
import { CONSTANTS } from '@app/services/constants';
import { StorageType } from '@app/services/storage-types.enum';

@Injectable({ providedIn: 'root' })

@Component
({
	templateUrl: './pay.component.html',
	styleUrls: ['./pay.component.scss']
})

export class PayComponent implements OnInit
{
	CONSTANTS = CONSTANTS;
	quoteInformation: any;
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
	affiliate:any;

	constructor
	(
		private injector: Injector,
		public session: SessionStorageService,
		private http: HttpClient,
		private router: Router,
		private apiService: ApiService,
		private ngxService: NgxUiLoaderService,
		private sanitizer: DomSanitizer,
		private util: UtilitiesService,
		private facebookPixelService: FacebookPixelService,
	)
	{
		this.affiliate = this.session.get('affiliate');
		const getQuoteFormData = this.session.get('getQuoteForm') || "[]";
		this.quoteInformation = this.session.get(StorageType.QUOTE_PC_DATA) || "[]";
		const getApplyFormData = this.session.get("getApplyForm") || "[]";
		const extension = this.session.get("extensionData_PC") || "[]";
		this.getFile();

		this.basicInformation = getQuoteFormData.basicInformation;
		this.calculationInformation = getQuoteFormData.calculatePremium;
		this.totalCashBenefit = this.calculationInformation.totalCashBenefit.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
		this.healthCondition = getQuoteFormData.healthCondition;
		this.beneficialInformation = getApplyFormData.beneficiaryDetails;
		this.declarationFormInformation = getApplyFormData.declarationForm;
		this.employmentFormInformation = getApplyFormData.employment;
		this.identificationFormInformation = getApplyFormData.identification;
		this.personalInformation = getApplyFormData.personalInformation;
		this.extensionData = extension;
		this.insuredIdentityDocumentImageData = this.session.get("insuredIdentityDocumentImageData_PC");

		this.employment = getQuoteFormData.employment;
		this.identication = getQuoteFormData.identification;
		this.getinnerForm = this.session.get("getinnerForm");
		this.age = this.session.get("age");
		this.paymentAmount = this.getinnerForm.amount;
		//image convert to pdf
		this.pdfblogImage = this.getinnerForm.pdfbasestring;
		this.landline = this.basicInformation.landline ? "+63" + this.basicInformation.landline : "";
		this.policyNo = this.session.get(StorageType.POLICYNO);
		this.health1 = this.healthCondition.healthCondition1;
		this.health2 = this.healthCondition.healthCondition2;
		this.health3 = this.healthCondition.healthCondition3;
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
		//console.log(arr);
		//alert(JSON.stringify(this.age));
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


	sendData()
	{
		if (this.isNullOrWhiteSpace(this.policyNo))
			this.createApplication();
		else
			this.callPaymentUrl();
	}

	editDetails() {
		debugger
		this.router.navigate(['/prime-care/quote'], {
			queryParams: {
				plan: this.quoteInformation.Plan
			}
		});
	}
	createApplication()
	{

		let ownerSuffixID = this.nullIfZero(this.basicInformation.suffix);
		let insuredSuffixId = this.nullIfZero(this.basicInformation.suffix);
		let benefSuffixId = this.nullIfZero(this.beneficialInformation.suffix);

		let arrData: any =
		{
			"PlanCode": "AH0017",
			"PlanName": "Prime Care",

			"PaymentMode": this.calculationInformation.paymentMode == "Monthly" ? 1 : 12,
			"FaceAmount": parseFloat(this.calculationInformation.totalCashBenefit),

			"OwnerIsInsured": 1,
			"OwnerRelationToInsuredId": 24, // Same Person

			"OwnerPrefixId": this.nullIfZero(this.basicInformation.prefix),
			// "OwnerSuffixId": this.nullIfZero(this.basicInformation.suffix),
			"OwnerFirstName": this.basicInformation.fname,
			"OwnerLastName": this.basicInformation.lname,
			"OwnerMiddleName": this.basicInformation.mname,

			"OwnerBirthday": this.formatDate(this.calculationInformation.dateofbirth),
			"OwnerGenderId": this.nullIfZero(this.calculationInformation.gender),

			"OwnerEmailAddress": this.basicInformation.email,
			"OwnerResidencePhoneNumber": this.basicInformation.landline,
			"OwnerMobileNo": this.basicInformation.mobile,

			"InsuredPrefixId": this.nullIfZero(this.basicInformation.prefix),
			// "InsuredSuffixId": this.nullIfZero(this.basicInformation.suffix),
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
					// "SuffixId": this.nullIfZero(this.beneficialInformation.suffix),
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
			"Premium": this.quoteInformation.PlanPremium,

			"AgentCode": this.quoteInformation.AgentCode,
			"RefFirstName": this.quoteInformation?.AffiliateType == 'INLIFE AFFILIATE' ? null : this.quoteInformation?.AgentFirstName,
			"RefLastName": this.quoteInformation?.AffiliateType == 'INLIFE AFFILIATE' ? null : this.quoteInformation?.AgentLastName,
			"AffiliateCode": this.quoteInformation?.AffiliateStatus == 'ACTIVE' || this.quoteInformation?.AffiliateType == 'UNIONBANK BRANCH' || this.quoteInformation?.AffiliateType == 'INSULAR LIFE AGENT' ? this.quoteInformation?.AffiliateCode : null,
			"AffiliateName": this.quoteInformation?.AffiliateName,
			"AffiliateStatus": !this.quoteInformation?.AffiliateStatus ? null : this.quoteInformation?.AffiliateStatus == 'ACTIVE' ? this.quoteInformation?.AffiliateStatus : null
		}

		// Do not include in request body if suffix is not applicable
		if(ownerSuffixID !== 1)
			arrData.OwnerSuffixId = ownerSuffixID
		if(insuredSuffixId !== 1)
			arrData.InsuredSuffixId = insuredSuffixId;

		if(benefSuffixId !== 1)
			arrData.Beneficiary[0].SuffixId = benefSuffixId;

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
		let errorMsg = `We apologize things don't appear to be working at the moment. Please try again.`;

		if(!this.policyNo) { // If policy no if null means new application
			this.http
			.post(endpoint, body, options)
			.pipe(
				retry(1),
			)
			.subscribe(data =>
			{
				// TODO: Start a new ngxService with a different message
				this.policyNo = <string>data;

				// LOG FOR DEBUGGING


				// Special case for PrimeCare API
				// It returns STATUS OK 200 and an empty string for the policy number when there's an internal error in the API
				// Show an error message if policy number is empty
				if (this.isNullOrWhiteSpace(this.policyNo))
				{
					this.util.ShowGeneralMessagePrompt({message: errorMsg});
					this.ngxService.stopAll();
				}
				else
				{
					this.facebookPixelService.track('CompleteRegistration');
					this.facebookPixelService.track('SubmitApplication');
					this.session.set(StorageType.POLICYNO, this.policyNo);
					this.session.set('amount', this.paymentAmount);
					this.session.set('refNo', '1357246812'.concat(Math.floor(Math.random() * 100001).toString()));
					this.session.set(StorageType.ACQUIRED_PLAN, {plan: 'PrimeCare', variant: ''});
					this.callPaymentUrl();
				}
			}, error => {
				this.ngxService.stopAll();
				this.util.ShowGeneralMessagePrompt({message: errorMsg});
			});
		} else { // Call payment request after payment transaction fails
			this.callPaymentUrl();
		}

	}

	callPaymentUrl()
	{
		this.ngxService.start();
		this.facebookPixelService.track('InitiateCheckout');
		// TODO: Change to OrderId from API
		// Numbers only
		//let refNo = moment().format('YYYYMMDDHHmmssSSS');
		let refNo = this.session.get('refNo');
		let policyNo = this.policyNo;
		let amount = String(this.paymentAmount).replace(/\D/g, '');

		let endpoint = environment.paymentGatewayEndpoint;
		let returnUrl = `${window.location.protocol}//${window.location.host}/payment-callback?policy=${policyNo}`;
		let targetUrl = `${endpoint}?RefNo=${refNo}&Amount=${amount}&RetURL=${returnUrl}`;

		// LOG FOR DEBUGGING
		//console.log(`Redirecting to the payment gateway:`);
		//console.log(targetUrl);
		this.session.set('PaymentGatewayURL_PC', targetUrl);
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
}
