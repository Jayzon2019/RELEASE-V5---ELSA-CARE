import { Component, PLATFORM_ID, APP_ID, Inject } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { Router, NavigationEnd, NavigationStart } from '@angular/router';
import { GoogleAnalyticsService } from './services/google-analytics.service';
import { FacebookPixelService } from './services/facebook-pixel.service';
import { NgxUiLoaderService } from 'ngx-ui-loader';

declare let gtag: Function;
declare let fbq: Function;

@Component
({
	selector: 'app-root',
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.css']
})
export class AppComponent
{
	title = 'InLife Store';

	constructor
		(
		@Inject(PLATFORM_ID) private platformId: Object,
		@Inject(APP_ID) private appId: string,
		public router: Router,
		public googleAnalyticsService: GoogleAnalyticsService,
		public facebookPixelService: FacebookPixelService,
		private ngxService: NgxUiLoaderService
	)
	{
		this.router.events.subscribe(event =>
		{
			if (event instanceof NavigationEnd)
			{
				console.log('Router NavigationEnd:');
				console.log(event.urlAfterRedirects);
				this.ngxService.stopAll();

				//gtag('config', 'UA-173350504-1',
				//{
				//	'page_path': event.urlAfterRedirects
				//});
				this.googleAnalyticsService.config
					({
						'page_path': event.urlAfterRedirects
					});

				//fbq('track', 'PageView');
				this.facebookPixelService.track('PageView');
			} else if (event instanceof NavigationStart) {
				if(event.id !== 1) { // if not first load
					this.ngxService.start();
				}
				
			}
		});
	}

	onActivate(event: any)
	{
		if (isPlatformBrowser(this.platformId))
		{
			let scrollToTop = window.setInterval(() =>
			{
				let pos = window.pageYOffset;
				if (pos > 0)
				{
					window.scrollTo(0, pos - 50); // how far to scroll on each step
				}
				else
				{
					window.clearInterval(scrollToTop);
				}
			}, 16);
		}
	}
}
