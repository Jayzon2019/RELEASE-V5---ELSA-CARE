import { StorageType } from '@app/services/storage-types.enum';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { SessionStorageService, FacebookPixelService } from '@app/services';

@Component
({
	selector: 'app-thank-you',
	templateUrl: './thank-you.component.html',
	styleUrls: ['./thank-you.component.css', './thank-you.component.scss']
})
export class ThankYouComponent implements OnInit
{
	plan: string = '';
	isPrimeCare: boolean = false;
	isPrimeSecure: boolean = false;
	isPrimeSecureLite: boolean = false;

	constructor
	(
		private session: SessionStorageService,
		private router: Router,
		private facebookPixelService: FacebookPixelService
	)
	{

	}

	resetForm()
	{
		const planAcquired = this.session.get(StorageType.ACQUIRED_PLAN);

		if(planAcquired.plan === 'PrimeSecureLite') {
			// Remove policy no for verification of new application
			this.session.remove(StorageType.POLICYNO);
			this.session.remove('UnderWritingStatus');
			this.session.remove('age');
			this.session.remove('getinnerForm');
			this.session.remove('getQuoteForm');
			this.session.remove('getApplyForm');
			this.session.remove('extensionData');
			this.session.remove('insuredIdentityDocumentImageData');
			this.session.remove('insuredIdentityDocumentImagePreview');
			this.session.remove(StorageType.QUOTE_INTERNAL_DATA);
			this.session.remove(StorageType.QUOTE_EXTERNAL_DATA);
			this.session.remove(StorageType.ACQUIRED_PLAN);
			this.session.remove(StorageType.APPLY_DATA);
		} else if(planAcquired.plan === 'PrimeCare') {
			// Remove policy no for verification of new application
			this.session.remove(StorageType.POLICYNO);
			this.session.remove('age');
			this.session.remove('getinnerForm');
			this.session.remove('getQuoteForm');
			this.session.remove('getApplyForm');
			this.session.remove('extensionData');
			this.session.remove('insuredIdentityDocumentImageData');
			this.session.remove('insuredIdentityDocumentImagePreview');
			this.session.remove(StorageType.ACQUIRED_PLAN);
			this.session.remove(StorageType.APPLY_PC_DATA);
		}
	}

	backToHome() {
		this.resetForm();
		this.router.navigate(['/']);
	}

	ngOnInit(): void
	{
		const planAcquired = this.session.get(StorageType.ACQUIRED_PLAN);

		let amount = "0.00";

		if (planAcquired.plan === 'PrimeSecureLite') {
			this.isPrimeSecureLite = true;
			let age = this.session.get('age');
			amount = age.age <= 47 ? "2500" : "3000";

		} else if (planAcquired.plan === 'PrimeCare') {
			this.isPrimeCare = true;
			amount = this.session.get('getinnerForm').amount;
		} else {
			this.isPrimeSecure = true;
		}
		this.plan = (planAcquired.plan == 'PrimeSecureLite') ?
			"Prime Secure Lite" : (planAcquired.plan == 'PrimeCare') ?
				"Prime Care" : "Prime Secure";
		this.facebookPixelService.track('Purchase', { value: parseInt(amount).toFixed(2), currency: 'USD' });
	}
}
