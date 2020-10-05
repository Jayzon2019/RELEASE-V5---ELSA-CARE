import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { ApiService } from '../app/services/api.service';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ApiTesterComponent } from './api-tester/api-tester.component';
import { GetQuoteComponent } from './get-quote/get-quote.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { MakePaymentComponent } from './make-payment/make-payment.component';
import { ApplyComponent } from './apply/apply.component';
import { InnerHeaderComponent } from './common/inner-header/inner-header.component';
import { InnerSidebarComponent } from './common/inner-sidebar/inner-sidebar.component';
import { MatNativeDateModule } from '@angular/material/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DefaultComponent } from './default/default.component';
import { PrimeCareComponent } from './prime-care/prime-care.component';
import { IneligibleComponent } from './ineligible/ineligible.component';
import { ThankYouComponent } from './thank-you/thank-you.component';
import { MatTooltipModule } from '@angular/material/tooltip'
import { AngularWebStorageModule } from 'angular-web-storage';
import { NgxUiLoaderModule, NgxUiLoaderConfig, SPINNER } from 'ngx-ui-loader';
import { PaymentCallbackComponent } from './payment-callback/payment-callback.component';
import { PaymentFailedComponent } from './payment-failed/payment-failed.component';

import { GoogleAnalyticsService } from './services/google-analytics.service';
import { FacebookPixelService } from './services/facebook-pixel.service';

const ngxUiLoaderConfig: NgxUiLoaderConfig =
{
	"bgsColor": "#e35730",
	"bgsOpacity": 0.5,
	"bgsPosition": "bottom-right",
	"bgsSize": 60,
	"bgsType": "ball-spin-clockwise",
	"blur": 5,
	"delay": 0,
	"fastFadeOut": true,
	"fgsColor": "#e35730",
	"fgsPosition": "center-center",
	"fgsSize": 60,
	"fgsType": "double-bounce",
	"gap": 24,
	"logoPosition": "center-center",
	"logoSize": 120,
	"logoUrl": "",
	"masterLoaderId": "master",
	"overlayBorderRadius": "0",
	"overlayColor": "rgba(255, 255, 255, 0.8)",
	"pbColor": "red",
	"pbDirection": "ltr",
	"pbThickness": 3,
	"hasProgressBar": false,
	"text": "",
	"textColor": "#FFFFFF",
	"textPosition": "center-center",
	"maxTime": -1,
	"minTime": 300
};

@NgModule
({
	declarations:
	[
		AppComponent,
		ApiTesterComponent,
		GetQuoteComponent,
		NotFoundComponent,
		MakePaymentComponent,
		ApplyComponent,
		InnerHeaderComponent,
		InnerSidebarComponent,
		DefaultComponent,
		PrimeCareComponent,
		IneligibleComponent,
		ThankYouComponent,
		PaymentCallbackComponent,
		PaymentFailedComponent
	],
	imports:
	[
		MatDatepickerModule,
		MatNativeDateModule,
		BrowserAnimationsModule,
		AngularWebStorageModule,
		BrowserModule,
		AppRoutingModule,
		FormsModule,
		ReactiveFormsModule,
		HttpClientModule,
		MatTooltipModule,
		NgxUiLoaderModule.forRoot(ngxUiLoaderConfig)
	],
	providers:
	[
		ApiService,
		GoogleAnalyticsService,
		FacebookPixelService
	],
	bootstrap: [AppComponent]
})
export class AppModule { }
