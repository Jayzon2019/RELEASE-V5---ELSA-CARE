import { NgModule } from '@angular/core';
import { CommonModule, CurrencyPipe } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { HttpClientModule } from '@angular/common/http';
import { MatNativeDateModule } from '@angular/material/core';
import { NgxUiLoaderModule, NgxUiLoaderConfig, SPINNER } from 'ngx-ui-loader';
import { MatTooltipModule } from '@angular/material/tooltip'

import
{
	ApiService,
	SessionStorageService,
	GoogleAnalyticsService,
	FacebookPixelService
} from '@app/services';

import { QuoteComponent } from './quote.component';
import { QuoteRoutingModule } from './quote.routing';

import { ControlsModule } from '@app/controls/controls.module';

@NgModule
({
	imports:
	[
		CommonModule,
		ControlsModule,
		HttpClientModule,
		QuoteRoutingModule,
		MatDatepickerModule,
		MatNativeDateModule,
		FormsModule,
		ReactiveFormsModule,
		MatTooltipModule//,
		//NgxUiLoaderModule.forRoot(ngxUiLoaderConfig)
	],
	declarations:
	[
		QuoteComponent
	],
	providers:
	[
		ApiService,
		SessionStorageService,
		GoogleAnalyticsService,
		FacebookPixelService,
		CurrencyPipe
	],
	bootstrap: [QuoteComponent]
})
export class QuoteModule { }
