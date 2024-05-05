import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductosService {
  private myAppUrl = 'https://localhost:7126/';
  private myApiUrl = 'api/Productos/'

  constructor(private http: HttpClient) { }

  getListProductos(): Observable<any> {
    return this.http.get(this.myAppUrl + this.myApiUrl);
  }

  getProductoById(id: number): Observable<any> {
    return this.http.get(this.myAppUrl + this.myApiUrl + id);
  }  

  deleteProducto(id: number): Observable<any> {
    return this.http.delete(this.myAppUrl + this.myApiUrl + id)
  }

  saveProducto(producto: any): Observable<any> {
    return this.http.post(this.myAppUrl + this.myApiUrl, producto);
  }

  updateProducto(id: number, producto: any): Observable<any> {
    return this.http.put(this.myAppUrl + this.myApiUrl + id, producto);
  }
}
