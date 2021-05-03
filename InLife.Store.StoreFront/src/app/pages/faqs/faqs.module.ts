import { NgModule } from "@angular/core";
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgxUiLoaderModule, NgxUiLoaderConfig, SPINNER } from 'ngx-ui-loader';

import
{
	ApiService,
	SessionStorageService,
	GoogleAnalyticsService,
	FacebookPixelService
} from '@app/services';

import { FaqsComponent } from './faqs.component';
import { FaqsRoutingModule } from './faqs.routing';

@NgModule
({
	imports:
	[
		CommonModule,
		HttpClientModule,
		FaqsRoutingModule
	],
	declarations:
	[
		FaqsComponent
	],
	providers:
	[
		ApiService,
		SessionStorageService,
		GoogleAnalyticsService,
		FacebookPixelService
	],
	bootstrap: [FaqsComponent]
})
export class FaqsModule { }
