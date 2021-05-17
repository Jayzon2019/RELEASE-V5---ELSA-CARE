import { Component, OnInit } from '@angular/core';
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
import { ApiService, FacebookPixelService, SessionStorageService } from '@app/services';
import { DynamicGrid } from './extension.model';

@Component
({
	selector: 'app-apply',
	templateUrl: './apply.component.html',
	styleUrls: ['./apply.component.css', './../main/prime-care-global.scss'],
	providers: []
})
export class ApplyComponent implements OnInit
{
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
		private vps: ViewportScroller,
		private facebookPixelService: FacebookPixelService,
	)
	{
		this.ngxService.start();
		this.getQuoteFormData = this.session.get('getQuoteForm') || "[]";
		const getApplyFormData = this.session.get("getApplyForm_PC") || "[]";

		this.insuredIdentityDocumentImagePreview = this.session.get("insuredIdentityDocumentImagePreview_PC");
		this.insuredIdentityDocumentImageData = this.session.get("insuredIdentityDocumentImageData_PC");
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
		this.newDynamic = { CompanyName: "", LifeFaceAmount: "", DreadDiseaseFaceAmount: "", AccidentalFaceAmount: "", IssueYear: "" };
		this.dynamicArray.push(this.newDynamic);

		if (this.getApplyForm.get('personalInformation').get('country').value == '170')
		{
			if (this.getApplyForm.get('personalInformation').get('province').value)
			{
				this.onChangeProviance(this.getApplyForm.get('personalInformation').get('province').value);
			}
			this.getApplyForm.get('personalInformation').get('province').enable();
			this.getApplyForm.get('personalInformation').get('municipality').enable();
			this.getApplyForm.get('personalInformation').get('province').setValidators([Validators.required]);
			this.getApplyForm.get('personalInformation').get('municipality').setValidators([Validators.required]);

		} else
		{
			this.getApplyForm.get('personalInformation').get('province').disable();
			this.getApplyForm.get('personalInformation').get('municipality').disable();
		}

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
			result =>
			{
				let el = document.getElementById("secondaryLegalIdType");
				if (result !== '')
				{
					el.classList.add('black');
				} else
				{
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



		this.getApplyForm.get('employment').get('occupation').valueChanges.subscribe(
			result =>
			{
				if (result == '1')
				{
					this.getApplyForm.get('employment').get('company').disable();
					this.getApplyForm.get('employment').get('monthlyIncome').disable();

				} else
				{

					this.getApplyForm.get('employment').get('company').enable();
					this.getApplyForm.get('employment').get('monthlyIncome').enable();
					this.getApplyForm.get('employment').get('company').setValidators([Validators.required]);
					this.getApplyForm.get('employment').get('monthlyIncome').setValidators([Validators.required]);

				}
				this.getApplyForm.get('employment').updateValueAndValidity();
			});


		this.getApplyForm.get('personalInformation').get('nationality').valueChanges.subscribe(
			result =>
			{
				if (result !== '170')
				{
					this.getApplyForm.get('personalInformation').get('nationality').setValue('170');
				}
			});

		this.getApplyForm.get('personalInformation').get('country').valueChanges.subscribe(
			result =>
			{
				if (result == '170')
				{
					this.getApplyForm.get('personalInformation').get('province').enable();
					this.getApplyForm.get('personalInformation').get('municipality').enable();
					this.getApplyForm.get('personalInformation').get('province').setValidators([Validators.required]);
					this.getApplyForm.get('personalInformation').get('municipality').setValidators([Validators.required]);
				} else
				{
					this.getApplyForm.get('personalInformation').get('province').disable();
					this.getApplyForm.get('personalInformation').get('municipality').disable();
					this.getApplyForm.get('personalInformation').get('country').setValue('170');
				}
				this.getApplyForm.get('personalInformation').updateValueAndValidity();
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

		if (this.getApplyForm.get('beneficiaryDetails').get('insuredCountry').value == '170')
		{
			if (this.getApplyForm.get('beneficiaryDetails').get('insuredProvince').value)
			{
				this.onChangeBeneficiaryProviance(this.getApplyForm.get('beneficiaryDetails').get('insuredProvince').value);
			}
			this.getApplyForm.get('beneficiaryDetails').get('insuredProvince').enable();
			this.getApplyForm.get('beneficiaryDetails').get('insuredMunicipality').enable();
			this.getApplyForm.get('beneficiaryDetails').get('insuredProvince').setValidators([Validators.required]);
			this.getApplyForm.get('beneficiaryDetails').get('insuredMunicipality').setValidators([Validators.required]);

		} else
		{
			this.getApplyForm.get('beneficiaryDetails').get('insuredBirthProvince').disable();
			this.getApplyForm.get('beneficiaryDetails').get('insuredBirthMunicipality').disable();
		}

		this.getApplyForm.get('beneficiaryDetails').get('insuredCountry').valueChanges.subscribe(
			result =>
			{
				if (result == '170')
				{
					this.getApplyForm.get('beneficiaryDetails').get('insuredProvince').enable();
					this.getApplyForm.get('beneficiaryDetails').get('insuredMunicipality').enable();
					this.getApplyForm.get('beneficiaryDetails').get('insuredProvince').setValidators([Validators.required]);
					this.getApplyForm.get('beneficiaryDetails').get('insuredMunicipality').setValidators([Validators.required]);
				} else
				{
					this.getApplyForm.get('beneficiaryDetails').get('insuredProvince').disable();
					this.getApplyForm.get('beneficiaryDetails').get('insuredMunicipality').disable();
				}
				this.getApplyForm.get('beneficiaryDetails').updateValueAndValidity();
			});


		this.showSecondStep = this.getApplyForm.get('personalInformation').valid === true ? true : false;
		this.showThirdStep = this.getApplyForm.get('employment').valid === true ? true : false;
		this.showFourthStep = this.getApplyForm.get('identification').valid === true ? true : false;
		this.showFifthStep = this.getApplyForm.get('beneficiaryDetails').valid === true ? true : false;
		this.ngxService.stop();
	}

	initForm(getApplyFormData)
	{
		this.getApplyForm = this.formBuilder.group({
			personalInformation: this.addPersonalInformation(getApplyFormData.personalInformation || ''),
			employment: this.addEmployment(getApplyFormData.employment || ''),
			identification: this.addIdentification(getApplyFormData.identification || ''),
			beneficiaryDetails: this.addBeneficiaryDetails(getApplyFormData.beneficiaryDetails || ''),
			declarationForm: this.addFinalDeclaration(getApplyFormData.declarationForm || ''),
		});

	}
	setShowSecond()
	{
		this.showSecondStep = true;

		setTimeout(function ()
		{
			document.querySelector('#step2_head').scrollIntoView({
				behavior: 'smooth'
			});
		}, 100);
	}

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
			zipCode: new FormControl(data.zipCode || '', [Validators.required, Validators.pattern("^[0-9]{1,5}$")]),
			civilStatus: new FormControl(data.civilStatus || '', Validators.required),
			country: new FormControl(data.country || '170', Validators.required),
			province: new FormControl(data.province || this.getQuoteFormData.basicInformation.province),
			municipality: new FormControl(data.municipality || this.getQuoteFormData.basicInformation.municipality),
			birthCountry: new FormControl(data.birthCountry || '170', Validators.required),
			birthProvince: new FormControl(data.birthProvince || ''),
			birthMunicipality: new FormControl(data.birthMunicipality || ''),
			nationality: new FormControl(data.nationality || '170', Validators.required),
		});
	}

	addEmployment(data)
	{
		return this.formBuilder.group({
			company: new FormControl(data.company || '', Validators.required),
			occupation: new FormControl(data.occupation || '', Validators.required),
			fundSource: new FormControl(data.fundSource || '', Validators.required),
			monthlyIncome: new FormControl(data.monthlyIncome || '', Validators.required),
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
			file: new FormControl(data.file || '')
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
			insuredCountry: new FormControl(data.insuredCountry || '170', Validators.required),
			insuredProvince: new FormControl(data.insuredProvince || ''),
			insuredMunicipality: new FormControl(data.insuredMunicipality || ''),
			insuredBirthProvince: new FormControl(data.insuredBirthProvince || '', Validators.required),
			insuredBirthMunicipality: new FormControl(data.insuredBirthMunicipality || ''),
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
		this.facebookPixelService.track('Lead');
		if (this.getApplyForm.valid)
		{
			this.session.set("getApplyForm_PC", this.getApplyForm.value);
			this.session.set("extensionData_PC", this.dynamicArray);
			this.session.set("insuredIdentityDocumentImageData_PC", this.insuredIdentityDocumentImageData);
			this.session.set("insuredIdentityDocumentImagePreview_PC", this.insuredIdentityDocumentImagePreview);

			const getApplyFormData = this.session.get("getApplyForm_PC") || "[]";
			const getQuoteForm = this.session.get('getQuoteForm') || "[]";

			this.router.navigate(['prime-care/pay']);

			// const paymentUrl = "https://beta2.insularlife.com.ph/CustomerPortal/Customer/E-Payment/ILPay.ashx?RefNo=0000000012&&Amount=246075&&RetURL=http://localhost:4200/apply";
			// window.open(paymentUrl, '_self');

			// this.apiService.postRequest('OAapi/OnlineStore/CreateApplication', bookData).subscribe((data: any) => {
			//   this.submitted = false;
			//   console.log(data);
			//   //this.inquiryForm.reset();
			//   //this.router.navigate(['/prime-care/pay']);
			// }, (err) => {
			//   console.log(err);
			//   if (err && err.hasOwnProperty('error')) {
			//     //this.CommonService.errorMsg(err.error.message);
			//   } else {
			//     //this.CommonService.errorMsg(CONSTANTS.GLOBAL_ERROR_MSG);
			//   }
			//   this.submitted = false;
			// });
		}
		else
		{
			//this.submitted = false;
		}
		return;
	}

	backClicked()
	{
		this.session.set("getApplyForm_PC", this.getApplyForm.value);
		this.location.back();
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
			this.getApplyForm.get('beneficiaryDetails').value.insuredCountry = personalInformation.country;
			this.getApplyForm.get('beneficiaryDetails').value.insuredZipCode = personalInformation.zipCode;
		}
		else
		{
			this.beneficiaryMuncipality = [];
			this.getApplyForm.get('beneficiaryDetails').value.insuredVillage = '';
			this.getApplyForm.get('beneficiaryDetails').value.insuredStreet = '';
			this.getApplyForm.get('beneficiaryDetails').value.insuredProvince = '';
			this.getApplyForm.get('beneficiaryDetails').value.insuredMunicipality = '';
			this.getApplyForm.get('beneficiaryDetails').value.insuredCountry = '';
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
	}

	deleteImage()
	{
		this.hasImage = false;
		this.insuredIdentityDocumentImageData = "";
		this.insuredIdentityDocumentImagePreview = ""
		// this.onFileChanged("");
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
		console.log(this.dynamicArray);
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
}
