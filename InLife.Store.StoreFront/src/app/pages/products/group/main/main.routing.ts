import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ApplyComponent } from '../apply/apply.component';
import { IneligibleComponent } from '../ineligible/ineligible.component';
import { MakePaymentComponent } from '../pay/make-payment.component';
import { QuoteComponent } from '../quote/quote.component';

import { MainComponent } from './main.component';
import { ReferenceMessageComponent } from '../shared/application-reference-message/reference-message.component';
import { PlanSummaryComponent } from '../plan-summary/plan-summary.component';
import { ThankYouComponent } from '../thank-you/thank-you.component';
const routes: Routes =
[
	{ 
		path: '', 
		component: MainComponent, 
		// resolve: {
		// 	GroupHeroSliders: GroupHomeResolver
		// } 
	},
	{
		path:'quote',
		component:QuoteComponent
	},
	{
		path:'apply',
		component:ApplyComponent
	},
	{
		path:'pay',
		component: MakePaymentComponent
	},
	{ path: 'application-status', loadChildren: () => import('@app/pages/products/group/application-status/application-status.module').then(m => m.ApplicationStatusModule) },
	{ path: 'application-reference/:referenceCode', component: ReferenceMessageComponent},
	
	{ path: 'plan-summary', loadChildren: () => import('@app/pages/products/group/plan-summary/plan-summary.module').then(m => m.PlanSummaryModule) },
	{ path: 'thank-you', loadChildren: () => import('@app/pages/products/group/thank-you/thank-you.module').then(m => m.ThankYouModule) },


];

@NgModule
({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule]
})
export class MainRoutingModule { }
