import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { IneligibleComponent } from './ineligible.component';

const routes: Routes =
[
	{ path: '', component: IneligibleComponent }
];

@NgModule
({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule]
})
export class IneligibleRoutingModule { }
