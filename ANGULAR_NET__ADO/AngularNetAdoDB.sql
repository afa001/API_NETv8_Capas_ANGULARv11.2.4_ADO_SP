USE [AngularNetAdoDB]
GO
/****** Object:  Table [dbo].[CatProductos]    Script Date: 2/4/2025 6:50:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CatProductos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NombreProducto] [varchar](50) NOT NULL,
	[ImagenProducto] [image] NULL,
	[PrecioUnitario] [decimal](18, 2) NOT NULL,
	[ext] [varchar](5) NULL,
 CONSTRAINT [PK_CatProductos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CatTipoCliente]    Script Date: 2/4/2025 6:50:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CatTipoCliente](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TipoCliente] [varchar](50) NOT NULL,
 CONSTRAINT [PK_CatTipoCliente] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblClientes]    Script Date: 2/4/2025 6:50:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblClientes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RazonSocial] [varchar](200) NOT NULL,
	[IdTipoCliente] [int] NOT NULL,
	[FechaCreacion] [date] NOT NULL,
	[RFC] [varchar](50) NOT NULL,
 CONSTRAINT [PK_TblClientes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblDetallesFactura]    Script Date: 2/4/2025 6:50:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblDetallesFactura](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdFactura] [int] NOT NULL,
	[IdProducto] [int] NOT NULL,
	[CantidadDeProducto] [int] NOT NULL,
	[PrecioUnitarioProducto] [decimal](18, 2) NOT NULL,
	[SubtotalProducto] [decimal](18, 2) NOT NULL,
	[Notas] [varchar](200) NULL,
 CONSTRAINT [PK_TblDetallesFactura] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblFacturas]    Script Date: 2/4/2025 6:50:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblFacturas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FechaEmisionFactura] [datetime] NOT NULL,
	[IdCliente] [int] NOT NULL,
	[NumeroFactura] [int] NOT NULL,
	[NumeroTotalArticulos] [int] NOT NULL,
	[SubTotalFacturas] [decimal](18, 2) NOT NULL,
	[TotalImpuestos] [decimal](18, 2) NOT NULL,
	[TotalFactura] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_TblFacturas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TblClientes]  WITH CHECK ADD  CONSTRAINT [FK_TblClientes_CatTipoCliente] FOREIGN KEY([IdTipoCliente])
REFERENCES [dbo].[CatTipoCliente] ([Id])
GO
ALTER TABLE [dbo].[TblClientes] CHECK CONSTRAINT [FK_TblClientes_CatTipoCliente]
GO
ALTER TABLE [dbo].[TblDetallesFactura]  WITH CHECK ADD  CONSTRAINT [FK_TblDetallesFactura_CatProductos] FOREIGN KEY([IdProducto])
REFERENCES [dbo].[CatProductos] ([Id])
GO
ALTER TABLE [dbo].[TblDetallesFactura] CHECK CONSTRAINT [FK_TblDetallesFactura_CatProductos]
GO
ALTER TABLE [dbo].[TblDetallesFactura]  WITH CHECK ADD  CONSTRAINT [FK_TblDetallesFactura_TblFacturas] FOREIGN KEY([IdFactura])
REFERENCES [dbo].[TblFacturas] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TblDetallesFactura] CHECK CONSTRAINT [FK_TblDetallesFactura_TblFacturas]
GO
ALTER TABLE [dbo].[TblFacturas]  WITH CHECK ADD  CONSTRAINT [FK_TblFacturas_TblClientes] FOREIGN KEY([IdCliente])
REFERENCES [dbo].[TblClientes] ([Id])
GO
ALTER TABLE [dbo].[TblFacturas] CHECK CONSTRAINT [FK_TblFacturas_TblClientes]
GO
/****** Object:  StoredProcedure [dbo].[AddCliente]    Script Date: 2/4/2025 6:50:24 PM ******/
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
/****** Object:  StoredProcedure [dbo].[AddDetalleFactura]    Script Date: 2/4/2025 6:50:24 PM ******/
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
/****** Object:  StoredProcedure [dbo].[AddFactura]    Script Date: 2/4/2025 6:50:24 PM ******/
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
/****** Object:  StoredProcedure [dbo].[AddProducto]    Script Date: 2/4/2025 6:50:24 PM ******/
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
/****** Object:  StoredProcedure [dbo].[DeleteCliente]    Script Date: 2/4/2025 6:50:24 PM ******/
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
/****** Object:  StoredProcedure [dbo].[DeleteDetalleFactura]    Script Date: 2/4/2025 6:50:24 PM ******/
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
/****** Object:  StoredProcedure [dbo].[DeleteFactura]    Script Date: 2/4/2025 6:50:24 PM ******/
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
/****** Object:  StoredProcedure [dbo].[DeleteProducto]    Script Date: 2/4/2025 6:50:24 PM ******/
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
/****** Object:  StoredProcedure [dbo].[GetAllClientes]    Script Date: 2/4/2025 6:50:24 PM ******/
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
/****** Object:  StoredProcedure [dbo].[GetAllDetallesFactura]    Script Date: 2/4/2025 6:50:24 PM ******/
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
/****** Object:  StoredProcedure [dbo].[GetAllFacturas]    Script Date: 2/4/2025 6:50:24 PM ******/
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
/****** Object:  StoredProcedure [dbo].[GetAllProductos]    Script Date: 2/4/2025 6:50:24 PM ******/
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
/****** Object:  StoredProcedure [dbo].[GetAlltiposCliente]    Script Date: 2/4/2025 6:50:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAlltiposCliente]
AS
BEGIN
    SELECT * FROM CatTipoCliente
END

GO
/****** Object:  StoredProcedure [dbo].[GetClienteById]    Script Date: 2/4/2025 6:50:24 PM ******/
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
/****** Object:  StoredProcedure [dbo].[GetDetalleFacturaById]    Script Date: 2/4/2025 6:50:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento almacenado para obtener un detalle de factura por ID
CREATE PROCEDURE [dbo].[GetDetalleFacturaById]
    @Id INT
AS
BEGIN
    SELECT * FROM TblDetallesFactura WHERE IdFactura = @Id;
END

GO
/****** Object:  StoredProcedure [dbo].[GetFacturaById]    Script Date: 2/4/2025 6:50:24 PM ******/
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
/****** Object:  StoredProcedure [dbo].[GetFacturasByClienteId]    Script Date: 2/4/2025 6:50:24 PM ******/
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
/****** Object:  StoredProcedure [dbo].[GetFacturasByNumeroFactura]    Script Date: 2/4/2025 6:50:24 PM ******/
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
/****** Object:  StoredProcedure [dbo].[GetProductoById]    Script Date: 2/4/2025 6:50:24 PM ******/
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
/****** Object:  StoredProcedure [dbo].[GetTipoClienteById]    Script Date: 2/4/2025 6:50:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetTipoClienteById]
    @Id INT
AS
BEGIN
    SELECT * FROM CatTipoCliente WHERE Id = @Id
END

GO
/****** Object:  StoredProcedure [dbo].[UpdateCliente]    Script Date: 2/4/2025 6:50:24 PM ******/
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
/****** Object:  StoredProcedure [dbo].[UpdateDetalleFactura]    Script Date: 2/4/2025 6:50:24 PM ******/
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
/****** Object:  StoredProcedure [dbo].[UpdateFactura]    Script Date: 2/4/2025 6:50:24 PM ******/
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
/****** Object:  StoredProcedure [dbo].[UpdateProducto]    Script Date: 2/4/2025 6:50:24 PM ******/
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
