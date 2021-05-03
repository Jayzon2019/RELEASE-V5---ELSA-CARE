import { ApplicationCancelledComponent } from './application-cancelled.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Routes, RouterModule } from '@angular/router';

import { ControlsModule } from '@app/controls/controls.module';
const routes: Routes =
[
	{ path: '', component: ApplicationCancelledComponent },

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
			ApplicationCancelledComponent
	],
	providers: [],
	bootstrap: [ApplicationCancelledComponent]
})
export class ApplicationCancelledModule { }
