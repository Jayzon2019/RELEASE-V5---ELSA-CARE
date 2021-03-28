import { ApplicationStatusService } from './../../services/application-status.service';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { ApplicationStatusBaseComponent } from '../application-status-base.component';
import { Subject } from 'rxjs';
import { takeUntil, map, finalize } from 'rxjs/operators';
import { Application } from '../../model/application.model';
import { StorageType } from '@app/services/storage-types.enum';
import { SessionStorageService } from '@app/services';
import { CONSTANTS } from '@app/services/constants';
import { NgxUiLoaderService } from 'ngx-ui-loader';
@Component({
  selector: 'group-requirements-pending',
  templateUrl: './requirements-pending.component.html',
  styleUrls: ['../styles.scss','./requirements-pending.component.scss']
})
export class RequirementsPendingComponent extends ApplicationStatusBaseComponent implements OnInit, OnDestroy {
  destroy$ = new Subject();
  groupPlan: any;
  groupQuoteData: any;
  cities: any =[];
  requirementTypesTitle: any = ['EmployeeCesusForm', 'EntityPlanForm', 'AuthRepresentativeId', 'BIRNoticeForm', 'SECRegistration', 'IncorporationArticles', 'IdentityCertificate', 'PostPolicyForm'];
  requirementsTypes: any = {
		EmployeeCesusForm: { type: '', title: '', fileInfo: {}, error: { msg: '*Required' }, uploaded: false},
		EntityPlanForm: { type: '', title: '', fileInfo: {}, error: { msg: '*Required' }, uploaded: false},
		AuthRepresentativeId: { type: '', title: '', fileInfo: {}, error: {msg: '*Required' }, uploaded: false},
		BIRNoticeForm: { type: '', title: '', fileInfo: {}, error: {}, uploaded: false},
		SECRegistration: { type: '', title: '', fileInfo: {}, error: {msg: '*Required' }, uploaded: false},
		IncorporationArticles: { type: '', title: '', fileInfo: {}, error: {}, uploaded: false},
		IdentityCertificate: { type: '', title: '', fileInfo: {}, error: {msg: '*Required' }, uploaded: false},
		PostPolicyForm: { type: '', title: '', fileInfo: {}, error: {}, uploaded: false}};

  constructor(router: Router, 
              activatedRoute: ActivatedRoute, 
              private ngxService: NgxUiLoaderService,
              private session: SessionStorageService,
              private appStatusService_API: ApplicationStatusService) { 
    super(router, activatedRoute);
  }

  ngOnInit(): void {
    this.getApplicationSummary();
  }
  ngOnDestroy() {
    this.destroy$.next(true);
    this.destroy$.unsubscribe();
  }

  numberWithCommas(amount) {
    return amount.replace(/\B(?<!\.\d*)(?=(\d{3})+(?!\d))/g, ",");
  }
  getPlan(code) {
    if(code === 'Administrative and Office-based') {
      return '1';
    } else if(code === 'Security Agencies') {
      return '2';
    } else {
      return '3';
    }
  }

  getApplicationSummary() {
    this.ngxService.start();
    this.appStatusService_API.getApplicationSummary(this.referenceCode)
      .pipe(takeUntil(this.destroy$), finalize(() => this.ngxService.stopAll()))
      .subscribe((data: Application) => {
        let planVariantCodeArr = data.planVariantCode.split(' ');
        let prodName = planVariantCodeArr.slice(0, planVariantCodeArr.length -1);
        this.groupPlan = {
          annualPremium: Number(data.planPremium),
          insuranceCoverage: this.numberWithCommas(data.planFaceAmount.toString()),
          totalPremium: this.numberWithCommas((data.planPremium * Number(data.totalMembers)).toString()),
          planCode: this.getPlan(data.planCode) + ' - ' + data.planCode, // 1 - Administrative and Office-based
          plan: this.getPlan(data.planCode),
          productType: Number(planVariantCodeArr[planVariantCodeArr.length - 1]),
          productName: prodName.join(' '), // Employee Secure Plan
        }
        this.groupQuoteData = {
          AuthEamilId: data.representativeEmailAddress,
          AuthFristName: data.representativeFirstName,
          AuthLandlineNo: data.representativePhoneNumber,
          AuthLastName: data.representativeLastName,
          AuthMiddleName: data.representativeMiddleName,
          AuthMobileNumber: data.representativeMobileNumber,
          AuthPrefixName: this.getPrefixObject(data.representativeNamePrefix), 
          AuthPrefixTxt: data.representativeNamePrefix,
          AuthSuffixName: this.getSuffixObject(data.representativeNameSuffix),
          AuthSuffixTxt: data.representativeNameSuffix,
          Barangaya: data.companyTown,//"12",
          BarangayaTxt: data.companyTown,
          BusinessTxt: "",
          BusinessType: data.businessStructure,
          Region: this.getRegionObject(data.companyRegion),
          RegionTxt: data.companyRegion,
          City: this.getCityObj(data.companyCity),
          CityTxt: data.companyCity,
          CompanyLandLineNo: data.companyPhoneNumber,
          CompanyMobileNo: data.companyMobileNumber,
          CompanyName: data.companyName,
          PlanType: Number(planVariantCodeArr[planVariantCodeArr.length - 1]),
          ProductName: prodName.join(' '),
          SelectedPlan: this.getPlan(data.planCode),
          Status: 1,
          StreetNumer: data.companyAddress1,
          TotalNumberOfMembers: data.totalMembers,
          TotalNumberOfStudents: data.totalStudents || null,
          TotalNumberOfTeachers: data.totalTeachers || null,
          VillageName: data.companyAddress1,
          ZipCode: data.companyZipCode,
          privacyPolicy: true,
        }

        this.requirementTypesTitle.forEach(type => {
          let lowerCaseType = '';
          if(type === 'SECRegistration' || type === 'BIRNoticeForm') {
            lowerCaseType = type.charAt(0).toLowerCase() + type.charAt(1).toLowerCase() + type.charAt(2).toLowerCase() + type.slice(3);
          } else {
            lowerCaseType = type.charAt(0).toLowerCase() + type.slice(1);
          }

          console.log(lowerCaseType);
          
          if(data[lowerCaseType]) {
            this.mapRequirementsFileDetails(type, data[lowerCaseType]);
          } else {
            console.log(this.requirementsTypes[type]);
          }
          
        });
        
      });
  }

  mapRequirementsFileDetails(type: string, data: any) {
    this.requirementsTypes[type] = {
      type: 'general',
      title: data,
      fileInfo: { ext: 'generic', loc: 'assets/images/generic-file.svg' },
      error:{
        type: 'success',
        msg: "(You've already uploaded this requirement.)"
      },
      uploaded: true
    };
  }

  continueApplication() {
    console.log(this.groupPlan,this.groupQuoteData);
    this.session.set('selectedGroupPlanData', this.groupPlan);
    this.session.set(StorageType.POST_GROUP_QUOTE, this.groupQuoteData);
    this.session.set(StorageType.REQUIREMENTS_DATA, this.requirementsTypes);
    
    this.router.navigate(['/group/apply']);
  }

  getPrefixObject(prefix) {
    let prefixbj: any = CONSTANTS.PREFIX.filter(i => i.name == prefix )[0];
    return prefixbj;
  }

  getSuffixObject(suffix) {
    let suffixbj: any = CONSTANTS.SUFFIX.filter(i => i.name == suffix)[0];
    return suffixbj;
  }

  getRegionObject(province) {
    let provinceObj = CONSTANTS.PROVIANCE.filter(i => i.Province == province);
    this.cities = provinceObj.map(i => i.Municipality);
    let regionObj = {
      id: Number(provinceObj.map(i => i.id)),
      name: province
    }
    return regionObj;
  }

  getCityObj(city) {
    let cityobj: any = this.cities[0].filter(i => i.name == city)[0];
    return cityobj;
  }
}
