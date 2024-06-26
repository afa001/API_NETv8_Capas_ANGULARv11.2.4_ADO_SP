USE [LabDev]
GO
/****** Object:  StoredProcedure [dbo].[AddCliente]    Script Date: 4/18/2024 4:50:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddCliente]
    @RazonSocial VARCHAR(200),
    @IdTipoCliente INT,
    @FechaCreacion DATE,
    @RFC VARCHAR(50)
AS
BEGIN
    INSERT INTO TblClientes (RazonSocial, IdTipoCliente, FechaCreacion, RFC)
    VALUES (@RazonSocial, @IdTipoCliente, @FechaCreacion, @RFC)
END

GO
/****** Object:  StoredProcedure [dbo].[AddDetalleFactura]    Script Date: 4/18/2024 4:50:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento almacenado para agregar un nuevo detalle de factura
CREATE PROCEDURE [dbo].[AddDetalleFactura]
    @IdFactura INT,
    @IdProducto INT,
    @CantidadDeProducto INT,
    @PrecioUnitarioProducto DECIMAL,
    @SubtotalProducto DECIMAL,
    @Notas NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO TblDetallesFactura (IdFactura, IdProducto, CantidadDeProducto, PrecioUnitarioProducto, SubtotalProducto, Notas) 
    VALUES (@IdFactura, @IdProducto, @CantidadDeProducto, @PrecioUnitarioProducto, @SubtotalProducto, @Notas);
END

GO
/****** Object:  StoredProcedure [dbo].[AddFactura]    Script Date: 4/18/2024 4:50:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddFactura]
    @FechaEmisionFactura DATETIME,
    @IdCliente INT,
    @NumeroFactura INT,
    @NumeroTotalArticulos INT,
    @SubTotalFacturas DECIMAL(18, 2),
    @TotalImpuestos DECIMAL(18, 2),
    @TotalFactura DECIMAL(18, 2)
AS
BEGIN
    INSERT INTO TblFacturas (FechaEmisionFactura, IdCliente, NumeroFactura, NumeroTotalArticulos, SubTotalFacturas, TotalImpuestos, TotalFactura)
    OUTPUT INSERTED.Id
    VALUES (@FechaEmisionFactura, @IdCliente, @NumeroFactura, @NumeroTotalArticulos, @SubTotalFacturas, @TotalImpuestos, @TotalFactura)
END

GO
/****** Object:  StoredProcedure [dbo].[AddProducto]    Script Date: 4/18/2024 4:50:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddProducto]
    @NombreProducto NVARCHAR(50),
    @ImagenProducto VARBINARY(MAX),
    @PrecioUnitario DECIMAL(18, 2),
    @ext NVARCHAR(50)
AS
BEGIN
    INSERT INTO CatProductos (NombreProducto, ImagenProducto, PrecioUnitario, ext)
    OUTPUT INSERTED.Id
    VALUES (@NombreProducto, @ImagenProducto, @PrecioUnitario, @ext)
END

GO
/****** Object:  StoredProcedure [dbo].[DeleteCliente]    Script Date: 4/18/2024 4:50:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteCliente]
    @Id INT
AS
BEGIN
    DELETE FROM TblClientes WHERE Id = @Id
END

GO
/****** Object:  StoredProcedure [dbo].[DeleteDetalleFactura]    Script Date: 4/18/2024 4:50:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento almacenado para eliminar un detalle de factura
CREATE PROCEDURE [dbo].[DeleteDetalleFactura]
    @Id INT
AS
BEGIN
    DELETE FROM TblDetallesFactura WHERE Id = @Id;
END

GO
/****** Object:  StoredProcedure [dbo].[DeleteFactura]    Script Date: 4/18/2024 4:50:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteFactura]
    @Id INT
AS
BEGIN
    DELETE FROM TblFacturas WHERE Id = @Id
END

GO
/****** Object:  StoredProcedure [dbo].[DeleteProducto]    Script Date: 4/18/2024 4:50:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteProducto]
    @Id INT
AS
BEGIN
    DELETE FROM CatProductos WHERE Id = @Id
END

GO
/****** Object:  StoredProcedure [dbo].[GetAllClientes]    Script Date: 4/18/2024 4:50:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllClientes]
AS
BEGIN
    SELECT * FROM TblClientes
END

GO
/****** Object:  StoredProcedure [dbo].[GetAllDetallesFactura]    Script Date: 4/18/2024 4:50:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento almacenado para obtener todos los detalles de factura
CREATE PROCEDURE [dbo].[GetAllDetallesFactura]
AS
BEGIN
    SELECT * FROM TblDetallesFactura;
END

GO
/****** Object:  StoredProcedure [dbo].[GetAllFacturas]    Script Date: 4/18/2024 4:50:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllFacturas]
AS
BEGIN
    SELECT * FROM TblFacturas
END

GO
/****** Object:  StoredProcedure [dbo].[GetAllProductos]    Script Date: 4/18/2024 4:50:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllProductos]
AS
BEGIN
    SELECT * FROM CatProductos
END

GO
/****** Object:  StoredProcedure [dbo].[GetClienteById]    Script Date: 4/18/2024 4:50:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetClienteById]
    @Id INT
AS
BEGIN
    SELECT * FROM TblClientes WHERE Id = @Id
END

GO
/****** Object:  StoredProcedure [dbo].[GetDetalleFacturaById]    Script Date: 4/18/2024 4:50:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento almacenado para obtener un detalle de factura por ID
CREATE PROCEDURE [dbo].[GetDetalleFacturaById]
    @Id INT
AS
BEGIN
    SELECT * FROM TblDetallesFactura WHERE Id = @Id;
END

GO
/****** Object:  StoredProcedure [dbo].[GetFacturaById]    Script Date: 4/18/2024 4:50:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetFacturaById]
    @Id INT
AS
BEGIN
    SELECT * FROM TblFacturas WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[GetFacturasByClienteId]    Script Date: 4/18/2024 4:50:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetFacturasByClienteId]
    @IdCliente INT
AS
BEGIN
    SELECT *
    FROM TblFacturas
    WHERE IdCliente = @IdCliente
END

GO
/****** Object:  StoredProcedure [dbo].[GetFacturasByNumeroFactura]    Script Date: 4/18/2024 4:50:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetFacturasByNumeroFactura]
    @NumeroFactura INT
AS
BEGIN
    SELECT *
    FROM TblFacturas
    WHERE NumeroFactura = @NumeroFactura
END

GO
/****** Object:  StoredProcedure [dbo].[GetProductoById]    Script Date: 4/18/2024 4:50:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetProductoById]
    @Id INT
AS
BEGIN
    SELECT * FROM CatProductos WHERE Id = @Id
END

GO
/****** Object:  StoredProcedure [dbo].[UpdateCliente]    Script Date: 4/18/2024 4:50:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateCliente]
    @Id INT,
    @RazonSocial VARCHAR(200),
    @IdTipoCliente INT,
    @FechaCreacion DATE,
    @RFC VARCHAR(50)
AS
BEGIN
    UPDATE TblClientes
    SET RazonSocial = @RazonSocial,
        IdTipoCliente = @IdTipoCliente,
        FechaCreacion = @FechaCreacion,
        RFC = @RFC
    WHERE Id = @Id
END

GO
/****** Object:  StoredProcedure [dbo].[UpdateDetalleFactura]    Script Date: 4/18/2024 4:50:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento almacenado para actualizar un detalle de factura
CREATE PROCEDURE [dbo].[UpdateDetalleFactura]
    @Id INT,
    @IdFactura INT,
    @IdProducto INT,
    @CantidadDeProducto INT,
    @PrecioUnitarioProducto DECIMAL,
    @SubtotalProducto DECIMAL,
    @Notas NVARCHAR(MAX)
AS
BEGIN
    UPDATE TblDetallesFactura 
    SET IdFactura = @IdFactura, 
        IdProducto = @IdProducto, 
        CantidadDeProducto = @CantidadDeProducto, 
        PrecioUnitarioProducto = @PrecioUnitarioProducto, 
        SubtotalProducto = @SubtotalProducto, 
        Notas = @Notas 
    WHERE Id = @Id;
END

GO
/****** Object:  StoredProcedure [dbo].[UpdateFactura]    Script Date: 4/18/2024 4:50:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateFactura]
    @Id INT,
    @FechaEmisionFactura DATETIME,
    @IdCliente INT,
    @NumeroFactura INT,
    @NumeroTotalArticulos INT,
    @SubTotalFacturas DECIMAL(18, 2),
    @TotalImpuestos DECIMAL(18, 2),
    @TotalFactura DECIMAL(18, 2)
AS
BEGIN
    UPDATE TblFacturas
    SET FechaEmisionFactura = @FechaEmisionFactura,
        IdCliente = @IdCliente,
        NumeroFactura = @NumeroFactura,
        NumeroTotalArticulos = @NumeroTotalArticulos,
        SubTotalFacturas = @SubTotalFacturas,
        TotalImpuestos = @TotalImpuestos,
        TotalFactura = @TotalFactura
    WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateProducto]    Script Date: 4/18/2024 4:50:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateProducto]
    @Id INT,
    @NombreProducto NVARCHAR(50),
    @ImagenProducto VARBINARY(MAX),
    @PrecioUnitario DECIMAL(18, 2),
    @ext NVARCHAR(50)
AS
BEGIN
    UPDATE CatProductos
    SET NombreProducto = @NombreProducto,
        ImagenProducto = @ImagenProducto,
        PrecioUnitario = @PrecioUnitario,
        ext = @ext
    WHERE Id = @Id
END

GO
