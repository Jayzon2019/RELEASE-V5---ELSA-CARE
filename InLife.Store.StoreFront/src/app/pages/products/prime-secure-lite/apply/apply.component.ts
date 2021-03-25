import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Router } from '@angular/router';
import { ViewportScroller } from '@angular/common';
import { Location } from '@angular/common';
import { FormBuilder, FormControl, FormGroup, Validators, FormArray } from '@angular/forms';
import { MomentDateAdapter, MAT_MOMENT_DATE_ADAPTER_OPTIONS } from '@angular/material-moment-adapter';
import { MatDatepicker } from '@angular/material/datepicker';
import { NgxUiLoaderService } from 'ngx-ui-loader';

import { jsPDF } from 'jspdf';

import { CONSTANTS } from '@app/services/constants';
import { Enumerations } from '@app/common/enumerations';
import { ApiService, SessionStorageService } from '@app/services';
import { DynamicGrid } from './extension.model';

@Component
({
	selector: 'app-apply',
	templateUrl: './apply.component.html',
	styleUrls: ['./apply.component.css','./apply.component.scss'],
	providers: []
})
export class ApplyComponent implements OnInit
{

	@ViewChild('fileInput') fileinput: ElementRef;
	getApplyForm: FormGroup;
	getQuoteFormData: any;
	submitted: boolean = false;
	CONSTANTS = CONSTANTS;
	Enumerations = Enumerations;
	public imagePath;
	imgURL: any;
	filePreview: any;
	muncipalitys: any;
	imageSrc: string;
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

	constructor
	(
		private router: Router,
		private location: Location,
		private formBuilder: FormBuilder,
		private apiService: ApiService,
		private session: SessionStorageService,
		private ngxService: NgxUiLoaderService,
		private vps: ViewportScroller
	)
	{
		this.ngxService.start();
		this.getQuoteFormData = this.session.get("getQuoteForm") || "";
		const getApplyFormData = this.session.get("getApplyForm") || "";

		this.insuredIdentityDocumentImagePreview = this.session.get("insuredIdentityDocumentImagePreview");
		this.insuredIdentityDocumentImageData = this.session.get("insuredIdentityDocumentImageData");
		if (this.insuredIdentityDocumentImageData)
		{
			this.hasImage = true;
		}
		this.initForm(getApplyFormData);
	}

	dynamicArray: Array<DynamicGrid> = [];
	newDynamic: any = {};

	ngOnInit(): void
	{
		const extension: Array<any> = this.session.get("extensionData");

		if (extension) {
			if (extension.length > 0) {
				extension.forEach((value, index) => {
					this.dynamicArray.push(extension[index]);
				});
			}
		} else {
			this.newDynamic = { CompanyName: "", LifeFaceAmount: "", DreadDiseaseFaceAmount: "", AccidentalFaceAmount: "", IssueYear: "" };
			this.dynamicArray.push(this.newDynamic);
		}

		if (this.getApplyForm.get('personalInformation').get('province').value)
		{
			this.onChangeProviance(this.getApplyForm.get('personalInformation').get('province').value);
		}

		this.getApplyForm.get('personalInformation').get('province').enable();
		this.getApplyForm.get('personalInformation').get('municipality').enable();
		this.getApplyForm.get('personalInformation').get('province').setValidators([Validators.required]);
		this.getApplyForm.get('personalInformation').get('municipality').setValidators([Validators.required]);

		if (this.getApplyForm.get('personalInformation').get('birthCountry').value == '170')
		{
			if (this.getApplyForm.get('personalInformation').get('birthProvince').value)
			{
				this.onChangeProviances(this.getApplyForm.get('personalInformation').get('birthProvince').value);
			}
			this.getApplyForm.get('personalInformation').get('birthProvince').enable();
			this.getApplyForm.get('personalInformation').get('birthMunicipality').enable();
			this.getApplyForm.get('personalInformation').get('birthProvince').setValidators([Validators.required]);
			this.getApplyForm.get('personalInformation').get('birthMunicipality').setValidators([Validators.required]);

		} else
		{
			this.getApplyForm.get('personalInformation').get('birthProvince').disable();
			this.getApplyForm.get('personalInformation').get('birthMunicipality').disable();
		}
		this.getApplyForm.get('personalInformation').updateValueAndValidity();

		this.getApplyForm.get('identification').get('secondaryLegalIdType').valueChanges.subscribe(
			result => {
				let el = document.getElementById("secondaryLegalIdType");

				if (el && result !== '') {
					el.classList.add('black');
				} else {
					el.classList.remove('black');
				}
			});

		this.getApplyForm.get('declarationForm').get('declaration').valueChanges.subscribe(
			result =>
			{
				if (!result)
					this.getApplyForm.get('declarationForm').get('declaration').setValue(null);
			}
		);

		this.getApplyForm.get('declarationForm').get('confirmdeclare').valueChanges.subscribe(
			result =>
			{
				if (!result)
					this.getApplyForm.get('declarationForm').get('confirmdeclare').setValue(null);
			}
		);

		this.getApplyForm.get('declarationForm').get('antimoneylaundary').valueChanges.subscribe(
			result =>
			{
				if (result === false)
				{
					this.getApplyForm.get('declarationForm').get('antimoneylaundary').setValue(null);
				}
			});
		
			this.getApplyForm.get('declarationForm').get('obligations').valueChanges.subscribe(
				result =>
				{
					if (result === false)
					{
						this.getApplyForm.get('declarationForm').get('obligations').setValue(null);
					}
				});

		this.getApplyForm.get('declarationForm').get('confirmcorrect').valueChanges.subscribe(
			result =>
			{
				if (result === false)
				{
					this.getApplyForm.get('declarationForm').get('confirmcorrect').setValue(null);
				}
			});

		this.getApplyForm.get('declarationForm').get('submitedPhilphine').valueChanges.subscribe(
			result =>
			{
				if (result === false)
				{
					this.getApplyForm.get('declarationForm').get('submitedPhilphine').setValue(null);
				}
			});

		this.getApplyForm.get('declarationForm').get('informproduct').valueChanges.subscribe(
			result =>
			{
				if (result === false)
				{
					this.getApplyForm.get('declarationForm').get('informproduct').setValue(null);
				}
			});


		this.getApplyForm.get('identification').get('secondaryLegalIdType').valueChanges.pipe().subscribe(data => {
			if(data) {
				this.getApplyForm.get('identification').get('secondaryLegalIdNumber').setValidators([Validators.pattern("[0-9]{0,11}"), Validators.required]);
				this.getApplyForm.get('identification').get('secondaryLegalIdNumber').updateValueAndValidity();
			} else {
				this.getApplyForm.get('identification').get('secondaryLegalIdNumber').clearValidators();
				this.getApplyForm.get('identification').get('secondaryLegalIdNumber').updateValueAndValidity();
			}
		});

		//this.getApplyForm.get('employment').get('occupation').valueChanges.subscribe(
		//	result =>
		//	{
		//		if (result == '1')
		//		{
		//			this.getApplyForm.get('employment').get('company').disable();
		//			this.getApplyForm.get('employment').get('monthlyIncome').disable();

		//		} else
		//		{

		//			this.getApplyForm.get('employment').get('company').enable();
		//			this.getApplyForm.get('employment').get('monthlyIncome').enable();
		//			this.getApplyForm.get('employment').get('company').setValidators([Validators.required]);
		//			this.getApplyForm.get('employment').get('monthlyIncome').setValidators([Validators.required]);

		//		}
		//		this.getApplyForm.get('employment').updateValueAndValidity();
		//	});


		this.getApplyForm.get('personalInformation').get('nationality').valueChanges.subscribe(
			result =>
			{
				if (result !== '170')
				{
					this.getApplyForm.get('personalInformation').get('nationality').setValue('170');
				}
			});

		//this.getApplyForm.get('personalInformation').get('country').valueChanges.subscribe(
		//	result =>
		//	{
		//		if (result == '170')
		//		{
		//			this.getApplyForm.get('personalInformation').get('province').enable();
		//			this.getApplyForm.get('personalInformation').get('municipality').enable();
		//			this.getApplyForm.get('personalInformation').get('province').setValidators([Validators.required]);
		//			this.getApplyForm.get('personalInformation').get('municipality').setValidators([Validators.required]);
		//		} else
		//		{
		//			this.getApplyForm.get('personalInformation').get('province').disable();
		//			this.getApplyForm.get('personalInformation').get('municipality').disable();
		//			this.getApplyForm.get('personalInformation').get('country').setValue('170');
		//		}
		//		this.getApplyForm.get('personalInformation').updateValueAndValidity();
		//	});

		this.getApplyForm.get('personalInformation').get('birthCountry').valueChanges.subscribe(
			result =>
			{
				if (result == '170')
				{
					this.getApplyForm.get('personalInformation').get('birthProvince').enable();
					this.getApplyForm.get('personalInformation').get('birthMunicipality').enable();
					this.getApplyForm.get('personalInformation').get('birthProvince').setValidators([Validators.required]);
					this.getApplyForm.get('personalInformation').get('birthMunicipality').setValidators([Validators.required]);
				} else
				{
					this.getApplyForm.get('personalInformation').get('birthProvince').disable();
					this.getApplyForm.get('personalInformation').get('birthMunicipality').disable();
				}
				this.getApplyForm.get('personalInformation').updateValueAndValidity();
			});

		if (this.getApplyForm.get('beneficiaryDetails').get('insuredBirthCountry').value == '170')
		{
			if (this.getApplyForm.get('beneficiaryDetails').get('insuredBirthProvince').value)
			{
				this.onChangeBeneficiaryProviances(this.getApplyForm.get('beneficiaryDetails').get('insuredBirthProvince').value);
			}
			this.getApplyForm.get('beneficiaryDetails').get('insuredBirthProvince').enable();
			this.getApplyForm.get('beneficiaryDetails').get('insuredBirthMunicipality').enable();
			this.getApplyForm.get('beneficiaryDetails').get('insuredBirthProvince').setValidators([Validators.required]);
			//this.getApplyForm.get('beneficiaryDetails').get('insuredBirthMunicipality').setValidators([Validators.required]);

		} else
		{
			this.getApplyForm.get('beneficiaryDetails').get('insuredBirthProvince').disable();
			this.getApplyForm.get('beneficiaryDetails').get('insuredBirthMunicipality').disable();
		}

		this.getApplyForm.get('beneficiaryDetails').get('insuredBirthCountry').valueChanges.subscribe(
			result =>
			{
				if (result == '170')
				{
					this.getApplyForm.get('beneficiaryDetails').get('insuredBirthProvince').enable();
					this.getApplyForm.get('beneficiaryDetails').get('insuredBirthMunicipality').enable();
					this.getApplyForm.get('beneficiaryDetails').get('insuredBirthProvince').setValidators([Validators.required]);
					this.getApplyForm.get('beneficiaryDetails').get('insuredBirthMunicipality').setValidators([Validators.required]);
				} else
				{
					this.getApplyForm.get('beneficiaryDetails').get('insuredBirthProvince').disable();
					this.getApplyForm.get('beneficiaryDetails').get('insuredBirthMunicipality').disable();
				}
				this.getApplyForm.get('beneficiaryDetails').updateValueAndValidity();
			});

		//if (this.getApplyForm.get('beneficiaryDetails').get('insuredCountry').value == '170')
		//{
			if (this.getApplyForm.get('beneficiaryDetails').get('insuredProvince').value)
			{
				this.onChangeBeneficiaryProviance(this.getApplyForm.get('beneficiaryDetails').get('insuredProvince').value);
			}
			this.getApplyForm.get('beneficiaryDetails').get('insuredProvince').enable();
			this.getApplyForm.get('beneficiaryDetails').get('insuredMunicipality').enable();
			this.getApplyForm.get('beneficiaryDetails').get('insuredProvince').setValidators([Validators.required]);
			this.getApplyForm.get('beneficiaryDetails').get('insuredMunicipality').setValidators([Validators.required]);

		//} else
		//{
		//	this.getApplyForm.get('beneficiaryDetails').get('insuredBirthProvince').disable();
		//	this.getApplyForm.get('beneficiaryDetails').get('insuredBirthMunicipality').disable();
		//}

		//this.getApplyForm.get('beneficiaryDetails').get('insuredCountry').valueChanges.subscribe(
		//	result =>
		//	{
		//		if (result == '170')
		//		{
		//			this.getApplyForm.get('beneficiaryDetails').get('insuredProvince').enable();
		//			this.getApplyForm.get('beneficiaryDetails').get('insuredMunicipality').enable();
		//			this.getApplyForm.get('beneficiaryDetails').get('insuredProvince').setValidators([Validators.required]);
		//			this.getApplyForm.get('beneficiaryDetails').get('insuredMunicipality').setValidators([Validators.required]);
		//		} else
		//		{
		//			this.getApplyForm.get('beneficiaryDetails').get('insuredProvince').disable();
		//			this.getApplyForm.get('beneficiaryDetails').get('insuredMunicipality').disable();
		//		}
		//		this.getApplyForm.get('beneficiaryDetails').updateValueAndValidity();
		//	});


		//this.showSecondStep = this.getApplyForm.get('personalInformation').valid;
		this.showThirdStep = this.getApplyForm.get('personalInformation').valid;
		this.showFourthStep = this.getApplyForm.get('identification').valid;
		this.showFifthStep = this.getApplyForm.get('beneficiaryDetails').valid;

		this.ngxService.stop();
	}
	

	initForm(getApplyFormData)
	{
		this.getApplyForm = this.formBuilder.group({
			personalInformation: this.addPersonalInformation(getApplyFormData.personalInformation || ''),
			//employment: this.addEmployment(getApplyFormData.employment || ''),
			identification: this.addIdentification(getApplyFormData.identification || ''),
			beneficiaryDetails: this.addBeneficiaryDetails(getApplyFormData.beneficiaryDetails || ''),
			declarationForm: this.addFinalDeclaration(getApplyFormData.declarationForm || ''),
		});
	}
	//setShowSecond()
	//{
	//	this.showSecondStep = true;

	//	setTimeout(function ()
	//	{
	//		document.querySelector('#step2_head').scrollIntoView({
	//			behavior: 'smooth'
	//		});
	//	}, 100);
	//}

	setShowThird()
	{
		this.showThirdStep = true;
		setTimeout(function ()
		{
			document.querySelector('#step3_head').scrollIntoView({
				behavior: 'smooth'
			});
		}, 100);
	}

	setShowFourth()
	{
		this.showFourthStep = true;
		setTimeout(function ()
		{
			document.querySelector('#step4_head').scrollIntoView({
				behavior: 'smooth'
			});
		}, 100);
	}


	setShowFifth()
	{
		this.showFifthStep = true;
		setTimeout(function ()
		{
			document.querySelector('#step5_head').scrollIntoView({
				behavior: 'smooth'
			});
		}, 100);
	}

	addFinalDeclaration(data)
	{
		return this.formBuilder.group({
			declaration: new FormControl(data.declaration || '', Validators.required),
			uslawpersion: new FormControl(data.uslawpersion || '', Validators.required),
			usnotlaw: new FormControl(data.usnotlaw || '', Validators.required),
			confirmdeclare: new FormControl(data.confirmdeclare || '', Validators.required),

			changeexstinginsurance: new FormControl(data.changeexstinginsurance || '', Validators.required),
			premiumpaid: new FormControl(data.premiumpaid || '', Validators.required),
			submitedPhilphine: new FormControl(data.submitedPhilphine || '', Validators.required),
			informproduct: new FormControl(data.informproduct || '', Validators.required),
			confirmcorrect: new FormControl(data.confirmcorrect || '', Validators.required),
			antimoneylaundary: new FormControl(data.antimoneylaundary || '', Validators.required),
			obligations: new FormControl(data.obligations || '', Validators.required),

			companyname: new FormControl(data.companyname || ''),
			basiccover: new FormControl(data.basiccover || ''),
			dreaddeaise: new FormControl(data.dreaddeaise || ''),
			accidentrider: new FormControl(data.accidentrider || ''),
			yearofissue: new FormControl(data.yearofissue || ''),

		});
	}

	addPersonalInformation(data)
	{
		return this.formBuilder.group({
			street: new FormControl(data.street || '', Validators.required),
			village: new FormControl(data.village || '', Validators.required),
			barangay: new FormControl(data.barangay || '', Validators.required),
			province: new FormControl(data.province || (this.getQuoteFormData != "" ? this.getQuoteFormData.basicInformation.province : ''), Validators.required),
			municipality: new FormControl(data.municipality || (this.getQuoteFormData != "" ? this.getQuoteFormData.basicInformation.municipality : ''), Validators.required),
			zipCode: new FormControl(data.zipCode || '', Validators.required),
			civilStatus: new FormControl(data.civilStatus || '', Validators.required),
			//country: new FormControl(data.country || '170', Validators.required),
			nationality: new FormControl(data.nationality || '170', Validators.required),
			birthCountry: new FormControl(data.birthCountry || '170', Validators.required),
			birthProvince: new FormControl(data.birthProvince || '', Validators.required),
			birthMunicipality: new FormControl(data.birthMunicipality || '', Validators.required),
		});
	}

	//addEmployment(data)
	//{
	//	return this.formBuilder.group({
	//		company: new FormControl(data.company || ''/*this.getQuoteFormData.employment.company*/, Validators.required),
	//		occupation: new FormControl(data.occupation || ''/*this.getQuoteFormData.employment.occupation*/, Validators.required),
	//		fundSource: new FormControl(data.fundSource || ''/*this.getQuoteFormData.employment.fundSource*/, Validators.required),
	//		monthlyIncome: new FormControl(data.monthlyIncome || ''/*this.getQuoteFormData.employment.monthlyIncome*/, Validators.required),
	//	});
	//}

	addIdentification(data)
	{
		if (data.file !== '' && data !== null && data.file !== undefined)
		{
			this.files.push(data.file.split('\\')[2]);
		}
		else
		{
			this.files = [];
		}

		return this.formBuilder.group({
			legalIdType: new FormControl(data.legalIdType || '', Validators.required),
			LegalIdNumber: new FormControl(data.LegalIdNumber || '', Validators.required),
			secondaryLegalIdType: new FormControl(data.secondaryLegalIdType || ''),
			secondaryLegalIdNumber: new FormControl(data.secondaryLegalIdNumber || '', [Validators.pattern("[0-9]{0,11}")]),
			file: new FormControl(data.file || '', Validators.required)
		});
	}

	addBeneficiaryDetails(data)
	{
		return this.formBuilder.group({
			relation: new FormControl(data.relation || '', Validators.required),
			prefix: new FormControl(data.prefix || '', Validators.required),
			fname: new FormControl(data.fname || '', Validators.required),
			mname: new FormControl(data.mname || ''),
			lname: new FormControl(data.lname || '', Validators.required),
			suffix: new FormControl(data.suffix || '', Validators.required),
			insuredStreet: new FormControl(data.insuredStreet || '', Validators.required),
			sameAddress: new FormControl(data.sameAddress || ''),
			insuredVillage: new FormControl(data.insuredVillage || '', Validators.required),
			insuredZipCode: new FormControl(data.insuredZipCode || '', Validators.required),
			insuredBarangay: new FormControl(data.insuredBarangay || '', Validators.required),
			insuredProvince: new FormControl(data.insuredProvince || '', Validators.required),
			insuredMunicipality: new FormControl(data.insuredMunicipality || '', Validators.required),
			insuredBirthProvince: new FormControl(data.insuredBirthProvince || '', Validators.required),
			insuredBirthMunicipality: new FormControl(data.insuredBirthMunicipality || '', Validators.required),
			insuredBirthCountry: new FormControl(data.insuredBirthCountry || '170', Validators.required),
			insuredNationality: new FormControl(data.insuredNationality || '170', Validators.required),
			insuredLandline: new FormControl(data.insuredLandline || '', [Validators.pattern("^[0-9]{9}$")]),
			insuredMobile: new FormControl(data.insuredMobile || '', [Validators.required, Validators.pattern("^[1-9]{1}[0-9]{9}$")]),
			insuredCivilStatus: new FormControl(data.insuredCivilStatus || '', Validators.required),
			insuredGender: new FormControl(data.insuredGender || '', Validators.required),
			insuredDateofbirth: new FormControl(data.insuredDateofbirth || '', Validators.required),
			designation: new FormControl(data.designation || 'P', Validators.required),
			type: new FormControl(data.type || 'O', Validators.required),
		});
	}

	addDeclarations(data)
	{
		// declaration,uslawpersion,usnotlaw,confirmdeclare,changeexstinginsurance,premiumpaid,antimoneylaundary,confirmcorrect,
		// informproduct
		return this.formBuilder.group({
			company: new FormControl(data.company || '', Validators.required),
			occupation: new FormControl(data.occupation || '', Validators.required)
		});
	}

	submitApplyForm()
	{
		this.submitted = true;
		if (this.getApplyForm.valid)
		{
			this.session.set("getApplyForm", this.getApplyForm.value);
			this.session.set("extensionData", this.dynamicArray);
			this.session.set("insuredIdentityDocumentImageData", this.insuredIdentityDocumentImageData);
			this.session.set("insuredIdentityDocumentImagePreview", this.insuredIdentityDocumentImagePreview);

			this.pay();

			//const getApplyFormData = this.session.get("getApplyForm") || "[]";
			//const getQuoteForm = this.session.get("getQuoteForm") || "[]";

			//const bookData = {} as any;
			//bookData.InsuredPrefixId = 0;
			//bookData.InsuredFirstName = 0;
			//bookData.InsuredMiddleName = 0;
			//bookData.InsuredLastName = 0;
			//bookData.InsuredSuffixId = 0;
			//bookData.InsuredBirthday = 0;
			//bookData.InsuredGenderId = 0;
			//bookData.InsuredEmailAddress = 0;
			//bookData.InsuredResidencePhoneNumber = 0;
			//bookData.InsuredMobileNo = 0;
			//bookData.OwnerIsInsured = true;
			//bookData.OwnerRelationToInsuredId = 0;
			//bookData.OwnerPrefixId = 0;
			//bookData.OwnerFirstName = 0;
			//bookData.OwnerMiddleName = 0;
			//bookData.OwnerLastName = 0;
			//bookData.OwnerSuffixId = 0;
			//bookData.OwnerBirthday = 0;
			//bookData.OwnerGenderId = 0;
			//bookData.OwnerEmailAddress = 0;
			//bookData.OwnerResidencePhoneNumber = 0;
			//bookData.InsuredSuffixId = 0;
			//bookData.InsuredBirthday = 0;
			//bookData.InsuredGenderId = 0;
			//bookData.InsuredEmailAddress = 0;
			//bookData.InsuredResidencePhoneNumber = 0;
			//bookData.InsuredMobileNo = 0;
			//bookData.OwnerIsInsured = true;
			//bookData.OwnerRelationToInsuredId = 0;
			//bookData.OwnerPrefixId = 0;
			//bookData.OwnerFirstName = 0;
			//bookData.OwnerMiddleName = 0;
			//bookData.OwnerLastName = 0;
			//bookData.OwnerSuffixId = 0;
			//bookData.OwnerBirthday = 0;
			//bookData.OwnerGenderId = 0;
			//bookData.OwnerEmailAddress = 0;
			//bookData.OwnerResidencePhoneNumber = 0;
			//bookData.OwnerMobileNo = 0;
			//bookData.OwnerMobileNo = 0;
			//bookData.PlanName = 0;
			//bookData.PaymentMode = 0;
			//bookData.FaceAmount = 0;
			//bookData.Premium = 0;
			//bookData.Health1 = 0;
			//bookData.Health2 = 0;
			//bookData.Health3 = 0;
			//bookData.InsuredResidenceAddress1 = 0;
			//bookData.InsuredResidenceAddress2 = 0;
			//bookData.InsuredResidenceAddress3 = 0;
			//bookData.InsuredResidenceMunicipalityId = 0;
			//bookData.InsuredResidenceProvinceId = 0;
			//bookData.InsuredResidenceZipCode = 0;
			//bookData.Fatca1 = 0;
			//bookData.Fatca2 = 0;
			//bookData.InsuredCitizenshipId = 0;
			//bookData.InsuredCivilStatusId = 0;
			//bookData.InsuredPrimaryOccupationCompanyName = 0;
			//bookData.InsuredPrimaryOccupationMonthlyIncome = 0;
			//bookData.InsuredFundSourceId = 0;
			//bookData.InsuredPreferredMailingAddress = 0;
			//bookData.InsuredPrimaryOccupationAddress1 = 0;
			//bookData.InsuredPrimaryOccupationAddress2 = 0;
			//bookData.InsuredPrimaryOccupationAddress3 = 0;
			//bookData.InsuredPrimaryOccupationZipCode = 0;
			//bookData.InsuredPrimaryOccupationProvinceId = 0;
			//bookData.InsuredPrimaryOccupationMunicipalityId = 0;
			//bookData.InsuredOfficePhoneNumber = 0;
			//bookData.InsuredTinNo = 0;
			//bookData.InsuredOtherIdNoType = 0;
			//bookData.InsuredOtherIdNo = 0;
			//bookData.Question1 = 0;
			//bookData.Question2 = 0;
			//const beneficiary = {} as any;
			//bookData.FirstName = 0;
			//bookData.MiddleName = 0;
			//bookData.LastName = 0;
			//bookData.SuffixId = 0;
			//bookData.AddressType = 0;
			//bookData.Address1 = 0;
			//bookData.Address2 = 0;
			//bookData.Address3 = 0;
			//bookData.ProvinceId = 0;
			//bookData.MunicipalityId = 0;
			//bookData.ZipCode = 0;
			//bookData.LandLineNumber = 0;
			//bookData.CivilStatusId = 0;
			//bookData.GenderId = 0;
			//bookData.Birthday = 0;
			//bookData.RelationToInsuredId = 0;
			//bookData.Priority = 0;
			//bookData.Right = 0;
			//bookData.Beneficiary = [beneficiary];

			//const existingOtherInsurance = {} as any;
			//bookData.Id = 0;
			//bookData.InsuredId = 0;
			//bookData.CompanyName = 0;
			//bookData.LifeFaceAmount = 0;
			//bookData.DreadDiseaseFaceAmount = 0;
			//bookData.AccidentalFaceAmount = 0;
			//bookData.IssueYear = 0;
			//bookData.ExistingOtherInsurance = [existingOtherInsurance];

			//bookData.RefFirstName = 0;
			//bookData.RefLastName = 0;

			// const paymentUrl = "https://beta2.insularlife.com.ph/CustomerPortal/Customer/E-Payment/ILPay.ashx?RefNo=0000000012&&Amount=246075&&RetURL=http://localhost:4200/apply";
			// window.open(paymentUrl, '_self');

			// this.apiService.postRequest('OAapi/OnlineStore/CreateApplication', bookData).subscribe((data: any) => {
			//   this.submitted = false;
			//   console.log(data);
			//   //this.inquiryForm.reset();
			//   //this.router.navigate(['compare-mortages']);
			// }, (err) => {
			//   console.log(err);
			//   if (err && err.hasOwnProperty('error')) {
			//     //this.CommonService.errorMsg(err.error.message);
			//   } else {
			//     //this.CommonService.errorMsg(CONSTANTS.GLOBAL_ERROR_MSG);
			//   }
			//   this.submitted = false;
			// });


			//thank-you.navigate(['apply']);
		}
		else
		{
			//this.submitted = false;
		}
		return;
	}

	backClicked()
	{
		this.session.set("getApplyForm",this.getApplyForm.value);
		this.router.navigate(['prime-secure-lite/quote']);
	}

	/* START: For Personal information */
	onChangeProviance(value)
	{
		//console.log(value);
		for (let i = 0; i < CONSTANTS.PROVIANCE.length; i++)
		{
			if (CONSTANTS.PROVIANCE[i].id == value)
			{
				this.muncipality = CONSTANTS.PROVIANCE[i].Municipality;

			}

		}
	}

	onChangeProviances(value)
	{
		//console.log(value);
		for (let i = 0; i < CONSTANTS.PROVIANCE.length; i++)
		{
			if (CONSTANTS.PROVIANCE[i].id == value)
			{
				this.muncipalitys = CONSTANTS.PROVIANCE[i].Municipality;
			}

		}
	}
	/* END: For Personal information */


	/* START: For Personal information */
	onChangeBeneficiaryProviance(value)
	{
		for (let i = 0; i < CONSTANTS.PROVIANCE.length; i++)
		{
			if (CONSTANTS.PROVIANCE[i].id == value)
			{
				this.beneficiaryMuncipality = CONSTANTS.PROVIANCE[i].Municipality;
			}

		}
	}

	onChangeBeneficiaryProviances(value)
	{
		//console.log(value);
		for (let i = 0; i < CONSTANTS.PROVIANCE.length; i++)
		{
			if (CONSTANTS.PROVIANCE[i].id == value)
			{
				this.beneficiaryMuncipalities = CONSTANTS.PROVIANCE[i].Municipality;
			}

		}
	}
	/* END: For Personal information */


	setSameAddress(data)
	{
		const personalInformation = this.getApplyForm.get('personalInformation').value;
		this.showCorrespondingAddress = !data.target.checked;

		// this.getApplyForm.get('beneficiaryDetails').setValue({'insuredCountry':personalInformation.country});
		if (data.target.checked)
		{
			this.beneficiaryMuncipality = this.muncipality;
			this.getApplyForm.get('beneficiaryDetails').value.insuredVillage = personalInformation.village;
			this.getApplyForm.get('beneficiaryDetails').value.insuredStreet = personalInformation.street;
			this.getApplyForm.get('beneficiaryDetails').value.insuredProvince = personalInformation.province;
			this.getApplyForm.get('beneficiaryDetails').value.insuredMunicipality = personalInformation.municipality;
			this.getApplyForm.get('beneficiaryDetails').value.insuredBarangay = personalInformation.barangay;
			this.getApplyForm.get('beneficiaryDetails').value.insuredZipCode = personalInformation.zipCode;
		}
		else
		{
			this.beneficiaryMuncipality = [];
			this.getApplyForm.get('beneficiaryDetails').value.insuredVillage = '';
			this.getApplyForm.get('beneficiaryDetails').value.insuredStreet = '';
			this.getApplyForm.get('beneficiaryDetails').value.insuredProvince = '';
			this.getApplyForm.get('beneficiaryDetails').value.insuredMunicipality = '';
			this.getApplyForm.get('beneficiaryDetails').value.insuredBarangay = '';
			this.getApplyForm.get('beneficiaryDetails').value.insuredZipCode = '';
		}

		const beneficiaryInformation = this.getApplyForm.get('beneficiaryDetails').value;
		this.getApplyForm.get('beneficiaryDetails').setValue(beneficiaryInformation);
		// beneficiaryInformation.

	}

	_handleReaderLoaded(e)
	{
		let reader = e.target;
		this.imageSrc = reader.result;
		console.log(this.imageSrc)
	}

	onFileChanged(event)
	{
		//const fileSizeLimit = 512000; // 500KB
		//const fileSizeLimit = 5242880; // 5MB

		const limitFileSize = 512000; // 500KB
		const limitWidth = 2000;
		const limitHeight = 2000;
		const imageType = 'image/jpeg';

		const reader: any = new FileReader();

		if (event.target.files.length !== 0)
		{
			const file = event.target.files[0];

			this.sizeError = false;
			reader.onloadend = (event) =>
			{
				// Get the event.target.result from the reader (base64 of the image)
				let uploadedImage = event.target.result;
				//this.insuredIdentityDocumentImagePreview = event.target.result;

				const image = new Image();
				image.onload = (event) =>
				{
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
					do
					{
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

					this.insuredIdentityDocumentImagePreview = newDataUrl;

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

		console.log(event);
	}

	deleteImage()
	{
		this.hasImage = false;
		this.insuredIdentityDocumentImageData = "";
		this.insuredIdentityDocumentImagePreview = ""
		console.log(this.fileinput);
		// this.onFileChanged("");
		this.fileinput.nativeElement.value = '';
	}

	deleteAttachment(index)
	{
		this.files.splice(index, 1);
		this.files = [];
	}


	addRow()
	{
		this.newDynamic = { CompanyName: "", LifeFaceAmount: "", DreadDiseaseFaceAmount: "", AccidentalFaceAmount: "", IssueYear: "" };
		this.dynamicArray.push(this.newDynamic);
		return true;

	}
	removeRow(index)
	{
		if (this.dynamicArray.length == 1)
		{
			return false;
		} else
		{
			this.dynamicArray.splice(index, 1);
			return true;
		}
	}

	degradeImageToLimit(dataUrl: string, imageType: string, imageQuality: number, limit: number, callback: any)
	{
		let image = new Image();
		image.onload = (event) =>
		{
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

	logRemainingSessionStorage()
	{
		let limit = 1024 * 1024 * 5; // 5 MB
		let remSpace = limit - unescape(encodeURIComponent(JSON.stringify(sessionStorage))).length;
		console.log(remSpace);
	}

	pay(){
		this.router.navigate(['prime-secure-lite/pay']);
		this.ngxService.stop();
	}
}
