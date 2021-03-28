import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { DomSanitizer } from '@angular/platform-browser';
import $ from 'jquery';

@Component
({
	templateUrl: './faqs.component.html'
})
export class FaqsComponent implements OnInit {
	public faqsCat: any;
	public faqs: any;
	public tabLinks: any;
	public tabcontent: any;
	public oldId: 0;
	public routeTab: any;
	public routeCategory: any;
	constructor(private apiService: ApiService, private sanitizer: DomSanitizer, private route: ActivatedRoute) { }

	ngOnInit(): void {
		this.getfaqCats();
		this.route.queryParams.subscribe(params =>
		{
			if(params?.tab) {
				this.routeTab = params.tab;
			}
		});

	}

	getfaqCats() {
		var url = "/Faq/GetFaqCatList";
		this.apiService.sendGetRequest(url).subscribe((responseBody) => {
			this.faqsCat = responseBody;
			this.getfaqs();
		});
	}

	getfaqs() {
		var url = "/Faq/GetFaqList";
		this.apiService.sendGetRequest(url).subscribe((responseBody) => {
			this.faqs = responseBody;
			this.setFaqs();
		});
	}

	setFaqs() {
		if (this.faqsCat.length > 0 && this.faqs.length > 0) {
			var faqTabs = "<div class=\"tab\">";
			for (var i = 0; i < this.faqsCat.length; i++) {
				// if(this.faqsCat[i].id !== 7 && this.faqsCat[i].id !== 5) { //hide group insurance and prime secure in faq
					var id = this.faqsCat[i].name;
					id = id.replace(/\s/g, '');
					faqTabs = faqTabs + "<button class=\"tablinks " + id + "\">" + this.faqsCat[i].name + "</button>"
				// }
			}
			faqTabs = faqTabs + "</div>";
			$(".tab-section").append(faqTabs);
			for (var i = 0; i < this.faqsCat.length; i++) {
				var cat = this.faqsCat[i].name;
				cat = cat.replace(/\s/g, '');
				var faq = "<div id=\"" + cat + "\" class=\"faqMainDiv tabcontent\">";

				for (var x = 0; x < this.faqs.length; x++) {
					var faqCid = this.faqsCat[i].id;
					var fCId = this.faqs[x].categoryId;
					if (fCId == faqCid) {
						var idChanged = this.checkIds(fCId);
						if (idChanged == true) {
							faq = faq + "<div class=\"palet row\"><div class=\"col-xs-12\"><h3 class=\"palet-title\"><a href=\"#faqs" + [x] + "\" onclick=\"return false;\" data-toggle=\"collapse\">" + this.faqs[x].question + "</a></h3>";
							faq = faq + "<div id=\"faqs" + [x] + "\" class=\"lh-35 collapse show\">" + this.faqs[x].answer + "</div></div></div>";
						}
						else {
							faq = faq + "<div class=\"palet row\"><div class=\"col-xs-12\"><h3 class=\"palet-title\"><a href=\"#faqs" + [x] + "\" onclick=\"return false;\" data-toggle=\"collapse\" class=\"collapsed\">" + this.faqs[x].question + "</a></h3>";
							faq = faq + "<div id=\"faqs" + [x] + "\" class=\"lh-35 collapse\">" + this.faqs[x].answer + "</div></div></div>";
						}
					}
				}
				faq = faq + "</div>";
				$(".tab-section").append(faq);
			}
		}

		this.tabLinks = document.querySelectorAll('.tablinks');
		this.tabcontent = document.querySelectorAll('.tabcontent');
		this.setActiveTab();
	}


	checkIds(old) {
		if (this.oldId == 0) {
			this.oldId = old;
			return true;
		}
		else if (this.oldId == old) {
			return false;
		}
		else if (this.oldId != old) {
			this.oldId = old;
			return true;
		}
	}

	setActiveTab() {
		var allTabs = document.getElementsByClassName("tablinks");
		if(this.routeTab) {
			allTabs[this.routeTab].classList.add("active");
			$(".faqMainDiv").hide("fast");
			$("#" + allTabs[this.routeTab].classList[1]).show();
		} else {
			allTabs[0].classList.add("active");
			$(".faqMainDiv").hide("fast");
			$("#" + allTabs[0].classList[1]).show();
		}
		
	}



}
