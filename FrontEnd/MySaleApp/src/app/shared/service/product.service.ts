import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ResponseApi } from '../Interface/response-api';
import { Product } from '../Interface/product';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private urlApi: string = environment.apiUrl + 'Product';

  constructor(private http: HttpClient) {}

  GetList(): Observable<ResponseApi> {
    return this.http.get<ResponseApi>(this.urlApi);
  }

  Create(req: Product): Observable<ResponseApi> {
    return this.http.post<ResponseApi>(`${this.urlApi}/Create`, req);
  }

  Update(req: Product): Observable<ResponseApi> {
    return this.http.put<ResponseApi>(`${this.urlApi}/Update`, req);
  }

  Delete(id: number): Observable<ResponseApi> {
    return this.http.delete<ResponseApi>(`${this.urlApi}/${id}`);
  }
}
