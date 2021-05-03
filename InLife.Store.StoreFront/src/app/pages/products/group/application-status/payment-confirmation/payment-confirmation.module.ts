import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Routes, RouterModule } from '@angular/router';

import { ControlsModule } from '@app/controls/controls.module';
import { PaymentConfirmationComponent } from './payment-confirmation.component';
const routes: Routes =
[
	{ path: '', component: PaymentConfirmationComponent },

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
        PaymentConfirmationComponent
	],
	providers: [],
	bootstrap: [PaymentConfirmationComponent]
})
export class PaymentConfirmationModule { }
