import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { ApplicationStatusComponent } from './application-status.component';
import { ApplicationStatusRoutingModule } from './application-status-routing.module';

import { OtpConfirmationComponent } from './otp-confirmation/otp-confirmation.component';
import { RequirementsPendingComponent } from './requirements-pending/requirements-pending.component';
import { PaymentPendingComponent } from './payment-pending/payment-pending.component';
import { CancelApplicationComponent } from './cancel-application/cancel-application.component';
import { PaymentConfirmationComponent } from './payment-confirmation/payment-confirmation.component';
import { ApplicationCancelledComponent } from './application-cancelled/application-cancelled.component';
import { ApplyService } from '../services/apply.service';

@NgModule({
  imports: [ 
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    ApplicationStatusRoutingModule
  ],
  declarations: [ 
    ApplicationStatusComponent,
    OtpConfirmationComponent,
    RequirementsPendingComponent,
    PaymentPendingComponent,
    CancelApplicationComponent,
    PaymentConfirmationComponent,
    ApplicationCancelledComponent,
  ],
  providers: [
    ApplyService
  ]
})
export class ApplicationStatusModule { }