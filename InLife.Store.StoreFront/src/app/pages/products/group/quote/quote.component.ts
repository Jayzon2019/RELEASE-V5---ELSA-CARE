import { StorageType } from '@app/services/storage-types.enum';
import { environment } from '@environment';
import { Injectable, Injector, OnDestroy } from '@angular/core';
import { Component, OnInit, ViewChild } from '@angular/core';
import { DecimalPipe, ViewportScroller } from '@angular/common';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators, FormArray } from '@angular/forms';
import { HttpClient, HttpResponse, HttpHeaders, HttpParams, HttpErrorResponse } from '@angular/common/http';
import { DomSanitizer } from '@angular/platform-browser';
import { NgxUiLoaderService } from 'ngx-ui-loader';
const FileSaver = require('file-saver');
import { Observable, Subject, Subscription, throwError } from 'rxjs';
import { retry, catchError, takeUntil, finalize, map } from 'rxjs/operators';
import $ from "jquery";
import { CONSTANTS } from '@app/services/constants';
import { ApiService, SessionStorageService } from '@app/services';
import { QuoteService } from '../services/quote.service';
import { UtilitiesService } from '@app/shared/services/utilities.service';

@Component
({
	selector: 'app-quote',
	templateUrl: './quote.component.html',
	styleUrls: ['./quote.component.scss']
})
export class QuoteComponent implements OnInit, OnDestroy
{
	destroy$ = new Subject();
	getQuoteForm: FormGroup;
	submitted = false;
	today = new Date(new Date().setFullYear(new Date().getFullYear() - 18));
	maxYear = new Date(2020, 11, 31);
	muncipality;
	CONSTANTS = CONSTANTS;
	plan = '';
	productName :string;
	productType :any;
	planCode :string;
	amount = 0;
	showSecondStep:Boolean=false;
	showThirdStep:Boolean=false;
	hasData: boolean = false;
	isValidTotalPremium: boolean = false;
	calulatedAge:number=0;
	privacyFile:any;
	affiliate:any;
	totalPremium: any = '00000';
	insuranceCoverage: any = '00000';
	annualPremium: any = '00000';
	annualPremiumStr: any = '00000';
	lc1:any = 0;
	lc2: any = 0;
	lc3: any = 0;
	lc4: any = 0;
	Pr1: any = 0;
	Pr2: any = 0;
	Pr3: any = 0;
	Pr4: any = 0;
	
	groupQuoteData: any;
	selectedPlanType: any;
	authPrefixTxt: string = '';
	authSuffixTxt: string = '';
	barangayaText: string = '';
	BusinessTxt: string = '';
	cityText: string = '';
	regionText: string = '';
	selectedPlan: number = 1
	errorInfo: any;
	//Total Premiums = Total No. of Members x Selected Premium per Member

	valueChanged: Subscription;

	constructor
	( 
		private router: Router,
		private formBuilder: FormBuilder,
		private route: ActivatedRoute,
		private apiService: ApiService,
		private session: SessionStorageService,
		private ngxService: NgxUiLoaderService,
		private quoteService_API: QuoteService,
		private vps: ViewportScroller,
		private http: HttpClient,
		private sanitizer: DomSanitizer,
		private util: UtilitiesService,
		private decimalPipe: DecimalPipe
	)
	{ 
		this.ngxService.start();
		this.initForm();
		
		
		this.route.queryParams.pipe(takeUntil(this.destroy$)).subscribe(params =>
		{
			if (params && (params.plan == '1' || params.plan == '2' || params.plan == '3'))
			{
				
				this.productName = params.productName;
				this.productType = Number(params.productType) == 0 ? null : Number(params.productType);
				this.planCode = params.planCode;
				this.selectedPlan = params.plan;
				
				switch(params.plan){
					case '1':
						this.lc1 = "100,000";
						this.Pr1 = 400;
						this.lc2 = "150,000";
						this.Pr2 = 600;
						this.lc3 = "200,000";
						this.Pr3 = 800;
						this.lc4 = "250,000";
						this.Pr4 = 1000;
						// this.totalPremium = 400;
						// this.insuranceCoverage = 100000;
						// this.annualPremium = 400;
						this.getQuoteForm.get('groupPackage').get('TotalNumberOfMembers').setValidators([Validators.required, Validators.min(3), Validators.max(160), Validators.pattern(/^-?(0|[1-9]\d*)?$/)]);
						break;
					case '2':
						this.lc1 = "25,000";
						this.Pr1 = 194.75;
						this.lc2 = "35,000";
						this.Pr2 = 261.25;
						this.lc3 = "55,000";
						this.Pr3 = 394.25;
						this.lc4 = "65,000";
						this.Pr4 = 460.75;
						// this.totalPremium = 194.75;
						// this.insuranceCoverage = 25000;
						// this.annualPremium = 194.75;
						this.getQuoteForm.get('groupPackage').get('TotalNumberOfMembers').setValidators([Validators.required, Validators.min(25),Validators.pattern(/^-?(0|[1-9]\d*)?$/)]);
						break;
					case '3':
						this.lc1 = "10,000";
						this.Pr1 = 25.50;
						this.lc2 = "20,000";
						this.Pr2 = 55.50;
						this.lc3 = "25,000";
						this.Pr3 = 70.50;
						this.lc4 = "35,000";
						this.Pr4 = 100.50;
						// this.totalPremium = 25.5;
						// this.insuranceCoverage = 10000;
						// this.annualPremium = 25.5;
						this.groupPackageCtrl.get('TotalNumberOfStudents').setValidators([Validators.required, Validators.min(50), Validators.pattern(/^-?(0|[1-9]\d*)?$/)]);	
						this.groupPackageCtrl.get('TotalNumberOfTeachers').setValidators([Validators.required, Validators.min(15), Validators.pattern(/^-?(0|[1-9]\d*)?$/)]);	

						this.groupPackageCtrl.get('TotalNumberOfStudents').valueChanges
							.pipe(takeUntil(this.destroy$))
							.subscribe(val => {
								if(this.groupPackageCtrl.get('TotalNumberOfStudents').valid) {
									this.groupPackageCtrl.get('TotalNumberOfTeachers').setValidators([Validators.max(val),
														Validators.required, Validators.min(15), Validators.pattern(/^-?(0|[1-9]\d*)?$/)]);
														this.groupPackageCtrl.get('TotalNumberOfTeachers').updateValueAndValidity();
									this.onPlan(this.productType);
								}
							});
						break;
					default:
				}




				this.plan = params.plan;
				console.log(params.plan);
			}
			else
			{
				this.router.navigate(['/']);
			}
		});


	}

	ngOnInit(): void
	{
		this.ngxService.stop();
		let group_plan = this.session.get('selectedGroupPlanData');
		if(group_plan) {
			this.productType = group_plan.productType;
			this.onPlan(this.productType);
		}
	}

	ngOnDestroy() {
		this.destroy$.next(true);
    	this.destroy$.unsubscribe();
		this.ngxService.stop();
	}

	getcall()
	{
		//alert("ss");
	}

	calPay()
	{
		const plans = this.plan;
			return plans;
	}


	initForm()
	{
		
		let quote_form = this.session.get(StorageType.POST_GROUP_QUOTE);
		let group_plan = this.session.get('selectedGroupPlanData');
		console.log(group_plan);
		if(quote_form && group_plan) {
			this.groupQuoteData = quote_form;
			this.showSecondStep = true;
			this.hasData = true;
			this.authPrefixTxt = quote_form.AuthPrefixName.name;
			this.authSuffixTxt = quote_form.AuthSuffixName.name;
			this.regionText = quote_form.Region.name;
			this.cityText = quote_form.City.name;
			this.barangayaText = quote_form.Barangaya;
			this.onChangeProviance(quote_form.Region.id);
		}

		this.getQuoteForm = this.formBuilder.group({
			groupPackage : this.formBuilder.group({ 
				TotalNumberOfMembers: new FormControl(this.hasData ? quote_form.TotalNumberOfMembers : "", [Validators.pattern("^[0-9]{1,15}$")]),
				TotalNumberOfStudents: new FormControl(this.hasData ? quote_form.TotalNumberOfStudents : "", [Validators.pattern("^[0-9]{1,15}$")]),
				TotalNumberOfTeachers: new FormControl(this.hasData ? quote_form.TotalNumberOfTeachers : "", [Validators.pattern("^[0-9]{1,15}$")]),
				PlanType: new FormControl(this.hasData ? Number(group_plan.productType) : "", [Validators.required]),
			}),
			basicInformation :this.formBuilder.group({ 
				CompanyName: new FormControl(this.hasData ? quote_form.CompanyName: "", [Validators.required, Validators.pattern("[A-Za-zÑñ@',.+&0-9 ]+")]),
				CompanyLandLineNo: new FormControl(this.hasData ? quote_form.CompanyLandLineNo: "", [Validators.required, Validators.pattern("^[0-9]{9}$")]),
				CompanyMobileNo: new FormControl(this.hasData ? quote_form.CompanyMobileNo: "", [Validators.required,  Validators.pattern("^[1-9]{1}[0-9]{9}$")]),
				StreetNumer: new FormControl(this.hasData ? quote_form.StreetNumer: "", [Validators.required,  Validators.maxLength(50)]),
				VillageName: new FormControl(this.hasData ? quote_form.VillageName: "", [Validators.required, Validators.maxLength(50)]),
				Barangaya: new FormControl(this.hasData ? quote_form.Barangaya: "", [Validators.required, Validators.maxLength(50)]),
				BusinessType: new FormControl(this.hasData ? quote_form.BusinessType:"", [Validators.required]),
				Region: new FormControl(this.hasData ? quote_form.Region.id:"", [Validators.required]),
				City: new FormControl(this.hasData ? quote_form.City.id:"", [Validators.required]),
				ZipCode: new FormControl(this.hasData ? quote_form.ZipCode:"", [Validators.required, Validators.pattern("^[0-9]{1,5}$")]),
				AuthPrefixName: new FormControl(this.hasData ? quote_form.AuthPrefixName.id:"", [Validators.required, Validators.maxLength(50)]),
				AuthFristName: new FormControl(this.hasData ? quote_form.AuthFristName:"", [Validators.required, Validators.maxLength(50), Validators.pattern("[A-Za-zÑñ@',.+& ]+")]),
				AuthMiddleName:new FormControl(this.hasData ? quote_form.AuthMiddleName:"", [Validators.maxLength(50), Validators.pattern("[A-Za-zÑñ@',.+& ]+")]),
				AuthLastName: new FormControl(this.hasData ? quote_form.AuthLastName:"", [Validators.required, Validators.maxLength(50), Validators.pattern("[A-Za-zÑñ@',.+& ]+")]),
				AuthSuffixName: new FormControl(this.hasData ? quote_form.AuthSuffixName.id:"", [Validators.required, Validators.maxLength(50)]),
				AuthEamilId: new FormControl(this.hasData ? quote_form.AuthEamilId:"", [Validators.required, Validators.pattern(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/)]),
				AuthMobileNumber: new FormControl(this.hasData ? quote_form.AuthMobileNumber:"", [Validators.required, Validators.pattern("^[1-9]{1}[0-9]{9}$")]),
				AuthLandlineNo: new FormControl(this.hasData ? quote_form.AuthLandlineNo:"", [Validators.pattern("^[0-9]{9}$")]),
				Status: new FormControl(1, [Validators.required]),
				privacyPolicy: new FormControl(this.hasData ? quote_form.privacyPolicy:false, [Validators.required]),
			}),
			
		});
		console.log(this.getQuoteForm);

		
	}
	onPlan(type){
		debugger
		this.productType = type;
		this.isValidTotalPremium = false;
		this.groupPackageCtrl.patchValue({ PlanType: type});
		let total_premium:any = 0;
		switch(type){
			case 1:
				console.log(this.groupPackageCtrl);
				if(this.plan == '1') {
					total_premium = 400 * (this.getQuoteForm.get('groupPackage').get('TotalNumberOfMembers').value || 1);
					this.totalPremium = this.decimalPipe.transform(total_premium, '1.0-0');
					this.insuranceCoverage = "100,000";//* (this.getQuoteForm.get('groupPackage').get('TotalNumberOfMembers').value | 1);
					this.annualPremium = 400;
					this.annualPremiumStr = '400';
				} else if(this.plan == '2') {
					total_premium = 184.75 * (this.getQuoteForm.get('groupPackage').get('TotalNumberOfMembers').value || 1);
					this.totalPremium = this.decimalPipe.transform(total_premium, '1.2-2'); 
					this.insuranceCoverage = "25,000";//* (this.getQuoteForm.get('groupPackage').get('TotalNumberOfMembers').value | 1);
					this.annualPremium = 184.75;
					this.annualPremiumStr = '184.75';
				} else if(this.plan == '3') {
					
					total_premium = 25.5 * (this.getQuoteForm.get('groupPackage').value.TotalNumberOfStudents || 1);
					this.totalPremium = this.decimalPipe.transform(total_premium, '1.2-2');
					this.insuranceCoverage = "10,000";//* (this.getQuoteForm.get('groupPackage').get('TotalNumberOfMembers').value | 1);
					this.annualPremium = '25.50';
					this.annualPremiumStr = '25.50';
				}
				
				this.isValidTotalPremium = (total_premium >= CONSTANTS.MINIMUM_TOTAL_ANUAL_PREMIUM) ? true : false;
				break;
			case 2:
				if(this.plan == '1') {
					total_premium = 600 * (this.getQuoteForm.get('groupPackage').get('TotalNumberOfMembers').value || 1);
					this.totalPremium = this.decimalPipe.transform(total_premium, '1.0-0');
					this.insuranceCoverage = "150,000";//* (this.getQuoteForm.get('groupPackage').get('TotalNumberOfMembers').value | 1);
				    this.annualPremium = 600;
					this.annualPremiumStr = '600';
				} else if( this.plan == '2') {
					total_premium = 261.25 * (this.getQuoteForm.get('groupPackage').get('TotalNumberOfMembers').value || 1)
					this.totalPremium = this.decimalPipe.transform(total_premium, '1.2-2');
					this.insuranceCoverage = "35,000";//* (this.getQuoteForm.get('groupPackage').get('TotalNumberOfMembers').value | 1);
					this.annualPremium = 261.25;
					this.annualPremiumStr = '261.25';
				} else if(this.plan == '3') {
					total_premium = 55.5 * (this.getQuoteForm.get('groupPackage').value.TotalNumberOfStudents || 1);
					this.totalPremium = this.decimalPipe.transform(total_premium, '1.2-2');
					this.insuranceCoverage = "20,000";//* (this.getQuoteForm.get('groupPackage').get('TotalNumberOfMembers').value | 1);
					this.annualPremium = '55.50';
					this.annualPremiumStr = '55.50';
				}
				this.isValidTotalPremium = (total_premium >= CONSTANTS.MINIMUM_TOTAL_ANUAL_PREMIUM) ? true : false;
				break;
			case 3:
				if(this.plan == '1') {
					total_premium = 800 * (this.getQuoteForm.get('groupPackage').get('TotalNumberOfMembers').value || 1);
					this.totalPremium = this.decimalPipe.transform(total_premium, '1.0-0');
					this.insuranceCoverage = "200,000";//* (this.getQuoteForm.get('groupPackage').get('TotalNumberOfMembers').value | 1);
					this.annualPremium = 800;
					this.annualPremiumStr = '800';
				} else if( this.plan == '2') {
					total_premium = 394.25 * (this.getQuoteForm.get('groupPackage').get('TotalNumberOfMembers').value || 1);
					this.totalPremium = this.decimalPipe.transform(total_premium, '1.2-2');
					this.insuranceCoverage = "55,000";//* (this.getQuoteForm.get('groupPackage').get('TotalNumberOfMembers').value | 1);
					this.annualPremium = 394.25;
					this.annualPremiumStr = '394.25';
				} else if(this.plan == '3') {
					total_premium = 70.5 * (this.getQuoteForm.get('groupPackage').value.TotalNumberOfStudents || 1);
					this.totalPremium = this.decimalPipe.transform(total_premium, '1.2-2');
					this.insuranceCoverage = "25,000";//* (this.getQuoteForm.get('groupPackage').get('TotalNumberOfMembers').value | 1);
					this.annualPremium = '70.50';
					this.annualPremiumStr = '70.50';
				}
				this.isValidTotalPremium = (total_premium >= CONSTANTS.MINIMUM_TOTAL_ANUAL_PREMIUM) ? true : false;
				break;
			case 4:
				if(this.plan == '1') {
					total_premium = 1000 * (this.getQuoteForm.get('groupPackage').get('TotalNumberOfMembers').value || 1);
					this.totalPremium = this.decimalPipe.transform(total_premium, '1.0-0');
					this.insuranceCoverage = "250,000";//* (this.getQuoteForm.get('groupPackage').get('TotalNumberOfMembers').value | 1);
					this.annualPremium = 1000;
					this.annualPremiumStr = '1000';
				} else if( this.plan == '2') {
					total_premium = 460.75 * (this.getQuoteForm.get('groupPackage').get('TotalNumberOfMembers').value || 1);
					this.totalPremium = this.decimalPipe.transform(total_premium, '1.2-2');
					this.insuranceCoverage = "65,000";//* (this.getQuoteForm.get('groupPackage').get('TotalNumberOfMembers').value | 1);
					this.annualPremium = 460.75;
					this.annualPremiumStr = '460.75';
				} else if(this.plan == '3') {
					total_premium = 100.5 * (this.getQuoteForm.get('groupPackage').value.TotalNumberOfStudents || 1);
					this.totalPremium = this.decimalPipe.transform(total_premium, '1.2-2');
					this.insuranceCoverage = "35,000";//* (this.getQuoteForm.get('groupPackage').get('TotalNumberOfMembers').value | 1);
					this.annualPremium = '100.50';
					this.annualPremiumStr = '100.50';
				}
				this.isValidTotalPremium = (total_premium >= CONSTANTS.MINIMUM_TOTAL_ANUAL_PREMIUM) ? true : false;
				break;
			default:
		}
		
	}
	setShowSecond()
	{
		this.showSecondStep=true;

		setTimeout(function ()
		{
			document.querySelector('#step2_head').scrollIntoView
			({
				behavior: 'smooth',
				inline: 'nearest'
			});
		}, 100);
	}

	getFile()
	{
		var url = "/Home/GetFiles";
		this.apiService.sendGetRequest(url)
			.pipe(takeUntil(this.destroy$))
			.subscribe((responseBody) =>
			{
				this.privacyFile= "data:application/pdf;base64," + responseBody[0].primeCareFile;
			});
	}

	sanitize(url: string)
	{
		return this.sanitizer.bypassSecurityTrustUrl(url);
	}

	backtoGroupPage() {
		this.router.navigate(['/group']);
	}

	getCity(e){
		let selectedIndex = e.target.options.selectedIndex;
		this.cityText = e.target.options[selectedIndex].text; 
	}
	getRegion(e){
		let selectedIndex = e.target.options.selectedIndex;
		this.regionText = e.target.options[selectedIndex].text; 
		console.log(this.regionText);
	}
	getBarangaya(e){
		let selectedIndex = e.target.options.selectedIndex;
		this.barangayaText = e.target.options[selectedIndex].text; 
	}
	
	getBusinessType(e){
		let selectedIndex = e.target.options.selectedIndex;
		this.BusinessTxt = e.target.options[selectedIndex].text; 
	}
	
	getPrefix(e) {
		let selectedIndex = e.target.options.selectedIndex;
		this.authPrefixTxt = e.target.options[selectedIndex].text; 
	}
	getSuffix(e) {
		let selectedIndex = e.target.options.selectedIndex;
		this.authSuffixTxt = e.target.options[selectedIndex].text; 
	} 
	
	saveQuoteForm(type: any = 'submit')
	{
		this.ngxService.start();
		const groupPlanData = {
			totalPremium: this.totalPremium,
			insuranceCoverage: this.insuranceCoverage,
			annualPremium: this.annualPremium,
			productName: this.productName, // Employee Secure Plan
			productType: this.groupPackageCtrl.get('PlanType').value, // 1,2,3,4
			planCode: this.planCode, // 1 - Administrative and Office-based
			plan: this.selectedPlan, // 1
		}
		const packageInfo = this.getQuoteForm.get('groupPackage').value;
		const basicInfo = this.getQuoteForm.get('basicInformation').value;
		basicInfo.AuthPrefixName = { id: basicInfo.AuthPrefixName, name: this.authPrefixTxt };
		basicInfo.AuthSuffixName = { id: basicInfo.AuthSuffixName, name: this.authSuffixTxt };
		basicInfo.Region = { id: basicInfo.Region, name: this.regionText };
		basicInfo.City = { id: basicInfo.City, name: this.cityText };
		let data = { ...packageInfo, ...basicInfo };
		const groupQuoteDataNew = {
			...data,
			SelectedPlan: this.selectedPlan,
			ProductName: this.productName,
			CityTxt: this.cityText,
			RegionTxt: this.regionText,
			BarangayaTxt: this.barangayaText,
			BusinessTxt: this.BusinessTxt, AuthSuffixTxt: this.authSuffixTxt, AuthPrefixTxt: this.authPrefixTxt
		};

		if (type == 'save') {
			this.session.set('selectedGroupPlanData', groupPlanData);
			this.session.set(StorageType.POST_GROUP_QUOTE, groupQuoteDataNew);

			document.getElementById("closeModal-quote").click();
			this.ngxService.stopAll();
		}
		this.submitted = true;
		if (this.getQuoteForm.valid) {
			const access = this.session.get(StorageType.ACCESS_DATA);
			
			if(this.hasData) {
				
				if(JSON.stringify(this.groupQuoteData) != JSON.stringify(groupQuoteDataNew)) {
					console.log(this.groupQuoteData, groupQuoteDataNew);
					console.log('edit');
					this.quoteService_API.updateQuote(this.mapFormData(), access.referenceCode)
						.pipe(
							takeUntil(this.destroy$),
							finalize(() => {
								this.ngxService.stopAll();
							})
						)
						.subscribe((data: any) => {
							this.session.set('selectedGroupPlanData', groupPlanData);
							this.session.set(StorageType.POST_GROUP_QUOTE, groupQuoteDataNew);
							this.session.set(StorageType.ACCESS_DATA, data);
							this.successState(type, data.referenceCode);
						}, error => {
							this.errorState(error, type);
						})
				} else {
					console.log('same');
					this.successState(type, access.referenceCode);
				}
			} else {
				console.log('new');
				this.quoteService_API.addQuote(this.mapFormData())
					.pipe(
						takeUntil(this.destroy$),
						finalize(() => {
							this.ngxService.stopAll();
						})
					)
					.subscribe((data: any) => {
						this.session.set('selectedGroupPlanData', groupPlanData);
						this.session.set(StorageType.POST_GROUP_QUOTE, groupQuoteDataNew);
						this.session.set(StorageType.ACCESS_DATA, data);
						this.session.set(StorageType.SESSION, data.session);
						this.successState(type, data.referenceCode);
					}, error => {
						this.errorState(error, type);
					})
			}
			
		}
	}

	successState(type: any, refCode: any){
		this.ngxService.stopAll();
		if(type == 'submit') {
			this.router.navigate(['/group/apply']);
		} else {
			this.router.navigate(['/group/application-reference', refCode]);
		}
	}

	errorState(error: any, type: any) {
		this.ngxService.stopAll();
		this.errorInfo = error.message;
	}


	mapFormData(){
		// let pc = this.planCode;
		let formData: any = {
			// planCode: pc.split(' - ').pop(),
			planCode: this.planCode,
			planVariantCode: this.productName +' '+this.groupPackageCtrl.get('PlanType').value,
			planFaceAmount: parseFloat(this.insuranceCoverage.toString().replace(/,/g, '')),
			planPremium: this.annualPremium,
			totalMembers: Number(this.groupPackageCtrl.get('TotalNumberOfMembers').value),
			representativeNamePrefix: this.authPrefixTxt,
			representativeNameSuffix: this.authSuffixTxt,
			representativeFirstName: this.basicInfoCtrl.get('AuthFristName').value,
			representativeMiddleName: this.basicInfoCtrl.get('AuthMiddleName').value, 
			representativeLastName: this.basicInfoCtrl.get('AuthLastName').value,
			representativePhoneNumber: this.basicInfoCtrl.get('AuthLandlineNo').value,
			representativeMobileNumber: this.basicInfoCtrl.get('AuthMobileNumber').value,
			representativeEmailAddress: this.basicInfoCtrl.get('AuthEamilId').value,
			businessStructure: this.basicInfoCtrl.get('BusinessType').value,
			companyName: this.basicInfoCtrl.get('CompanyName').value,
			companyAddress1:  this.basicInfoCtrl.get('StreetNumer').value,
			companyAddress2:  this.basicInfoCtrl.get('VillageName').value,
			companyTown: this.basicInfoCtrl.get('Barangaya').value,
			companyCity: this.cityText,
			companyRegion: this.regionText,
			companyZipCode: this.basicInfoCtrl.get('ZipCode').value,
			companyPhoneNumber: this.basicInfoCtrl.get('CompanyLandLineNo').value,
			companyMobileNumber: this.basicInfoCtrl.get('CompanyMobileNo').value,
		};

		if(this.selectedPlan.toString() == '3') {
			formData.totalTeachers =  Number(this.groupPackageCtrl.get('TotalNumberOfTeachers').value);
			formData.totalStudents =  Number(this.groupPackageCtrl.get('TotalNumberOfStudents').value);
		}

		return formData;
	}

	onChangeProviance(value)
	{
		console.log(value);
		for (let i = 0; i < CONSTANTS.PROVIANCE.length; i++)
		{
			if (CONSTANTS.PROVIANCE[i].id == value)
				this.muncipality = CONSTANTS.PROVIANCE[i].Municipality;
		}
	}

	openNewWindow(url: string) {
		this.util.openNewWindow(url);
	}

	downloadPlanDes() {
		var filePath = "";
		var pdfName = "";
		if (this.selectedPlan == 1) {
			filePath = '../../../../../assets/documents/Employee Secure_Plan Description.pdf';
			pdfName = 'Employee Secure_Plan Description';
		}
		else if (this.selectedPlan == 2) {
			filePath = '../../../../../assets/documents/Security Guard_Plan Description.pdf';
			pdfName = 'Security Guard_Plan Description';
		}
		else if (this.selectedPlan == 3) {
			filePath = '../../../../../assets/documents/Students And Teachers_Plan Description.pdf';
			pdfName = 'Students And Teachers_Plan Description';
		}
		this.openNewWindow(filePath + '#page=' + 1);
	}

	getReferenceData(list: any[], item: any)
	{
		let id = Number(item.value);

		// If 0 return null
		if(id === 0)
			return null;

		// Return match
		for(var i = 0; i < list.length; i++)
			if(Number(list[i].id) === id)
				return list[i];

		// If no match return null
		return null;
	}

	getReferenceDataName(list: any[], item: any): string
	{
		let match = this.getReferenceData(list, item);

		if(match === null)
			return null;

		return String(match.name).toUpperCase();
	}

	getReferenceDataId(list: any[], item: any): number | null
	{
		let match = this.getReferenceData(list, item);

		if(match === null)
			return null;

		return Number(match.id);
	}

	nullIfEmpty(value: string): string
	{
		if(this.isNullOrWhiteSpace(value))
			return null;

		return value;
	}

	isNullOrWhiteSpace(value: string)
	{
		return value === null || value.match(/^ *$/) !== null;
	}

	conformSave() {
		document.getElementById("conformBtn").click();
	}

	get groupPackageCtrl() {
		return this.getQuoteForm.get('groupPackage');
	}

	get basicInfoCtrl() {
		return this.getQuoteForm.get('basicInformation');
	}
}
