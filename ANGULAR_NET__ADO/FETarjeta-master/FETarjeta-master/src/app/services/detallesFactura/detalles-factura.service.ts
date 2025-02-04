import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class DetallesFacturaService {
  private readonly BASE_URL = 'https://localhost:7126/';
  private readonly API_URL = `${this.BASE_URL}api/DetallesFactura/`;

  constructor(private http: HttpClient) { }

  private getHeaders(token: string): HttpHeaders {
    return new HttpHeaders().set('Authorization', `Bearer ${token}`);
  }

  private handleError(error: HttpErrorResponse): Observable<never> {
    console.error('An error occurred:', error);
    return throwError('Something went wrong; please try again later.');
  }

  getListDetallesFactura(token: string): Observable<any> {
    const headers = this.getHeaders(token);
    return this.http.get(this.API_URL, { headers }).pipe(
      catchError(this.handleError)
    );
  }

  getDetalleFacturaById(token: string, id: number): Observable<any> {
    const headers = this.getHeaders(token);
    return this.http.get(`${this.API_URL}${id}`, { headers }).pipe(
      catchError(this.handleError)
    );
  }

  deleteDetalleFactura(token: string, id: number): Observable<any> {
    const headers = this.getHeaders(token);
    return this.http.delete(`${this.API_URL}${id}`, { headers }).pipe(
      catchError(this.handleError)
    );
  }

  saveDetalleFactura(token: string, detalleFactura: any): Observable<any> {
    const headers = this.getHeaders(token);
    return this.http.post(this.API_URL, detalleFactura, { headers }).pipe(
      catchError(this.handleError)
    );
  }

  updateDetalleFactura(token: string, id: number, detalleFactura: any): Observable<any> {
    const headers = this.getHeaders(token);
    return this.http.put(`${this.API_URL}${id}`, detalleFactura, { headers }).pipe(
      catchError(this.handleError)
    );
  }
}