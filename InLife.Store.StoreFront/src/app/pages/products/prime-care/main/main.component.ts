import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ApiService, SessionStorageService } from '@app/services';
import { DomSanitizer } from '@angular/platform-browser';
import { StorageType } from '@app/services/storage-types.enum';

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
		this.session.remove(StorageType.ACQUIRED_PLAN);

		if(this.activateRoute.snapshot.fragment === 'ihc')
			this.scroll();

	}

	getPrimeHeroSlider()
	{
		var url = "/PrimeHero/GetPrimeHeroSliders";
		this.apiService.sendGetRequest(url).subscribe((responseBody) =>
		{
			this.slider = responseBody[0];
		});
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
		return "url('data:image/png;base64," + url + "')";
	}

	scroll()
	{
		setTimeout(function ()
		{
			document.querySelector('#ihc').scrollIntoView
			({
				behavior: 'smooth'
			});
		}, 100);
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