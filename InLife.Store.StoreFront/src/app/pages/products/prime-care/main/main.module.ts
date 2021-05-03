import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { HttpClientModule } from '@angular/common/http';
import { MatNativeDateModule } from '@angular/material/core';
import { NgxUiLoaderModule, NgxUiLoaderConfig, SPINNER } from 'ngx-ui-loader';
import { MatTooltipModule } from '@angular/material/tooltip'
import { CarouselModule } from 'ngx-owl-carousel-o';

import {
	ApiService,
	SessionStorageService,
	GoogleAnalyticsService,
	FacebookPixelService
} from '@app/services';

import { MainComponent } from './main.component';
import { MainRoutingModule } from './main.routing';
@NgModule
	({
		imports:
			[
				CommonModule,
				CarouselModule,
				HttpClientModule,
				MainRoutingModule,
				MatDatepickerModule,
				MatNativeDateModule,
				FormsModule,
				ReactiveFormsModule,
				MatTooltipModule//,
				//NgxUiLoaderModule.forRoot(ngxUiLoaderConfig)
			],
		declarations:
			[
				MainComponent
			],
		providers:
			[
				ApiService,
				SessionStorageService,
				GoogleAnalyticsService,
				FacebookPixelService
			],
		bootstrap: [MainComponent]
	})
export class MainModule { }
