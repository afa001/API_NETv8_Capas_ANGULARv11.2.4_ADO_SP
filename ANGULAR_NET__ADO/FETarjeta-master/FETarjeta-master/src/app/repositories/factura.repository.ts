import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Factura } from 'src/app/models/factura';

@Injectable({
  providedIn: 'root'
})
export class FacturaRepository {
  private myAppUrl = 'https://localhost:7126/';
  private myApiUrl = 'api/Facturas/';

  constructor(private http: HttpClient) { }

  getListFacturas(): Observable<Factura[]> {
    return this.http.get<Factura[]>(this.myAppUrl + this.myApiUrl);
  }

  deleteFactura(id: number): Observable<any> {
    return this.http.delete(this.myAppUrl + this.myApiUrl + id);
  }

  saveFactura(factura: Factura): Observable<any> {
    return this.http.post(this.myAppUrl + this.myApiUrl, factura).pipe(
      map((response: any) => {
        return {
          id: response.id, 
          message: response.message,  
          statusCode: response.statusCode,  
        };        
      })
    );
  }

  updateFactura(id: number, factura: Factura): Observable<any> {
    return this.http.put(this.myAppUrl + this.myApiUrl + id, factura);
  }

  buscarByCliente(clienteId: number): Observable<Factura[]> {
    return this.http.get<Factura[]>(this.myAppUrl + this.myApiUrl + "GetFacturasByCliente/" + clienteId);
  }  

  buscarByFactura(numeroFactura: number): Observable<Factura[]> {
    return this.http.get<Factura[]>(this.myAppUrl + this.myApiUrl + "GetFacturasByNumeroFactura/" + numeroFactura);
  }  
}
