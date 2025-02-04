import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Factura } from 'src/app/models/factura';

@Injectable({
  providedIn: 'root'
})
export class FacturaRepository {
  private readonly BASE_URL = 'https://localhost:7126/';
  private readonly API_URL = `${this.BASE_URL}api/Facturas/`;

  constructor(private http: HttpClient) { }

  private getHeaders(token: string): HttpHeaders {
    return new HttpHeaders().set('Authorization', `Bearer ${token}`);
  }

  private handleError(error: HttpErrorResponse): Observable<never> {
    console.error('An error occurred:', error);
    return throwError('Something went wrong; please try again later.');
  }

  getListFacturas(token: string): Observable<Factura[]> {
    const headers = this.getHeaders(token);
    return this.http.get<Factura[]>(this.API_URL, { headers }).pipe(
      catchError(this.handleError)
    );
  }

  deleteFactura(token: string, id: number): Observable<any> {
    const headers = this.getHeaders(token);
    return this.http.delete(`${this.API_URL}${id}`, { headers }).pipe(
      catchError(this.handleError)
    );
  }

  saveFactura(token: string, factura: Factura): Observable<any> {
    const headers = this.getHeaders(token);
    return this.http.post(this.API_URL, factura, { headers }).pipe(
      catchError(this.handleError)
    );
  }

  updateFactura(token: string, id: number, factura: Factura): Observable<any> {
    const headers = this.getHeaders(token);
    return this.http.put(`${this.API_URL}${id}`, factura, { headers }).pipe(
      catchError(this.handleError)
    );
  }

  buscarByCliente(token: string, clienteId: number): Observable<Factura[]> {
    const headers = this.getHeaders(token);
    return this.http.get<Factura[]>(`${this.API_URL}GetFacturasByCliente/${clienteId}`, { headers }).pipe(
      catchError(this.handleError)
    );
  }

  buscarByFactura(token: string, numeroFactura: number): Observable<Factura[]> {
    const headers = this.getHeaders(token);
    return this.http.get<Factura[]>(`${this.API_URL}GetFacturasByNumeroFactura/${numeroFactura}`, { headers }).pipe(
      catchError(this.handleError)
    );
  }
}