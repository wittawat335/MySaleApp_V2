import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ResponseApi } from '../Interface/response-api';

@Injectable({
  providedIn: 'root',
})
export class DashboardService {
  private url: string = environment.apiUrl + 'Dashboard';

  constructor(private http: HttpClient) {}

  Summary(): Observable<ResponseApi> {
    return this.http.get<ResponseApi>(this.url + '/Summary');
  }
}
