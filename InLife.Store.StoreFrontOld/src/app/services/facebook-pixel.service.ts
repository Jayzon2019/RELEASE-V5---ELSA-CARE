import { Injectable } from '@angular/core';

declare let fbq: Function;

@Injectable
({
	providedIn: 'root'
})
export class FacebookPixelService
{
	constructor() { }

	public track
	(
		eventName: string,
		eventParams: any = null
	)
	{
		console.log('FacebookPixelService.track');
		console.log(eventName);
		console.log(eventParams);

		fbq('track', eventName, eventParams);
	}
}
