import { Component, OnInit } from '@angular/core';
import { ApiService } from 'src/app/services/api.service';
import { LocalStorageService, SessionStorageService, LocalStorage, SessionStorage } from 'angular-web-storage';
@Component({
  selector: 'app-inner-sidebar',
  templateUrl: './inner-sidebar.component.html',
  styleUrls: ['./inner-sidebar.component.css']
})
export class InnerSidebarComponent implements OnInit {
  subscription;
  subscriptions;
  status;
  constructor(private apiService: ApiService,public session: SessionStorageService) { }

  ngOnInit(): void {
  const getQuoteFormData = this.session.get("getinnerForm");
  
  this.subscriptions = getQuoteFormData;
  this.status=1;
 if(!getQuoteFormData)
 {
  this.status=0;
 }
    this.subscription = this.apiService.sidebar_value.subscribe(
      (message) => {

        this.subscription = message;
         this.status=0;
        
        
      }
    );
  }

}
