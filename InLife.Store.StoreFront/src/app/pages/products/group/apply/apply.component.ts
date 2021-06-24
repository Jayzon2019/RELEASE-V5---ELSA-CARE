import { StorageType } from '@app/services/storage-types.enum';
import { ApplyService } from './../services/apply.service';
import { environment } from './../../../../../environments/environment';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ViewportScroller } from '@angular/common';
import { Location } from '@angular/common';
import { FormBuilder, FormControl, FormGroup, Validators, FormArray } from '@angular/forms';
import { MomentDateAdapter, MAT_MOMENT_DATE_ADAPTER_OPTIONS } from '@angular/material-moment-adapter';
import { MatDatepicker } from '@angular/material/datepicker';
import { NgxUiLoaderService } from 'ngx-ui-loader';
const FileSaver = require('file-saver');
import { jsPDF } from 'jspdf';

import { CONSTANTS } from '@app/services/constants';
import { Enumerations } from '@app/common/enumerations';
import { ApiService, SessionStorageService } from '@app/services';
import { DynamicGrid } from './extension.model';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { catchError, finalize, retry, takeUntil, switchMap } from 'rxjs/operators';
import { throwError, Subject, Observable } from 'rxjs';
import { finished } from 'stream';
import { UtilitiesService } from '@app/shared/services/utilities.service';

@Component
	({
		selector: 'app-apply',
		templateUrl: './apply.component.html',
		styleUrls: ['./apply.component.css', './apply.component.scss'],
		providers: []
	})
export class ApplyComponent implements OnInit, OnDestroy {
	
	destroy$ = new Subject();
	hasError: boolean = false;
	errorMsg: string;
	numberofUploaded$ = new Subject();
	dynamicid: any;
	getApplyForm: FormGroup;
	getQuoteFormData: any;
	accessData: any;
	submitted: boolean = false;
	CONSTANTS = CONSTANTS;
	Enumerations = Enumerations;
	public imagePath;
	imgURL: any;
	selectedPlan: any;
	filePreview: any;
	muncipalitys: any;
	imageSrc: string;
	finishUpload: boolean;
	insuredIdentityDocumentImageData: string;
	insuredIdentityDocumentImagePreview: string;
	hasImage: boolean = false;
	public message: string;
	today = new Date(new Date().setFullYear(new Date().getFullYear() - 18));
	maxYear = new Date(2020, 11, 31);
	muncipality;
	beneficiaryMuncipality: any = [];
	beneficiaryMuncipalities: any = [];
	showCorrespondingAddress: boolean = true;
	files: any = [];
	containers = [];
	minDate = new Date(1990, 0, 1);
	maxDate = new Date(2040, 0, 1);
	showSecondStep: Boolean = false;
	showThirdStep: Boolean = false;
	showFourthStep: Boolean = false;
	showFifthStep: Boolean = false;
	sizeError: boolean = false;
	getQuoteForm: FormGroup;
	totalPremium: any = "";
	insuranceCoverage: any = "";
	annualPremium: any = "";
	isFileSizeError: boolean = false;
	selectedGroupPlanData: any;
	hasRequirementsData: boolean = false;
	showErrorPrompt: boolean = false;
	hasQueue: boolean = false;
	requirementsTypes: any = {
		EmployeeCesusForm: { type: '', title: '', fileInfo: {}, error: { msg: '*Required' }, uploaded: false},
		EntityPlanForm: { type: '', title: '', fileInfo: {}, error: { msg: '*Required' }, uploaded: false},
		AuthRepresentativeId: { type: '', title: '', fileInfo: {}, error: {msg: '*Required' }, uploaded: false},
		BIRNoticeForm: { type: '', title: '', fileInfo: {}, error: {}, uploaded: false},
		SECRegistration: { type: '', title: '', fileInfo: {}, error: {msg: '*Required' }, uploaded: false},
		IncorporationArticles: { type: '', title: '', fileInfo: {}, error: {}, uploaded: false},
		IdentityCertificate: { type: '', title: '', fileInfo: {}, error: {msg: '*Required' }, uploaded: false},
		PostPolicyForm: { type: '', title: '', fileInfo: {}, error: {}, uploaded: false},
		
	}

	requests$ = new Subject();
	queue: PendingRequest[] = [];


	constructor
		(
			private router: Router,
			private location: Location,
			private formBuilder: FormBuilder,
			private apiService: ApiService,
			private apply_API: ApplyService,
			private http: HttpClient,
			private session: SessionStorageService,
			private ngxService: NgxUiLoaderService,
			private vps: ViewportScroller,
			private util: UtilitiesService
		) {
		this.ngxService.start();
		this.requests$
			.pipe(takeUntil(this.destroy$))
			.subscribe((request: PendingRequest) => this.execute(request));
		this.initForm();

	}


	


	dynamicArray: Array<DynamicGrid> = [];
	newDynamic: any = {};

	ngOnInit() {
		this.ngxService.stop();
		this.initForm();

		setTimeout(function () {
			document.querySelector('#apply-page').scrollIntoView({
				behavior: 'smooth'
			});
		}, 1000);
	}

	initForm() {
		this.showThirdStep = false;
		this.accessData = this.session.get(StorageType.ACCESS_DATA);
		const getQuoteFormData = this.session.get(StorageType.POST_GROUP_QUOTE) || "[]";
		const getGroupPlanData = this.session.get(StorageType.GROUP_PLAN_DATA);
		const getRequirementsData = this.session.get(StorageType.REQUIREMENTS_DATA);
		this.selectedPlan = getQuoteFormData.SelectedPlan || '';
		this.onPlan(getQuoteFormData.TotalNumberOfMembers, getQuoteFormData.SelectedPlan);
		this.getApplyForm = this.formBuilder.group({
			basicInformation: this.formBuilder.group({
				CompanyName: new FormControl(getQuoteFormData.CompanyName || '', [Validators.required]),
				BusinessType: new FormControl(getQuoteFormData.BusinessType || '', [Validators.required]),
				CompanyLandLineNo: new FormControl(getQuoteFormData.CompanyLandLineNo || '', [Validators.required, Validators.pattern("^[0-9]{9}$")]),
				CompanyMobileNo: new FormControl(getQuoteFormData.CompanyMobileNo || '', [Validators.required, Validators.pattern("^[1-9]{1}[0-9]{9}$")]),
				StreetNumer: new FormControl(getQuoteFormData.StreetNumer || '', [Validators.required]),
				VillageName: new FormControl(getQuoteFormData.VillageName || '', [Validators.required]),
				Barangaya: new FormControl(getQuoteFormData.Barangaya || '', [Validators.required]),
				Region: new FormControl(getQuoteFormData.Region.id || '', [Validators.required]),
				City: new FormControl(getQuoteFormData.City.id || '', [Validators.required]),
				ZipCode: new FormControl(getQuoteFormData.ZipCode || '', [Validators.required]),
				AuthPrefixName: new FormControl(getQuoteFormData.AuthPrefixName.id || '', [Validators.required]),
				AuthFristName: new FormControl(getQuoteFormData.AuthFristName || '', [Validators.required]),
				AuthMiddleName: new FormControl(getQuoteFormData.AuthMiddleName || ''),
				AuthLastName: new FormControl(getQuoteFormData.AuthLastName || '', [Validators.required]),
				AuthSuffixName: new FormControl(getQuoteFormData.AuthSuffixName.id || '', [Validators.required]),
				AuthEamilId: new FormControl(getQuoteFormData.AuthEamilId || '', [Validators.required, Validators.pattern(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/)]),
				AuthMobileNumber: new FormControl(getQuoteFormData.AuthMobileNumber || '', [Validators.required, Validators.pattern("^[1-9]{1}[0-9]{9}$")]),
				AuthLandlineNo: new FormControl(getQuoteFormData.AuthLandlineNo || '', [Validators.pattern("^[0-9]{9}$")]),
				Status: new FormControl(2, [Validators.required]),
				privacyPolicy: new FormControl(getQuoteFormData.privacyPolicy || '', [Validators.required]),
			}),
			requirementsForm: this.formBuilder.group({
				EmployeeCesusForm: new FormControl("", [Validators.required]),
				EntityPlanForm: new FormControl("", [Validators.required]),
				AuthRepresentativeId: new FormControl("", [Validators.required]),
				BIRNoticeForm: new FormControl(""),
				SECRegistration: new FormControl("", [Validators.required]),
				IncorporationArticles: new FormControl(""),
				IdentityCertificate: new FormControl("", [Validators.required]),
				PostPolicyForm: new FormControl(""),
			}),
			declarationsForm: this.formBuilder.group({
				IsCheckDataPrivacy: new FormControl(getGroupPlanData ? getGroupPlanData.IsCheckDataPrivacy : false, Validators.requiredTrue),
				IsCheckDataUNSCR: new FormControl(getGroupPlanData ? getGroupPlanData.IsCheckDataUNSCR : false, Validators.requiredTrue),
				IsCheckDeclarationStatement: new FormControl(getGroupPlanData ? getGroupPlanData.IsCheckDeclarationStatement : false, Validators.requiredTrue),
				IsCheckSubmittedPhlippinesApp: new FormControl(getGroupPlanData ? getGroupPlanData.IsCheckSubmittedPhlippinesApp : false, Validators.requiredTrue),
				IsCheckLifeProducts: new FormControl(getGroupPlanData ? getGroupPlanData.IsCheckLifeProducts : false, Validators.requiredTrue),

				// IsCheckDataPrivacy: new FormControl("", [Validators.required]),
				// IsCheckDataUNSCR: new FormControl("", [Validators.required]),
				// IsCheckDeclarationStatement: new FormControl("", [Validators.required]),
				// IsCheckSubmittedPhlippinesApp: new FormControl("", [Validators.required]),
				// IsCheckLifeProducts: new FormControl( "", [Validators.required]),
			})
		});

		console.log(this.getApplyForm.get('declarationsForm'));
		if(getRequirementsData)  {
			this.hasRequirementsData = true;
			this.requirementsTypes = getRequirementsData;
			let uploadedFilesArray = Object.entries(getRequirementsData);
			uploadedFilesArray.map(([key, val]) => {
				this.showErrorPrompt = true;
				let value: any= val;
				if(value.type == 'image') {
					this.getApplyForm.get('requirementsForm').get(key).patchValue(value.fileInfo);
				} else {
					this.getApplyForm.get('requirementsForm').get(key).patchValue(value.fileInfo.loc);
				}
			});
			// this.showThirdStep = (this.getApplyForm.get('requirementsForm').valid) ? true : false;
			// if(this.showThirdStep) {
			// 	this.getApplyForm.get('declarationsForm').patchValue({
			// 		IsCheckDataPrivacy: true,
			// 		IsCheckDataUNSCR: true,
			// 		IsCheckDeclarationStatement: true,
			// 		IsCheckSubmittedPhlippinesApp: true,
			// 		IsCheckLifeProducts: true,
			// 	})
			// }
		}

		this.setMunicipalities(getQuoteFormData.Region.id);

	}

	openNewWindow(url: string) {
		this.util.openNewWindow(url);
	}

	setMunicipalities(region: any) {
		for (let i = 0; i < CONSTANTS.PROVIANCE.length; i++)
		{
			if (CONSTANTS.PROVIANCE[i].id == Number(region))
				this.beneficiaryMuncipalities = CONSTANTS.PROVIANCE[i].Municipality;
		}
	}

	changedDeclartions(event, type)
	{
		const isChecked = event.target.checked;
		this.getApplyForm.get('declarationsForm').get(type).patchValue(isChecked);
	}

	onPlan(TotalNumberOfMembers, type) { 
		const getQuoteFormData = this.session.get("selectedGroupPlanData") || "[]";
		this.selectedGroupPlanData= getQuoteFormData;
		this.totalPremium = getQuoteFormData.totalPremium || 0;
		this.annualPremium = getQuoteFormData.annualPremium || 0;
		this.insuranceCoverage = getQuoteFormData.insuranceCoverage || 0;
	}


	setShowThird() {
		this.showThirdStep = true;

		setTimeout(function () {
			document.querySelector('#step3_head').scrollIntoView({
				behavior: 'smooth'
			});
		}, 100);
	}

	backtoUploadFiles() {
		this.showThirdStep = false;
		this.getApplyForm.get('declarationsForm').reset();
		setTimeout(function () {
			document.querySelector('#step3_head').scrollIntoView({
				behavior: 'smooth'
			});
		}, 100);
	}


	addApplyForm(formType: any = 'submit') {
		this.submitted = true;
		this.hasError = false;
		this.finishUpload = false;
		this.ngxService.start();

		this.apply_API.updateDeclaration(this.accessData.referenceCode)
			.pipe(takeUntil(this.destroy$), finalize(() => this.ngxService.stopAll()))
			.subscribe((resp) => {
				const basicInformationFormData = this.getApplyForm.get('basicInformation').value;
				const requirementsFormData = this.getApplyForm.get('requirementsForm').value;
				const declarationsFormData = this.getApplyForm.get('declarationsForm').value;
				let data = { ...basicInformationFormData, ...declarationsFormData };
				this.session.set(StorageType.GROUP_PLAN_DATA, data);
				this.session.set(StorageType.REQUIREMENTS_DATA, this.requirementsTypes);

				if(formType == 'save') {
					document.getElementById("closeModal").click();
					this.router.navigate(['/group/application-reference', this.accessData.referenceCode]);
				} else {
					this.router.navigate(['/group/plan-summary']);
				}
				return;
			}, error => {
				this.errorMsg = error.message;
				this.hasError = true;
			});
	}


	uploadByFile(fileData, type, dbType) {
		this.addRequestToQueue(fileData, type, dbType);
	}

	execute(requestData: PendingRequest) {
		this.hasError = false;
		if(true) {
			let url = environment.appApi.host + `/group/applications/${this.accessData.referenceCode}/files/${requestData.dbType}`;
			let formData = new FormData();
			formData.append("file", requestData.fileData);
			const req = this.apply_API.uploadRequirement(url, formData, requestData.fileData.type, requestData.fileData.name)
				.pipe(
					takeUntil(this.destroy$),
					finalize(() => { 
						//remove duplicate upload
						console.log(requestData, this.queue[0]);
						if(requestData === this.queue[0])
							this.queue.shift();
						else 
							this.queue.shift();
						this.startNextRequest();

						this.getApplyForm.get('requirementsForm').get(requestData.type).enable();
						this.getApplyForm.get('requirementsForm').get(requestData.type).updateValueAndValidity();
					})
				)
				.subscribe(data => {
					this.getApplyForm.get('requirementsForm').get(requestData.type).setValue(requestData.fileData);
					this.requirementsTypes[requestData.type].error = {
						type: 'success',
						msg: '(File successfully uploaded.)'
					}
					this.requirementsTypes[requestData.type].uploaded = true;

					// this.session.set(StorageType.REQUIREMENTS_DATA, this.requirementsTypes);
					this.validateIncompleteRequirement(requestData.type);
				}, error => {
					this.ngxService.stopAll();
					this.hasError = true;
					this.errorMsg = error.message;
					this.requirementsTypes[requestData.type].error = {
						type: 'upload',
						msg: '(Failed to upload this file.)'
					}
					this.requirementsTypes[requestData.type].uploaded = false;
				});
				console.log(req);
		}
	}

	validateIncompleteRequirement(type: string) {
		if(type == 'IdentityCertificate')
			this.showErrorPrompt = true;
	}

	addRequestToQueue(fileData, type, dbType) {
		this.hasQueue = true;
		const sub = new Subject<any>();
		const request = new PendingRequest(fileData, type, dbType, sub)
		console.log(this.queue.length);
		// if there are no pending req's, execute immediately.
		if (this.queue.length === 0) {
			this.queue.push(request);
			this.requests$.next(request);
		} else {
			// otherwise put it to queue.
			this.queue.push(request);
		}
		return sub;
	}

	startNextRequest() {
		// get next request, if any.
		if (this.queue.length) {
			this.hasQueue = true;
			this.execute(this.queue[0]);
		} else {
			this.hasQueue = false;
		}
	}


	backtoQoute() {
		console.log(this.selectedGroupPlanData);
		 this.router.navigate(['/group/quote'], { queryParams: { 
		 	plan: this.selectedGroupPlanData.plan, 
		 	planCode: this.selectedGroupPlanData.planCode, 
		 	productName:this.selectedGroupPlanData.productName, 
		 	productType: this.selectedGroupPlanData.productType
		 }});
	}

	backClicked() {
		this.session.set("getApplyForm", JSON.stringify(this.getApplyForm.value));
		this.location.back();
	}

	_handleReaderLoaded(e) {
		let reader = e.target;
		this.imageSrc = reader.result;
		console.log(this.imageSrc)
	}

	fileInfo(file: any) {
		switch (file.type) {
			case 'application/pdf':
				return { ext: 'pdf', loc: 'assets/images/pdf.png' };
			case 'application/zip' || 'application/x-7z-compressed':
				return { ext: 'zip', loc: 'assets/images/zip.png' };
			case 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet':
				return { ext: 'xlsx', loc: 'assets/images/excel.png' };
			case 'application/vnd.ms-excel':
				return { ext: 'xls', loc: 'assets/images/excel.png' };
			default:
				return { ext: 'doc', loc: 'assets/images/word.png' };
		}
	}

	evaluateFiles() {
		const arrayOfObj: any = Object.entries(this.requirementsTypes).map((e) => (e[1]));
		let hasError = arrayOfObj.find(i => i.error?.type === 'size' || i.error?.type === 'upload');
		this.isFileSizeError =  hasError ? true: false;
	}

	onFileChanged(event, type, dbType: any ='') {
		//const fileSizeLimit = 512000; // 500KB
		//const fileSizeLimit = 5242880; // 5MB
		this.sizeError = false;
		const limitFileSize = 512000; // 500KB
		const limitWidth = 500;
		const limitHeight = 500;
		const imageType = 'image/jpeg';

		const reader: any = new FileReader();
		if (event.target.files.length !== 0) {
			const file = event.target.files[0];
			const generalType = file.type.split('/')[0];

			let fileType = '.'+file.name.split('.').pop();
			if(!CONSTANTS.REQUIREMENTS_ALLOWED_FILE_TYPE.split(',').find(i => i == fileType)) return;

			if(generalType == 'application') {
				reader.readAsDataURL(file);
				reader.onloadend = () => {
					this.requirementsTypes[type] = {
						type: 'document',
						title: file.name,
						fileInfo : this.fileInfo(file),
						error: (file.size > CONSTANTS.MAX_UPLOAD_FILE_SIZE) ? {
							type: 'size',
							msg: '(File size exceeded.)'
						} : {
							type: 'uploading',
							msg: '(Uploading file...)'
						}
					};
					this.evaluateFiles()
					if(!(file.size > CONSTANTS.MAX_UPLOAD_FILE_SIZE)) {
						this.getApplyForm.get('requirementsForm').get(type).disable();
						this.uploadByFile(file, type, dbType);
					}
				};
			} else {
				reader.onloadend = (event) => {
					// Get the event.target.result from the reader (base64 of the image)
					let uploadedImage = event.target.result;

					//this.insuredIdentityDocumentImagePreview = event.target.result;
	
					const image = new Image();
					image.onload = (event) => {
						// Fit image to bounding box
						let scaleFactor = (limitWidth / image.width < limitHeight / image.height)
							? (limitWidth / image.width)
							: (limitHeight / image.height);
	
						let newWidth = image.width * scaleFactor;
						let newHeight = image.height * scaleFactor;
	
						const canvas = document.createElement('canvas');
						canvas.width = newWidth;
						canvas.height = newHeight;
	
						const ctx = canvas.getContext('2d');
						ctx.drawImage(image, 0, 0, newWidth, newHeight);
	
						let newDataUrl = '';
						let newBase64ImageString = '';
						let newFileSize = 0;
						let newImageQuality = 100;
	
						// Degrade image until file size is less than the limit
						do {
							newDataUrl = canvas.toDataURL(imageType, newImageQuality / 100);
							newBase64ImageString = newDataUrl.split(',')[1];
							newFileSize = Math.round(newBase64ImageString.length * 3 / 4);
	
							//console.log(newImageQuality);
							//console.log(newFileSize.toFixed(2));
							//console.log(newBase64ImageString);
	
							newImageQuality -= (newImageQuality > 10)
								? 5
								: 1;
	
						} while (newFileSize > limitFileSize && newImageQuality > 0);
						
						this.requirementsTypes[type] = {
							type: 'image',
							title: '',
							fileInfo : newDataUrl,
							error: {
								type: 'uploading',
								msg: '(Uploading file...)'
							}
						}
						this.evaluateFiles();
						// if(!(file.size > CONSTANTS.MAX_UPLOAD_FILE_SIZE)) {
							this.getApplyForm.get('requirementsForm').get(type).disable();
							this.getApplyForm.get('requirementsForm').get(type).updateValueAndValidity();
							this.uploadByFile(file, type, dbType);
						// }
						//	this.insuredIdentityDocumentImagePreview = newDataUrl;
	
						// Convert to PDF
						const doc = new jsPDF
							({
								orientation: (newWidth > newHeight) ? 'l' : 'p',
								unit: 'px',
								format: [newWidth, newHeight]
							});
	
						doc.addImage(newBase64ImageString, 0, 0, newWidth, newHeight);
	
						// Output PDF to base64 string and strip to DATA only
						const base64PdfString = (doc.output('datauristring') as string).split(',')[1];
						//console.log(base64PdfString);
	
						this.insuredIdentityDocumentImageData = base64PdfString;
						this.hasImage = true;
					};
	
					image.src = uploadedImage;
				};
				reader.readAsDataURL(file);
			}
			

		}
		//	console.log(this.insuredIdentityDocumentImagePreview)
	}


	deleteImage(type) {
		let requirementFormCtrl = this.getApplyForm.get('requirementsForm').get(type);
		requirementFormCtrl.setValue('');
		if(!requirementFormCtrl.valid)
			this.requirementsTypes[type] = {error: { msg: '*Required' }};
		else
			this.requirementsTypes[type] = {};
	}

	deleteAttachment(index) {
		this.files.splice(index, 1);
		this.files = [];
	}


	addRow() {
		this.newDynamic = { CompanyName: "", LifeFaceAmount: "", DreadDiseaseFaceAmount: "", AccidentalFaceAmount: "", IssueYear: "" };
		this.dynamicArray.push(this.newDynamic);
		console.log(this.dynamicArray);
		return true;

	}
	removeRow(index) {
		if (this.dynamicArray.length == 1) {
			return false;
		} else {
			this.dynamicArray.splice(index, 1);
			return true;
		}
	}

	degradeImageToLimit(dataUrl: string, imageType: string, imageQuality: number, limit: number, callback: any) {
		let image = new Image();
		image.onload = (event) => {
			const newCanvas = document.createElement('canvas');
			newCanvas.width = image.width;
			newCanvas.height = image.height;

			const newContext = newCanvas.getContext('2d');
			newContext.drawImage(image, 0, 0, image.width, image.height);

			const newDataUrl = newCanvas.toDataURL(imageType, imageQuality);
			const newBase64ImageString = dataUrl.split(',')[1];
			const newFileSize = Math.round(newBase64ImageString.length * 3 / 4);

			console.log('DEGRADE /////////////////////////////////////////////');
			console.log(newBase64ImageString);
			console.log(newFileSize);

			if (newFileSize > limit)
				this.degradeImageToLimit(newDataUrl, imageType, imageQuality, limit, callback);
			else
				callback(newDataUrl);
		};
		image.src = dataUrl;
	}

	logRemainingSessionStorage() {
		let limit = 1024 * 1024 * 5; // 5 MB
		let remSpace = limit - unescape(encodeURIComponent(JSON.stringify(sessionStorage))).length;
		console.log(remSpace);
	}

	downloadPlanDes() {
		var filePath = "";
		var pdfName = "";
		if (this.selectedPlan == '1') {
			filePath = '../../../../../assets/documents/Employee Secure_Plan Description.pdf';
			pdfName = 'Employee Secure_Plan Description';
		}
		else if (this.selectedPlan == '2') {
			filePath = '../../../../../assets/documents/Security Guard_Plan Description.pdf';
			pdfName = 'Security Guard_Plan Description';
		}
		else if (this.selectedPlan == '3') {
			filePath = '../../../../../assets/documents/Students And Teachers_Plan Description.pdf';
			pdfName = 'Students And Teachers_Plan Description';
		}
		this.openNewWindow(filePath + '#page=' + 1);
	}
	conformSave() {
		document.getElementById("conformBtnApply").click();
	}
	downloadFile(fileName: string) {
		var filePath = "";
		var pdfName = "";
		if (fileName == "EmployeeCensus") {
			filePath = '../../../../../assets/documents/Census Template_Prototype Plan-final.xlsx';
			pdfName = 'Employee Census';
		}
		else if (fileName == "EntityPlan") {
			filePath = '../../../../../assets/documents/Entity Plan Admin Form.pdf';
			pdfName = 'Entity Plan Admin Form';
		} 
		FileSaver.saveAs(filePath, pdfName);
	}

	get getRequirementsCtrl() {
		return this.getApplyForm.controls;
	}

	ngOnDestroy() {
		this.destroy$.next(true);
		this.destroy$.unsubscribe();
	}
}
export class PendingRequest {
	url: string;
	fileData: any; 
	type: string; 
	dbType: string
	subscription: Subject<any>;
  
	constructor(fileData: any, type: string, dbType, subscription: Subject<any>) {
	  this.fileData = fileData;
	  this.type = type;
	  this.dbType = dbType;
	  this.subscription = subscription;
	}
  }
