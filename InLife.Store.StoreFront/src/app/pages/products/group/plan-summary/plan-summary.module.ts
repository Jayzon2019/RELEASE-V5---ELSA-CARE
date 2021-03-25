import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { HttpClientModule } from '@angular/common/http';
import { MatNativeDateModule } from '@angular/material/core';
import { NgxUiLoaderModule, NgxUiLoaderConfig, SPINNER } from 'ngx-ui-loader';
import { MatTooltipModule } from '@angular/material/tooltip'
import { Routes, RouterModule } from '@angular/router';
import
{
	ApiService,
	SessionStorageService,
	GoogleAnalyticsService,
	FacebookPixelService
} from '@app/services';

import { PlanSummaryComponent } from './plan-summary.component';

import { ControlsModule } from '@app/controls/controls.module';
const routes: Routes =
[
	{ path: '', component: PlanSummaryComponent },

];
@NgModule
({
	imports:
	[
		CommonModule,
		ControlsModule,
		HttpClientModule,
		MatDatepickerModule,
		MatNativeDateModule,
		FormsModule,
		ReactiveFormsModule,
		MatTooltipModule,
		//NgxUiLoaderModule.forRoot(ngxUiLoaderConfig)
		RouterModule.forChild(routes)
	],
	declarations:
	[
			PlanSummaryComponent
	],
	providers:
	[
		ApiService,
		SessionStorageService,
		GoogleAnalyticsService,
		FacebookPixelService
	],
	bootstrap: [PlanSummaryComponent]
})
export class PlanSummaryModule { }
