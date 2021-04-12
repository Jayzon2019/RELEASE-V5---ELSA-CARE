import { ApplicationStatusService } from './../pages/products/group/services/application-status.service';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { PrimeCareThankYouComponent } from './component/primecare-thank-you/primecare-thank-you.component';
import { PSLiteThankYouComponent } from './component/pslite-thank-you/pslite-thank-you.component';
import { PSLiteService } from './services/pslite.servce';
import { NgModule } from "@angular/core";
import { MatDialogModule } from "@angular/material/dialog";
import { GeneralMessagePromptComponent } from "./component/general-message-prompt/general-message-prompt.component";
import { UtilitiesService } from "./services/utilities.service";
import { ThankYouComponent } from './component/thank-you/thank-you.component';

@NgModule({
  imports: [
    BrowserModule,
    MatDialogModule,
    RouterModule
  ],
  declarations: [
    ThankYouComponent,
    GeneralMessagePromptComponent,
    PSLiteThankYouComponent,
    PrimeCareThankYouComponent
  ],
  exports: [
    ThankYouComponent,
    GeneralMessagePromptComponent,
    PSLiteThankYouComponent,
    PrimeCareThankYouComponent
  ],
  providers: [
    UtilitiesService,
    PSLiteService,
    ApplicationStatusService
  ],
  entryComponents: [GeneralMessagePromptComponent]
})
export class SharedModule { }
