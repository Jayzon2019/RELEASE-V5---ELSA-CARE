import { Component, OnInit } from '@angular/core';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { SessionStorageService } from '@app/services';
import { StorageType } from '@app/services/storage-types.enum';

@Component
({
	selector: 'app-payment-failed',
	templateUrl: './payment-failed.component.html',
	styleUrls: ['./payment-failed.component.scss']
})
export class PaymentFailedComponent implements OnInit
{
	responseCode: any;
	text: any;

	constructor
	(
		private ngxService: NgxUiLoaderService,
		private routerlink: Router,
		private route: ActivatedRoute,
		private session: SessionStorageService,
		private http: HttpClient
	)
	{
		this.ngxService.start();
		this.responseCode = this.route.snapshot.params['id'];
	}

	ngOnInit(): void
	{
		var responseCode =
		{
			'?': 'Response Unknown',
			'1': 'Transaction Declined',
			'2': 'Bank Declined Transaction',
			'3': 'No Reply from Bank',
			'4': 'Expired Card',
			'5': 'Insufficient Funds',
			'6': 'Error Communicating with Bank',
			'7': 'Payment Server detected an error',
			'8': 'Transaction Type Not Supported',
			'9': 'Bank declined transaction (Do not contact Bank)',
			'A': 'Transaction Aborted',
			'B': ' Transaction Blocked - The Verification Security Level of the 3-D Secure transaction is insufficient to allow processing to continue',
			'C': 'Transaction Cancelled  ',
			'D': 'Deferred transaction has been received and is awaiting processing',
			'E': ' Issuer Returned a Referral Response',
			'F': '3-D Secure Authentication failed ',
			'L': 'Card Security Code verification failed',
			'N': 'Cardholder is not enrolled in Authentication scheme ',
			'P': 'Transaction has been received by the Payment Adaptor and is being processed',
			'R': 'Transaction was not processed - Reached limit of retry attempts allowed',
			'S': 'Duplicate SessionID',
			'T': 'Address Verification Failed',
			'U': 'Card Security Code Failed ',
			'V': 'Address Verification and Card Security Code Failed ',
		}
		this.ngxService.stop();

		this.text = responseCode[this.responseCode];
	}

	routeToSpecificPage() {
		const planAcquired = this.session.get(StorageType.ACQUIRED_PLAN);

		if(planAcquired.plan === 'PrimeSecureLite') {
			this.routerlink.navigate(['prime-secure-lite/pay']);
		} else if(planAcquired.plan === 'PrimeCare') {
			this.routerlink.navigate(['prime-care/pay']);
		} else {
			// Navigate to prime secure pay page
		}
	}

	routeToHomePage() {
		this.routerlink.navigate(['/']);
	}

}
