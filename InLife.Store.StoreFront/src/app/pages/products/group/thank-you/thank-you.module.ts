import { NgModule } from "@angular/core";
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgxUiLoaderModule, NgxUiLoaderConfig, SPINNER } from 'ngx-ui-loader';
import { Routes, RouterModule } from '@angular/router';
import
{
	ApiService,
	SessionStorageService,
	GoogleAnalyticsService,
	FacebookPixelService
} from '@app/services';

import { ThankYouComponent } from './thank-you.component';
import { ControlsModule } from '@app/controls/controls.module';
const routes: Routes =
[
	{ path: '', component: ThankYouComponent },

];
@NgModule
({
	imports:
	[
		CommonModule,
			HttpClientModule,
			ControlsModule,
			RouterModule.forChild(routes)
	],
	declarations:
		[
			ThankYouComponent
	],
	providers:
	[
		ApiService,
		SessionStorageService,
		GoogleAnalyticsService,
		FacebookPixelService
		],
	bootstrap: [ThankYouComponent]
})
export class ThankYouModule { }
