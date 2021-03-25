import { NgxUiLoaderService } from 'ngx-ui-loader';
import { ApplicationStatusService } from './../../services/application-status.service';
import { catchError, takeUntil } from 'rxjs/operators';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { ApplicationStatusBaseComponent } from '../application-status-base.component';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
@Component({
  selector: 'group-cancel-application',
  templateUrl: './cancel-application.component.html',
  styleUrls: ['./cancel-application.component.scss']
})
export class CancelApplicationComponent extends ApplicationStatusBaseComponent implements OnInit {
  destroy$ = new Subject();
  isSuccess: boolean = false;
  hasError: boolean = false;
  errorMsg: string;
  cancelForm: FormGroup;
  listofReason = [
  {id: 1, name: "Premiums are expensive."},
  {id: 2, name: "I can't complete the requirements."},
  {id: 3, name: "My company does not suit the required industry."},
  {id: 4, name: "I have encountered technical difficulties."},
  {id: 5, name: "I'm looking for other group plans that are not present in the InLife Store."},
  {id: 6, name: "Others, please specify:"}
];


  constructor(router: Router,  
              activatedRoute: ActivatedRoute,
              private ngxService: NgxUiLoaderService,
              private formBuilder: FormBuilder,
              private cd: ChangeDetectorRef,
              private appStatusService_API: ApplicationStatusService) { 
    super(router, activatedRoute)
  }

  ngOnInit(): void {

    this.cancelForm = this.formBuilder.group({ 
			feedbackOption: new FormControl("", [Validators.required, Validators.maxLength(50)]),
			feedbackText: new FormControl(),
      commentandsugggestions: new FormControl("", [Validators.required, Validators.maxLength(2000)])
      });

    this.cancelForm.get('feedbackOption').valueChanges
      .pipe(takeUntil(this.destroy$))
      .subscribe(data => {
        console.log(data);
        if(data.id == 6) {
          this.cancelForm.get('feedbackText').setValidators( [Validators.required,Validators.maxLength(2000)]);
          this.cancelForm.get('feedbackText').updateValueAndValidity();
        } else {
          this.cancelForm.get('feedbackText').clearValidators();
          this.cancelForm.get('feedbackText').updateValueAndValidity();
        }
      })

    this.cd.detectChanges();
  }

  cancelApplication() {
    this.submitted = true;
    this.hasError = false;
    if(this.cancelForm.valid) {
      this.ngxService.start();
      let formData = this.cancelForm.value;
      let data = {
        reason: (formData.feedbackOption.id === 6) ? formData.feedbackText : formData.feedbackOption.name,
        comments: formData.commentandsugggestions
      }

      this.appStatusService_API.cancelApplication(this.referenceCode, data)
        .pipe(
          takeUntil(this.destroy$),
        )
        .subscribe(data => {
          this.ngxService.stopAll();
          this.router.navigate(['../application-cancelled'],{relativeTo: this.activatedRoute});
        }, error => {
          this.hasError = true;
          this.ngxService.stopAll();
          this.errorMsg = error.message;
        });
    }
  }
}
