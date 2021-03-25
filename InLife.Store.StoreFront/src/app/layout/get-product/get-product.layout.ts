import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, NavigationEnd } from '@angular/router';
import { ApiService } from '@app/services';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import $ from 'jquery';

@Component
({
	templateUrl: './get-product.layout.html',
	styleUrls: ['./get-product.layout.css']
})
export class GetProductLayoutComponent implements OnInit
{
	public footerLinks: any;
	isShowFooterandHeader = true;
	isPSChild: boolean = false;
	destroy$ = new Subject();
	constructor(
		private router: Router,
		private route: ActivatedRoute,
		private apiService: ApiService,
		private ngxService: NgxUiLoaderService, ) {
		this.ngxService.start();
		this.router.events
			.pipe(takeUntil(this.destroy$))
			.subscribe(r => {
				if (r instanceof NavigationEnd) {
					console.log(r.urlAfterRedirects);
					let url = r.urlAfterRedirects.split('/');
					console.log(url);
					if(url.length > 1 && ( url[1] == 'prime-secure-lite' || url[1] == 'prime-care' || url[1] == 'prime-secure')) {
						
						if(url.length > 2) {
							this.isPSChild = true;
							this.isShowFooterandHeader = false;
						} else {
							this.isShowFooterandHeader = true;
						}
					} else {
						this.isShowFooterandHeader = true;
						this.isPSChild = false;
					}

					if(r.urlAfterRedirects == '/') {
						this.isShowFooterandHeader = true;
					}

					this.apiService.isShowFooterandHeader$.next(this.isShowFooterandHeader);
					this.mobileMenuClick();
				}

				
			});

		this.apiService.getFooterHeaderStatus().subscribe(data => {
			this.isShowFooterandHeader = data;
		})
	}

	ngOnInit() {
		this.getFooterLinks();
		this.ngxService.stop();
	}

	ngOnDestroy() {
		this.destroy$.next(true);
		this.destroy$.unsubscribe();
	}

	home() {
		this.router.navigate(['/']);
	}
	mobileMenuClick() {
		$("a.burger-menu").toggleClass("active")
		$(".main-nav").toggleClass("show");
	}
	getFooterLinks() {
		var url = "/FooterLinks/GetFooterLinks";
		this.apiService.sendGetRequest(url).subscribe((responseBody) => {
			this.footerLinks = responseBody[0];
		});
	}
}
