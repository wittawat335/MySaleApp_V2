import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ResponseApi } from '../Interface/response-api';
import { Login } from '../Interface/login';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private urlApi: string = environment.apiUrl + 'User';

  constructor(private http: HttpClient) {}

  GetList(): Observable<ResponseApi> {
    return this.http.get<ResponseApi>(this.urlApi);
  }

  Login(req: Login): Observable<ResponseApi> {
    return this.http.post<ResponseApi>(`${this.urlApi}/Login`, req);
  }

  Register(req: Login): Observable<ResponseApi> {
    return this.http.post<ResponseApi>(`${this.urlApi}/Register`, req);
  }

  Update(req: Login): Observable<ResponseApi> {
    return this.http.put<ResponseApi>(`${this.urlApi}/Update`, req);
  }

  Delete(id: number): Observable<ResponseApi> {
    return this.http.delete<ResponseApi>(`${this.urlApi}/${id}`);
  }
}
