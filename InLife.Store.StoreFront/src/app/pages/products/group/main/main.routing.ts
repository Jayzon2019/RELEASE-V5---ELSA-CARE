import { PaymentPendingModule } from './../application-status/payment-pending/payment-pending.module';
import { RequirementsPendingComponent } from './../application-status/requirements-pending/requirements-pending.component';
import { PaymentPendingComponent } from './../application-status/payment-pending/payment-pending.component';
import { PaymentConfirmationComponent } from './../application-status/payment-confirmation/payment-confirmation.component';
import { CancelApplicationComponent } from './../application-status/cancel-application/cancel-application.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ApplyComponent } from '../apply/apply.component';
import { MakePaymentComponent } from '../pay/make-payment.component';
import { QuoteComponent } from '../quote/quote.component';
import { MainComponent } from './main.component';
import { ReferenceMessageComponent } from '../shared/application-reference-message/reference-message.component';
import { OtpConfirmationComponent } from '../application-status/otp-confirmation/otp-confirmation.component';
import { ApplicationCancelledComponent } from '../application-status/application-cancelled/application-cancelled.component';
const routes: Routes =
[
	{ path: '', component: MainComponent },
	{ path:'quote', component:QuoteComponent },
	{ path:'apply', component:ApplyComponent },
	{ path:'pay', component: MakePaymentComponent },

	// Application status checking
	{ path: 'application-reference/:referenceCode', component: ReferenceMessageComponent},
	{ path: 'application-status/otp-confirmation', component: OtpConfirmationComponent },
	{ path: 'application-status/requirements-pending', loadChildren: () => import('@app/pages/products/group/application-status/requirements-pending/requirements-pending.module').then(m => m.RequirementsPendingModule)},
	{ path: 'application-status/payment-pending', loadChildren: () => import('@app/pages/products/group/application-status/payment-pending/payment-pending.module').then(m => m.PaymentPendingModule)},
	
	{ path: 'application-status/payment-confirm', loadChildren: () => import('@app/pages/products/group/application-status/payment-confirmation/payment-confirmation.module').then(m => m.PaymentConfirmationModule)},
	{ path: 'application-status/cancel-application', loadChildren: () => import('@app/pages/products/group/application-status/cancel-application/cancel-application.module').then(m => m.CancelApplicationModule)},
	{ path: 'application-status/application-cancelled', loadChildren: () => import('@app/pages/products/group/application-status/application-cancelled/application-cancelled.module').then(m => m.ApplicationCancelledModule)},

	{ path: 'plan-summary', loadChildren: () => import('@app/pages/products/group/plan-summary/plan-summary.module').then(m => m.PlanSummaryModule) },
	{ path: 'thank-you', loadChildren: () => import('@app/pages/products/group/thank-you/thank-you.module').then(m => m.ThankYouModule) },

	
];

@NgModule
({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule]
})
export class MainRoutingModule { }
