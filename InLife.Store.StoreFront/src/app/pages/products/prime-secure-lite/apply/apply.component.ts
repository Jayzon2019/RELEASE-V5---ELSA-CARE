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
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { environment } from '@environment';
import { catchError, finalize, retry } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { GeneralMessagePromptComponent } from '@app/shared/component/general-message-prompt/general-message-prompt.component';
import { MatDialog } from '@angular/material/dialog';
import { UtilitiesService } from '@app/shared/services/utilities.service';
import { StorageType } from '@app/services/storage-types.enum';

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
	pdfName: string = '';
	pdfBase64 = '';

	constructor
	(
		private router: Router,
		private formBuilder: FormBuilder,
		private session: SessionStorageService,
		private ngxService: NgxUiLoaderService,
		private http: HttpClient,
		private dialog: MatDialog,
		private util: UtilitiesService
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
				if (result === false)
				{
					this.getApplyForm.get('declarationForm').get('declaration').setValue(null);
				}
			});

		this.getApplyForm.get('declarationForm').get('confirmdeclare').valueChanges.subscribe(
			result =>
			{
				if (result === false)
				{
					this.getApplyForm.get('declarationForm').get('confirmdeclare').setValue(null);
				}
			});

		this.getApplyForm.get('declarationForm').get('antimoneylaundary').valueChanges.subscribe(
			result =>
			{
				if (result === false)
				{
					this.getApplyForm.get('declarationForm').get('antimoneylaundary').setValue(null);
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

		this.getApplyForm.get('personalInformation').get('nationality').valueChanges.subscribe(
			result =>
			{
				if (result !== '170')
				{
					this.getApplyForm.get('personalInformation').get('nationality').setValue('170');
				}
			});

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

			if (this.getApplyForm.get('beneficiaryDetails').get('insuredProvince').value)
			{
				this.onChangeBeneficiaryProviance(this.getApplyForm.get('beneficiaryDetails').get('insuredProvince').value);
			}
			this.getApplyForm.get('beneficiaryDetails').get('insuredProvince').enable();
			this.getApplyForm.get('beneficiaryDetails').get('insuredMunicipality').enable();
			this.getApplyForm.get('beneficiaryDetails').get('insuredProvince').setValidators([Validators.required]);
			this.getApplyForm.get('beneficiaryDetails').get('insuredMunicipality').setValidators([Validators.required]);

		this.showThirdStep = this.getApplyForm.get('personalInformation').valid;
		this.showFourthStep = this.getApplyForm.get('identification').valid;
		this.showFifthStep = this.getApplyForm.get('beneficiaryDetails').valid;

		this.ngxService.stop();
	}
	

	initForm(getApplyFormData)
	{
		this.getApplyForm = this.formBuilder.group({
			personalInformation: this.addPersonalInformation(getApplyFormData.personalInformation || ''),
			identification: this.addIdentification(getApplyFormData.identification || ''),
			beneficiaryDetails: this.addBeneficiaryDetails(getApplyFormData.beneficiaryDetails || ''),
			declarationForm: this.addFinalDeclaration(getApplyFormData.declarationForm || ''),
		});
	}

	setShowThird()
	{
		this.showThirdStep = true;
		setTimeout(function ()
		{
			document.querySelector('#personal-info-anchor').scrollIntoView({
				behavior: 'smooth'
			});
		}, 100);
	}

	setShowFourth()
	{
		this.showFourthStep = true;
		setTimeout(function ()
		{
			document.querySelector('#id-anchor').scrollIntoView({
				behavior: 'smooth'
			});
		}, 100);
	}


	setShowFifth()
	{
		this.showFifthStep = true;
		setTimeout(function ()
		{
			document.querySelector('#bd-anchor').scrollIntoView({
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
			nationality: new FormControl(data.nationality || '170', Validators.required),
			birthCountry: new FormControl(data.birthCountry || '170', Validators.required),
			birthProvince: new FormControl(data.birthProvince || '', Validators.required),
			birthMunicipality: new FormControl(data.birthMunicipality || '', Validators.required),
		});
	}

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
			mname: new FormControl(data.mname || '', Validators.required),
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

	isValidFATCA(): boolean{
		let fatca1 = this.getApplyForm.get('declarationForm').get('uslawpersion').value;
		let fatca2 = this.getApplyForm.get('declarationForm').get('usnotlaw').value;

		return (fatca1 === "No" && fatca2 === "No");
	}

	submitApplyForm()
	{
		this.submitted = true;
		if (this.getApplyForm.valid && this.isValidFATCA())
		{
			this.ngxService.start();
			this.session.set("getApplyForm", this.getApplyForm.value);
			this.session.set("extensionData", this.dynamicArray);
			this.session.set("insuredIdentityDocumentImageData", this.insuredIdentityDocumentImageData);
			this.session.set("insuredIdentityDocumentImagePreview", this.insuredIdentityDocumentImagePreview);
			
			const refNo = this.session.get("refNo");
			const getQuoteForm = this.session.get("getQuoteForm");
			const extensionData = this.session.get("extensionData");
			const underwritingstatus = this.session.get("UnderWritingStatus");
			const healthCon = getQuoteForm.healthCondition;

			let bday = new Date(getQuoteForm.calculatePremium.dateofbirth).toLocaleDateString();

			let ownerSuffixID = this.nullIfZero(getQuoteForm.basicInformation.suffix);
			let insuredSuffixId = this.nullIfZero(getQuoteForm.basicInformation.suffix);
			let benefSuffixId = this.nullIfZero(this.getApplyForm.get('beneficiaryDetails').get('suffix').value);

			let totalInches = Math.round(healthCon.heightInFeet * 12) + healthCon.heightInInches;

			let payload: any = {
				"PlanCode": "TR0091",
				"PlanName": "Prime Secure Lite",
				"PaymentMode": 12,//12,
				"FaceAmount": parseFloat(getQuoteForm.calculatePremium.totalCashBenefit.substring(1).replace(/,/g, '')),//720000, ñ ;
				"Health1": getQuoteForm.healthCondition.healthDeclaration1 === 'No' ? "0" : "1",//"0",healthCondition
				"Health2": getQuoteForm.healthCondition.healthDeclaration2 === 'No' ? "0" : "1",//"0",
				"Health3": getQuoteForm.healthCondition.healthDeclaration3 === 'No' ? "0" : "1",//"0",
				"Fatca1": this.getApplyForm.get('declarationForm').get('uslawpersion').value,//"No",
				"Fatca2": this.getApplyForm.get('declarationForm').get('usnotlaw').value,//"No",
				"Question1": getQuoteForm.covidForm.privacyPolicy ? "Yes" : "No",//"No",
				"Question2": getQuoteForm.covidForm.privacyPolicy2 ? "Yes" : "No",//"No",
				"PolicyDeliveryOption": "digitalhard",
				"ServicingAgentBranchCode": "DO6437",
				"IsBanca": false,
				"ProposalId": underwritingstatus?.proposalId || 50148,
				
				"OwnerIsInsured": 1,
				"OwnerRelationToInsuredId": 24,
				"OwnerPrefixId": this.nullIfZero(getQuoteForm.basicInformation.prefix),//24,
				// "OwnerSuffixId": this.nullIfZero(getQuoteForm.basicInformation.suffix),//24,
				"OwnerFirstName": getQuoteForm.basicInformation.fname,//"testFaname",
				"OwnerLastName": getQuoteForm.basicInformation.lname,//"testLName",
				"OwnerMiddleName": getQuoteForm.basicInformation.mname,//"testMName",
				"OwnerBirthday": bday,//"01/01/1980",
				"OwnerGenderId": this.nullIfZero(getQuoteForm.calculatePremium.gender),//8,
				"OwnerEmailAddress": getQuoteForm.basicInformation.email,//"test@gmail1.com",
				"OwnerResidencePhoneNumber": this.nullIfZero(getQuoteForm.basicInformation.landline),//12345678,
				"OwnerMobileNo": this.nullIfZero(getQuoteForm.basicInformation.mobile),//1234567,

				"InsuredFirstName": getQuoteForm.basicInformation.fname,//"TestFLead3",
				"InsuredMiddleName": getQuoteForm.basicInformation.mname,//"TestMLead3",
				"InsuredLastName": getQuoteForm.basicInformation.lname,//"TestLLead",
				"InsuredBirthday": bday,//"01/01/1980",
				"InsuredGenderId": this.nullIfZero(getQuoteForm.calculatePremium.gender),//8,
				"InsuredPrefixId": this.nullIfZero(getQuoteForm.basicInformation.prefix),//24,
				// "InsuredSuffixId": this.nullIfZero(getQuoteForm.basicInformation.suffix),//24,
				"InsuredEmailAddress": getQuoteForm.basicInformation.email,//"test@gmail1.com",
				"InsuredResidencePhoneNumber": this.nullIfZero(getQuoteForm.basicInformation.landline),//12345678,
				"InsuredMobileNo": this.nullIfZero(getQuoteForm.basicInformation.mobile),//12345567,
				"InsuredResidenceAddress1": this.getApplyForm.get('personalInformation').get('street').value,//"Add1",
				"InsuredResidenceAddress2": this.getApplyForm.get('personalInformation').get('village').value,//"Add2",
				"InsuredResidenceAddress3": this.getApplyForm.get('personalInformation').get('barangay').value,//"Add3",
				"InsuredResidenceMunicipalityId": this.nullIfZero(this.getApplyForm.get('personalInformation').get('municipality').value),//1,
				"InsuredResidenceProvinceId": this.nullIfZero(this.getApplyForm.get('personalInformation').get('province').value),//1,
				"InsuredResidenceZipCode": this.getApplyForm.get('personalInformation').get('zipCode').value,//4118,
				"InsuredCitizenshipId" : 170,
				"InsuredCivilStatusId": this.nullIfZero(this.getApplyForm.get('personalInformation').get('civilStatus').value),//5,
				"InsuredPrimaryOccupationCompanyName": getQuoteForm.basicInformation.company,//"testcompanyname",
				"InsuredPrimaryOccupationId": this.nullIfZero(getQuoteForm.basicInformation.occupation),//3,
				"InsuredPrimaryOccupationMonthlyIncome": parseFloat(getQuoteForm.basicInformation.monthlyIncome.substring(1).replace(/,/g, '')),//50000,
				"InsuredFundSourceId": this.nullIfZero(getQuoteForm.basicInformation.sourceOfFunds),//122,
				"InsuredPreferredMailingAddress": "Home",
				"InsuredPrimaryOccupationAddress1": this.getApplyForm.get('personalInformation').get('street').value,//"Address1",
				"InsuredPrimaryOccupationAddress2": this.getApplyForm.get('personalInformation').get('village').value,//"Address2",
				"InsuredPrimaryOccupationAddress3": this.getApplyForm.get('personalInformation').get('barangay').value,//"Address3",
				"InsuredPrimaryOccupationZipCode": this.getApplyForm.get('personalInformation').get('zipCode').value,//"4118",
				"InsuredPrimaryOccupationProvinceId": this.nullIfZero(this.getApplyForm.get('personalInformation').get('province').value),//1,
				"InsuredPrimaryOccupationMunicipalityId": this.nullIfZero(this.getApplyForm.get('personalInformation').get('municipality').value),//1,
				"InsuredOfficePhoneNumber": this.nullIfZero(getQuoteForm.basicInformation.landline),//1234567,
				"InsuredIdTypeId": this.nullIfZero(this.getApplyForm.get('identification').get('secondaryLegalIdType').value),
				"InsuredTinNo": this.getApplyForm.get('identification').get('secondaryLegalIdNumber').value,//123456789,
				"InsuredOtherIdNoType": this.getApplyForm.get('identification').get('legalIdType').value,//"Driver's License",legalIdType
				"InsuredOtherIdNo": this.getApplyForm.get('identification').get('LegalIdNumber').value,//"12345678",

				"Beneficiary": [{
					"FirstName": this.getApplyForm.get('beneficiaryDetails').get('fname').value,//"SampleBeneFname",
					"MiddleName": this.getApplyForm.get('beneficiaryDetails').get('mname').value,//"SampleBeneMname",
					"LastName": this.getApplyForm.get('beneficiaryDetails').get('lname').value,//"SampleBeneLastName",
					// "SuffixId": this.nullIfZero(this.getApplyForm.get('beneficiaryDetails').get('suffix').value),
					"PrefixId": this.nullIfZero(this.getApplyForm.get('beneficiaryDetails').get('prefix').value),
					"AddressType": 2,//2,Permanent
					"Address1": this.getApplyForm.get('beneficiaryDetails').get('insuredStreet').value,//"Address1",
					"Address2": this.getApplyForm.get('beneficiaryDetails').get('insuredVillage').value,//"Address2",
					"Address3": this.getApplyForm.get('beneficiaryDetails').get('insuredBarangay').value,//"Address3",
					"ProvinceId": this.nullIfZero(this.getApplyForm.get('beneficiaryDetails').get('insuredProvince').value),//1,
					"MunicipalityId": this.nullIfZero(this.getApplyForm.get('beneficiaryDetails').get('insuredMunicipality').value),//1,
					"ZipCode": this.getApplyForm.get('beneficiaryDetails').get('insuredZipCode').value,//"4118",
					"LandLineNumber": this.nullIfZero(this.getApplyForm.get('beneficiaryDetails').get('insuredLandline').value),//1234567,
					"MobileNumber": this.nullIfZero(this.getApplyForm.get('beneficiaryDetails').get('insuredMobile').value),
					"CivilStatusId": this.nullIfZero(this.getApplyForm.get('beneficiaryDetails').get('insuredCivilStatus').value),//5,
					"GenderId": this.nullIfZero(this.getApplyForm.get('beneficiaryDetails').get('insuredGender').value),//8,
					"Birthday": new Date(this.getApplyForm.get('beneficiaryDetails').get('insuredDateofbirth').value).toLocaleDateString(),//"01/01/1980", 
					"RelationToInsuredId": this.nullIfZero(this.getApplyForm.get('beneficiaryDetails').get('relation').value),//25,
					"Priority": this.getApplyForm.get('beneficiaryDetails').get('designation').value,//0,
					"Right": this.getApplyForm.get('beneficiaryDetails').get('type').value//0
				}],
				// "clientId": 123123,
				"ExistingOtherInsurance": extensionData,
				"InsuredValidIdImage": this.insuredIdentityDocumentImageData || this.pdfBase64,//"XXXXXXXXXXXXXXX",
				"OwnerValidIdImage": this.insuredIdentityDocumentImageData || this.pdfBase64,//"XXXXXXXXXXXXX",
				"RefFirstName": getQuoteForm.basicInformation.afname || "",//"SampleFName",
				"RefLastName": getQuoteForm.basicInformation.alname || "",//"SampleLName",
				"InsuredHeight": totalInches,
				"InsuredWeight": getQuoteForm.healthCondition.weight
			};

			// Do not include in request body if suffix is not applicable
			if(ownerSuffixID !== 1)
				payload.OwnerSuffixId = ownerSuffixID
			if(insuredSuffixId !== 1)
				payload.InsuredSuffixId = insuredSuffixId;

			if(benefSuffixId !== 1)
				payload.Beneficiary[0].SuffixId = benefSuffixId;

			this.session.set(StorageType.APPLY_DATA, payload);
			this.router.navigate(['prime-secure-lite/pay']);
		}
		else
		{
			//this.submitted = false;
		}
		return;
	}

	nullIfZero(id: any): number | null
	{
		let value = Number(id);

		return (value === 0)
			? null
			: value;
	}

	backClicked()
	{
		this.session.set("getApplyForm",this.getApplyForm.value);
		this.router.navigate(['prime-secure-lite/quote']);
	}

	/* START: For Personal information */
	onChangeProviance(value)
	{
		for (let i = 0; i < CONSTANTS.PROVIANCE.length; i++)
		{
			if (CONSTANTS.PROVIANCE[i].id == value)
			{
				this.muncipality = CONSTANTS.PROVIANCE[i].Municipality;

			}

		}
	}
	isNullOrWhiteSpace(value: string)
	{
		if (typeof value === 'undefined' || value == null)
			return true;

		return value.replace(/\s/g, '').length < 1;
	}

	onChangeProviances(value)
	{
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
	}

	async checkFileBase64(file): Promise<string> {
        let promise = this.getBase64(file);
        return await promise;
    }

	getBase64(file): Promise<any> {
		return new Promise(function (resolve, reject) {
            let reader = new FileReader();
            reader.onload = function () { 
				resolve(reader.result); 
			};
            reader.onerror = reject;
            reader.readAsDataURL(file);
        });
	}

	onFileChanged(event)
	{
		//const fileSizeLimit = 512000; // 500KB
		const limitFileSize = 512000; // 500KB
		const limitWidth = 2000;
		const limitHeight = 2000;
		const imageType = 'image/jpeg';

		const reader: any = new FileReader();

		if (event.target.files.length !== 0)
		{
			const file = event.target.files[0];

			if(file.type === 'application/pdf'){ //PDF
				const fileSizeLimit = 5242880; // 5MB

				if(file.size <= fileSizeLimit) {
					this.pdfName = file.name;
					this.checkFileBase64(file).then(res => {
						this.pdfBase64 = res.split(',')[1];
					});
				}
				else {
					this.deleteImage();
					this.util.ShowGeneralMessagePrompt({
						message: `File size exceeds the allowable limit of 5MB.`
					});
				}

			} else { //JPEG or PNG
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

						this.insuredIdentityDocumentImageData = base64PdfString;
						this.hasImage = true;
					};

					image.src = uploadedImage;
				};

				reader.readAsDataURL(file);
			}
		}
	}

	deleteImage()
	{
		this.hasImage = false;
		this.insuredIdentityDocumentImageData = "";
		this.insuredIdentityDocumentImagePreview = "";
		this.pdfName = '';
		this.pdfBase64 = '';
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
	}
}
