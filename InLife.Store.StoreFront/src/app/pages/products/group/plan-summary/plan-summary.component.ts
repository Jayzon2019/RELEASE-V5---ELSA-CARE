import { UtilitiesService } from './../services/utilities.service';
import { environment } from '@environment';
import { AfterViewInit, Injectable, Injector } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpResponse, HttpHeaders, HttpParams, HttpErrorResponse } from '@angular/common/http';
import { DomSanitizer } from '@angular/platform-browser';
import { getMatScrollStrategyAlreadyAttachedError } from '@angular/cdk/overlay/scroll/scroll-strategy';
import { Router } from '@angular/router';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import * as moment from 'moment';
import { jsPDF } from 'jspdf';
import { ApiService, SessionStorageService } from '@app/services';
import { CONSTANTS } from '@app/services/constants';
import { parse } from 'url';
import { StorageType } from '@app/services/storage-types.enum';

declare var require: any
const FileSaver = require('file-saver');

@Injectable({ providedIn: 'root' })

@Component
	({
		templateUrl: './plan-summary.component.html',
		styleUrls: ['./plan-summary.component.css']
	})

export class PlanSummaryComponent implements OnInit {
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
	totalStudents: any;
	totalTeachers: any;
	premiumPerHead: string;
	totalPremium: string;
	LifeCoverageperHead: any;
	ADD_TPDCoverageperHead: string;
	BurialCoverageperHead: string;
	extensionData: string;
	groupPolicyNo: string;
	privacyFile: any;
	policy: any;
	authSuffixTxt: string;
	authPrefixTxt: string;
	selectedGroupPlanData: any;
	IsAlreadyRead: boolean = false;
	StudentsTeachersOtherBenefits: any = {};
	constructor
		(
			private router: Router,
			private injector: Injector,
			public session: SessionStorageService,
			private http: HttpClient,
			private apiService: ApiService,
			private ngxService: NgxUiLoaderService,
			private sanitizer: DomSanitizer,
			private util: UtilitiesService
		) {
		const getApplyGroupFormData = this.session.get(StorageType.POST_GROUP_QUOTE) || "[]";
		const getGroupQuoteFormData = this.session.get("selectedGroupPlanData") || "[]";
		const groupExtension = this.session.get("groupExtensionData") || "[]";
		this.selectedGroupPlanData = getGroupQuoteFormData;
		this.getFile();
		var suffix = getApplyGroupFormData.AuthSuffixTxt || "";
		if (suffix == "Not Applicable") {
			suffix = "";
		}
		console.log(getApplyGroupFormData);
		this.authSuffixTxt = suffix;
		this.authPrefixTxt = getApplyGroupFormData.AuthPrefixTxt || "";
		this.companyName = getApplyGroupFormData.CompanyName || "";
		this.address = getApplyGroupFormData.StreetNumer + " " + getApplyGroupFormData.VillageName;
		this.city = getApplyGroupFormData.City || "";
		this.cityTxt = getApplyGroupFormData.CityTxt || "";
		this.region = getApplyGroupFormData.Region || "";
		this.regionTxt = getApplyGroupFormData.RegionTxt || "";
		this.country = getApplyGroupFormData.Barangaya || '';
		this.countryTxt = getApplyGroupFormData.Barangaya || '';
		this.cityZip = getApplyGroupFormData.RegionTxt + " " + (getApplyGroupFormData.CityTxt || '') + " " + getApplyGroupFormData.ZipCode;
		this.landline = getApplyGroupFormData.CompanyLandLineNo ? "+63 " + getApplyGroupFormData.CompanyLandLineNo : "";
		this.mobileNumber = getApplyGroupFormData.CompanyMobileNo ? "+63 " + getApplyGroupFormData.CompanyMobileNo : "";
		this.companyEmail = getApplyGroupFormData.CompanyEmail || '';
		this.representativeName = getApplyGroupFormData.AuthPrefixTxt + " " + getApplyGroupFormData.AuthFristName + " " + getApplyGroupFormData.AuthMiddleName + " " + getApplyGroupFormData.AuthLastName + " " + suffix;
		this.representativeLandline = getApplyGroupFormData.AuthLandlineNo ? "+63 " + getApplyGroupFormData.AuthLandlineNo : "";
		this.representativeMobileNumber = getApplyGroupFormData.AuthMobileNumber ? "+63 " + getApplyGroupFormData.AuthMobileNumber : "";
		this.representativeEmail = getApplyGroupFormData.AuthEamilId;
		this.productName = getApplyGroupFormData.ProductName;
		this.planNo = getApplyGroupFormData.PlanType || '';
		this.totalMembers = getApplyGroupFormData.TotalNumberOfMembers;
		this.totalStudents = getApplyGroupFormData.TotalNumberOfStudents;
		this.totalTeachers = getApplyGroupFormData.TotalNumberOfTeachers;
		this.premiumPerHead = Number(getGroupQuoteFormData.annualPremium).toLocaleString();
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
		this.BurialCoverageperHead = getGroupQuoteFormData.insuranceCoverage || '';
		this.groupPolicyNo = this.session.get("groupPolicyNo");

		if(this.selectedGroupPlanData.plan == '3') {
			this.studentsTeachersOtherBenefits(this.selectedGroupPlanData.productType);
			console.log(this.StudentsTeachersOtherBenefits);
			this.ADD_TPDCoverageperHead = this.StudentsTeachersOtherBenefits.Life_Insurance_Accident;
			this.BurialCoverageperHead = this.ADD_TPDCoverageperHead;
		}
	}

	ngOnInit(): void {
		this.ngxService.stopAll();
	}

	studentsTeachersOtherBenefits(productType: any) {
		switch(productType){
			case 1:
				this.StudentsTeachersOtherBenefits = CONSTANTS.STUDENTS_TEACHERS_BENEFITS.map(i => i.PLAN_1)[0];
				break;
			case 2:
				this.StudentsTeachersOtherBenefits = CONSTANTS.STUDENTS_TEACHERS_BENEFITS.map(i => i.PLAN_2)[0];
				break;
			case 3:
				this.StudentsTeachersOtherBenefits = CONSTANTS.STUDENTS_TEACHERS_BENEFITS.map(i => i.PLAN_3)[0];
				break;
			case 4:
				this.StudentsTeachersOtherBenefits = CONSTANTS.STUDENTS_TEACHERS_BENEFITS.map(i => i.PLAN_4)[0];
				break;
		}
	}

	cancel() {
		document.getElementById("closeModal").click();
		const refCode = this.session.get(StorageType.ACCESS_DATA);
		this.router.navigate([`/group/application-status/${refCode.referenceCode}/cancel-application`]);
	}
	pay() {
		if(this.StudentsTeachersOtherBenefits) {
			this.session.set(StorageType.STUDENTS_TEACHERS_BENEFITS, this.StudentsTeachersOtherBenefits);
		}
		this.ngxService.start();
		this.router.navigate(['/group/pay']);
	}
	quote() {
		this.router.navigate(['/group/quote'], {
			queryParams: {
				plan: this.selectedGroupPlanData.plan, 
				planCode: this.selectedGroupPlanData.planCode, 
				productName:this.selectedGroupPlanData.productName, 
				productType: this.selectedGroupPlanData.productType
			},
		});
	}

	downloadPrivacyFile() {
		var filePath = '../../../../../assets/documents/Group Cancellation and Refund.pdf';
		var pdfName = 'Group Cancellation and Refund';
		// FileSaver.saveAs(filePath, pdfName);
		this.openNewWindow(filePath + '#page=' + 1);
	}

	downloadPlanDes() {
		var filePath = "";
		var pdfName = "";
		if (this.selectedGroupPlanData.plan == "1") {
			filePath = '../../../../../assets/documents/Employee Secure_Plan Description.pdf';
			pdfName = 'Employee Secure_Plan Description';
		}
		else if (this.selectedGroupPlanData.plan == "2") {
			filePath = '../../../../../assets/documents/Security Guard_Plan Description.pdf';
			pdfName = 'Security Guard_Plan Description';
		}
		else if (this.selectedGroupPlanData.plan == "3") {
			filePath = '../../../../../assets/documents/Students And Teachers_Plan Description.pdf';
			pdfName = 'Students And Teachers_Plan Description';
		}
		// FileSaver.saveAs(filePath, pdfName);
		this.openNewWindow(filePath + '#page=' + 1);
	}
	getFile() {
		var url = "/Home/GetFiles";
		this.apiService.sendGetRequest(url).subscribe((responseBody) => {
			this.privacyFile = "data:application/pdf;base64," + responseBody[0].primeCareFile;
			this.policy = "data:application/pdf;base64," + responseBody[1].primeCareFile;
		});

	}

	sanitize(url: string) {
		return this.sanitizer.bypassSecurityTrustUrl(url);
	}


	sendData() {
		if (this.isNullOrWhiteSpace(this.groupPolicyNo))
			this.createApplication();
		else
			this.callPaymentUrl();
	}

	openNewWindow(url: string) {
		this.util.openNewWindow(url);
	}

	createApplication() {
		var arrData =
		{
			"PlanCode": "AH0017",
			"PlanName": this.productName,
			"TotalMembers": this.totalMembers,
			"PremiumPerHead": this.premiumPerHead,
			"TotalPremium": this.totalPremium,
			"CompanyName": this.companyName,
			"Address": this.address,
			"Country": this.country,
			"City": this.cityZip,
			"LandLineNumber": this.landline,
			"MobileNumber": this.mobileNumber,
			"CompanyEmail": this.companyEmail,

			"RepresentativeName": this.representativeName,
			"RepresentativeLandline": this.representativeLandline,
			"RepresentativeMobileNumber": this.representativeMobileNumber,
			"RepresentativeEmail": this.representativeEmail,


			//this.LifeCoverageperHead = getApplyGroupFormData.LifeCoverageperHead;
			//this.ADD_TPDCoverageperHead = getApplyGroupFormData.LifeCoverageperHead;
			//this.groupPolicyNo = this.session.get("groupPolicyNo");

			"ExistingOtherInsurance": this.extensionData,
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
				catchError((error: HttpErrorResponse) => {
					this.ngxService.stop();
					let errorMessage = '';
					if (error.error instanceof ErrorEvent) {
						// client-side error
						errorMessage = `Error: ${error.error.message}`;
					}
					else {
						// server-side error
						errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
					}

					window.alert(errorMessage);
					return throwError(errorMessage);
				})
			)
			.subscribe(data => {
				// TODO: Start a new ngxService with a different message
				this.ngxService.stop();

				this.groupPolicyNo = <string>data;

				// LOG FOR DEBUGGING
				this.session.set('groupPolicyNo', this.groupPolicyNo);
				//this.session.set('amount', this.paymentAmount);
				//console.log(`Policy Number: ${this.policyNo}`);
				//console.log(`Amount: ${this.paymentAmount}`);

				// Special case for PrimeCare API
				// It returns STATUS OK 200 and an empty string for the policy number when there's an internal error in the API
				// Show an error message if policy number is empty
				if (this.isNullOrWhiteSpace(this.groupPolicyNo)) {
					window.alert(`We apologize things don't appear to be working at the moment. Please try again later.`);
					this.ngxService.stop();
				}
				else {
					this.callPaymentUrl();
				}
			});
	}

	callPaymentUrl() {
		this.ngxService.start();

		// TODO: Change to OrderId from API
		// Numbers only
		//let refNo = moment().format('YYYYMMDDHHmmssSSS');
		let refNo = this.session.get('refNo');
		let groupPolicyNo = this.groupPolicyNo;
		let amount = "";//String(this.paymentAmount).replace(/\D/g, '');

		let endpoint = environment.paymentGatewayEndpoint;
		let returnUrl = `${window.location.protocol}//${window.location.hostname}/redirect.html?target=payment-callback%26policy=${groupPolicyNo}`;
		let targetUrl = `${endpoint}?RefNo=${refNo}&Amount=${amount}&RetURL=${returnUrl}`;

		// LOG FOR DEBUGGING
		//console.log(`Redirecting to the payment gateway:`);
		//console.log(targetUrl);
		this.session.set('PaymentGatewayURL', targetUrl);
		//window.alert(`This is for debugging purpose only. You can now check the console logs before we redirect you to the payment gateway.`);

		window.location.href = targetUrl;
	}

	handleError(error: any) {
		let errorMessage = '';
		if (error.error instanceof ErrorEvent) {
			// client-side error
			errorMessage = `Error: ${error.error.message}`;
		}
		else {
			// server-side error
			errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
		}
		window.alert(errorMessage);
		return throwError(errorMessage);
	}

	formatDate(dateString: any): string {
		let date = moment(dateString);
		let formatted = date.format('MM/DD/YYYY');
		return formatted;
	}

	getReferenceData(list: any[], item: any) {
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

	getReferenceDataName(list: any[], item: any): string {
		let match = this.getReferenceData(list, item);

		if (match === null)
			return null;

		return String(match.name).toUpperCase();
	}

	getReferenceDataId(list: any[], item: any): number | null {
		let match = this.getReferenceData(list, item);

		if (match === null)
			return null;

		return Number(match.id);
	}

	getReferenceDataById(list: any[], id: any): any {
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

	nullIfZero(id: any): number | null {
		let value = Number(id);

		return (value === 0)
			? null
			: value;
	}

	isNullOrWhiteSpace(value: string) {
		if (typeof value === 'undefined' || value == null)
			return true;

		return value.replace(/\s/g, '').length < 1;
	}

	parseYesNo(value: any): string {
		value = String(value).toUpperCase();
		return value == 'YES'
			? '1'
			: '0';
	}

	// Convert base64 image data to base64 pdf data
	convertImageToPdf(w: number, h: number, imgData: string): string {
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

	conformSave() {
		document.getElementById("conformBtn").click();
	}

}
