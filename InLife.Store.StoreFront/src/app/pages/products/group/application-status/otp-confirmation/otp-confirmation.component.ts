import { ApplicationStatusService } from './../../services/application-status.service';
import { StorageType } from './../../../../../services/storage-types.enum';
import { interval, Subject, Subscription } from 'rxjs';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Router } from '@angular/router';
import { ApplicationStatusBaseComponent } from '../application-status-base.component';
import { ApiService, SessionStorageService } from '@app/services';
import { map, takeUntil, switchMap, take, finalize } from 'rxjs/operators';
import { HttpHeaders, HttpParams, HttpClient } from '@angular/common/http';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { CONSTANTS } from '@app/services/constants';
import { Application } from '../../model/application.model';

@Component({
  selector: 'group-otp-confirmation',
  templateUrl: './otp-confirmation.component.html',
  styleUrls: ['../styles.scss','./otp-confirmation.component.scss']
})

export class OtpConfirmationComponent extends ApplicationStatusBaseComponent implements OnInit, OnDestroy {
  public otpCode = '';
  public refCode = '';
  destroy$ = new Subject();
  notifMsg: any;
  invalidReferenceCode: boolean;
  hasCompletedOTPRequest: boolean;
  isAbletoResend: boolean = false;
  hasError: boolean = false;
  errorMsg: string;
  placeholder: string;
  groupPlan: any;
  groupQuoteData: any;
  cities: any =[];

  public dateNow = new Date().getMinutes() + 5;
  public dDay = new Date();
  milliSecondsInASecond = 1000;
  SecondsIn5Minutes = 300;
  public timeDifference;
  public secondsToDday;

  constructor(router: Router, 
              activatedRoute:ActivatedRoute, 
              private http: HttpClient,
              private apiService: ApiService, 
              private ngxService: NgxUiLoaderService,
              private appStatus_API: ApplicationStatusService,
              private session: SessionStorageService) { 
    super(router,activatedRoute);
  }

  ngOnInit(): void {
    this.placeholder = 'enter OTP here';
    this.hasCompletedOTPRequest = false;
    this.activatedRoute.params
      .pipe(takeUntil(this.destroy$))
      .subscribe((param: Params) => {
        this.refCode = param["referenceCode"];
        this.sendOTPRequest(this.refCode);
      });
  }

  startOTPResendCountDown() {
    console.log(this.dateNow, this.dDay);
    let countDown = 0;
    interval(1000)
    .pipe(takeUntil(this.destroy$))
    .subscribe(x => {
      countDown = this.SecondsIn5Minutes - this.allocateTimeUnits();
      console.log(countDown);
      if(countDown === this.SecondsIn5Minutes) {
        this.isAbletoResend = true;
        this.destroy$.next(true);
      }
    });
  }


  private allocateTimeUnits (): number {
    this.timeDifference = this.dateNow - this.dDay.getTime();
    this.secondsToDday = (this.timeDifference) / (this.milliSecondsInASecond) % this.SecondsIn5Minutes;
    return Math.floor(this.secondsToDday);
  }

  sendOTPRequest(refCode: any) {
		if (refCode) {
      this.ngxService.start();
      // this.startOTPResendCountDown();
      this.appStatus_API.sendOTPRequest(refCode)
        .pipe(
          takeUntil(this.destroy$),
          finalize(() => {
            this.ngxService.stopAll();
            this.hasCompletedOTPRequest = true;
          })
        )
        .subscribe(data => {
          this.invalidReferenceCode = false;
          this.notifMsg = {
            status: 'success',
            messsage: 'Successfully send OTP Request'
          };
          
        }, error => {
          if(error.status == 404) {
            this.invalidReferenceCode = true;
          }
        });
		}
  }

  validateOtp(valid: boolean){
		this.submitted = true;
    this.hasError = false;
    if (!valid) return;
    this.ngxService.start();
    this.appStatus_API.getRequestSession(this.refCode, this.otpCode)
      .pipe(
        map((session: any) => {
          this.session.set(StorageType.SESSION, session.session);
        }),
        switchMap(() => this.appStatus_API.getApplicationStatus(this.refCode)),
        takeUntil(this.destroy$),
        finalize(() => {
          this.ngxService.stopAll();
        })
      )
      .subscribe(accessData => {
        this.session.set(StorageType.ACCESS_DATA, accessData);
        this.evaluateStatus(accessData.status);
      }, error => {
        this.hasError = true;
        this.otpCode = '';
        this.placeholder = 'Invalid OTP';
        this.errorMsg = (error.status == 404 || error.status == 401) ? 'OTP is incorrect' : error.message;
      })
	}

  evaluateStatus(status: any = '') {
    switch(status) {
      case 'Application':
        this.router.navigate(['../requirements-pending'],{relativeTo: this.activatedRoute});
        break;
      case 'Payment':
        this.getApplicationSummary();
        break;
      case 'PaymentProof':
        this.router.navigate(['../payment-pending'],{relativeTo: this.activatedRoute});
        break;
      case 'Complete':
        this.router.navigate(['../payment-confirm'],{relativeTo: this.activatedRoute});
        break;
      case 'Cancelled':
        this.router.navigate(['../application-cancelled'],{relativeTo: this.activatedRoute});
        break;
      case 'Feedback':
        this.router.navigate(['feedback', this.refCode]);
        break;

    }
  }

  backToGroup() {
    this.backToGroupPage();
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
    this.appStatus_API.getApplicationSummary(this.referenceCode)
      .pipe(
        takeUntil(this.destroy$),
        finalize(() => {
          this.ngxService.stopAll();
        })
      )
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
        this.session.set('selectedGroupPlanData', this.groupPlan);
        this.session.set(StorageType.POST_GROUP_QUOTE, this.groupQuoteData);
        this.router.navigate(['/group/pay']);
      });
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
