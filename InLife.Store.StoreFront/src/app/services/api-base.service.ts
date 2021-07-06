import { HttpHeaders } from '@angular/common/http';
import { SessionStorageService } from '@app/services';
import { StorageType } from '@app/services/storage-types.enum';
import { environment } from '@environment';
import { Observable, throwError } from 'rxjs';

export abstract class ApiBaseService {
  constructor(private session: SessionStorageService) {}
  protected handleError(error: any) {
    let response: any = error;
    let type = response.status;
    if(!response?.ok) {
      switch(type){
        case 0: 
          response = {
            ok: error?.ok,
            status: type,
            statusText: error?.statusText,
            message: 'No Internet Connection. If error persist please contact support.'
          }
          break;
        case 400:
          response = {
            ok: error?.ok,
            status: type,
            statusText: error?.statusText,
            message: error?.error?.detail
          }
          break;
        case 401:
          response = {
            ok: error?.ok,
            status: type,
            statusText: error?.statusText,
            message: "Application session has been expired. Kindly continue your application by using reference code sent to your email and contact number in InLife's Group page."
          }
          break;
        case 500:
          response = {
            ok: error?.ok,
            status: type,
            statusText: error?.statusText,
            message: error?.error?.detail
          }
          break;
        default:
          response = {
            ok: error?.ok,
            status: type,
            statusText: error?.statusText,
            message: "Unknown error occured. If error persist please contact support."
          }
          break;
      }
    }

    return throwError(response);
  }

  protected baseURL() {
    return environment.appApi.host;
  }

  protected baseURLPayment() {
    return environment.primeCareApi.host;
  }

  protected headerWithSession() {
    const token = this.session.get(StorageType.SESSION);
    let headers: HttpHeaders = new HttpHeaders();
    headers = headers.append('Session', token);
    return headers;
  }

  protected headerPaymentGateWay() {
    let headers: HttpHeaders = new HttpHeaders();
		headers = headers.append('Content-Type', 'application/json');
		headers = headers.append('Ocp-Apim-Subscription-Key', environment.primeCareApi.subscriptionKey);
    return headers;
  }
}
