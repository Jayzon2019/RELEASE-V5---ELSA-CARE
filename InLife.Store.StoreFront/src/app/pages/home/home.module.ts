import { NgModule } from "@angular/core";
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgxUiLoaderModule, NgxUiLoaderConfig, SPINNER } from 'ngx-ui-loader';
import { SlickCarouselModule } from 'ngx-slick-carousel';

import {
	ApiService,
	SessionStorageService,
	GoogleAnalyticsService,
	FacebookPixelService
} from '@app/services';

import { HomeComponent } from './home.component';
import { HomeRoutingModule } from './home.routing';

@NgModule
	({
		imports:
			[
				CommonModule,
				SlickCarouselModule,
				HttpClientModule,
				HomeRoutingModule
			],
		declarations:
			[
				HomeComponent
			],
		providers:
			[
				ApiService,
				SessionStorageService,
				GoogleAnalyticsService,
				FacebookPixelService
			],
		bootstrap: [HomeComponent]
	})
export class HomeModule { }
