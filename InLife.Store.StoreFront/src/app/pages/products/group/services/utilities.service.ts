import { SessionStorageService } from '@app/services';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, map } from 'rxjs/operators';
import { ApiBaseService } from './api-base.service';
import { MatDialog } from '@angular/material/dialog';
import { PromptMessageComponent } from '../shared/prompt-message/prompt-message.component';

@Injectable()
export class UtilitiesService extends ApiBaseService
{

	constructor(private http: HttpClient,session: SessionStorageService, private dialog: MatDialog)
	{
        super(session);
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
}
