import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Subject } from 'rxjs';
import { GeneralMessagePromptComponent } from '../component/general-message-prompt/general-message-prompt.component';
import { MatDialog } from '@angular/material/dialog';
import { PromptMessageComponent } from '../component/prompt-message/prompt-message.component';

@Injectable()
export class UtilitiesService
{
	headers_object;
	public sidebar_value = new Subject();
	public isShowFooterandHeader$: Subject<boolean> = new Subject<boolean>();

	constructor(private http: HttpClient, private dialog: MatDialog)
	{
	}


    openNewWindow(url: string) {
        const dialogRef = this.dialog.open(PromptMessageComponent, {
			width: '300px',
			data: {url: url}
		});
        dialogRef.afterClosed().pipe().subscribe(data => {
            if(data) {
                window.open(url, '_blank');
            }
        })
    }

    ShowGeneralMessagePrompt(data: any){
        const dialogRef = this.dialog.open(GeneralMessagePromptComponent, {
            width: '300px',
            data: {
                message: data.message
            }
        });
    }

}
