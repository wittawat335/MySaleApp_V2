import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ResponseApi } from '../Interface/response-api';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class RoleService {
  private urlApi: string = environment.apiUrl + 'Role';

  constructor(private http: HttpClient) {}

  GetList(): Observable<ResponseApi> {
    return this.http.get<ResponseApi>(this.urlApi);
  }
}
