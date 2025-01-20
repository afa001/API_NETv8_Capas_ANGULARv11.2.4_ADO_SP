USE [LabDev]
GO
/****** Object:  Table [dbo].[CatProductos]    Script Date: 02/04/2024 04:47:38 p. m. ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CatTipoCliente]    Script Date: 02/04/2024 04:47:39 p. m. ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblClientes]    Script Date: 02/04/2024 04:47:39 p. m. ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblDetallesFactura]    Script Date: 02/04/2024 04:47:39 p. m. ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblFacturas]    Script Date: 02/04/2024 04:47:39 p. m. ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
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
ON DELETE CASCADE;
GO
ALTER TABLE [dbo].[TblDetallesFactura] CHECK CONSTRAINT [FK_TblDetallesFactura_TblFacturas]
GO
ALTER TABLE [dbo].[TblFacturas]  WITH CHECK ADD  CONSTRAINT [FK_TblFacturas_TblClientes] FOREIGN KEY([IdCliente])
REFERENCES [dbo].[TblClientes] ([Id])
GO
ALTER TABLE [dbo].[TblFacturas] CHECK CONSTRAINT [FK_TblFacturas_TblClientes]
GO
