import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { ApiService, SessionStorageService } from '@app/services';
@Component({
  selector: 'app-prime-secure-lite-sidebar',
  templateUrl: './prime-secure-lite-sidebar.component.html',
  styleUrls: ['./prime-secure-lite-sidebar.component.scss']
})
export class PrimeSecureLiteSidebarComponent implements OnInit {
  	subscription;
	subscriptions;
	status;
	annual: string;
  constructor(private apiService: ApiService,
	private router: Router,
		private session: SessionStorageService) { }

  ngOnInit(): void {
	  	this.annual = '0.00';
    	const getQuoteFormData = this.session.get('getinnerForm');

		this.subscriptions = getQuoteFormData;
		this.status = 1;
		if (!getQuoteFormData)
		{
			this.status = 0;
		}
		this.subscription = this.apiService.sidebar_value.subscribe(
			(message) =>
			{
				this.subscriptions = message;
				this.status = 0;
			}
		);
  }

}
