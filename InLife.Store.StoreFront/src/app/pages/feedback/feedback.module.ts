import { CustomerFeedBackService } from './../../services/customer-feedback.service';
import { NgModule } from "@angular/core";
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { MatRadioModule } from '@angular/material/radio';

import
{
	ApiService,
	SessionStorageService,
	GoogleAnalyticsService,
	FacebookPixelService
} from '@app/services';

import { FeedbackComponent } from './feedback.component';
import { FeedbackRoutingModule } from './feedback.routing';
import { FormsModule } from "@angular/forms";
import { NgxUiLoaderService } from 'ngx-ui-loader';

@NgModule
({
	imports:
	[
		CommonModule,
		HttpClientModule,
		FormsModule,
		FeedbackRoutingModule,
		MatRadioModule
	],
	declarations:
	[
		FeedbackComponent
	],
	providers:
	[
		ApiService,
		SessionStorageService,
		GoogleAnalyticsService,
		FacebookPixelService,
		CustomerFeedBackService
	],
	bootstrap: [FeedbackComponent]
})
export class FeedbackModule { }
