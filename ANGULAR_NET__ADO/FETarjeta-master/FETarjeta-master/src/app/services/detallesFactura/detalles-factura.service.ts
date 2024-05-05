import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DetallesFacturaService {
  private myAppUrl = 'https://localhost:7126/';
  private myApiUrl = 'api/DetallesFactura/'

  constructor(private http: HttpClient) { }

  getListDetallesFactura(): Observable<any> {
    return this.http.get(this.myAppUrl + this.myApiUrl);
  }

  getDetalleFacturaById(id: number): Observable<any> {
    return this.http.get(this.myAppUrl + this.myApiUrl + id);
  }  

  deleteDetalleFactura(id: number): Observable<any> {
    return this.http.delete(this.myAppUrl + this.myApiUrl + id)
  }

  saveDetalleFactura(detalleFactura: any): Observable<any> {
    return this.http.post(this.myAppUrl + this.myApiUrl, detalleFactura);
  }

  updateDetalleFactura(id: number, detalleFactura: any): Observable<any> {
    return this.http.put(this.myAppUrl + this.myApiUrl + id, detalleFactura);
  }
}
