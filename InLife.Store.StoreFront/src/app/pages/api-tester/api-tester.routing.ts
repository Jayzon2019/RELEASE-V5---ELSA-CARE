import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ApiTesterComponent } from './api-tester.component';

const routes: Routes =
[
	{ path: '', component: ApiTesterComponent }
];

@NgModule
({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule]
})
export class ApiTesterRoutingModule { }
