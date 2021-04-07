import { NgModule } from "@angular/core";
import { MatDialogModule } from "@angular/material/dialog";
import { GeneralMessagePromptComponent } from "./component/general-message-prompt/general-message-prompt.component";
import { UtilitiesService } from "./services/utilities.service";

@NgModule({
  imports: [
    MatDialogModule
  ],
  declarations: [
    GeneralMessagePromptComponent
  ],
  exports: [
    GeneralMessagePromptComponent
  ],
  providers: [
    UtilitiesService
  ],
  entryComponents: [GeneralMessagePromptComponent]
})
export class SharedModule { }
