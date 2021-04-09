import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CancalApplicationComponent } from './pages/cancal-application/cancal-application.component';
import { SaveQuoteComponent } from './pages/save-quote/save-quote.component';
import { PaymentRecievedComponent } from './pages/payment-recieved/payment-recieved.component';
import { MainLayoutComponent, GetProductLayoutComponent } from '@app/layout';
import {  HeroSliderResolver } from './resolvers/home-sliders.resolver';
import { PaymentCallbackComponent } from './pages/payment/payment-callback/payment-callback.component';
import { PaymentFailedComponent } from './pages/payment/payment-failed/payment-failed.component';
import { ThankYouComponent } from './shared/component/thank-you/thank-you.component';

const routes: Routes =
	[
		{ path: 'api-tester', loadChildren: () => import('@app/pages/api-tester/api-tester.module').then(m => m.ApiTesterModule) },
		{ path: 'cancel-application', component: CancalApplicationComponent },
		{ path: 'save-quote', component: SaveQuoteComponent },
		{ path: 'payment-received', component: PaymentRecievedComponent },
		// { path: 'thank-you', loadChildren: '@app/pages/thank-you.module#ThankYouModule' },
		{ path: 'thank-you', component: ThankYouComponent},
		{ path: 'payment-callback', component: PaymentCallbackComponent },
		{ path: 'payment-failed/:id', component: PaymentFailedComponent },
		{
			path: '',
			component: MainLayoutComponent,
			children:
				[
					{ path: '', loadChildren: () => import('@app/pages/home/home.module').then(m => m.HomeModule), 
						  resolve: {
						 	HeroSliders: HeroSliderResolver
						  } 
					},
					{ path: 'faqs', loadChildren: () => import('@app/pages/faqs/faqs.module').then(m => m.FaqsModule) },
					{ path: 'prime-care', loadChildren: () => import('@app/pages/products/prime-care/main/main.module').then(m => m.MainModule) },
					// { path: 'prime-secure',loadChildren: () => import('@app/pages/products/prime-secure/main/main.module').then(m => m.MainModule) },
					{ path: 'prime-secure-lite',loadChildren: () => import('@app/pages/products/prime-secure-lite/main/main.module').then(m => m.MainModule) },
					//{ path: 'group',loadChildren: () => import('@app/pages/products/group/main/main.module').then(m => m.MainModule) },
					{ path: 'feedback', loadChildren: () => import('@app/pages/feedback/feedback.module').then(m => m.FeedbackModule) },
					{ path: 'prime-care/ineligible', loadChildren: () => import('@app/pages/products/prime-care/ineligible/ineligible.module').then(m => m.IneligibleModule) },
				]
		},
		{
			path: '',
			component: GetProductLayoutComponent,
			children:
				[
					{ path: 'prime-care/quote', loadChildren: () => import('@app/pages/products/prime-care/quote/quote.module').then(m => m.QuoteModule) },
					{ path: 'prime-care/apply', loadChildren: () => import('@app/pages/products/prime-care/apply/apply.module').then(m => m.ApplyModule) },
					{ path: 'prime-care/pay', loadChildren: () => import('@app/pages/products/prime-care/pay/pay.module').then(m => m.PayModule) },
					
					{ path: 'prime-secure-lite/quote', loadChildren: () => import('@app/pages/products/prime-secure-lite/quote/quote.module').then(m => m.QuoteModule)},
					{ path: 'prime-secure-lite/apply', loadChildren: () => import('@app/pages/products/prime-secure-lite/apply/apply.module').then(m => m.ApplyModule) },
					{ path: 'prime-secure-lite/pay', loadChildren: () => import('@app/pages/products/prime-secure-lite/pay/pay.module').then(m => m.PayModule) },
					{ path: 'prime-secure-lite/ineligible', loadChildren: () => import('@app/pages/products/prime-secure-lite/ineligible/ineligible.module').then(m => m.IneligibleModule) },
					// { path: 'prime-secure-lite/thank-you', loadChildren: () => import('@app/pages/products/prime-secure-lite/thank-you/thank-you.module').then(m => m.ThankYouModule) },
					{ path: 'prime-care/**', redirectTo: 'prime-care' },
					{ path: 'prime-secure-lite/**', redirectTo: 'prime-secure-lite' },
					

					//{ path: 'group/quote',      loadChildren: () => import('@app/pages/products/group/quote/quote.module').then(m => m.QuoteModule) },
					//{ path: 'group/apply',      loadChildren: () => import('@app/pages/products/group/apply/apply.module').then(m => m.ApplyModule) },
					//{ path: 'group/pay', loadChildren: () => import('@app/pages/products/group/pay/make-payment.component').then(m => m.MakePaymentComponent) },
					//{ path: 'group/thankYou', loadChildren: () => import('@app/pages/products/group/thank-you/thank-you.component').then(m => m.ThankYouComponent) },
					//{ path: 'group/planSummary', loadChildren: () => import('@app/pages/products/group/plan-summary/plan-summary.component').then(m => m.PlanSummaryComponent) },
					//{ path: 'group/ineligible', loadChildren: () => import('@app/pages/products/group/ineligible/ineligible.module').then(m => m.IneligibleModule) },
					//{ path: 'group/**',         redirectTo: 'group' },

					
				]
		},

		{ path: '**', redirectTo: '' }

		//{ path: 'not-found', component: NotFoundComponent, data: { message: 'Sorry, an error has occured, Requested page not found!' } },
		//{ path: '**', redirectTo: 'not-found' }
	];

@NgModule
	({
		imports: [RouterModule.forRoot(routes, { enableTracing: false, scrollPositionRestoration: 'enabled'})],

exports: [RouterModule]
	})
export class AppRoutingModule { }
