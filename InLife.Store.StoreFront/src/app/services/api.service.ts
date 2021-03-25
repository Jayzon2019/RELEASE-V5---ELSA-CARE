import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Subject } from 'rxjs';

@Injectable()
export class ApiService
{
	headers_object;
	public sidebar_value = new Subject();
	public isShowFooterandHeader$: Subject<boolean> = new Subject<boolean>();

	constructor(private http: HttpClient)
	{
		let token = localStorage.getItem('token') || null;
		this.headers_object = new HttpHeaders().set("Authorization", "Bearer " + token);
	}

	getFooterHeaderStatus() {
		return this.isShowFooterandHeader$.asObservable();
	}

	setMessage(value)
	{
		this.sidebar_value.next(value);
	}

	getRequest(url)
	{
		var obj = this.http.get(environment.appApi.host + url);
		return obj;
		//return this.http.get(environment.appApi.host + url, { headers: this.headers_object });
	}

	sendGetRequest(url)
	{
		return this.http.get(environment.appApi.host + url);
	}

	deleteRequest(url)
	{
		return this.http.delete(environment.appApi.host + url, { headers: this.headers_object });
	}

	postRequest(url, data)
	{
		return this.http.post(environment.appApi.host + url, data, { headers: this.headers_object });
	}

	updateRequest(url, data)
	{
		//return this.http.put(environment.baseUrl + 'country/updateCountry/' + id, data);
		return this.http.patch(environment.appApi.host + url, data, { headers: this.headers_object });
	}

	putRequest(url, data: any =''){
		return this.http.put(environment.appApi.host + url, data, { headers: this.headers_object });
	}

	uploadImage(url, fileToUpload)
	{
		const uploadData: FormData = new FormData();
		if (fileToUpload)
		{
			uploadData.append('file', fileToUpload, fileToUpload.name);
		}
		return this.http.post(environment.appApi.host + url, uploadData, { headers: this.headers_object });
	}

	getUserFromLocal(): any
	{
		return localStorage.getItem('token');
	}





}
