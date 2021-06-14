import { StorageType } from '@app/services/storage-types.enum';
import { environment } from '@environment';

import { Injectable, Injector } from '@angular/core';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ViewportScroller } from '@angular/common';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators, FormArray } from '@angular/forms';
import { HttpClient, HttpResponse, HttpHeaders, HttpParams, HttpErrorResponse } from '@angular/common/http';
import { DomSanitizer } from '@angular/platform-browser';
import { NgxUiLoaderService } from 'ngx-ui-loader';

import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';

import { CONSTANTS } from '@app/services/constants';
import { ApiService, FacebookPixelService, SessionStorageService } from '@app/services';

@Component
({
	selector: 'app-quote',
	templateUrl: './quote.component.html',
	styleUrls: ['./quote.component.scss', './../main/prime-care-global.scss']
})
export class QuoteComponent implements OnInit
{
	getQuoteForm: FormGroup;
	submitted = false;
	today = new Date(new Date().setFullYear(new Date().getFullYear() - 18));
	maxYear = new Date(2020, 11, 31);
	muncipality;
	CONSTANTS = CONSTANTS;
	plan = '';
	amount = 0;
	showSecondStep:Boolean=false;
	showThirdStep:Boolean=false;
	calulatedAge:number=0;
	privacyFile:any;
	affiliate:any;

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
		private facebookPixelService: FacebookPixelService,
	)
	{
		this.ngxService.start();
		this.route.queryParams.subscribe(params =>
		{
			if (params && (params.plan == 'plan_30' || params.plan == 'plan_50' || params.plan == 'plan_51'))
			{
				this.plan = params.plan;
				//console.log(params.plan);
			}
			else
			{
				this.router.navigate(['/']);
			}
		});
	}

	ngOnInit(): void
	{
		this.facebookPixelService.track('ViewContent');
		this.facebookPixelService.track('GetQuote');
		this.affiliate = this.affiliate = this.session.get('affiliate');
		//try { this.affiliate = JSON.parse(sessionStorage.getItem("affiliate")); }
		//catch(ex) { this.affiliate = {}; }

		const getQuoteFormData = this.session.get('getQuoteForm');
		//const getQuoteFormData = JSON.parse(this.session.get('getQuoteForm') || "[]");
		this.initForm(getQuoteFormData);
		this.getFile();

		if (this.getQuoteForm.get('basicInformation').get('country').value == '170'
			&& this.getQuoteForm.get('basicInformation').get('province').value)
		{
			if (this.getQuoteForm.get('basicInformation').get('province').value)
				this.onChangeProviance(this.getQuoteForm.get('basicInformation').get('province').value);

			this.getQuoteForm.get('basicInformation').get('province').enable();
			this.getQuoteForm.get('basicInformation').get('municipality').enable();
			this.getQuoteForm.get('basicInformation').get('province').setValidators([Validators.required]);
			this.getQuoteForm.get('basicInformation').get('municipality').setValidators([Validators.required]);
			//this.onChangeProviance(this.getQuoteForm.get('basicInformation').get('province').value);
		}
		else
		{
			// this.getQuoteForm.get('basicInformation').get('province').disable();
			// this.getQuoteForm.get('basicInformation').get('municipality').disable();
		}

		if (this.affiliate?.agentCode)
		{
			this.getQuoteForm.get('basicInformation').get('primeCare').setValue('1');
			this.getQuoteForm.get('basicInformation').get('acode').setValue(this.affiliate?.agentCode);
			this.getQuoteForm.get('basicInformation').get('afname').setValue(this.affiliate?.agentName);

			this.getQuoteForm.get('basicInformation').get('acode').enable();
			this.getQuoteForm.get('basicInformation').get('afname').enable();
			this.getQuoteForm.get('basicInformation').get('alname').enable();

			this.getQuoteForm.get('basicInformation').get('afname').clearValidators();
			this.getQuoteForm.get('basicInformation').get('alname').clearValidators();
		}
		else if (this.getQuoteForm.get('basicInformation').get('primeCare').value == '1')
		{
			this.getQuoteForm.get('basicInformation').get('acode').disable();
			this.getQuoteForm.get('basicInformation').get('afname').enable();
			this.getQuoteForm.get('basicInformation').get('alname').enable();

			this.getQuoteForm.get('basicInformation').get('afname').setValidators([Validators.required]);
			this.getQuoteForm.get('basicInformation').get('alname').setValidators([Validators.required]);
		}
		else
		{
			this.getQuoteForm.get('basicInformation').get('acode').disable();
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

		this.getQuoteForm.get('healthCondition').get('privacyPolicy').valueChanges.subscribe(result =>
		{
			if (!result)
				this.getQuoteForm.get('healthCondition').get('privacyPolicy').setValue(null);
		});

		this.getQuoteForm.get('calculatePremium').valueChanges.subscribe(result =>
		{
			if (this.getQuoteForm.get('calculatePremium').valid)
			{
				this.apiService.setMessage({ annual: this.getQuoteForm.get('calculatePremium').get('paymentMode').value, amount: this.calPay() });
				this.session.set('getinnerForm',
					{ annual: this.getQuoteForm.get('calculatePremium').get('paymentMode').value, amount: this.calPay() });
			}
			else
			{
				const getQuoteFormData1 = this.session.get('getinnerForm');
				//const getQuoteFormData1 = this.session.get("getinnerForm");
				if(!getQuoteFormData1)
				{
					this.apiService.setMessage({ annual: this.getQuoteForm.get('calculatePremium').get('paymentMode').value, amount: 0 });
					this.session.set('getinnerForm',
						{ annual: this.getQuoteForm.get('calculatePremium').get('paymentMode').value, amount: this.calPay() });
					//this.session.set("getinnerForm",{ annual: this.getQuoteForm.get('calculatePremium').get('paymentMode').value, amount: this.calPay() });
				}
			}
		});

		this.showSecondStep=this.getQuoteForm.get('calculatePremium').valid===true?true:false;
		this.showThirdStep=this.getQuoteForm.get('basicInformation').valid===true?true:false;
		this.getAge(this.getQuoteForm.get('calculatePremium').get('dateofbirth').value);
		this.ngxService.stop();
	}

	getcall()
	{
		//alert("ss");
	}

	calPay()
	{
		const plans = this.plan;

		if (this.getQuoteForm.get('calculatePremium').get('totalCashBenefit').value == '1800000')
			this.plan = "plan_50";
		else if (this.getQuoteForm.get('calculatePremium').get('totalCashBenefit').value == '1080000')
			this.plan = "plan_30";
		else
			this.plan = this.plan;

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
		console.log(getQuoteFormData);
		this.getQuoteForm = this.formBuilder.group
		({
			calculatePremium: this.addCalculatePremium(getQuoteFormData?.calculatePremium ? getQuoteFormData.calculatePremium : ''),
			basicInformation: this.addBasicForm(getQuoteFormData?.basicInformation ? getQuoteFormData.basicInformation: ''),
			healthCondition: this.addHealthCondition(getQuoteFormData?.healthCondition ? getQuoteFormData.healthCondition : '')
		});
	}

	addCalculatePremium(data)
	{
		const plan = this.plan === 'plan_30' ? '1080000' : this.plan === 'plan_50' ? '1800000' : '';
		this.apiService.setMessage({ annual: data === '' ? '' : data.paymentMode, amount: '0.00' });
		return this.formBuilder.group
		({
			totalCashBenefit: new FormControl((data === '' ? plan : data.totalCashBenefit) || '', Validators.required),
			dateofbirth: new FormControl(data.dateofbirth || '', Validators.required),
			gender: new FormControl(data.gender || '', Validators.required),
			paymentMode: new FormControl(data.paymentMode || '', Validators.required),
		});
	}

	setShowSecond()
	{
		this.showSecondStep=true;
		this.facebookPixelService.track('BasicInformation');
		setTimeout(function ()
		{
			document.querySelector('#step2_head').scrollIntoView
			({
				behavior: 'smooth'
			});
		}, 100);
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

	setShowThird()
	{
		this.showThirdStep=true;
		this.facebookPixelService.track('HealthCondition');
		setTimeout(function ()
		{
			document.querySelector('#step3_head').scrollIntoView
			({
				behavior: 'smooth'
			});
		}, 100);
	}

	addBasicForm(data)
	{
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
			/*email: new FormControl(data.email || '', [Validators.required, Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$")]),*/
			email: new FormControl(data.email || '', [Validators.required, Validators.pattern(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/)])
		});
	}

	addHealthCondition(data)
	{
		return this.formBuilder.group
		({
			healthCondition1: new FormControl(data.healthCondition1 || '', Validators.required),
			healthCondition2: new FormControl(data.healthCondition2 || '', Validators.required),
			healthCondition3: new FormControl(data.healthCondition3 || '', Validators.required),
			privacyPolicy: new FormControl(data.privacyPolicy || '', Validators.required)
		});
	}

	submitQuoteForm()
	{
		var dd = this.getQuoteForm.value.country;
		this.submitted = true;
		this.facebookPixelService.track('Lead');

		// TODO: Move eligibility checking on server
		if (this.getQuoteForm.valid)
		{
			this.session.set('getQuoteForm', this.getQuoteForm.value);
			//this.session.set("getQuoteForm", JSON.stringify(this.getQuoteForm.value));
			const healthCondition = this.getQuoteForm.get('healthCondition').value;

			if (this.getQuoteForm.get('calculatePremium').get('paymentMode').value == 'Monthly')
				this.amount = parseFloat(this.amount.toString().replace(/,/g, '')) * 12;
			else
				this.amount = parseFloat(this.amount.toString().replace(/,/g, ''));

			this.saveQuoteForm
			(
				!(
					(
						healthCondition.healthCondition1 === 'Yes'
						|| healthCondition.healthCondition2 === 'Yes'
						|| healthCondition.healthCondition3 === 'Yes'
					)
					|| this.getQuoteForm.value.basicInformation.country !== '170'
					|| this.amount >= parseFloat(("50,000").replace(/,/g, ''))
				)
			);
			// if ((healthCondition.healthCondition1 === 'Yes' || healthCondition.healthCondition2 === 'Yes'
			// 	|| (this.plan === 'plan_30' && this.calulatedAge>59) || (this.plan !== 'plan_30' && this.calulatedAge>50)
			// 	|| healthCondition.healthCondition3 === 'Yes') || this.getQuoteForm.value.basicInformation.country!=='170'
			// 	||parseFloat(this.amount.toString().replace(/,/g, ''))>=parseFloat(("50,000").replace(/,/g, '')))
			// {
			// 	this.router.navigate(['Ineligible']);
			// 	location.href = "ineligible.html";
			// }
			// else
			// {
			// 	this.router.navigate(['apply']);
			// }
		}
		else
		{
			//this.submitted = false;
		}
		return;
	}

	saveQuoteForm(isEligible: boolean)
	{
		const health = this.getQuoteForm.get('healthCondition').value;
		const basicInfo  = this.getQuoteForm.get('basicInformation');
		const calcInfo = this.getQuoteForm.get('calculatePremium');

		let country = this.getReferenceDataName(CONSTANTS.COUNTRY, basicInfo.get('country'));
		let region: string = null;
		let city: string = null;
		if(country == 'PHILIPPINES')
		{
			let regionList = this.getReferenceData(CONSTANTS.PROVIANCE, basicInfo.get('province'));
			region = String(regionList.Province).toUpperCase();
			city = this.getReferenceDataName(regionList.Municipality, basicInfo.get('municipality'));
		}

		let plan = this.plan.replace('_', ' ').toUpperCase();

		var data: any =
		{
			"PlanCode": plan,
			"PlanName": plan,
			"ProductFaceAmount": this.calPay(),
			"PaymentFrequency": calcInfo.get('paymentMode').value,

			"NamePrefix": this.getReferenceDataName(CONSTANTS.PREFIX, basicInfo.get('prefix')),
			"FirstName": this.nullIfEmpty(basicInfo.get('fname').value),
			"MiddleName": this.nullIfEmpty(basicInfo.get('mname').value),
			"LastName": this.nullIfEmpty(basicInfo.get('lname').value),

			"Gender": this.getReferenceDataName(CONSTANTS.GENDER, calcInfo.get('gender')),
			"BirthDate": calcInfo.get('dateofbirth').value,

			"EmailAddress": this.nullIfEmpty(String(basicInfo.get('email').value).toLowerCase()),
			"MobileNumber": this.nullIfEmpty(basicInfo.get('mobile').value),
			"PhoneNumber": this.nullIfEmpty(basicInfo.get('landline').value),
			"Country": country,
			"Region": region,
			"City": city,

			"ReferralSource": this.getReferenceDataName(CONSTANTS.PRIME_CARE, basicInfo.get('primeCare')),
			"AgentCode": basicInfo.get('acode').value,
			"AgentFirstName": basicInfo.get('afname').value,
			"AgentLastName": basicInfo.get('alname').value,

			"Health1": (health.healthCondition1 == 'Yes'),
			"Health2": (health.healthCondition2 == 'Yes'),
			"Health3": (health.healthCondition3 == 'Yes'),

			"IsEligible": isEligible
		};

		if(basicInfo.get('suffix').value !== '1')
			data.NameSuffix = this.getReferenceDataName(CONSTANTS.SUFFIX, basicInfo.get('suffix'));

		let headers: HttpHeaders = new HttpHeaders();
		headers = headers.append('Content-Type', 'application/json');
		//headers = headers.append('Ocp-Apim-Subscription-Key', environment.primeCareApi.subscriptionKey);

		let options =
		{
			headers: headers,
			params: new HttpParams()
		};

		let body = JSON.stringify(data);
		let endpoint = environment.appApi.host + environment.primeCareApi.quoteEndpoint;

		this.ngxService.start();

		// LOG FOR DEBUGGING
		//console.log(`Posting to ${endpoint}`);
		this.session.set(StorageType.QUOTE_PC_DATA, data);

		// if(isEligible) {
		// 	this.router.navigate(['/prime-care/apply']);
		// } else {
		// 	this.router.navigate(['/prime-care/ineligible']);
		// }

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
			.subscribe((data: any) =>
			{
				let refNo = String(data.id).padStart(10, '0');
				this.session.set('refNo', refNo)
				if(isEligible)
				{
					this.facebookPixelService.track('Lead');
					this.router.navigate(['/prime-care/apply']);
				}
				else
				{
					this.router.navigate(['/prime-care/ineligible']);
				}
			});
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
}
