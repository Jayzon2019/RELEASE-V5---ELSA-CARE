import { PaymentFailedComponent } from './pages/payment/payment-failed/payment-failed.component';
import { PaymentCallbackComponent } from './pages/payment/payment-callback/payment-callback.component';
import { HeroSliderResolver } from './resolvers/home-sliders.resolver';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgxUiLoaderModule, NgxUiLoaderConfig, SPINNER } from 'ngx-ui-loader';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app.routing';
import { AppComponent } from './app.component';
import { CarouselModule } from 'ngx-owl-carousel-o';
import { MainLayoutComponent, GetProductLayoutComponent } from '@app/layout';
import
{
	ApiService,
	SessionStorageService,
	GoogleAnalyticsService,
	FacebookPixelService
} from '@app/services';
import { CancalApplicationComponent } from './pages/cancal-application/cancal-application.component';
import { SaveQuoteComponent } from './pages/save-quote/save-quote.component';
import { PaymentRecievedComponent } from './pages/payment-recieved/payment-recieved.component';
import { ControlsModule } from './controls/controls.module';
import { CryptographyService } from './services/cryptography.service';
import { SharedModule } from './shared/shared.module';
import { CommonModule } from '@angular/common';
import { ThankYouComponent } from './pages/thank-you/thank-you.component';

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
		MainLayoutComponent,
		GetProductLayoutComponent,
		CancalApplicationComponent,
		SaveQuoteComponent,
		PaymentRecievedComponent,
		PaymentCallbackComponent,
		PaymentFailedComponent,
		ThankYouComponent
	],
	imports:
	[
		// CommonModule,
		BrowserModule,
		BrowserAnimationsModule,
		CarouselModule,
		AppRoutingModule,
		HttpClientModule,
		ControlsModule,
		SharedModule,
		NgxUiLoaderModule.forRoot(ngxUiLoaderConfig),
	],
	providers:
	[
		ApiService,
		SessionStorageService,
		CryptographyService,
		GoogleAnalyticsService,
		FacebookPixelService,
		HeroSliderResolver
	],
	bootstrap: [AppComponent]
})
export class AppModule { }
