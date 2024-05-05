import { TestBed } from '@angular/core/testing';

import { DetallesFacturaService } from './detalles-factura.service';

describe('DetallesFacturaService', () => {
  let service: DetallesFacturaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DetallesFacturaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
