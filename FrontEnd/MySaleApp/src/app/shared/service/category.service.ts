import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ResponseApi } from '../Interface/response-api';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  private url: string = environment.apiUrl + 'Category';

  constructor(private http: HttpClient) {}

  GetList(): Observable<ResponseApi> {
    return this.http.get<ResponseApi>(this.url);
  }
}
