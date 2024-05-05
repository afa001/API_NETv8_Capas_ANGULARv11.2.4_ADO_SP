import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Factura } from 'src/app/models/factura';
import { FacturaRepository } from 'src/app/repositories/factura.repository';

@Injectable({
  providedIn: 'root'
})
export class FacturasService {

  constructor(private facturaRepository: FacturaRepository) { }

  getListFacturas(): Observable<Factura[]> {
    return this.facturaRepository.getListFacturas();
  }

  deleteFactura(id: number): Observable<any> {
    return this.facturaRepository.deleteFactura(id);
  }

  saveFactura(factura: Factura): Observable<any> {
    return this.facturaRepository.saveFactura(factura);
  }

  updateFactura(id: number, factura: Factura): Observable<any> {
    return this.facturaRepository.updateFactura(id, factura);
  }

  buscarByCliente(clienteId: number): Observable<Factura[]> {
    return this.facturaRepository.buscarByCliente(clienteId);
  }

  buscarByFactura(numeroFactura: number): Observable<Factura[]> {
    return this.facturaRepository.buscarByFactura(numeroFactura);
  }
}
