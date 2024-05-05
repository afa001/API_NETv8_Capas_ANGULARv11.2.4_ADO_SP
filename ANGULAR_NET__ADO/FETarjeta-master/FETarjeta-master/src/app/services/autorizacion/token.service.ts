import { HttpClient,HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Login } from 'src/app/models/login';

@Injectable({
  providedIn: 'root'
})
export class TokenService {
  private myAppUrl = 'https://localhost:7126/';
  private myApiUrl = 'api/Login/'

  constructor(private http: HttpClient) { }

  Login(login: Login): Observable<any> {
    return this.http.post(this.myAppUrl + this.myApiUrl + "Login/", login);
  }  
}
