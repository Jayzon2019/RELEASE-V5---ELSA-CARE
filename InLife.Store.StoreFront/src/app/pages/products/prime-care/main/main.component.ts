import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ApiService, SessionStorageService } from '@app/services';
import { DomSanitizer } from '@angular/platform-browser';
import { StorageType } from '@app/services/storage-types.enum';
import { CONSTANTS } from '@app/services/constants';

@Component
({
	templateUrl: './main.component.html',
	styleUrls: ['./style.css']
})
export class MainComponent implements OnInit
{
	public slider: any;
	public heroBg: any;
	public mobHeroBg: any;
	planCarouselMobile: any = {
		center: false,
		items: 1,
		loop: false,
		margin: 20,
		dots: true,
		nav: true,
		responsive: {
			320: {
				items: 1,
			},
			768: {
				items: 1,
			}
		}
	}
	constructor(private router: Router, private activateRoute: ActivatedRoute, private apiService: ApiService, private sanitizer: DomSanitizer, private session: SessionStorageService) { }

	ngOnInit(): void
	{
		this.getPrimeHeroSlider();
		// Remove policy no for verification of new application
		this.session.remove(StorageType.POLICYNO);
		this.session.remove('age');
		this.session.remove('getinnerForm');
		this.session.remove('getQuoteForm');
		this.session.remove('getApplyForm');
		this.session.remove('extensionData');
		this.session.remove('insuredIdentityDocumentImageData');
		this.session.remove('insuredIdentityDocumentImagePreview');
		this.session.remove(StorageType.ACQUIRED_PLAN);
		this.session.remove(StorageType.APPLY_PC_DATA);

		if(this.activateRoute.snapshot.fragment === 'ihc')
			this.scroll();

	}

	getPrimeHeroSlider()
	{
		// var url = "/PrimeHero/GetPrimeHeroSliders";
		// this.apiService.sendGetRequest(url).subscribe((responseBody) =>
		// {
		// 	this.slider = responseBody[0];
		// });

		this.slider = CONSTANTS.HERO_SLIDERS[2];
	}

	sanitize(url: string)
	{
		if (url)
		{
			return this.sanitizer.bypassSecurityTrustResourceUrl(`data:image/png;base64, ${url}`);
		}
	}

	getUrl(url: string)
	{
		return "url('" + url + "')";
	}

	scroll()
	{
		setTimeout(function ()
		{
			document.querySelector('#ihc').scrollIntoView
			({
				behavior: 'smooth'
			});
		}, 1000);
	}

	getColor(color: string)
	{
		if (window.innerWidth < 768)
		{
			color = "#ffffff";
		}
		return color;
	}

	getPosition(position: string)
	{
		if (position != undefined && (position == "Right" && window.innerWidth > 768))
		{
			return "position: relative; left:700px;";
		}
	}

}
