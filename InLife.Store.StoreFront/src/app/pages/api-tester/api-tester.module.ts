import { NgModule } from "@angular/core";
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { ApiTesterComponent } from './api-tester.component';
import { ApiTesterRoutingModule } from './api-tester.routing';

@NgModule
({
	imports:
	[
		CommonModule,
		HttpClientModule,
		ReactiveFormsModule,
		ApiTesterRoutingModule
	],
	declarations:
	[
		ApiTesterComponent
	],
	providers: [ ],
	bootstrap: [ApiTesterComponent]
})
export class ApiTesterModule { }
