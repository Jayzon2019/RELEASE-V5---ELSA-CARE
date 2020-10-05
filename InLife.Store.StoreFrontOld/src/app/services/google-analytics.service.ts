import { Injectable } from '@angular/core';

declare let gtag: Function;

@Injectable
({
	providedIn: 'root'
})
export class GoogleAnalyticsService
{
	constructor() { }

	public config(params: any = null)
	{
		console.log('GoogleAnalyticsService.config');
		console.log(params);
		gtag('config', 'UA-173350504-1', params);
	}

	public eventEmitter
	(
		eventName: string,
		eventCategory: string,
		eventAction: string,
		eventLabel: string = null,
		eventValue: number = null
	)
	{
		console.log('GoogleAnalyticsService.eventEmitter');
		gtag('event', eventName,
		{
			eventCategory: eventCategory,
			eventAction: eventAction,
			eventLabel: eventLabel,
			eventValue: eventValue
		});
	}
}
