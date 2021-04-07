import { ApplyService } from './../../services/apply.service';
import { jsPDF } from 'jspdf';
import { SessionStorageService } from './../../../../../services/session-storage.service';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Router } from '@angular/router';
import { ApplicationStatusBaseComponent } from '../application-status-base.component';
import { CONSTANTS } from '@app/services/constants';
import { FormControl, Validators } from '@angular/forms';
import { FormBuilder } from '@angular/forms';
import { environment } from '@environment';
import { Subject, throwError } from 'rxjs';
import { catchError, takeUntil } from 'rxjs/operators';
import { HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { StorageType } from '@app/services/storage-types.enum';
import { NgxUiLoaderService } from 'ngx-ui-loader';
@Component({
	selector: 'group-payment-pending',
	templateUrl: './payment-pending.component.html',
	styleUrls: ['../styles.scss', './payment-pending.component.scss']
})
export class PaymentPendingComponent extends ApplicationStatusBaseComponent implements OnInit, OnDestroy {
	public isPaymentUploaded: boolean = false;
	sizeError: boolean;
	paymentProofType: any;
	CONSTANTS = CONSTANTS;
	requirementsForm: any;
	destroy$ = new Subject();
	hasError: boolean = false;
	errorMsg: string;
	constructor(router: Router, 
				activatedRoute: ActivatedRoute, 
				private session: SessionStorageService, 
				private formBuilder: FormBuilder,
				private ngxService: NgxUiLoaderService
				,
				private applySerice_API: ApplyService) {
		super(router, activatedRoute)
	}

	ngOnInit(): void {
		this.requirementsForm = this.formBuilder.group({
			ProofOfPayment: new FormControl("", [Validators.required]),
		});
	}

	uploadPaymentProof() {
		this.ngxService.start();
		this.hasError = false;
		let url = environment.appApi.host + `/group/applications/${this.referenceCode}/files/payment-proof`;
		let data = this.requirementsForm.get('ProofOfPayment').value;
		if(this.sizeError) return;

		this.applySerice_API.uploadRequirement(url, data, data.type, data.name)
			.pipe(
				takeUntil(this.destroy$)
			)
			.subscribe(data => {
				this.isPaymentUploaded = true;
				this.ngxService.stopAll();
			}, error => {
				this.hasError = true;
				this.errorMsg = error.message;
				this.ngxService.stopAll();
			});
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

	deleteImage() {
		this.requirementsForm.get('ProofOfPayment').setValue('');
	}
	onFileChanged(event) {
		const limitFileSize = 512000; // 500KB
		const limitWidth = 500;
		const limitHeight = 500;
		const imageType = 'image/jpeg';

		const reader: any = new FileReader();

		if (event.target.files.length !== 0) {
			const file = event.target.files[0];
			console.log(file);
				

			const generalType = file.type.split('/')[0];
			let fileType = '.'+file.name.split('.').pop();
			if(!CONSTANTS.REQUIREMENTS_ALLOWED_FILE_TYPE.split(',').find(i => i == fileType)) return;

			if (generalType == 'application') {
				reader.readAsDataURL(file);
				reader.onloadend = () => {
					this.requirementsForm.get('ProofOfPayment').setValue(file);
					this.paymentProofType = {
						type: 'document',
						title: file.name,
						fileInfo: this.fileInfo(file)
					}
					this.sizeError = (file.size > CONSTANTS.MAX_UPLOAD_FILE_SIZE) ? true : false;
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

						do {
							newDataUrl = canvas.toDataURL(imageType, newImageQuality / 100);
							newBase64ImageString = newDataUrl.split(',')[1];
							newFileSize = Math.round(newBase64ImageString.length * 3 / 4);
							newImageQuality -= (newImageQuality > 10)
								? 5
								: 1;

						} while (newFileSize > limitFileSize && newImageQuality > 0);
						this.requirementsForm.get('ProofOfPayment').setValue(file);
						this.paymentProofType = {
							type: 'image',
							title: file.name,
							image: newDataUrl
						}
						this.sizeError = (file.size > CONSTANTS.MAX_UPLOAD_FILE_SIZE) ? true : false;
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

					};

					image.src = uploadedImage;
				};
				reader.readAsDataURL(file);
			}
		}
	}

	ngOnDestroy() {
		this.destroy$.next(true);
		this.destroy$.unsubscribe();
	}
}
