import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Rant } from 'src/app/models/rant';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class HttpService {
  private endpoint = 'main/rant';

  constructor(
    protected httpClient: HttpClient
  ) { }

  public create(rant: Rant): Observable<Rant> {
    return this.httpClient
      .post<Rant>(`${environment.baseUrl}/${this.endpoint}`, rant);
  }

  public update(rant: Rant): Observable<Rant> {
    return this.httpClient
      .put<Rant>(`${environment.baseUrl}/${this.endpoint}/${rant.id}`, rant);
  }

  read(id: number): Observable<Rant> {
    return this.httpClient
      .get<Rant>(`${environment.baseUrl}/${this.endpoint}/${id}`);
  }

  list(): Observable<Rant[]> {
    return this.httpClient
      .get<Rant[]>(`${environment.baseUrl}/${this.endpoint}`);
  }

  delete(id: number) {
    return this.httpClient
      .delete(`${environment.baseUrl}/${this.endpoint}/${id}`);
  }
}
