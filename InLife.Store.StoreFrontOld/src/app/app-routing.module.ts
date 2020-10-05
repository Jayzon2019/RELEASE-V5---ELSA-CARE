import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ApiTesterComponent } from './api-tester/api-tester.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { GetQuoteComponent } from './get-quote/get-quote.component';
import { ApplyComponent } from './apply/apply.component';
import { DefaultComponent } from './default/default.component';
import { PrimeCareComponent } from './prime-care/prime-care.component';
import { MakePaymentComponent } from './make-payment/make-payment.component';
import { ThankYouComponent } from './thank-you/thank-you.component';
import { IneligibleComponent } from './ineligible/ineligible.component';
import { PaymentCallbackComponent } from './payment-callback/payment-callback.component';
import { PaymentFailedComponent } from './payment-failed/payment-failed.component';

const routes: Routes = [
	// {
	// 	path: '',
	// 	redirectTo: '',
	// 	pathMatch: 'full'
	// },
	{
		path: '',
		component: DefaultComponent
	},
	 {
      path: 'payment-callback',
      component: PaymentCallbackComponent
    },
    {
      path: 'payment-failed/:id',
      component: PaymentFailedComponent
    },

	{
		path: 'prime-care',
		component: PrimeCareComponent
	},
	{
		path: 'get-quote',
		component: GetQuoteComponent
	},
	{
		path: 'apply',
		component: ApplyComponent
	},
	{
		path: 'make-payment',
		component: MakePaymentComponent
	},
	{
		path: 'thank-you',
		component: ThankYouComponent
	},
	{
		path: 'ineligible',
		component: IneligibleComponent
	},
	{
		path: 'api-tester',
		component: ApiTesterComponent
	},
	{ path: 'not-found', component: NotFoundComponent, data: { message: 'Sorry, an error has occured, Requested page not found!' } },
	{ path: '**', redirectTo: 'not-found' }
];

@NgModule({
	imports: [RouterModule.forRoot(routes, { useHash: true })],
	exports: [RouterModule]
})
export class AppRoutingModule { }
