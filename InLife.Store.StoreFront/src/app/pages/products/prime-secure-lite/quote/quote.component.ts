import { environment } from '@environment';

import { Injectable, Injector, ElementRef } from '@angular/core';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ViewportScroller, CurrencyPipe } from '@angular/common';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators, FormArray } from '@angular/forms';
import { HttpClient, HttpResponse, HttpHeaders, HttpParams, HttpErrorResponse } from '@angular/common/http';
import { DomSanitizer } from '@angular/platform-browser';
import { NgxUiLoaderService } from 'ngx-ui-loader';

import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';

import { CONSTANTS } from '@app/services/constants';
import { ApiService, SessionStorageService } from '@app/services';
import * as moment from 'moment';
import { MatDialog } from '@angular/material/dialog';
import { GeneralMessagePromptComponent } from '@app/shared/component/prompt-message/general-message-prompt.component';
import { MatTooltip } from '@angular/material/tooltip';
@Component
({
	selector: 'app-quote',
	templateUrl: './quote.component.html',
	styleUrls: ['./quote.component.css', './quote.component.scss']
})
export class QuoteComponent implements OnInit
{
	getQuoteForm: FormGroup;
	submitted = false;
	today = new Date(new Date().setFullYear(new Date().getFullYear() - 18));
	sixtyYearsAgo = new Date(new Date().getFullYear() - 60, 0, 1);//Jan 1, 60 years ago
	maxYear = new Date(2020, 11, 31);
	muncipality;
	CONSTANTS = CONSTANTS;
	plan = '';
	amount = 0;
	showSecondStep:Boolean=false;
	showThirdStep:Boolean=false;
	showForthStep:Boolean=false;
	calulatedAge:number=0;
	privacyFile:any;
	affiliate:any;

	quoteDetails: any;
	subscription:any;
	subscriptions:any;
	status:any;
	PRIME_SECURE_LITE = CONSTANTS.PRIME_SECURE_LITE
	bodyMassIndex: string;
	eligiblePlan: string;
	localMontlyIncome: number;
	@ViewChild(MatTooltip) tooltip: MatTooltip;
	
	constructor
	(
		private router: Router,
		private formBuilder: FormBuilder,
		private route: ActivatedRoute,
		private apiService: ApiService,
		private session: SessionStorageService,
		private ngxService: NgxUiLoaderService,
		private vps: ViewportScroller,
		private http: HttpClient,
		private sanitizer: DomSanitizer,
		private currencyPipe: CurrencyPipe,
		private dialog: MatDialog
	)
	{
		this.ngxService.start();
	}

	ngOnInit(): void
	{
		this.affiliate = this.affiliate = this.session.get('affiliate');

		const getQuoteFormData = this.session.get('getQuoteForm');
		this.quoteDetails = this.session.get('getinnerForm');
		this.initForm(getQuoteFormData);
		this.getFile();

		if (this.affiliate?.agentCode)
		{
			this.getQuoteForm.get('basicInformation').get('primeCare').setValue('1');

			this.getQuoteForm.get('basicInformation').get('acode').setValue(this.affiliate?.agentCode);
			this.getQuoteForm.get('basicInformation').get('afname').setValue(this.affiliate?.agentName);
			this.getQuoteForm.get('basicInformation').get('agentBranchCode').setValue(this.affiliate?.branchCode);

			this.getQuoteForm.get('basicInformation').get('acode').enable();
			this.getQuoteForm.get('basicInformation').get('afname').enable();
			this.getQuoteForm.get('basicInformation').get('alname').enable();
			this.getQuoteForm.get('basicInformation').get('agentBranchCode').enable();

			this.getQuoteForm.get('basicInformation').get('afname').clearValidators();
			this.getQuoteForm.get('basicInformation').get('alname').clearValidators();
		}
		else if (this.getQuoteForm.get('basicInformation').get('primeCare').value == '1')
		{
			this.getQuoteForm.get('basicInformation').get('acode').disable();
			this.getQuoteForm.get('basicInformation').get('agentBranchCode').disable();

			this.getQuoteForm.get('basicInformation').get('afname').enable();
			this.getQuoteForm.get('basicInformation').get('alname').enable();

			this.getQuoteForm.get('basicInformation').get('afname').setValidators([Validators.required]);
			this.getQuoteForm.get('basicInformation').get('alname').setValidators([Validators.required]);
		}
		else
		{
			this.getQuoteForm.get('basicInformation').get('acode').disable();
			this.getQuoteForm.get('basicInformation').get('agentBranchCode').disable();

			this.getQuoteForm.get('basicInformation').get('afname').disable();
			this.getQuoteForm.get('basicInformation').get('alname').disable();
		}

		this.getQuoteForm.get('basicInformation').get('country').valueChanges.subscribe(result =>
		{
			if (result === '170')
			{
				this.getQuoteForm.get('basicInformation').get('province').enable();
				this.getQuoteForm.get('basicInformation').get('municipality').enable();
				this.getQuoteForm.get('basicInformation').get('province').setValidators([Validators.required]);
				this.getQuoteForm.get('basicInformation').get('municipality').setValidators([Validators.required]);
			}
			else
			{
				this.getQuoteForm.get('basicInformation').get('province').disable();
				this.getQuoteForm.get('basicInformation').get('municipality').disable();
			}
			this.getQuoteForm.updateValueAndValidity();
		});

		this.getQuoteForm.get('basicInformation').get('primeCare').valueChanges.subscribe(result =>
		{
			if (result === '1')
			{
				this.getQuoteForm.get('basicInformation').get('afname').enable();
				this.getQuoteForm.get('basicInformation').get('alname').enable();

				this.getQuoteForm.get('basicInformation').get('afname').setValidators([Validators.required]);
				this.getQuoteForm.get('basicInformation').get('alname').setValidators([Validators.required]);
			}
			else
			{
				this.getQuoteForm.get('basicInformation').get('afname').disable();
				this.getQuoteForm.get('basicInformation').get('alname').disable();
			}
		});

		this.getQuoteForm.get('calculatePremium').valueChanges.subscribe(result =>
		{
			let data = { annual: this.getAnualPremium(), amount: this.calPay()  }
			this.apiService.setMessage(data);
		});

		this.getQuoteForm.get('basicInformation').valueChanges.subscribe(result => {
				if (result?.monthlyIncome) {
					this.getQuoteForm.get('basicInformation').patchValue({
						monthlyIncome: this.currencyPipe.transform(result.monthlyIncome.replace(/\D/g, '').replace(/^0+/, ''), '₱', 'symbol', '1.0-0')
					}, {emitEvent: false})
				}
		})

		this.getQuoteForm.get('healthCondition').valueChanges.subscribe(result => {
				if (result?.heightInFeet && result?.heightInInches && result?.weight) {
					let tempheight = this.feetToInches(result.heightInFeet) + result.heightInInches
					let tempCalculationBMI = this.calculateBMIByInchesAndPounds(tempheight, result.weight)
					this.bodyMassIndex = Math.round(tempCalculationBMI).toString();
				}
		});

		this.getQuoteForm.get('covidForm').get('privacyPolicy').valueChanges.subscribe( result => {
			if (!result){
				this.getQuoteForm.get('covidForm').get('privacyPolicy').patchValue(null, {emitEvent: false} );
				this.getQuoteForm.get('covidForm').get('privacyPolicy').updateValueAndValidity({emitEvent: false});
			}
		});

		this.getQuoteForm.get('covidForm').get('privacyPolicy2').valueChanges.subscribe( result => {
			if (!result){
				this.getQuoteForm.get('covidForm').get('privacyPolicy2').patchValue(null, {emitEvent: false} );
				this.getQuoteForm.get('covidForm').get('privacyPolicy2').updateValueAndValidity({emitEvent: false});
			}
		});

		this.showSecondStep=this.getQuoteForm.get('calculatePremium').valid;
		this.showThirdStep=this.getQuoteForm.get('basicInformation').valid;
		this.showForthStep=this.getQuoteForm.get('healthCondition').valid;
		this.getAge(this.getQuoteForm.get('calculatePremium').get('dateofbirth').value);
		this.ngxService.stop();
	}

	showBirthTooltip(){
		this.tooltip.show(0);
	}

	getAnualPremium() {
		let age = this.getAge(this.getQuoteForm.get('calculatePremium').get('dateofbirth').value);
		if(this.isBetween(18, 47, age)) {
			this.eligiblePlan = '2500';
			return '2,500';
		} else if (this.isBetween(48, 60, age)) {
			this.eligiblePlan = '3000';
			return '3,000';
		}
	}

	backClicked() {
		this.session.set("getQuoteForm",this.getQuoteForm.value);
		this.router.navigate(['prime-secure-lite']);
	}

	getcall()
	{
		//alert("ss");
	}

	private feetToInches(feet: number) {
		return feet * 12
	}

	private calculateBMIByInchesAndPounds(height: number, weight: number) {
		return (weight * 703) / (height * height)
	}

	calPay()
	{
		const plans = this.plan;

		let paymentMode;
		let gender;
		if (this.plan == "plan_50")
		{
			if (this.getQuoteForm.get('calculatePremium').get('paymentMode').value == 'Monthly')
				paymentMode = CONSTANTS.PLANS[1][this.plan][0].Monthly;

			if (this.getQuoteForm.get('calculatePremium').get('paymentMode').value == 'Annual')
				paymentMode = CONSTANTS.PLANS[1][this.plan][1].Annual;
		}
		else if(this.plan == "plan_30")
		{
			if (this.getQuoteForm.get('calculatePremium').get('paymentMode').value == 'Monthly')
				paymentMode = CONSTANTS.PLANS[0][this.plan][0].Monthly;

			if (this.getQuoteForm.get('calculatePremium').get('paymentMode').value == 'Annual')
				paymentMode = CONSTANTS.PLANS[0][this.plan][1].Annual;
		}

		// Sloppy coding, tsk
		// Male = 8
		if (this.getQuoteForm.get('calculatePremium').get('gender').value == '8' && paymentMode)
			gender = paymentMode[0].Male;

		// Female = 7
		if (this.getQuoteForm.get('calculatePremium').get('gender').value == '7' && paymentMode)
			gender = paymentMode[1].Female;

		let age = this.getAge(this.getQuoteForm.get('calculatePremium').get('dateofbirth').value);
		this.session.set('age', { age: age });
		//this.session.set("age",{ age:age  });
		let price = 0;

		if (gender)
		{
			for (let i = 0; i < gender.length; i++)
			{
				if (gender[i].Age == age)
					price = gender[i].Price;
			}

			this.amount = price;
			return price;
		}
	}

	getAge(DOB)
	{
		//console.log(DOB);
		const today = new Date();
		const birthDate = new Date(DOB);
		let age = today.getFullYear() - birthDate.getFullYear();
		const m = today.getMonth() - birthDate.getMonth();
		if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate()))
		{
			age = age - 1;
		}
		this.calulatedAge=age;
		return age;
	}

	initForm(getQuoteFormData)
	{
		this.getQuoteForm = this.formBuilder.group
		({
			calculatePremium: this.addCalculatePremium(getQuoteFormData?.calculatePremium || ''),
			basicInformation: this.addBasicForm(getQuoteFormData?.basicInformation || ''),
			healthCondition: this.addHealthCondition(getQuoteFormData?.healthCondition || ''),
			covidForm: this.addCovidForm(getQuoteFormData?.covidForm || '')
		});
		if(getQuoteFormData) {
			this.getAnualPremium();
			let height = this.feetToInches(this.getQuoteForm.get('healthCondition').get('heightInFeet').value) + this.getQuoteForm.get('healthCondition').get('heightInInches').value
			let calculationBMI = this.calculateBMIByInchesAndPounds(height, this.getQuoteForm.get('healthCondition').get('weight').value)
			let bmi = Math.round(calculationBMI).toString();
			this.bodyMassIndex = this.quoteDetails.hasOwnProperty('bmi') ? this.quoteDetails.bmi : bmi;
		}

	}

	addCalculatePremium(data)
	{
		// const plan = this.plan === 'plan_30' ? '1080000' : this.plan === 'plan_50' ? '1800000' : '';
		this.apiService.setMessage({ annual: data === '' ? '' : data.paymentMode, amount: '0.00' });
		return this.formBuilder.group
		({
			totalCashBenefit: new FormControl((data.totalCashBenefit) || '', Validators.required),
			chas: new FormControl((data.chas) || '', Validators.required),
			covid: new FormControl((data.covid) || '', Validators.required),
			dateofbirth: new FormControl(data.dateofbirth || '', Validators.required),
			gender: new FormControl(data.gender || '', Validators.required),
			paymentMode: new FormControl(data.paymentMode || ''),
			esc: new FormControl(data.esc || '', Validators.required)
		});
	}

	getFile()
	{
		var url = "/Home/GetFiles";
		this.apiService.sendGetRequest(url).subscribe((responseBody) =>
		{
			this.privacyFile= "data:application/pdf;base64," + responseBody[0].primeCareFile;
		});
	}

	sanitize(url: string)
	{
		return this.sanitizer.bypassSecurityTrustUrl(url);
	}

	setShowSecond()
	{
		this.showSecondStep=true;

		setTimeout(function ()
		{
			document.querySelector('#step2_head').scrollIntoView
			({
				behavior: 'smooth'
			});
		}, 100);
	}

	setShowThird()
	{
		this.showThirdStep=true;

		setTimeout(function ()
		{
			document.querySelector('#step3_head').scrollIntoView
			({
				behavior: 'smooth'
			});
		}, 100);
	}

	setShowForth()
	{
		this.showForthStep=true;

		setTimeout(function ()
		{
			document.querySelector('#step4_head').scrollIntoView
			({
				behavior: 'smooth'
			});
		}, 100);
	}

	addBasicForm(data)
	{
		if(data) {
			this.onChangeProviance(data.province);
		}

		return this.formBuilder.group
		({
			prefix: new FormControl(data.prefix || '', Validators.required),
			fname: new FormControl(data.fname || '', [Validators.required, Validators.maxLength(50), Validators.pattern("^[A-Za-z -]+$")]),
			mname: new FormControl(data.mname || '', [Validators.maxLength(50), Validators.pattern("^[A-Za-z -]+$")]),
			lname: new FormControl(data.lname || '', [Validators.required, Validators.maxLength(50), Validators.pattern("^[A-Za-z -]+$")]),
			suffix: new FormControl(data.suffix || '', Validators.required),
			landline: new FormControl(data.landline || '', [Validators.pattern("^[0-9]{9}$")]),
			mobile: new FormControl(data.mobile || '', [Validators.required, Validators.pattern("^[1-9]{1}[0-9]{9}$")]),
			country: new FormControl(data.country || '170', Validators.required),

			province: new FormControl(data.province ||'', Validators.required),
			municipality: new FormControl(data.municipality ||'', Validators.required),
			primeCare: new FormControl(data.primeCare || '', Validators.required),
			acode: new FormControl(data.acode || '', Validators.required),
			afname: new FormControl(data.afname || '', Validators.required),
			alname: new FormControl(data.alname || '', Validators.required),
			agentBranchCode: new FormControl(data.agentBranchCode || '', [Validators.maxLength(50)]),
			/*email: new FormControl(data.email || '', [Validators.required, Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$")]),*/
			email: new FormControl(data.email || '', [Validators.required, Validators.pattern(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/)]),
			company: new FormControl(data.company || '', Validators.required),
			occupation: new FormControl(data.occupation || '', Validators.required),
			sourceOfFunds: new FormControl(data.sourceOfFunds || '', Validators.required),
			monthlyIncome: new FormControl(data.monthlyIncome || '', Validators.required),
		});
	}

	addHealthCondition(data)
	{
		return this.formBuilder.group
		({
			heightInFeet: new FormControl(data.heightInFeet || '', Validators.required),
			heightInInches: new FormControl(data.heightInInches || '', Validators.required),
			weight: new FormControl(data.weight || '', Validators.required),
			healthDeclaration1: new FormControl(data.healthDeclaration1 || '', Validators.required),
			healthDeclaration2: new FormControl(data.healthDeclaration2 || '', Validators.required),
			healthDeclaration3: new FormControl(data.healthDeclaration3 || '', Validators.required),
			healthDeclaration4: new FormControl(data.healthDeclaration4 || '', Validators.required)
		});
	}

	addCovidForm(data)
	{
		return this.formBuilder.group
		({
			healthCondition1: new FormControl(data.healthCondition1 || '', Validators.required),
			healthCondition2: new FormControl(data.healthCondition2 || '', Validators.required),
			healthCondition3: new FormControl(data.healthCondition3 || '', Validators.required),
			healthCondition4: new FormControl(data.healthCondition4 || '', Validators.required),
			privacyPolicy: new FormControl(data.privacyPolicy || '', Validators.required),
			privacyPolicy2: new FormControl(data.privacyPolicy2 || '', Validators.required)
		});
	}

	submitQuoteForm()
	{

		var dd = this.getQuoteForm.value.country;
		this.submitted = true;
		// TODO: Move eligibility checking on server
		if (this.getQuoteForm.valid)
		{
			this.session.set('getQuoteForm', this.getQuoteForm.value);
			//this.session.set("getQuoteForm", JSON.stringify(this.getQuoteForm.value));
			const healthCondition = this.getQuoteForm.get('healthCondition').value;
			const covidForm = this.getQuoteForm.get('covidForm').value;

			if (this.getQuoteForm.get('calculatePremium').get('paymentMode').value == 'Monthly')
				this.amount = parseFloat(this.amount.toString().replace(/,/g, '')) * 12;
			else
				this.amount = parseFloat(this.amount.toString().replace(/,/g, ''));


			let isEligible = (
				healthCondition.healthDeclaration1.toUpperCase() === 'NO' &&
				healthCondition.healthDeclaration2.toUpperCase() === 'NO' &&
				healthCondition.healthDeclaration3.toUpperCase() === 'NO' &&
				healthCondition.healthDeclaration4.toUpperCase() === 'NO' &&

				covidForm.healthCondition1.toUpperCase() === 'NO' &&
				covidForm.healthCondition2.toUpperCase() === 'NO' &&
				covidForm.healthCondition3.toUpperCase() === 'YES' &&
				covidForm.healthCondition4.toUpperCase() === 'NO' &&
				this.getQuoteForm.value.basicInformation.country === '170' &&
				(Number(this.bodyMassIndex) < 29) &&
				this.isValidOccupation(this.getQuoteForm.get('basicInformation').get('occupation').value)
			);

			this.saveQuoteForm(isEligible);

		}
		else
		{
		}
		return;
	}

	isValidOccupation(id): boolean {
		if(id) {
			const invalidOccupation = ['', '1', '4', '5', '8', '9', '11', '12', '13', '16', '18', '27', '29', '35', '39', '41', '42',
						'47', '52', '54', '58', '59', '60', '61', '69', '70', '71', '74', '76', '79', '80', '85', '87',
						'92', '94', '95', '100', '110', '112', '113', '116', '117', '120', '123', '125', '127', '132', '135', '139',
						'140', '141', '144', '145', '146', '147', '149', '155', '156', '161', '164', '168', '170', '182', '183', '139',
						'188', '189', '190', '191', '200', '201', '202', '203', '204', '205', '206', '298', '301', '307', '310', '313',
						'316', '319', '321', '207', '208', '209', '210', '211', '212', '213', '214', '215', '216', '217', '219', '221',
						'222', '223', '232', '233', '237', '238', '244', '246', '247', '248', '249', '250', '251', '253', '258', '259',
						'260', '269', '270', '273', '274', '276', '279', '281', '286', '287'];


			for(let i=0; i< invalidOccupation.length; i++) {
				if(invalidOccupation[i] === id) {
					return false;
				}
			}
		}

		return true;
	}

	saveQuoteForm(isEligible: boolean)
	{
		const health = this.getQuoteForm.get('healthCondition').value;
		const basicInfo  = this.getQuoteForm.get('basicInformation');
		const calcInfo = this.getQuoteForm.get('calculatePremium');

		let planDetails = { annual: this.getAnualPremium(), amount: this.calPay(), bmi: this.bodyMassIndex };
		this.session.set('getinnerForm', planDetails);

		let country = this.getReferenceDataName(CONSTANTS.COUNTRY, basicInfo.get('country'));
		let region: string = null;
		let city: string = null;
		if(country == 'PHILIPPINES')
		{
			let regionList = this.getReferenceData(CONSTANTS.PROVIANCE, basicInfo.get('province'));
			region = String(regionList.Province).toUpperCase();
			city = this.getReferenceDataName(regionList.Municipality, basicInfo.get('municipality'));
		}

		let faceAmount = parseFloat(calcInfo.get('totalCashBenefit').value.substring(1).replace(/,/g, ''));
		let monthlyIncome = parseFloat(basicInfo.get('monthlyIncome').value.substring(1).replace(/,/g, ''));

		var dataInternalAPI =
		{
			planCode: 'PSLite',
			planVariantCode: 'PSLite' + this.eligiblePlan,
			planFaceAmount: faceAmount,
			planPremium: +this.eligiblePlan,
			customerNamePrefix: basicInfo.get('prefix').value,
			customerNameSuffix: basicInfo.get('suffix').value,
			customerFirstName: basicInfo.get('fname').value,
			customerMiddleName: basicInfo.get('mname').value,
			customerLastName: basicInfo.get('lname').value,
			customerPhoneNumber: basicInfo.get('landline').value,
			customerMobileNumber: basicInfo.get('mobile').value,
			customerEmailAddress: basicInfo.get('email').value,
			height: 1,
			weight: 1,
			company: basicInfo.get('company').value,
			occupation: basicInfo.get('occupation').value,
			incomeSource: basicInfo.get('sourceOfFunds').value,
			incomeAmount: monthlyIncome,
			addressCity: city,
			addressRegion: region,
			addressCountry: country
		};
		console.log(faceAmount);
		var dataExternalAPI = {
			InsuredPrefixId: +basicInfo.get('prefix').value,
			InsuredFirstName: basicInfo.get('fname').value,
			InsuredMiddleName: basicInfo.get('mname').value,
			InsuredLastName: basicInfo.get('lname').value,
			InsuredSuffixId: +basicInfo.get('suffix').value,
			InsuredBirthday: new Date(calcInfo.get('dateofbirth').value).toLocaleDateString(),
			InsuredGenderId: +calcInfo.get('gender').value,
			PlanCode: 'TR0091',
			PlanName: 'Prime Secure Lite',
			PaymentMode: 12,
			FaceAmount: faceAmount,
			Premium: +this.eligiblePlan,
			InsuredPrimaryOccupationId: +basicInfo.get('occupation').value,
			InsuredPrimaryOccupationMonthlyIncome: monthlyIncome,
			Bmi: +this.bodyMassIndex
		}

		this.session.set('PostQuote', dataInternalAPI);

		console.log(dataInternalAPI, dataExternalAPI);

		let headers: HttpHeaders = new HttpHeaders();
		headers = headers.append('Content-Type', 'application/json');
		headers = headers.append('Ocp-Apim-Subscription-Key', environment.primeCareApi.subscriptionKey);

		let options =
		{
			headers: headers,
			params: new HttpParams()
		};

		let body = JSON.stringify(dataExternalAPI);
		let endpoint = environment.primeCareApi.host + environment.primeCareApi.createQuoteEndpoint;
		this.ngxService.start();
		this.session.set('PostQuote', dataInternalAPI);

		if(isEligible) {
			this.http
			.post(endpoint, body, options)
			.pipe(retry(1))
			.subscribe((data: any) =>
			{
				this.ngxService.stopAll();
				if(data.underwritingStatus === 'CLEAN_CASE') {
					this.session.set('refNo', '1357246812'.concat(Math.floor(Math.random() * 101).toString()));
					this.session.set('UnderWritingStatus', data)
					this.router.navigate(['prime-secure-lite/apply']);
				} else {
					const dialogRef = this.dialog.open(GeneralMessagePromptComponent, {
						width: '300px',
						data: {
							message: `You applications status is ${data.underwritingStatus} which means it did not pass the application requirements.`
						}
					});
					dialogRef.afterClosed().pipe().subscribe(data => {
						if(data) {
							this.router.navigate(['prime-secure-lite/ineligible']);
						}
					})
				}
			}, (error)=>{
				 this.router.navigate(['prime-secure-lite/apply']);
			});

		} else {
			// this.session.clear();
			this.router.navigate(['prime-secure-lite/ineligible']);
		}


	}

	onChangeProviance(value)
	{
		for (let i = 0; i < CONSTANTS.PROVIANCE.length; i++)
		{
			if (CONSTANTS.PROVIANCE[i].id == value)
				this.muncipality = CONSTANTS.PROVIANCE[i].Municipality;
		}
	}

	onChangeState(stateValue)
	{
		//this.cityInfo=this.stateInfo[stateValue].Cities;
		//console.log(this.cityInfo);
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


	apply(){
		this.router.navigate(['prime-secure-lite/apply']);
	}

	handleDOBChange(event) {
		const age = this.getAge(this.getQuoteForm.get('calculatePremium').get('dateofbirth').value);
		this.calulatedAge = age;
		this.calculatePlan(age);
	}

	calculatePlan(age:number) {

		//AN 25
		if (this.isBetween(18, 39, age)) {
			this.getQuoteForm.get('calculatePremium').get('totalCashBenefit').setValue(this.PRIME_SECURE_LITE.an25A.totalCashBenefitFormatted)
			this.getQuoteForm.get('calculatePremium').get('chas').setValue(this.PRIME_SECURE_LITE.an25A.chasFormatted)
			this.getQuoteForm.get('calculatePremium').get('covid').setValue(this.PRIME_SECURE_LITE.an25A.covidFormatted)
			this.getQuoteForm.get('calculatePremium').get('esc').setValue(this.PRIME_SECURE_LITE.an25A.spouseFormatted)
		}

		if (this.isBetween(40, 45, age)) {
			this.getQuoteForm.get('calculatePremium').get('totalCashBenefit').setValue(this.PRIME_SECURE_LITE.an25B.totalCashBenefitFormatted)
			this.getQuoteForm.get('calculatePremium').get('chas').setValue(this.PRIME_SECURE_LITE.an25B.chasFormatted)
			this.getQuoteForm.get('calculatePremium').get('covid').setValue(this.PRIME_SECURE_LITE.an25B.covidFormatted)
			this.getQuoteForm.get('calculatePremium').get('esc').setValue(this.PRIME_SECURE_LITE.an25B.spouseFormatted)
		}

		if (this.isBetween(46, 47, age)) {
			this.getQuoteForm.get('calculatePremium').get('totalCashBenefit').setValue(this.PRIME_SECURE_LITE.an25C.totalCashBenefitFormatted)
			this.getQuoteForm.get('calculatePremium').get('chas').setValue(this.PRIME_SECURE_LITE.an25C.chasFormatted)
			this.getQuoteForm.get('calculatePremium').get('covid').setValue(this.PRIME_SECURE_LITE.an25C.covidFormatted)
			this.getQuoteForm.get('calculatePremium').get('esc').setValue(this.PRIME_SECURE_LITE.an25C.spouseFormatted)
		}
		//END AN 25

		//AN 30
		if (this.isBetween(48, 49, age)) {
			this.getQuoteForm.get('calculatePremium').get('totalCashBenefit').setValue(this.PRIME_SECURE_LITE.an30A.totalCashBenefitFormatted)
			this.getQuoteForm.get('calculatePremium').get('chas').setValue(this.PRIME_SECURE_LITE.an30A.chasFormatted)
			this.getQuoteForm.get('calculatePremium').get('covid').setValue(this.PRIME_SECURE_LITE.an30A.covidFormatted)
			this.getQuoteForm.get('calculatePremium').get('esc').setValue(this.PRIME_SECURE_LITE.an30A.spouseFormatted)
		}

		if (this.isBetween(50, 56, age)) {
			this.getQuoteForm.get('calculatePremium').get('totalCashBenefit').setValue(this.PRIME_SECURE_LITE.an30B.totalCashBenefitFormatted)
			this.getQuoteForm.get('calculatePremium').get('chas').setValue(this.PRIME_SECURE_LITE.an30B.chasFormatted)
			this.getQuoteForm.get('calculatePremium').get('covid').setValue(this.PRIME_SECURE_LITE.an30B.covidFormatted)
			this.getQuoteForm.get('calculatePremium').get('esc').setValue(this.PRIME_SECURE_LITE.an30B.spouseFormatted)
		}

		if (this.isBetween(57, 60, age)) {
			this.getQuoteForm.get('calculatePremium').get('totalCashBenefit').setValue(this.PRIME_SECURE_LITE.an30C.totalCashBenefitFormatted)
			this.getQuoteForm.get('calculatePremium').get('chas').setValue(this.PRIME_SECURE_LITE.an30C.chasFormatted)
			this.getQuoteForm.get('calculatePremium').get('covid').setValue(this.PRIME_SECURE_LITE.an30C.covidFormatted)
			this.getQuoteForm.get('calculatePremium').get('esc').setValue(this.PRIME_SECURE_LITE.an30C.spouseFormatted)
		}
		//END AN 30
	}



	private isBetween = (num1:number, num2: number, value: number) => value >= num1 && value <= num2

	formatCurrency($event) {
		const value = $event.target.value
		this.localMontlyIncome = value
		this.getQuoteForm.get('basicInformation').get('monthlyIncome').setValue(Number(value).toLocaleString('en-PH', {minimumFractionDigits: 0 }))
	}

	onFocusHandler($event) {
		console.log('event', $event.target.value)

	}
	//TESTING ONLY

	clickMe(){
		const button=document.getElementById('bttt');
		button.style.display='none';
	}
}
