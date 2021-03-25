import { SessionStorageService } from './../../../../services/session-storage.service';
import { Component, ElementRef } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';

@Component({
  selector: 'application-status-base',
  template: ''
})
export class ApplicationStatusBaseComponent {
    public referenceCode = '';
    public submitted: boolean;
    public error: string = '';
    constructor(
        public router: Router,
        public activatedRoute: ActivatedRoute,
    ) {
        this.referenceCode = this.activatedRoute.snapshot.paramMap.get("referenceCode");
     }


  ngOnInit() {

  }

  backToHome() {
    this.router.navigate(['/']);
  }

  backToGroupPage() {
    this.router.navigate(['/group']);
  }

  cancelApplication() {
    this.router.navigate(['../cancel-application'],{relativeTo: this.activatedRoute});
  }

  checkPaymentStatus() {
    this.router.navigate(['../payment-pending'],{relativeTo: this.activatedRoute});
  }
  ngOnDestroy() {

  }

}
