import { Component, OnInit, ViewChild } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ReactiveFormsModule, FormBuilder, FormControl, FormGroup, FormArray } from '@angular/forms';

import { HttpClient, HttpResponse, HttpHeaders, HttpParams } from '@angular/common/http';

import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';


@Component
({
	selector: 'app-api-tester',
	templateUrl: './api-tester.component.html',
	styleUrls: ['./api-tester.component.css']
})
export class ApiTesterComponent
{
	methods =
	[
		{name: 'POST', value: 'POST'},
		{name: 'PUT', value: 'PUT'},
		{name: 'GET', value: 'GET'},
		{name: 'REDIRECT', value: 'REDIRECT'}
	  ];


	formGroup: FormGroup = new FormGroup
	({
		endpoint: new FormControl(),
		body: new FormControl(),
		headerKey1: new FormControl('Content-Type'),
		headerValue1: new FormControl('application/json'),
		headerKey2: new FormControl('Accept'),
		headerValue2: new FormControl('application/json'),
		headerKey3: new FormControl(),
		headerValue3: new FormControl(),
		method: new FormControl(this.methods[0])
	});

	constructor
	(
		private router: Router,
		private formBuilder: FormBuilder,
		private route: ActivatedRoute,
		private http: HttpClient
	)
	{
	}

	submit(): void
	{
		console.log(this.formGroup.controls);

		let endpoint = this.formGroup.controls['endpoint'].value;
		console.log('endpoint: ' + endpoint);

		let body = this.formGroup.controls['body'].value;
		console.log('body: ' + body);

		let headers = new HttpHeaders();
		for(let i = 1; i<=3; i++)
		{
			let key = this.formGroup.controls['headerKey' + i].value;
			let value = this.formGroup.controls['headerValue' + i].value;

			if(key !== '' && value !== '')
			{
				headers = headers.append(key, value);
			}
		}
		console.log(headers);

		let options =
		{
			headers: new HttpHeaders(),
			params: new HttpParams()
		};

		let method = this.formGroup.controls['method'].value;
		switch(method.value)
		{
			case 'GET':
				console.log('GET');
				this.http
					.get(endpoint, options)
					.pipe(catchError(this.handleError))
					.subscribe(data => { console.log(data); });
				break;
			case 'POST':
				console.log('POST');
				this.http
					.post(endpoint, body, options)
					.pipe(catchError(this.handleError))
					.subscribe(data => { console.log(data); });
				break;
			case 'PUT':
				console.log('PUT');
				this.http
					.put(endpoint, body, options)
					.pipe(catchError(this.handleError))
					.subscribe(data => { console.log(data); });
				break;
			case 'REDIRECT':
				console.log('REDIRECT');
				window.location.href = endpoint;
				break;
		}
	}

	handleError(error: any)
	{
		let errMsg =
			(error.message) ? error.messagess
				: error.status ? `${error.status} - ${error.statusText}`
					: 'API error';

		console.error(errMsg);
		return throwError(error);
	}
}
