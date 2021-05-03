import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ApplicationStatusBaseComponent } from '../application-status-base.component';

@Component({
  selector: 'app-payment-confirmation',
  templateUrl: './payment-confirmation.component.html',
  styleUrls: ['../styles.scss','./payment-confirmation.component.scss']
})
export class PaymentConfirmationComponent extends ApplicationStatusBaseComponent implements OnInit {

  constructor(router: Router, 
              activatedRoute: ActivatedRoute,) {
                super(router, activatedRoute)
               }

  ngOnInit(): void {
  }

}
