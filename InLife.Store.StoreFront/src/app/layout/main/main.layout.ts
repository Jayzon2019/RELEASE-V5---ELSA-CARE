import { takeUntil } from 'rxjs/operators';
import { Component, OnInit, OnDestroy, HostListener } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import $ from 'jquery';
import { Router, ActivatedRoute, NavigationEnd } from '@angular/router';
import { Subject } from 'rxjs';
import { UtilitiesService } from '@app/shared/services/utilities.service';
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
	deferredPrompt: any;
	isShowButton = false;
    isStandalone = window.matchMedia('(display-mode: standalone)').matches;
    isIOS = navigator.platform && /iPad|iPhone|iPod/.test(navigator.platform);
    isSafari = navigator.vendor && 
	  		   navigator.userAgent &&
			   navigator.vendor.indexOf('Apple') > -1 &&
               navigator.userAgent.indexOf('CriOS') == -1 &&
               navigator.userAgent.indexOf('FxiOS') == -1;

	constructor(
		private router: Router,
		private apiService: ApiService,
		private ngxService: NgxUiLoaderService,
		private util: UtilitiesService ) {
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
		
        if (this.isIOS && this.isSafari) {
			// check if app is already installed
			if (!this.isStandalone) {
			  setTimeout(() => {
				this.isShowButton = true;
			  }, 15000);
			}
		  }
	}

	@HostListener('window:beforeinstallprompt', ['$event'])
    onbeforeinstallprompt(e) {
      console.log(e);
      e.preventDefault();
      this.deferredPrompt = e;
      this.isShowButton = true;
    }

    addToHomescreen() {
      if (this.isIOS && this.isSafari) {
		this.util.ShowGeneralMessagePrompt({message: `Install InLife Store on your home screen for quick and easy access. Just tap 'Share button' then 'Add to Home Screen'.`});
	  } 
	  else {
		this.isShowButton = false;
        this.deferredPrompt.prompt();
        this.deferredPrompt.userChoice
          .then((choiceResult) => {
            if (choiceResult.outcome === 'accepted') {
              console.log('User accepted the A2HS prompt');
            } else {
              console.log('User dismissed the A2HS prompt');
            }
            this.deferredPrompt = null;
          });
        }
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
