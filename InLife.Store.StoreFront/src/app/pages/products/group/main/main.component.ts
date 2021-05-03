import { PromptMessageComponent } from '../../../../shared/component/prompt-message/prompt-message.component';
import { SessionStorageService } from '@app/services';
import { Component, OnDestroy, OnInit, AfterViewInit, ApplicationRef } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ApiService } from '../../../../services/api.service';
import { DomSanitizer } from '@angular/platform-browser';
import $ from "jquery";
import { Subject } from 'rxjs';
import { map, takeUntil, switchMap, filter } from 'rxjs/operators';
import { MatDialog } from '@angular/material/dialog';
import { UtilitiesService } from '@app/shared/services/utilities.service';
import { StorageType } from '@app/services/storage-types.enum';
declare var require: any
const FileSaver = require('file-saver');
@Component
	({
		templateUrl: './main.component.html',
		styleUrls: ['./main.component.scss','./style.css']
	})
export class MainComponent implements OnInit, OnDestroy {
	public submitted: boolean;
	public referenceCode: string = '';
	public slider: any;
	public heroBg: any;
	public mobHeroBg: any;
	destroy$ = new Subject();
	isStableApp = new Subject();
	constructor(private router: Router, private route: ActivatedRoute,
		private apiService: ApiService, private sanitizer: DomSanitizer,
		private util: UtilitiesService,
		private session: SessionStorageService,
		private appRef: ApplicationRef,
		private dialog: MatDialog) { }

	ngOnInit(): void {
		this.session.remove('selectedGroupPlanData');
		this.session.remove(StorageType.POST_GROUP_QUOTE);
		this.session.remove(StorageType.GROUP_PLAN_DATA);
		this.session.remove(StorageType.REQUIREMENTS_DATA);
		this.session.remove(StorageType.STUDENTS_TEACHERS_BENEFITS);
		// this.session.clear();
		// this.getGroupHeroSlider();

		// this.route.data
		// 	.pipe(takeUntil(this.destroy$))
		// 	.subscribe((response) => {
		// 	this.slider = response.GroupHeroSliders[0];
        // });
		this.route.queryParams
			.pipe(
				map(params => {
					if (params?.refCode)
						this.referenceCode = params.refCode;
				}),
				switchMap(() => this.appRef.isStable),
				filter(stable => stable),
				takeUntil(this.isStableApp)
			)
			.subscribe(data => {
				this.isStableApp.next(data);
				if(this.referenceCode) {
					this.scroll('application-status');
				}
			});

	}

	ngOnDestroy() {
		this.destroy$.next(true);
		this.destroy$.unsubscribe();
	}


	openNewWindow(url: string) {
		this.util.openNewWindow(url);
	}

	planClick(id: string) {
		$(".plan-section").hide();
		$("#" + id).show();
		document.getElementById(id).scrollIntoView({
			behavior: 'smooth'
		});
	}

	closePlans() {
		$(".plan-section").hide();
		document.getElementById("view-products").scrollIntoView({
			behavior: 'smooth'
		});
	}
	checkReferenceCode(valid: boolean) {
		this.submitted = true;
		if (!valid) return;
		this.router.navigate(['group/application-status/otp-confirmation'], { queryParams: { referenceCode: this.referenceCode}});
	}
	// getGroupHeroSlider() {
	// 	var url = "/PrimeHero/GetPrimeHeroSliders";
	// 	this.apiService.sendGetRequest(url).subscribe((responseBody) => {
	// 		this.slider = responseBody[0];
	// 	});
	// }
	scroll(el: string) {
		document.getElementById(el).scrollIntoView({
			behavior: 'smooth'
		});

	}
	sanitize(url: string) {

		if (url) {
			return this.sanitizer.bypassSecurityTrustResourceUrl(`data:image/png;base64, ${url}`);
		}
	}

	getUrl(url: string) {
		return "url('data:image/png;base64," + url + "')";
	}

	getColor(color: string) {
		if (window.innerWidth < 768) {
			color = "#ffffff";
		}
		return color;
	}


	getPosition(position: string) {
		if (position != undefined && (position == "Right" && window.innerWidth > 768)) {
			return "position: relative; left:700px;";
		}
	}
	censusFile() {
		// var filePath = '../../../../../assets/documents/Census Template_Prototype Plan-final.xlsx';
		// var pdfName = 'Census Template_Prototype Plan-final';
		// FileSaver.saveAs(filePath, pdfName);
		this.openNewWindow('https://www.insularlife.com.ph/form-library?&scroll=group-insurance-downloadable');
	}
	adminForm() {
		// var filePath = '../../../../../assets/documents/Entity Plan Admin Form.pdf';
		// var pdfName = 'Entity Plan Admin Form';
		// FileSaver.saveAs(filePath, pdfName);
		this.openNewWindow('https://www.insularlife.com.ph/form-library?&scroll=group-insurance-downloadable');
	}
	IndividualApplicationForm() {
		// var filePath = '../../../../../assets/documents/Application Form Group Plan_(For Individual Members).pdf';
		// var pdfName = 'Application Form Group Plan_(For Individual Members)';
		// FileSaver.saveAs(filePath, pdfName);
		this.openNewWindow('https://www.insularlife.com.ph/form-library?&scroll=group-insurance-downloadable');
	}

	ciuForm() {
		// var filePath = '../../../../../assets/documents/CIU Form.pdf';
		// var pdfName = 'Application Form Group Plan_(For Individual Members)';
		// FileSaver.saveAs(filePath, pdfName);
		this.openNewWindow('https://www.insularlife.com.ph/form-library?&scroll=group-insurance-downloadable');
	}

	SecretarysCertificate() {
		// var filePath = '../../../../../assets/documents/Secretary Certificate-Board Resolution.pdf';
		// var pdfName = 'Secretary Certificate/Board Resolution';
		// FileSaver.saveAs(filePath, pdfName);
		this.openNewWindow('https://www.insularlife.com.ph/form-library?&scroll=group-insurance-downloadable');
	}

}
