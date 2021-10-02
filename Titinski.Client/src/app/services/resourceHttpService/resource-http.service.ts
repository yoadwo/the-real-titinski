import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Resource } from 'src/app/models/Resource';

export class ResourceHttpService<T extends Resource> {

  constructor(
    private httpClient: HttpClient,
    private url: string,
    private endpoint: string) {}

    public create(item: T): Observable<T> {
      return this.httpClient
        .post<T>(`${this.url}/${this.endpoint}`, item);
    }
  
    public update(item: T): Observable<T> {
      return this.httpClient
        .put<T>(`${this.url}/${this.endpoint}/${item.id}`, item);
    }
  
    read(id: number): Observable<T> {
      return this.httpClient
        .get<T>(`${this.url}/${this.endpoint}/${id}`);
    }
  
    list(): Observable<T[]> {
      return this.httpClient
        .get<T[]>(`${this.url}/${this.endpoint}`);        
    }
  
    delete(id: number) {
      return this.httpClient
        .delete(`${this.url}/${this.endpoint}/${id}`);
    }
}
