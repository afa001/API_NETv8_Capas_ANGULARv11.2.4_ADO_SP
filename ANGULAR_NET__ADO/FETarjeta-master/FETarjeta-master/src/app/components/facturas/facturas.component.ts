import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { FacturasService } from 'src/app/services/facturas/facturas.service';
import { ClientesService } from 'src/app/services/clientes/clientes.service';
import { DetallesFacturaService } from 'src/app/services/detallesFactura/detalles-factura.service';
import { ProductosService } from 'src/app/services/productos/productos.service';
import { Factura } from 'src/app/models/factura';
import { TokenService } from 'src/app/services/autorizacion/token.service';
import { switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-facturas',
  templateUrl: './facturas.component.html',
  styleUrls: ['./facturas.component.css']
})
export class FacturasComponent implements OnInit {

  listFacturas: Factura[] = [];
  listClientes: any[] = [];
  listProductos: any[] = [];
  accion = 'Agregar';
  form: FormGroup;
  formSearch: FormGroup;
  id: number | undefined;
  selectedProducto: any; 

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
      // idCliente: [{value: '', disabled: true}, Validators.required],
      numeroFactura: ['', [Validators.required, Validators.min(1)]],
      numeroTotalArticulos: ['', [Validators.required, Validators.min(1)]],
      subTotalFacturas: ['', [Validators.required, Validators.min(0.01)]],
      totalImpuestos: ['', [Validators.required, Validators.min(0.01)]],
      totalFactura: ['', [Validators.required, Validators.min(0.01)]],
      idFactura: ['', Validators.required],
      idProducto: ['', Validators.required],
      // idProducto: [{value: '', disabled: true}, Validators.required],
      cantidadDeProducto: ['', [Validators.required, Validators.min(1)]],
      precioUnitarioProducto: ['', [Validators.required, Validators.min(0.01)]],
      subtotalProducto: ['', [Validators.required, Validators.min(0.01)]],
      notas: ['']            
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
    this.obtenerFacturas();
    this.obtenerClientes();
    this.obtenerProductos();
  
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
  
  calculateSubTotalFacturas(cantidad: number, precioUnitario: number): number {
    return cantidad * precioUnitario;
  }  

  obtenerFacturas() {
    this._facturaService.getListFacturas().subscribe(data => {
      console.log(data);
      this.listFacturas = data;
    }, error => {
      console.log(error);
    })
  }  
   
  guardarFactura() {
    const factura: Factura = {
      Id:0,
      FechaEmisionFactura: this.form.get('fechaEmisionFactura')?.value,
      IdCliente: this.form.get('idCliente')?.value,
      NumeroFactura: this.form.get('numeroFactura')?.value,
      NumeroTotalArticulos: this.form.get('numeroTotalArticulos')?.value,
      SubTotalFacturas: this.form.get('subTotalFacturas')?.value,
      TotalImpuestos: this.form.get('totalImpuestos')?.value,
      TotalFactura: this.form.get('totalFactura')?.value,
    }

    if(this.id == undefined) {
        this._facturaService.saveFactura(factura).subscribe(data => {
          const idFacturaGuardada = data.id;
          const detalleFactura: any = {
            IdFactura: idFacturaGuardada,
            IdProducto: this.form.get('idProducto')?.value,
            CantidadDeProducto: this.form.get('cantidadDeProducto')?.value,
            PrecioUnitarioProducto: this.form.get('precioUnitarioProducto')?.value,
            SubtotalProducto: this.form.get('subtotalProducto')?.value,
            Notas: this.form.get('notas')?.value,
          }
          this._detalleFacturaService.saveDetalleFactura(detalleFactura).subscribe(data => {
            this.toastr.success('La factura fue registrada con exito!', 'Factura Registrada');
            this.obtenerFacturas();
            this.form.reset();
          }, error => {
            this.toastr.error('Opss.. ocurrio un error','Error')
            console.log(error);
          })
        }, error => {
          this.toastr.error('Opss.. ocurrio un error','Error')
          console.log(error);
        })
    }else {
        //EDIT  
        // factura.id = this.id;
        // this._facturaService.updateFactura(this.id, factura).subscribe(data => {
        //   const detalleFactura: any = {
        //     IdFactura: this.id,  // Usamos el id existente para el detalle de la factura
        //     IdProducto: this.form.get('IdProducto')?.value,
        //     CantidadDeProducto: this.form.get('CantidadDeProducto')?.value,
        //     PrecioUnitarioProducto: this.form.get('PrecioUnitarioProducto')?.value,
        //     SubtotalProducto: this.form.get('SubtotalProducto')?.value,
        //     Notas: this.form.get('Notas')?.value,
        //   }
        //   // Asumiendo que tienes un método para actualizar el detalle de la factura
        //   this._detalleFacturaService.updateDetalleFactura(this.id, detalleFactura).subscribe(data => {
        //     this.toastr.info('La factura y su detalle fueron actualizados con éxito!', 'Factura Actualizada');
        //     this.obtenerFacturas();
        //     this.form.reset();
        //     this.accion = 'Agregar';
        //     this.id = undefined;
        //   }, error => {
        //     console.log(error);
        //   })
        // }, error => {
        //   console.log(error);
        // })
    }
  }

  eliminarFactura(id: number) {
    this._facturaService.deleteFactura(id).subscribe(data => {
      this.toastr.error('La factura fue eliminada con exito!','Factura eliminada');
      this.obtenerFacturas();
    }, error => {
      console.log(error);
    })

  }

  // editarFactura(factura: Factura) {
  //   this.accion = 'Editar';
  //   this.id = factura.id;

  //   this.form.patchValue({
  //     titular: factura.titular,
  //     numeroFactura: factura.numeroFactura,
  //     fechaExpiracion: factura.fechaExpiracion,
  //     cvv: factura.cvv
  //   })
  // }

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

  // obtenerClientes() {
  //   this._clienteService.getListClientes().subscribe(data => {
  //     console.log(data);
  //     this.listClientes = data;
  //   }, error => {
  //     console.log(error);
  //   })
  // } 


  obtenerClientes() {
    const loginModel = { UserName: 'myUsername', Password: 'myPassword' };
  
    this._tokenService.Login(loginModel).pipe(
      switchMap(data => this._clienteService.getListClientes(data.token))
    ).subscribe(
      data => {
        console.log(data);
        this.listClientes = data;
      },
      error => {
        console.log(error);
      }
    );
  }
  

  obtenerProductos() {
    this._productoService.getListProductos().subscribe(data => {
      console.log(data);
      this.listProductos = data;
    }, error => {
      console.log(error);
    })
  }   

  onProductoSelected(idProducto: number) {
    this.selectedProducto = idProducto;
    const selectedProducto = this.listProductos.find(producto => producto.Id === +idProducto);
    if (selectedProducto) {
      this.form?.get('precioUnitarioProducto')?.setValue(selectedProducto.PrecioUnitario);
    }
  }  

  buscar() {
    const searchOption = this.formSearch.get('searchOption')?.value;
    if (searchOption === 'cliente') {
      const clienteId = this.formSearch.get('idClienteSearch')?.value;
      if (clienteId) {
        this._facturaService.buscarByCliente(clienteId).subscribe(
          data => {
            this.listFacturas = data;
          },
          error => {
            console.error('Error: ' + error);
          }
        );
      } else {
        console.log('Cliente ID is required');
      }
    } else {
      const numeroFactura = this.formSearch.get('numeroFacturaSearch')?.value;
      if (numeroFactura) {
        this._facturaService.buscarByFactura(numeroFactura).subscribe(
          data => {
            this.listFacturas = data;
          },
          error => {
            console.error('Error: ' + error);
          }
        );
      } else {
        console.log('Factura number is required');
      }
    }
  }
  
  
}
