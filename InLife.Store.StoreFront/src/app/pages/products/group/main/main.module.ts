import { UtilitiesService } from './../services/utilities.service';
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
import { PromptMessageComponent } from '../shared/prompt-message/prompt-message.component';
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
		PromptMessageComponent
	],
	providers:
	[
		ApiService,
		SessionStorageService,
		GoogleAnalyticsService,
		FacebookPixelService,
		UtilitiesService,

		ApplicationStatusService,
		ApplyService,
		QuoteService,
		PayService,

		HeroSliderResolver
		
	],
	bootstrap: [MainComponent]
})
export class MainModule { }
