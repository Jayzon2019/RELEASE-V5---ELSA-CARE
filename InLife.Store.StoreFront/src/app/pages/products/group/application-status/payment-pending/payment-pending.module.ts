import { PaymentPendingComponent } from './payment-pending.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { HttpClientModule } from '@angular/common/http';
import { MatNativeDateModule } from '@angular/material/core';
import { NgxUiLoaderModule, NgxUiLoaderConfig, SPINNER } from 'ngx-ui-loader';
import { MatTooltipModule } from '@angular/material/tooltip'
import { Routes, RouterModule } from '@angular/router';


import { ControlsModule } from '@app/controls/controls.module';
const routes: Routes =
[
	{ path: '', component: PaymentPendingComponent },

];
@NgModule
({
	imports:
	[
		CommonModule,
		ControlsModule,
		FormsModule,
		ReactiveFormsModule,
		RouterModule.forChild(routes)
	],
	declarations:
	[
			PaymentPendingComponent
	],
	providers:[],
	bootstrap: [PaymentPendingComponent]
})
export class PaymentPendingModule { }
