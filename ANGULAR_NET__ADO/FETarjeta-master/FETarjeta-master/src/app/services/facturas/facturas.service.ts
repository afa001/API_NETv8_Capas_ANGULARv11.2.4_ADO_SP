import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Factura } from 'src/app/models/factura';
import { FacturaRepository } from 'src/app/repositories/factura.repository';

@Injectable({
  providedIn: 'root'
})
export class FacturasService {

  constructor(private facturaRepository: FacturaRepository) { }

  getListFacturas(token:string): Observable<Factura[]> {
    return this.facturaRepository.getListFacturas(token);
  }

  deleteFactura(token:string, id: number): Observable<any> {
    return this.facturaRepository.deleteFactura(token, id);
  }

  saveFactura(token:string, factura: Factura): Observable<any> {
    return this.facturaRepository.saveFactura(token, factura);
  }

  updateFactura(token:string, id: number, factura: Factura): Observable<any> {
    return this.facturaRepository.updateFactura(token, id, factura);
  }

  buscarByCliente(token:string, clienteId: number): Observable<Factura[]> {
    return this.facturaRepository.buscarByCliente(token, clienteId);
  }

  buscarByFactura(token:string, numeroFactura: number): Observable<Factura[]> {
    return this.facturaRepository.buscarByFactura(token, numeroFactura);
  }
}
