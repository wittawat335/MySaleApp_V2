import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Sale } from '../Interface/sale';
import { ResponseApi } from '../Interface/response-api';

@Injectable({
  providedIn: 'root',
})
export class SaleService {
  private urlApi: string = environment.apiUrl + 'Sale';

  constructor(private http: HttpClient) {}

  Register(req: Sale): Observable<ResponseApi> {
    return this.http.post<ResponseApi>(`${this.urlApi}/Register`, req);
  }

  Record(
    search: string,
    saleNumber: string,
    startDate: string,
    endDate: string
  ): Observable<ResponseApi> {
    return this.http.get<ResponseApi>(
      `${this.urlApi}/Record?search=${search}&saleNumber=${saleNumber}&startDate=${startDate}&endDate=${endDate}`
    );
  }

  Report(startDate: string, endDate: string): Observable<ResponseApi> {
    return this.http.get<ResponseApi>(
      `${this.urlApi}/Report?startDate=${startDate}&endDate=${endDate}`
    );
  }
}
