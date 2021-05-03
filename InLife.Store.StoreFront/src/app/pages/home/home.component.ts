import { CONSTANTS } from '@app/services/constants';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit, TemplateRef, OnDestroy } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { DomSanitizer } from '@angular/platform-browser';
import $ from 'jquery';
import { takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';
//import { NgxUiLoaderService } from 'ngx-ui-loader';
@Component
({
	templateUrl: './home.component.html',
	styleUrls: ['./style.css']
})
export class HomeComponent implements OnInit, OnDestroy {
	public sliders: any;
	public slideConfig = {
		"draggable": true,
		"autoplay": true,
		"autoplaySpeed": 4000,
		"infinite": true,
		"dots": true,
		"slidesToShow": 1,
		"slidesToScroll": 1, };

		destroy$ = new Subject();
	public productsLst: any;
	public productsDetailLst: any;
	public productName: any;
	public productImg: any;
	public shortDescription: any;
	public productPrice: any;
	public priceWithOffer: any;
	constructor(
		private apiService: ApiService,
		private sanitizer: DomSanitizer,
		private route: ActivatedRoute) {
	}

	ngOnInit(): void {
		
		// this.getHomeSliders();
		this.getProductsLst();
		//  this.route.data
		//  	.pipe(takeUntil(this.destroy$))
		//  	.subscribe((response) => {
		//  	this.sliders = CONSTANTS.HERO_SLIDERS;
        //  });

		 this.sliders = CONSTANTS.HERO_SLIDERS;
	}

	ngOnDestroy() {
		this.destroy$.next(true);
		this.destroy$.unsubscribe();
	}
	// getHomeSliders() {
	// 	var url = "/Home/GetHeroSliders";
	// 	this.apiService.sendGetRequest(url).subscribe((responseBody) => {
	// 		this.sliders = responseBody;
	// 	});


	// }

	getProductsLst() {
		var url = "/Home/GetProductsList";
		this.apiService.sendGetRequest(url).subscribe((responseBody) => {
			this.productsLst = responseBody;
			this.productName = this.productsLst[0].productName;
			this.productImg = this.productsLst[0].productImg;
			this.shortDescription = this.productsLst[0].shortDescription;
			this.productPrice = this.productsLst[0].productPrice;
			this.priceWithOffer = this.productsLst[0].priceWithOffer;
			this.getProductsDetailLst();
		});
	}

	sanitize(url: string) {

		if (url) {
			return this.sanitizer.bypassSecurityTrustResourceUrl(`data:image/png;base64, ${url}`);
		}
	}

	getUrl(url: string) {
		return "url('" + url + "')";
	}

	getColor(color: string) {
		if (window.innerWidth < 768) {
			color = "#ffffff";
		}
		return color;
	}

	scrollToFeatured(type) {
		if(type == 'feature-plan')
			setTimeout(function ()
			{
				document.querySelector('#section-prime-secure').scrollIntoView
				({
					behavior: 'smooth'
				});
			}, 100);
	}


	getPosition(position: string) {
		if (position != undefined && (position == "Right" && window.innerWidth > 768)) {
			return "position: relative; left:700px;";
		}
	}

	makeIdFromProductName(productName: string) {
		if (productName) {
			productName = productName.replace(/\s/g, '-');
		}
		return "#" + productName;
	}

	getProductsDetailLst() {
		var url = "/Home/GetProductsDetailList";
		this.apiService.sendGetRequest(url).subscribe((responseBody) => {
			this.productsDetailLst = responseBody;
			this.addProducts();
			var myVideo: any = document.getElementById("video");
			myVideo.load();
			myVideo.muted = true;
		});
	}

	getProductname(productId: number) {
		var pro = this.productsLst.Where(x => x.productId == productId);
		return pro;
	}

	addProducts() {
		let pro = this.productsLst;
		let proDet = this.productsDetailLst;
	if (pro.length <= 4) {
		$(".product-list li")
			.slice(0, 4)
			.show();
		$("#loadmore").fadeOut("slow");
	}
	if (pro.length > 4) {
		$(".product-list li")
			.slice(0, 4)
			.show();
	}

	for (var i = 0; i < proDet.length; i++) {
		var product = pro.find(x => x.id == proDet[i].productId);
		var img = 'data:image/jpg;base64,' + product.productImg;
		var proName = product.productName;
		var proNameId = proName.replace(/\s+/g, '-');
		var modals = "<div id=\"" + proNameId + "\" class=\"modal\"><div><a href=\"#\" class=\"modal-close\" rel=\"modal:close\"><i class=\"fa fa-times\" aria-hidden=\"true\"></i></a>";
		modals += "<div class=\"modal-header\"><div class=\"modal-product\"><img src=\"" + img + "\" /></div><div class=\"modal-product-description\"><h3 class=\"modal-product-name\">" + proName + "</h3><p class=\"modal-product-price\"> ₱" + product.productPrice + "</p>";

		if (product.productCode) {
			modals += "<p class=\"modal-product-code\">Product code: " + product.productCode + "</p>";
		}
		if (product.priceWithOffer != undefined) {
			modals += "<p class=\"modal-product-price\">" + product.priceWithOffer + "</p>";
		}
		modals += "</div>";
		if (product.shortDescription != undefined) {
			modals += "<div class=\"modalBlock mb-4 mt-3\">" + product.shortDescription + "</div>";
		}
		modals += "</div><div class=\"modal-body\"><table class=\"product-options\">";

		if (proDet[i].casesCovered == undefined) {
			modals += "<tr><th>Cases Covered</th><td></td></tr>";
		}
		else {
			if (proDet[i].casesCovered.toLowerCase() != "na") {
				modals += "<tr><th>Cases Covered</th>";
				if (proDet[i].casesCovered.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].casesCovered + "</td></tr>";
				}
			}
		}

		if (proDet[i].benefitType == undefined) {
			modals += "<tr><th>Benefit Type</th><td></td></tr>";
		}
		else {
			if (proDet[i].benefitType.toLowerCase() != "na") {
				modals += "<tr><th>Benefit Type</th>";
				if (proDet[i].benefitType.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].benefitType + "</td></tr>";
				}
			}
		}

		if (proDet[i].ageEligibility == undefined) {
			modals += "<tr><th>Age Eligibility</th><td></td></tr>";
		}
		else {
			if (proDet[i].ageEligibility.toLowerCase() != "na") {
				modals += "<tr><th>Age Eligibility</th>";
				if (proDet[i].ageEligibility.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].ageEligibility + "</td></tr>";
				}
			}
		}
		if (proDet[i].coverage == undefined) {
			modals += "<tr><th>Coverage</th><td></td></tr>";
		}
		else {
			if (proDet[i].coverage.toLowerCase() != "na") {
				modals += "<tr><th>Coverage</th>";
				if (proDet[i].coverage.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].coverage + "</td></tr>";
				}
			}
		}

		if (proDet[i].medicalCoverage == undefined) {
			modals += "<tr><th>Medical Coverage</th><td></td></tr>";
		}
		else {
			if (proDet[i].medicalCoverage.toLowerCase() != "na") {
				modals += "<tr><th>Medical Coverage</th>";
				if (proDet[i].medicalCoverage.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].medicalCoverage + "</td></tr>";
				}
			}
		}


		if (proDet[i].preExistingConCover == undefined) {
			modals += "<tr><th>Pre-existing Condition Coverage</th><td></td></tr>";
		}
		else {
			if (proDet[i].preExistingConCover.toLowerCase() != "na") {
				modals += "<tr><th>Pre-existing Condition Coverage</th>";
				if (proDet[i].preExistingConCover.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].preExistingConCover + "</td></tr>";
				}
			}
		}

		if (proDet[i].exclusions == undefined) {
			modals += "<tr><th>Exclusions </th><td></td></tr>";
		}
		else {
			if (proDet[i].exclusions.toLowerCase() != "na") {
				modals += "<tr><th>Exclusions</th>";
				if (proDet[i].exclusions.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].exclusions + "</td></tr>";
				}
			}
		}


		if (proDet[i].numberOfAvailments == undefined) {
			modals += "<tr><th>Number of Availments</th><td></td></tr>";
		}
		else {
			if (proDet[i].numberOfAvailments.toLowerCase() != "na") {
				modals += "<tr><th>Number of Availments</th>";
				if (proDet[i].numberOfAvailments.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].numberOfAvailments + "</td></tr>";
				}
			}
		}

		if (proDet[i].benefitLimit == undefined) {
			modals += "<tr><th>Benefit Limit</th><td></td></tr>";
		}
		else {
			if (proDet[i].benefitLimit.toLowerCase() != "na") {
				modals += "<tr><th>Benefit Limit</th>";
				if (proDet[i].benefitLimit.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].benefitLimit + "</td></tr>";
				}
			}
		}

		if (proDet[i].inclusions == undefined) {
			modals += "<tr><th>Inclusions:</th><td></td></tr>";
		}
		else {
			if (proDet[i].inclusions.toLowerCase() != "na") {
				modals += "<tr><th>Inclusions:</th>";
				if (proDet[i].inclusions.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].inclusions + "</td></tr>";
				}
			}
		}

		if (proDet[i].docProFee == undefined) {
			modals += "<tr class=\"indent\"><td>Doctor's professional fee</td></tr>";
		}
		else {
			if (proDet[i].docProFee.toLowerCase() != "na") {
				modals += "<tr class=\"indent\"><td>Doctor's professional fee</td>";
				if (proDet[i].docProFee.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].docProFee + "</td></tr>";
				}
			}
		}

		if (proDet[i].roomAccommodation == undefined) {
			modals += "<tr class=\"indent\"><td>Room Accommodation</td></tr>";
		}
		else {
			if (proDet[i].roomAccommodation.toLowerCase() != "na") {
				modals += "<tr class=\"indent\"><td>Room Accommodation</td>";
				if (proDet[i].roomAccommodation.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].roomAccommodation + "</td></tr>";
				}
			}
		}

		if (proDet[i].laboratoryDiagnosticPro == undefined) {
			modals += "<tr class=\"indent\"><td>Laboratory and diagnostic procedures</td></tr>";
		}
		else {
			if (proDet[i].laboratoryDiagnosticPro.toLowerCase() != "na") {
				modals += "<tr class=\"indent\"><td>Laboratory and diagnostic procedures</td>";
				if (proDet[i].laboratoryDiagnosticPro.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].laboratoryDiagnosticPro + "</td></tr>";
				}
			}
		}

		if (proDet[i].medicinesAsMedicallyNeeded == undefined) {
			modals += "<tr class=\"indent\"><td>Medicines as medically needed</td></tr>";
		}
		else {
			if (proDet[i].medicinesAsMedicallyNeeded.toLowerCase() != "na") {
				modals += "<tr class=\"indent\"><td>Medicines as medically needed</td>";
				if (proDet[i].medicinesAsMedicallyNeeded.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].medicinesAsMedicallyNeeded + "</td></tr>";
				}
			}
		}


		if (proDet[i].useOfOperationRoom == undefined) {
			modals += "<tr class=\"indent\"><td>Use of operating room, recovery room, and ICU</td></tr>";
		}
		else {
			if (proDet[i].useOfOperationRoom.toLowerCase() != "na") {
				modals += "<tr class=\"indent\"><td>Use of operating room, recovery room, and ICU</td>";
				if (proDet[i].useOfOperationRoom.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].useOfOperationRoom + "</td></tr>";
				}
			}
		}


		if (proDet[i].surgerySurgonFees == undefined) {
			modals += "<tr class=\"indent\"><td>Surgery and surgeon’s fees when medically necessary</td></tr>";
		}
		else {
			if (proDet[i].surgerySurgonFees.toLowerCase() != "na") {
				modals += "<tr class=\"indent\"><td>Surgery and surgeon’s fees when medically necessary</td>";
				if (proDet[i].surgerySurgonFees.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].surgerySurgonFees + "</td></tr>";
				}
			}
		}


		if (proDet[i].specialModalities == undefined) {
			modals += "<tr class=\"indent\"><td>Special Modalities as medically needed subject to Php 5,000 sublimit</td></tr>";
		}
		else {
			if (proDet[i].specialModalities.toLowerCase() != "na") {
				modals += "<tr class=\"indent\"><td>Special Modalities as medically needed subject to Php 5,000 sublimit</td>";
				if (proDet[i].specialModalities.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].specialModalities + "</td></tr>";
				}
			}
		}

		if (proDet[i].laparoscopic == undefined) {
			modals += "<tr class=\"indent\"><td>Laparoscopic procedures</td></tr>";
		}
		else {
			if (proDet[i].laparoscopic.toLowerCase() != "na") {
				modals += "<tr class=\"indent\"><td>Laparoscopic procedures</td>";
				if (proDet[i].laparoscopic.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].laparoscopic + "</td></tr>";
				}
			}
		}

		if (proDet[i].mra == undefined) {
			modals += "<tr class=\"indent\"><td>Magnetic Resonance Angiography (MRA)</td></tr>";
		}
		else {
			if (proDet[i].mra.toLowerCase() != "na") {
				modals += "<tr class=\"indent\"><td>Magnetic Resonance Angiography (MRA)</td>";
				if (proDet[i].mra.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].mra + "</td></tr>";
				}
			}
		}

		if (proDet[i].mri == undefined) {
			modals += "<tr class=\"indent\"><td>Magnetic Resonance Imaging (MRI)</td></tr>";
		}
		else {
			if (proDet[i].mri.toLowerCase() != "na") {
				modals += "<tr class=\"indent\"><td>Magnetic Resonance Imaging (MRI)</td>";
				if (proDet[i].mri.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].mri + "</td></tr>";
				}
			}

		}

		if (proDet[i].ct == undefined) {
			modals += "<tr class=\"indent\"><td>Computerized Tomography (CT) Scans</td></tr>";
		}
		else {
			if (proDet[i].ct.toLowerCase() != "na") {
				modals += "<tr class=\"indent\"><td>Computerized Tomography (CT) Scans</td>";
				if (proDet[i].ct.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].ct + "</td></tr>";
				}
			}
		}

		if (proDet[i].therapetic == undefined) {
			modals += "<tr class=\"indent\"><td>Endoscopic Procedures (Therapeutic)</td></tr>";
		}
		else {
			if (proDet[i].therapetic.toLowerCase() != "na") {
				modals += "<tr class=\"indent\"><td>Endoscopic Procedures (Therapeutic)</td>";
				if (proDet[i].therapetic.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].therapetic + "</td></tr>";
				}
			}
		}

		if (proDet[i].painManagement == undefined) {
			modals += "<tr class=\"indent indent-double\"><td>Pain Management</td></tr>";
		}
		else {
			if (proDet[i].painManagement.toLowerCase() != "na") {
				modals += "<tr class=\"indent indent-double\"><td>Pain Management</td>";
				if (proDet[i].painManagement.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].painManagement + "</td></tr>";
				}
			}
		}

		if (proDet[i].arthoscopic == undefined) {
			modals += "<tr class=\"indent indent-double\"><td>Arthroscopic Procedures, Orthopedic Arthroscopy</td></tr>";
		}
		else {
			if (proDet[i].arthoscopic.toLowerCase() != "na") {
				modals += "<tr class=\"indent indent-double\"><td>Arthroscopic Procedures, Orthopedic Arthroscopy</td>";
				if (proDet[i].arthoscopic.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].arthoscopic + "</td></tr>";
				}
			}
		}

		if (proDet[i].otherMedical == undefined) {
			modals += "<tr class=\"indent indent-double\"><td>Other medically necessary modalities not mentioned above and those for which there are no comparable, conventional or traditional counterparts</td></tr>";
		}
		else {
			if (proDet[i].otherMedical.toLowerCase() != "na") {
				modals += "<tr class=\"indent indent-double\"><td>Other medically necessary modalities not mentioned above and those for which there are no comparable, conventional or traditional counterparts</td>";
				if (proDet[i].otherMedical.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].otherMedical + "</td></tr>";
				}
			}
		}

		if (proDet[i].unlimitedTeleMed == undefined) {
			modals += "<tr class=\"indent\"><td>Unlimited telemedicine access (for gatekeeping)</td></tr>";
		}
		else {
			if (proDet[i].unlimitedTeleMed.toLowerCase() != "na") {
				modals += "<tr class=\"indent\"><td>Unlimited telemedicine access (for gatekeeping)</td>";
				if (proDet[i].unlimitedTeleMed.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].unlimitedTeleMed + "</td></tr>";
				}
			}
		}


		if (proDet[i].oneTime == undefined) {
			modals += "<tr class=\"indent\"><td>One-time hospital cash benefit of Php 1000 in confinement</td></tr>";
		}
		else {
			if (proDet[i].oneTime.toLowerCase() != "na") {
				modals += "<tr class=\"indent\"><td>One-time hospital cash benefit of Php 1000 in confinement</td>";
				if (proDet[i].oneTime.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].oneTime + "</td></tr>";
				}
			}
		}

		if (proDet[i].usage == undefined) {
			modals += "<tr><td>Usage</td></tr>";
		}
		else {
			if (proDet[i].usage.toLowerCase() != "na") {
				modals += "<tr><td>Usage</td>";
				if (proDet[i].usage.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].usage + "</td></tr>";
				}
			}
		}

		if (proDet[i].hospitalNetwork == undefined) {
			modals += "<tr><td>Hospital Network</td></tr>";
		}
		else {
			if (proDet[i].hospitalNetwork.toLowerCase() != "na") {
				modals += "<tr><td>Hospital Network</td>";
				if (proDet[i].hospitalNetwork.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].hospitalNetwork + "</td></tr>";
				}
			}
		}


		if (proDet[i].accreditedHospitals == undefined) {
			modals += "<tr><td>Accredited Hospitals</td></tr>";
		}
		else {
			if (proDet[i].accreditedHospitals.toLowerCase() != "na") {
				modals += "<tr><td>Accredited Hospitals</td>";
				if (proDet[i].accreditedHospitals.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].accreditedHospitals + "</td></tr>";
				}
			}
		}

		if (proDet[i].nonAccreditedHos == undefined) {
			modals += "<tr><td>Non-Accredited Hospitals</td></tr>";
		}
		else {
			if (proDet[i].nonAccreditedHos.toLowerCase() != "na") {
				modals += "<tr><td>Non-Accredited Hospitals</td>";
				if (proDet[i].nonAccreditedHos.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].nonAccreditedHos + "</td></tr>";
				}
			}
		}


		if (proDet[i].registrationRules == undefined) {
			modals += "<tr><td>Registration Rules</td></tr>";
		}
		else {
			if (proDet[i].registrationRules.toLowerCase() != "na") {
				modals += "<tr><td>Registration Rules</td>";
				if (proDet[i].registrationRules.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].registrationRules + "</td></tr>";
				}
			}
		}


		if (proDet[i].mer == undefined) {
			modals += "<tr><td>Medical Examination Requirement</td></tr>";
		}
		else {
			if (proDet[i].mer.toLowerCase() != "na") {
				modals += "<tr><td>Medical Examination Requirement</td>";
				if (proDet[i].mer.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].mer + "</td></tr>";
				}
			}
		}


		if (proDet[i].afr == undefined) {
			modals += "<tr><td>Application Form Requirement</td></tr>";
		}
		else {
			if (proDet[i].afr.toLowerCase() != "na") {
				modals += "<tr><td>Application Form Requirement</td>";
				if (proDet[i].afr.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].afr + "</td></tr>";
				}
			}
		}


		if (proDet[i].arp == undefined) {
			modals += "<tr><td>Allowable registration period</td></tr>";
		}
		else {
			if (proDet[i].arp.toLowerCase() != "na") {
				modals += "<tr><td>Allowable registration period</td>";
				if (proDet[i].arp.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].arp + "</td></tr>";
				}
			}
		}


		if (proDet[i].validity == undefined) {
			modals += "<tr><td>Validity Period from the date of successful registration</td></tr>";
		}
		else {
			if (proDet[i].validity.toLowerCase() != "na") {
				modals += "<tr><td>Validity Period from the date of successful registration</td>";
				if (proDet[i].validity.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].validity + "</td></tr>";
				}
			}
		}


		if (proDet[i].waiting == undefined) {
			modals += "<tr><td>Waiting Period from registration to activation</td></tr>";
		}
		else {
			if (proDet[i].waiting.toLowerCase() != "na") {
				modals += "<tr><td>Waiting Period from registration to activation</td>";
				if (proDet[i].waiting.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].waiting + "</td></tr>";
				}
			}
		}


		if (proDet[i].numberOfRegistrations == undefined) {
			modals += "<tr><td>No. of registration within 12 months</td></tr>";
		}
		else {
			if (proDet[i].numberOfRegistrations.toLowerCase() != "na") {
				modals += "<tr><td>No. of registration within 12 months</td>";
				if (proDet[i].numberOfRegistrations.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].numberOfRegistrations + "</td></tr>";
				}
			}
		}




		if (proDet[i].registrationOfSucceedingVouchers == undefined) {
			modals += "<tr><td>Registration of succeeding vouchers within 12 months</td></tr>";
		}
		else {
			if (proDet[i].registrationOfSucceedingVouchers.toLowerCase() != "na") {
				modals += "<tr><td>Registration of succeeding vouchers within 12 months</td>";
				if (proDet[i].registrationOfSucceedingVouchers.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].registrationOfSucceedingVouchers + "</td></tr>";
				}
			}
		}

		if (proDet[i].voucherUsed == undefined) {
			modals += "<tr class=\"indent\"><td>If voucher is used</td></tr>";
		}
		else {
			if (proDet[i].voucherUsed.toLowerCase() != "na") {
				modals += "<tr class=\"indent\"><td>If voucher is used</td>";
				if (proDet[i].voucherUsed.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].voucherUsed + "</td></tr>";
				}
			}
		}


		if (proDet[i].voucherUnused == undefined) {
			modals += "<tr class=\"indent\"><td>If voucher is unused</td></tr>";
		}
		else {
			if (proDet[i].voucherUnused.toLowerCase() != "na") {
				modals += "<tr class=\"indent\"><td>If voucher is unused</td>";
				if (proDet[i].voucherUnused.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].voucherUnused + "</td></tr>";
				}
			}
		}

		if (proDet[i].combinability == undefined) {
			modals += "<tr><td>Combinability with another IHC product</td></tr>";
		}
		else {
			if (proDet[i].combinability.toLowerCase() != "na") {
				modals += "<tr><td>Combinability with another IHC product</td>";
				if (proDet[i].combinability.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].combinability + "</td></tr>";
				}
			}
		}




		if (proDet[i].prepaidPlan == undefined) {
			modals += "<tr class=\"indent\"><td>Prepaid Plan</td></tr>";
		}
		else {
			if (proDet[i].prepaidPlan.toLowerCase() != "na") {
				modals += "<tr class=\"indent\"><td>Prepaid Plan</td>";
				if (proDet[i].prepaidPlan.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].prepaidPlan + "</td></tr>";
				}
			}
		}

		if (proDet[i].consultationCards == undefined) {
			modals += "<tr class=\"indent indent-double\"><td>Consultation Cards</td></tr>";
		}
		else {
			if (proDet[i].consultationCards.toLowerCase() != "na") {
				modals += "<tr class=\"indent indent-double\"><td>Consultation Cards</td>";
				if (proDet[i].consultationCards.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].consultationCards + "</td></tr>";
				}
			}
		}


		if (proDet[i].consultation == undefined) {
			modals += "<tr class=\"indent indent-double\"><td>Consultation Benefit</td></tr>";
		}
		else {
			if (proDet[i].consultation.toLowerCase() != "na") {
				modals += "<tr class=\"indent indent-double\"><td>Consultation Benefit</td>";
				if (proDet[i].consultation.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].consultation + "</td></tr>";
				}
			}
		}



		if (proDet[i].ftfconsultation == undefined) {
			modals += "<tr class=\"indent\"><td>Face-to-Face consultation</td></tr>";
		}
		else {
			if (proDet[i].ftfconsultation.toLowerCase() != "na") {
				modals += "<tr class=\"indent\"><td>Face-to-Face consultation</td>";
				if (proDet[i].ftfconsultation.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].ftfconsultation + "</td></tr>";
				}
			}
		}


		if (proDet[i].telemedicine == undefined) {
			modals += "<tr class=\"indent\"><td>Telemedicine</td></tr>";
		}
		else {
			if (proDet[i].telemedicine.toLowerCase() != "na") {
				modals += "<tr class=\"indent\"><td>Telemedicine</td>";
				if (proDet[i].telemedicine.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].telemedicine + "</td></tr>";
				}
			}
		}


		if (proDet[i].dentalConsultation == undefined) {
			modals += "<tr class=\"indent\"><td>Dental Consultation</td></tr>";
		}
		else {
			if (proDet[i].dentalConsultation.toLowerCase() != "na") {
				modals += "<tr class=\"indent\"><td>Dental Consultation</td>";
				if (proDet[i].dentalConsultation.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].dentalConsultation + "</td></tr>";
				}
			}
		}


		if (proDet[i].dentalServicesBenefit == undefined) {
			modals += "<tr class=\"indent indent-double\"><td>Dental services benefit</td></tr>";
		}
		else {
			if (proDet[i].dentalServicesBenefit.toLowerCase() != "na") {
				modals += "<tr class=\"indent indent-double\"><td>Dental services benefit</td>";
				if (proDet[i].dentalServicesBenefit.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].dentalServicesBenefit + "</td></tr>";
				}
			}
		}



		if (proDet[i].inPatient == undefined) {
			modals += "<tr class=\"indent indent-double\"><td>Stand-alone Elective In-patient</td></tr>";
		}
		else {
			if (proDet[i].inPatient.toLowerCase() != "na") {
				modals += "<tr class=\"indent indent-double\"><td>Stand-alone Elective In-patient</td>";
				if (proDet[i].inPatient.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].inPatient + "</td></tr>";
				}
			}
		}


		if (proDet[i].outPatient == undefined) {
			modals += "<tr class=\"indent indent-double\"><td>Stand-alone Out-patient (diagnostics)</td></tr>";
		}
		else {
			if (proDet[i].outPatient.toLowerCase() != "na") {
				modals += "<tr class=\"indent indent-double\"><td>Stand-alone Out-patient (diagnostics)</td>";
				if (proDet[i].outPatient.toLowerCase() == "applicable") {
					modals += "<td><i class=\"fa fa-check-circle\" aria-hidden=\"true\"></i></td></tr>";
				}
				else {
					modals += "<td>" + proDet[i].outPatient + "</td></tr>";
				}
			}
		}

		modals += "</table></div><div class=\"modal-footer\"><div class=\"d-flex justify-content-center align-items-center\">";
		modals += "<a href=\"#buy-now-confirmation2\" data-buy-link=\"" + proDet[i].learnMoreBtnLink + "\" class=\"button button-secondary btnLearn\" rel=\"modal:open\">Learn More</a>";
		modals += "<a href=\"#buy-now-confirmation\" data-buy-link=\"" + proDet[i].buyNowBtnLink + "\" class=\"button button-primary btnBuy\" rel=\"modal:open\">Buy Now</a>";
		modals += "</div><p class=\"modal-footer-text\">You are viewing a product of InLife Health Care, Inc. (IHC), which is certified and registered with the Insurance Commission and duly authorized to act as a Health Maintenance Organization with Registration No. HMO-2017-02-R. IHC is a wholly-owned subsidiary of The Insular Life Assurance Company, Ltd (InLife) ";
		modals += "and a member of the Association of Health Maintenance Organizations of the Philippines, Inc.</p></div></div></div>";

		$(".modals").append(modals);
	}

}

	loadMore() {
	}
}
