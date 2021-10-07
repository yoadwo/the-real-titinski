import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Rant } from 'src/app/models/rant';
import { environment } from 'src/environments/environment';
import { ResourceHttpService } from '../resourceHttpService/resource-http.service';

@Injectable({
  providedIn: 'root'
})
export class RantHttpService extends ResourceHttpService<Rant> {  

  constructor(httpClient: HttpClient) {
    super(
      httpClient,
      environment.baseUrl,
      'main/rant')

      //this.httpClient = httpClient;
   }

   getPhoto(path: string) {
      return this.httpClient.get(environment.baseUrl + '/main/rantFile/' + path,
       { responseType: 'blob' });
  }
}
