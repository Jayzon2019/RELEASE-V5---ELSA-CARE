import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { ApiService, SessionStorageService } from '@app/services';
import { CustomerFeedBackService } from '@app/services/customer-feedback.service';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { Subject, throwError } from 'rxjs';
import { catchError, takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-feedback',
  templateUrl: './feedback.component.html',
  styleUrls: ['./feedback.component.scss']
})
export class FeedbackComponent implements OnInit, OnDestroy {

  public submitted: boolean;
  public feedbackOption = '';
  public feedbackText = '';
  public refCode = '';
  isSuccess: boolean = false;
  hasError: boolean = false;
  errorMsg: string;
  destroy$ = new Subject();
  constructor(private apiService: ApiService, 
              private router: Router,
              private activatedRoute: ActivatedRoute, 
              private session: SessionStorageService,
              private ngxService: NgxUiLoaderService,
              private customerFB_API: CustomerFeedBackService) { }

  ngOnInit(): void {
    this.ngxService.stopAll();
    this.activatedRoute.params
      .pipe(takeUntil(this.destroy$))
      .subscribe((param: Params) => {
        this.refCode = param["referenceCode"];
      });
  }

  ngOnDestroy() {
    this.session.clear();
    this.destroy$.next(true);
    this.destroy$.unsubscribe();
  }


  sendFeeback() {
    this.submitted = true;
    this.hasError = false;
    if(!this.feedbackOption || !this.feedbackText) {
      return;
    }
    this.ngxService.start();
    let data = {
      rating: Number(this.feedbackOption),
      message: this.feedbackText
    }

    this.customerFB_API.customerFeedBack(this.refCode, data)
      .pipe(
        takeUntil(this.destroy$),
      )
      .subscribe(data =>{
        this.ngxService.stopAll();
        this.isSuccess = true;
      }, error => {
        this.hasError = true;
        this.errorMsg = error.message;
        this.ngxService.stopAll();
      });
  
  }

  backToHome() {
    this.router.navigate(['group']).then(() => {
      window.location.reload();
    });
  }

}
