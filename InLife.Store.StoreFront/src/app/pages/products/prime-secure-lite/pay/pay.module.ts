import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
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

import { PayComponent } from './pay.component';
import { PayRoutingModule } from './pay.routing';

import { ControlsModule } from '@app/controls/controls.module';

@NgModule
({
	imports:
	[
		CommonModule,
		ControlsModule,
		HttpClientModule,
		PayRoutingModule,
		MatDatepickerModule,
		MatNativeDateModule,
		FormsModule,
		ReactiveFormsModule,
		MatTooltipModule//,
		//NgxUiLoaderModule.forRoot(ngxUiLoaderConfig)
	],
	declarations:
	[
		PayComponent
	],
	providers:
	[
		ApiService,
		SessionStorageService,
		GoogleAnalyticsService,
		FacebookPixelService
	],
	bootstrap: [PayComponent]
})
export class PayModule { }
