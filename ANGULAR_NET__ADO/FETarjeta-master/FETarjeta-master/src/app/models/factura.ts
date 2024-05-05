export class Factura {
    Id:number;
    FechaEmisionFactura: string;
    IdCliente: number;
    NumeroFactura: number;
    NumeroTotalArticulos: number;
    SubTotalFacturas: number;
    TotalImpuestos: number;
    TotalFactura: number;

    constructor(data: any) {
        this.Id = data.id;
        this.FechaEmisionFactura = data.FechaEmisionFactura;
        this.IdCliente = data.IdCliente;
        this.NumeroFactura = data.NumeroFactura;
        this.NumeroTotalArticulos = data.numeroTotalArticulos;
        this.SubTotalFacturas = data.SubTotalFacturas;
        this.TotalImpuestos = data.TotalImpuestos;
        this.TotalFactura = data.TotalFactura;
    }

    
}