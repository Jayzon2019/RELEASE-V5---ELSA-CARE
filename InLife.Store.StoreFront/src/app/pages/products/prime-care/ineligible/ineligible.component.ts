import { Component, OnInit } from '@angular/core';
import { NgxUiLoaderService } from 'ngx-ui-loader';

@Component({
	selector: 'app-ineligible',
	templateUrl: './ineligible.component.html',
	styleUrls: ['./ineligible.component.scss']
})
export class IneligibleComponent implements OnInit
{
	constructor(private ngxService: NgxUiLoaderService) { 
		this.ngxService.start();
	}

	ngOnInit(): void
	{
		this.ngxService.stopAll();
	}
}
