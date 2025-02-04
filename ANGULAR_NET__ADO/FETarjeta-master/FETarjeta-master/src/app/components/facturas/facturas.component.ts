import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { FacturasService } from 'src/app/services/facturas/facturas.service';
import { ClientesService } from 'src/app/services/clientes/clientes.service';
import { DetallesFacturaService } from 'src/app/services/detallesFactura/detalles-factura.service';
import { ProductosService } from 'src/app/services/productos/productos.service';
import { Factura } from 'src/app/models/factura';
import { TokenService } from 'src/app/services/autorizacion/token.service';
import { switchMap,tap, catchError, mergeMap } from 'rxjs/operators';
import { Observable, of } from 'rxjs';

@Component({
  selector: 'app-facturas',
  templateUrl: './facturas.component.html',
  styleUrls: ['./facturas.component.css']
})
export class FacturasComponent implements OnInit {

  listFacturas: any[] = [];
  listClientes: any[] = [];
  listProductos: any[] = [];
  accion = 'Agregar';
  form: FormGroup;
  formSearch: FormGroup;
  id: number | undefined;
  detalleFacturaId: number | undefined;
  selectedProducto: any; 
  token: string | undefined;

  constructor(private fb: FormBuilder,
    private toastr: ToastrService,
    private _facturaService: FacturasService,
    private _clienteService: ClientesService,
    private _detalleFacturaService: DetallesFacturaService,
    private _productoService: ProductosService,
    private _tokenService: TokenService,
    private formBuilder: FormBuilder    
    ) {
    this.form = this.fb.group({
      fechaEmisionFactura: ['', Validators.required],
      idCliente: ['', Validators.required],
      numeroFactura: ['', [Validators.required, Validators.min(1)]],
      numeroTotalArticulos: ['', [Validators.required, Validators.min(1)]],
      subTotalFacturas: ['', [Validators.required, Validators.min(0.01)]],
      totalImpuestos: ['', [Validators.required, Validators.min(0.01)]],
      totalFactura: ['', [Validators.required, Validators.min(0.01)]],
      idProducto: ['', Validators.required],
      cantidadDeProducto: ['', [Validators.required, Validators.min(1)]],
      precioUnitarioProducto: ['', [Validators.required, Validators.min(0.01)]],
      subtotalProducto: ['', [Validators.required, Validators.min(0.01)]],
      notas: ['', Validators.required]          
    });
      
    this.formSearch = this.formBuilder.group({
      idClienteSearch: [''],
      numeroFacturaSearch: [''],
      searchOption: ['cliente']
    });

    this.formSearch.get('idClienteSearch')?.enable();
    this.formSearch.get('numeroFacturaSearch')?.disable();

    this.formSearch.get('searchOption')?.valueChanges.subscribe(selectedOption => {
      if (selectedOption === 'cliente') {
        this.formSearch.get('idClienteSearch')?.enable();
        this.formSearch.get('numeroFacturaSearch')?.disable();
        this.formSearch.get('numeroFacturaSearch')?.reset();
      } else {
        this.formSearch.get('idClienteSearch')?.disable();
        this.formSearch.get('numeroFacturaSearch')?.enable();
        this.formSearch.get('idClienteSearch')?.reset();
      }      
    });   
  }

  ngOnInit(): void {
    this.cargarDatosDeInicio();

    const cantidadDeProductoControl = this.form.get('cantidadDeProducto');
    const precioUnitarioProductoControl = this.form.get('precioUnitarioProducto');
    const subtotalProductoControl = this.form.get('subtotalProducto');
    const subTotalFacturasControl = this.form.get('subTotalFacturas');
    const totalImpuestosControl = this.form.get('totalImpuestos');
    const totalFacturaControl = this.form.get('totalFactura');

    if (cantidadDeProductoControl && precioUnitarioProductoControl && subtotalProductoControl && subTotalFacturasControl && totalImpuestosControl && totalFacturaControl) {
      cantidadDeProductoControl.valueChanges.subscribe(cantidad => {
          const selectedProducto = this.listProductos.find(producto => producto.Id === +this.selectedProducto);
        
          if (selectedProducto) {
            const subtotalProducto = cantidad * selectedProducto.PrecioUnitario;
            subtotalProductoControl.setValue(subtotalProducto);
        
            const subTotalFacturas = this.calculateSubTotalFacturas(cantidad, selectedProducto.PrecioUnitario);
            subTotalFacturasControl.setValue(subTotalFacturas);
        
            const totalImpuestos = subTotalFacturas * 0.19;
            totalImpuestosControl.setValue(totalImpuestos);
        
            const totalFactura = subTotalFacturas + totalImpuestos;
            totalFacturaControl.setValue(totalFactura);
          }
      });
    }    
  }

  cargarDatosDeInicio() {
    const loginModel = { UserName: 'myUsername', Password: 'myPassword' };
    this._tokenService.Login(loginModel).pipe(
      switchMap(data => {
        this.token = data.token;
        return this.obtenerDatosIniciales(data.token); 
      }),
      catchError(error => {
        this.toastr.error('Error al obtener el token', 'Error');
        return of(null);
      })
    ).subscribe(() => {
      console.log('Datos iniciales cargados');
    });
  }

  calculateSubTotalFacturas(cantidad: number, precioUnitario: number): number {
    return cantidad * precioUnitario;
  }    
  
  obtenerDatosIniciales(token: string): Observable<any> {
    return this._clienteService.getListClientes(token).pipe(
      tap(clientes => {
        this.listClientes = clientes;
      }),
      catchError(error => {
        this.toastr.error('Error al obtener la lista de clientes', 'Error');
        return of(null); 
      }),
      mergeMap(() => {
        return this._productoService.getListProductos(token).pipe(
          tap(productos => {
            this.listProductos = productos;
          }),
          catchError(error => {
            this.toastr.error('Error al obtener la lista de productos', 'Error');
            return of(null); 
          })
        );
      }),
      mergeMap(() => {
        return this._facturaService.getListFacturas(token).pipe(
          tap(facturas => {
            this.listFacturas = facturas;
          }),
          catchError(error => {
            this.toastr.error('Error al obtener la lista de facturas', 'Error');
            return of(null); 
          })
        );
      })
    );
  }  

  nuevo() {
    this.form.reset({
        titular: '',
        numeroFactura: '',
        fechaExpiracion: '',
        cvv: '',
        fechaEmisionFactura: '',
        idCliente: '',
        numeroTotalArticulos: '',
        subTotalFacturas: '',
        totalImpuestos: '',
        totalFactura: '',
        IdFactura: '',
        IdProducto: '',
        CantidadDeProducto: '',
        PrecioUnitarioProducto: '',
        SubtotalProducto: '',
        Notas: ''        
    });
  }

  guardarFactura() {
    if (!this.isTokenAvailable()) {
      return;
    }
  
    const factura: Factura = {
      Id: 0,
      FechaEmisionFactura: this.form.get('fechaEmisionFactura')?.value,
      IdCliente: this.form.get('idCliente')?.value,
      NumeroFactura: this.form.get('numeroFactura')?.value,
      NumeroTotalArticulos: this.form.get('numeroTotalArticulos')?.value,
      SubTotalFacturas: this.form.get('subTotalFacturas')?.value,
      TotalImpuestos: this.form.get('totalImpuestos')?.value,
      TotalFactura: this.form.get('totalFactura')?.value,
    };
  
    if (this.id == undefined) {
      this._facturaService.saveFactura(this.token!, factura).pipe(
        switchMap(data => {
          const idFacturaGuardada = data.id;
          const detalleFactura: any = {
            IdFactura: idFacturaGuardada,
            IdProducto: this.form.get('idProducto')?.value,
            CantidadDeProducto: this.form.get('cantidadDeProducto')?.value,
            PrecioUnitarioProducto: this.form.get('precioUnitarioProducto')?.value,
            SubtotalProducto: this.form.get('subtotalProducto')?.value,
            Notas: this.form.get('notas')?.value,
          };
          return this.token ? this._detalleFacturaService.saveDetalleFactura(this.token, detalleFactura) : of(null);
        }),
        catchError(error => {
          this.toastr.error('Error al guardar la factura', 'Error');
          return of(null);
        })
      ).subscribe(data => {
        if (data) {
          this.toastr.success('La factura fue registrada con éxito!', 'Factura Registrada');
          this.obtenerFacturas();
          this.form.reset();
        }
      });
    } else {
      factura.Id = this.id;
      this._facturaService.updateFactura(this.token!, this.id, factura).pipe(
        switchMap(() => {
          const detalleFactura: any = {
            Id: this.detalleFacturaId,
            IdFactura: this.id,
            IdProducto: this.form.get('idProducto')?.value,
            CantidadDeProducto: this.form.get('cantidadDeProducto')?.value,
            PrecioUnitarioProducto: this.form.get('precioUnitarioProducto')?.value,
            SubtotalProducto: this.form.get('subtotalProducto')?.value,
            Notas: this.form.get('notas')?.value,
          };
          return this.token ? this._detalleFacturaService.updateDetalleFactura(this.token, detalleFactura.Id, detalleFactura) : of(null);
        }),
        catchError(error => {
          this.toastr.error('Error al actualizar la factura', 'Error');
          return of(null);
        })
      ).subscribe(data => {
        if (data) {
          this.toastr.info('La factura y su detalle fueron actualizados con éxito!', 'Factura Actualizada');
          this.obtenerFacturas();
          this.form.reset();
          this.accion = 'Agregar';
          this.id = undefined;
        }
      });
    }
  }
  
  private isTokenAvailable(): boolean {
    if (!this.token) {
      this.toastr.error('Token no disponible', 'Error');
      return false;
    }
    return true;
  }  

  eliminarFactura(id: number) {
    if (!this.isTokenAvailable()) {
      return;
    }    
  
    this._facturaService.deleteFactura(this.token!, id).pipe(
      catchError(error => {
        this.toastr.error('No se pudo eliminar la factura!', 'Error');
        return of(null);
      })
    ).subscribe(response => {
      if (response?.message) { 
        this.obtenerFacturas();
        this.toastr.success(response.message, 'Factura eliminada');
      } else {
        this.toastr.error('No se pudo eliminar la factura!', 'Error');
      }
    });
  }

  obtenerFacturas() {
    if (!this.isTokenAvailable()) {
      return;
    }    

    this._facturaService.getListFacturas(this.token!).pipe(
      catchError(error => {
        this.toastr.error('Error al obtener las facturas', 'Error');
        return of([]);
      })
    ).subscribe(data => {
      this.listFacturas = data;
    });
  }

  buscar() {
    if (!this.isTokenAvailable()) {
      return;
    }

    const searchOption = this.formSearch.get('searchOption')?.value;
    if (searchOption === 'cliente') {
      const clienteId = this.formSearch.get('idClienteSearch')?.value;
      if (clienteId) {
        this._facturaService.buscarByCliente(this.token!, clienteId).pipe(
          catchError(error => {
            this.toastr.error('Error al buscar por cliente', 'Error');
            return of([]);
          })
        ).subscribe(data => {
          this.listFacturas = data;
        });
      } else {
        this.toastr.warning('Cliente ID es requerido', 'Advertencia');
      }
    } else {
      const numeroFactura = this.formSearch.get('numeroFacturaSearch')?.value;
      if (numeroFactura) {
        this._facturaService.buscarByFactura(this.token!, numeroFactura).pipe(
          catchError(error => {
            this.toastr.error('Error al buscar por factura', 'Error');
            return of([]);
          })
        ).subscribe(data => {
          this.listFacturas = data;
        });
      } else {
        this.toastr.warning('Número de factura es requerido', 'Advertencia');
      }
    }
  }

  editarFactura(factura: Factura) {
    this.accion = 'Editar'; 
    this.id = factura.Id; 
  
    // Convertir la fecha al formato yyyy-MM-dd
    const fechaEmision = new Date(factura.FechaEmisionFactura);
    const formattedFechaEmision = fechaEmision.toISOString().split('T')[0];

    this.form.patchValue({
      fechaEmisionFactura: formattedFechaEmision,
      idCliente: factura.IdCliente,
      numeroFactura: factura.NumeroFactura,
      numeroTotalArticulos: factura.NumeroTotalArticulos,
      subTotalFacturas: factura.SubTotalFacturas,
      totalImpuestos: factura.TotalImpuestos,
      totalFactura: factura.TotalFactura
    });
  
    if (!this.isTokenAvailable()) {
      return;
    }

    this._detalleFacturaService.getDetalleFacturaById(this.token!, factura.Id).pipe(
      catchError(error => {
        this.toastr.error('Error al obtener los detalles de la factura', 'Error');
        return of(null);
      })
    ).subscribe(detalleFactura => {
      if (detalleFactura) {
        this.detalleFacturaId = detalleFactura.Id;          
        this.form.patchValue({
          idProducto: detalleFactura.IdProducto,
          cantidadDeProducto: detalleFactura.CantidadDeProducto,
          precioUnitarioProducto: detalleFactura.PrecioUnitarioProducto,
          subtotalProducto: detalleFactura.SubtotalProducto,
          notas: detalleFactura.Notas
        });
      }
    });
  }
  
  onProductoSelected(idProducto: number) {
    this.selectedProducto = idProducto;
    const selectedProducto = this.listProductos.find(producto => producto.Id === +idProducto);
    if (selectedProducto) {
      this.form?.get('precioUnitarioProducto')?.setValue(selectedProducto.PrecioUnitario);
    }
  }    
}