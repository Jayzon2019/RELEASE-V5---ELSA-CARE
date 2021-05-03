import { CancelApplicationComponent } from './cancel-application.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Routes, RouterModule } from '@angular/router';

import { ControlsModule } from '@app/controls/controls.module';
const routes: Routes =
[
	{ path: '', component: CancelApplicationComponent },

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
			CancelApplicationComponent
	],
	providers: [],
	bootstrap: [CancelApplicationComponent]
})
export class CancelApplicationModule { }
