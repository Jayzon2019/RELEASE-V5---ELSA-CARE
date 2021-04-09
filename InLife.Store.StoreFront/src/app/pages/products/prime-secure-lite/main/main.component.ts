import { SessionStorageService } from '@app/services';
import { Component, OnInit } from '@angular/core';
import { OwlOptions } from 'ngx-owl-carousel-o';
import { Router } from '@angular/router';
import { ApiService } from '../../../../services/api.service';
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

	customOptions: OwlOptions =
		{
		center: true,
		items: 2,
		loop: false,
		margin: 20,
		dots: true,
		slideBy: 2,
		responsive:
		{
			320:
			{
				items: 4,
			}
		},
		nav: true
	}

	planCarousel: any =
	{
		center: true,
		items: 1,
		loop: false,
		margin: 20,
		dots: true,
		nav: true,
		responsive:
		{
			320:
			{
				items: 1,
				stagePadding: 0,
				center: false,
			},
			1025:
			{
				items: 1,
				center: false,
				margin: 50,
			},
			1701:
			{
				items: 2
			}
		}
	}

	planCarouselMobile: any =
	{
		center: false,
		items: 1,
		loop: false,
		margin: 20,
		dots: true,
		nav: true,
		responsive:
		{
			320:
			{
				items: 1,
			},
			768:
			{
				items: 1,
			}
		}
	}

	OwlOptions: any =
	{
		center: true,
		items: 2,
		loop: false,
		margin: 20,
		dots: true,
		nav: true,
		slideBy: 2,
		responsive:
		{
			320:
			{
				items: 4,
			}
		}
	}

	constructor(private router: Router, private apiService: ApiService, private sanitizer: DomSanitizer, private session: SessionStorageService) { }

	ngOnInit(): void
	{
		this.getPrimeSecureLiteHeroSlider();
		// Remove policy no for verification of new application
		this.session.remove(StorageType.POLICYNO);
		this.session.remove('UnderWritingStatus');
		this.session.remove('age');
		this.session.remove('getinnerForm');
		this.session.remove(StorageType.QUOTE_INTERNAL_DATA);
		this.session.remove(StorageType.QUOTE_EXTERNAL_DATA);
		this.session.remove(StorageType.ACQUIRED_PLAN);
	}

	getPrimeSecureLiteHeroSlider()
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
