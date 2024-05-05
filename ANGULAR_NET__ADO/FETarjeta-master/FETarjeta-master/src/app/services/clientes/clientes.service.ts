import { Injectable } from '@angular/core';
import { HttpClient,HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Login } from 'src/app/models/login';
@Injectable({
  providedIn: 'root'
})
export class ClientesService {
  private myAppUrl = 'https://localhost:7126/';
  private myApiUrl = 'api/Clientes/'

  constructor(private http: HttpClient) { }      
    getListClientes(token:string): Observable<any> {
      const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
      return this.http.get(this.myAppUrl + this.myApiUrl, { headers });
    }

    getClienteById(token:string, id: number): Observable<any> {
      const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
      return this.http.get(this.myAppUrl + this.myApiUrl + id, { headers });
    }  

    deleteCliente(token:string, id: number): Observable<any> {
      const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
      return this.http.delete(this.myAppUrl + this.myApiUrl + id, { headers });
    }

    saveCliente(token:string, cliente: any): Observable<any> {
      const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
      return this.http.post(this.myAppUrl + this.myApiUrl, cliente, { headers });
    }

    updateCliente(token: string,id: number, cliente: any): Observable<any> {
      const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
      return this.http.put(this.myAppUrl + this.myApiUrl + id, cliente, { headers });
    }  

  // getListClientes(): Observable<any> {
  //   return this.http.get(this.myAppUrl + this.myApiUrl);
  // }

  // getClienteById(id: number): Observable<any> {
  //   return this.http.get(this.myAppUrl + this.myApiUrl + id);
  // }  

  // deleteCliente(id: number): Observable<any> {
  //   return this.http.delete(this.myAppUrl + this.myApiUrl + id)
  // }

  // saveCliente(cliente: any): Observable<any> {
  //   return this.http.post(this.myAppUrl + this.myApiUrl, cliente);
  // }

  // updateCliente(id: number, cliente: any): Observable<any> {
  //   return this.http.put(this.myAppUrl + this.myApiUrl + id, cliente);
  // }
}