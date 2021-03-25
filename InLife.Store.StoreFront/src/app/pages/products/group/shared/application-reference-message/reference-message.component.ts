import { SessionStorageService } from '@app/services';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Router } from '@angular/router';
import { Subject, Subscription } from 'rxjs';
import { take, takeUntil } from 'rxjs/operators';
import { NgxUiLoaderService } from 'ngx-ui-loader';
@Component({
  selector: 'group-reference-message',
  templateUrl: './reference-message.component.html',
  styleUrls: ['../../application-status/styles.scss','./reference-message.component.scss']
})
export class ReferenceMessageComponent implements OnInit, OnDestroy {
  public referenceCode = '';
  destroy$ = new Subject();
  constructor(private router: Router, private _Activatedroute:ActivatedRoute, private session: SessionStorageService, private ngxService: NgxUiLoaderService,) { 
  }

  ngOnInit(): void {
    this.ngxService.stopAll();
    this.session.clear();
    this._Activatedroute.params
      .pipe(takeUntil(this.destroy$))
      .subscribe((param: Params) => {
        this.referenceCode = param["referenceCode"];
      });
  }

  backToHome() {
    this.session.clear();
    this.router.navigate(['/group']);
  }

  continueApplication() {
    this.router.navigate(['group/apply']);
  }
  
  ngOnDestroy() {
    this.destroy$.next(true);
    this.destroy$.unsubscribe();
  }
}
