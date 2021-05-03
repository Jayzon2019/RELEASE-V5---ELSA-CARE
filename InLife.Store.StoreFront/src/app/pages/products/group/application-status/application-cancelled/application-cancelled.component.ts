import { SessionStorageService } from './../../../../../services/session-storage.service';
import { ApplicationStatusBaseComponent } from './../application-status-base.component';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-application-cancelled',
  templateUrl: './application-cancelled.component.html',
  styleUrls: ['../styles.scss','./application-cancelled.component.scss']
})
export class ApplicationCancelledComponent extends ApplicationStatusBaseComponent implements OnInit {

  constructor(router: Router, 
    activatedRoute: ActivatedRoute, private session: SessionStorageService) {
      super(router, activatedRoute)
     }

  ngOnInit(): void {
      this.session.clear();
  }

}
