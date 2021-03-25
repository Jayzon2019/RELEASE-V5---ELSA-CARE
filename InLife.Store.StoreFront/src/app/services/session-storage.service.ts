import { Injectable } from '@angular/core';
import { CryptographyService } from './cryptography.service';

@Injectable()
export class SessionStorageService
{

	private secrets = {
		session: {
		  key: "Z79nSF6xLP_tmXq9bJQKD",
		  secret: "ne3SQa38zf",
		}
	}

	constructor(private crypto: CryptographyService)
	{
	}

	set(key: string | any, value: any)
	{
		sessionStorage.setItem(key, this.crypto.Encrypt(JSON.stringify(value), this.secrets.session.secret));
		// sessionStorage.setItem(key, JSON.stringify(value));
	}

	get(key: string | any): any
	{
		let data = sessionStorage.getItem(key);
		try { return JSON.parse(this.crypto.Decrypt(data, this.secrets.session.secret)); }
		// try { return JSON.parse(data); }
		catch (ex) { return {}; }
	}

	remove(key: string)
	{
		sessionStorage.removeItem(key);
	}

	clear()
	{
		sessionStorage.clear();
	}
}
