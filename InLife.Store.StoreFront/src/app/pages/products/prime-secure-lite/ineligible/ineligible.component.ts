import { Component, OnInit } from '@angular/core';
import { FacebookPixelService } from '@app/services';

@Component({
	selector: 'app-ineligible',
	templateUrl: './ineligible.component.html',
	styleUrls: ['./ineligible.component.scss']
})
export class IneligibleComponent implements OnInit
{
	constructor(private facebookPixelService: FacebookPixelService) { }

	ngOnInit(): void
	{
		this.facebookPixelService.track('ViewContent');
		this.facebookPixelService.track('Ineligible');
	}
}
