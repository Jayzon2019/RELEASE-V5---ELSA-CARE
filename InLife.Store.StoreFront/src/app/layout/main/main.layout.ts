import { takeUntil } from 'rxjs/operators';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import $ from 'jquery';
import { Router, ActivatedRoute, NavigationEnd, NavigationCancel, RouteConfigLoadEnd, NavigationError, RouteConfigLoadStart, NavigationStart, RouterStateSnapshot } from '@angular/router';
import { Subject } from 'rxjs';
@Component
	({
		templateUrl: './main.layout.html',
		styleUrls: ['./style.css', './main.layout.scss']
	})
export class MainLayoutComponent implements OnInit, OnDestroy {
	public footerLinks: any;
	isShowFooterandHeader = true;
	isGroupChild: boolean = false;
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
					if(url.length > 1 && (url[1] == 'group' || url[1] == 'feedback')) {

						if(url.length > 2) {
							this.isGroupChild = true;
							this.isShowFooterandHeader = false;
						} else {
							this.isShowFooterandHeader = true;
						}
					} else {
						this.isShowFooterandHeader = true;
						this.isGroupChild = false;
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
