import { Component, Inject } from "@angular/core";
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';

@Component({
    templateUrl: 'prompt-message.component.html',
    styleUrls: ['prompt-message.component.scss']
})
export class PromptMessageComponent {

    constructor(
        public dialogRef: MatDialogRef<PromptMessageComponent>,
        @Inject(MAT_DIALOG_DATA) public data: any) {}

    onNoClick(): void {
        this.dialogRef.close(false);
    }

    onYesClick() {
        this.dialogRef.close(true);
    }

}