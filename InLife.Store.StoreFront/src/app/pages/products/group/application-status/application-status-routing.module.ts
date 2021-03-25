import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ApplicationStatusComponent } from './application-status.component';
import { OtpConfirmationComponent } from './otp-confirmation/otp-confirmation.component';
import { RequirementsPendingComponent } from './requirements-pending/requirements-pending.component';
import { PaymentPendingComponent } from './payment-pending/payment-pending.component';
import { CancelApplicationComponent } from './cancel-application/cancel-application.component';
import { PaymentConfirmationComponent } from './payment-confirmation/payment-confirmation.component';
import { ApplicationCancelledComponent } from './application-cancelled/application-cancelled.component';

const routes: Routes = [
    {
    path: '',
    component: ApplicationStatusComponent,
    children: [{
      path: ':referenceCode',
      children: [
        { path: '', redirectTo: 'otp-confirmation', pathMatch: 'full' },
        { path: 'otp-confirmation', component: OtpConfirmationComponent },
        { path: 'requirements-pending', component: RequirementsPendingComponent },
        { path: 'payment-pending', component: PaymentPendingComponent },
        { path: 'payment-confirm', component: PaymentConfirmationComponent },
        { path: 'cancel-application', component: CancelApplicationComponent },
        { path: 'application-cancelled', component: ApplicationCancelledComponent },
      ]
    }]
  }
];

@NgModule({
  imports: [ RouterModule.forChild(routes) ],
  exports: [ RouterModule ]
})
export class ApplicationStatusRoutingModule {}
