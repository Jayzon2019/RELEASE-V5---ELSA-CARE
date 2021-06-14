import { Component, OnInit } from '@angular/core';
import { FacebookPixelService } from '@app/services';
import { NgxUiLoaderService } from 'ngx-ui-loader';

@Component({
	selector: 'app-ineligible',
	templateUrl: './ineligible.component.html',
	styleUrls: ['./ineligible.component.scss']
})
export class IneligibleComponent implements OnInit
{
	constructor(private ngxService: NgxUiLoaderService, private facebookPixelService: FacebookPixelService) { 
		this.ngxService.start();
	}

	ngOnInit(): void
	{
		this.facebookPixelService.track('ViewContent');
		this.facebookPixelService.track('Ineligible');
		this.ngxService.stopAll();
	}
}
