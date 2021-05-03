import {  HeroSliderResolver } from '../../../../resolvers/home-sliders.resolver';
import { ApplyService } from './../services/apply.service';
import { ApplicationStatusService } from './../services/application-status.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { HttpClientModule } from '@angular/common/http';
import { MatNativeDateModule } from '@angular/material/core';
import { NgxUiLoaderModule, NgxUiLoaderConfig, SPINNER } from 'ngx-ui-loader';
import { MatTooltipModule } from '@angular/material/tooltip'
import { MatIconModule } from '@angular/material/icon';
import
{
	ApiService,
	SessionStorageService,
	GoogleAnalyticsService,
	FacebookPixelService
} from '@app/services';

import { MainComponent } from './main.component';
import { MainRoutingModule } from './main.routing';
import { MakePaymentComponent } from '../pay/make-payment.component';
import { QuoteComponent } from '../quote/quote.component';
import { ApplyComponent } from '../apply/apply.component';

import { ReferenceMessageComponent } from '../shared/application-reference-message/reference-message.component';
import { QuoteService } from '../services/quote.service';
import { PayService } from '../services/pay.service';
import { PromptMessageComponent } from '../../../../shared/component/prompt-message/prompt-message.component';
import { ApplicationCancelledComponent } from '../application-status/application-cancelled/application-cancelled.component';
import { CancelApplicationComponent } from '../application-status/cancel-application/cancel-application.component';
import { OtpConfirmationComponent } from '../application-status/otp-confirmation/otp-confirmation.component';
import { PaymentConfirmationComponent } from '../application-status/payment-confirmation/payment-confirmation.component';
import { PaymentPendingComponent } from '../application-status/payment-pending/payment-pending.component';
import { RequirementsPendingComponent } from '../application-status/requirements-pending/requirements-pending.component';
@NgModule
({
	imports:
	[
		CommonModule,
		HttpClientModule,
		MainRoutingModule,
		MatDatepickerModule,
		MatNativeDateModule,
		FormsModule,
		ReactiveFormsModule,
		MatTooltipModule,
		MatIconModule//,
		//NgxUiLoaderModule.forRoot(ngxUiLoaderConfig)
	],
	declarations:
	[
		MainComponent,
		MakePaymentComponent,
		QuoteComponent,
		ApplyComponent,
		ReferenceMessageComponent,

		OtpConfirmationComponent
	],
	providers:
	[
		ApiService,
		SessionStorageService,
		GoogleAnalyticsService,
		FacebookPixelService,
		ApplicationStatusService,
		ApplyService,
		QuoteService,
		PayService,

		HeroSliderResolver
		
	],
	entryComponents: [],
	bootstrap: [MainComponent]
})
export class MainModule { }
