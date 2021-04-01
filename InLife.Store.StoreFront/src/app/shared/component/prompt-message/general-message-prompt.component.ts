import { Component, Inject } from "@angular/core";
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';

@Component({
    templateUrl: 'general-message-prompt.component.html',
    styleUrls: ['general-message-prompt.component.scss']
})
export class GeneralMessagePromptComponent {

    constructor(
        public dialogRef: MatDialogRef<GeneralMessagePromptComponent>,
        @Inject(MAT_DIALOG_DATA) public data: any) {}

    onNoClick(): void {
        this.dialogRef.close(false);
    }

    onYesClick() {
        this.dialogRef.close(true);
    }

}