USE [TransactionDB]
GO
ALTER TABLE [dbo].[Movimientos] DROP CONSTRAINT [FK_Tipo_Movimientos]
GO
ALTER TABLE [dbo].[Movimientos] DROP CONSTRAINT [FK_Cuenta_Movimientos]
GO
ALTER TABLE [dbo].[Cuentas] DROP CONSTRAINT [FK_Tipo_Cuentas]
GO
ALTER TABLE [dbo].[Cuentas] DROP CONSTRAINT [FK_Cliente_Cuentas]
GO
ALTER TABLE [dbo].[Clientes] DROP CONSTRAINT [FK_Persona_Cliente]
GO
/****** Object:  Table [dbo].[TiposMovimiento]    Script Date: 24/4/2022 18:49:22 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TiposMovimiento]') AND type in (N'U'))
DROP TABLE [dbo].[TiposMovimiento]
GO
/****** Object:  Table [dbo].[TiposCuenta]    Script Date: 24/4/2022 18:49:22 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TiposCuenta]') AND type in (N'U'))
DROP TABLE [dbo].[TiposCuenta]
GO
/****** Object:  Table [dbo].[Personas]    Script Date: 24/4/2022 18:49:22 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Personas]') AND type in (N'U'))
DROP TABLE [dbo].[Personas]
GO
/****** Object:  Table [dbo].[Movimientos]    Script Date: 24/4/2022 18:49:22 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Movimientos]') AND type in (N'U'))
DROP TABLE [dbo].[Movimientos]
GO
/****** Object:  Table [dbo].[Cuentas]    Script Date: 24/4/2022 18:49:22 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Cuentas]') AND type in (N'U'))
DROP TABLE [dbo].[Cuentas]
GO
/****** Object:  Table [dbo].[Clientes]    Script Date: 24/4/2022 18:49:22 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Clientes]') AND type in (N'U'))
DROP TABLE [dbo].[Clientes]
GO
/****** Object:  Table [dbo].[Clientes]    Script Date: 24/4/2022 18:49:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clientes](
	[ClienteID] [int] IDENTITY(1,1) NOT NULL,
	[PersonaID] [int] NOT NULL,
	[Clave] [nvarchar](16) NOT NULL,
	[Estado] [int] NOT NULL,
 CONSTRAINT [PK_Clientes] PRIMARY KEY CLUSTERED 
(
	[ClienteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cuentas]    Script Date: 24/4/2022 18:49:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cuentas](
	[CuentaID] [int] IDENTITY(1,1) NOT NULL,
	[NumeroCuenta] [nvarchar](25) NOT NULL,
	[TipoID] [int] NOT NULL,
	[SaldoInicial] [decimal](18, 2) NOT NULL,
	[Estado] [int] NOT NULL,
	[ClienteID] [int] NOT NULL,
 CONSTRAINT [PK_Cuentas] PRIMARY KEY CLUSTERED 
(
	[CuentaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Movimientos]    Script Date: 24/4/2022 18:49:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movimientos](
	[MovimientoID] [int] IDENTITY(1,1) NOT NULL,
	[CuentaID] [int] NOT NULL,
	[TipoID] [int] NOT NULL,
	[Monto] [decimal](18, 2) NOT NULL,
	[Estado] [int] NOT NULL,
	[Fecha] [datetime2](7) NULL,
 CONSTRAINT [PK_Movimientos] PRIMARY KEY CLUSTERED 
(
	[MovimientoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Personas]    Script Date: 24/4/2022 18:49:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Personas](
	[PersonaID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[Genero] [nvarchar](1) NOT NULL,
	[Edad] [int] NOT NULL,
	[Identificacion] [nvarchar](14) NOT NULL,
	[Direccion] [nvarchar](150) NOT NULL,
	[Telefono] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_Personas] PRIMARY KEY CLUSTERED 
(
	[PersonaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TiposCuenta]    Script Date: 24/4/2022 18:49:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TiposCuenta](
	[TipoCuentaID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](25) NOT NULL,
 CONSTRAINT [PK_TiposCuenta] PRIMARY KEY CLUSTERED 
(
	[TipoCuentaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TiposMovimiento]    Script Date: 24/4/2022 18:49:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TiposMovimiento](
	[TipoID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](25) NOT NULL,
 CONSTRAINT [PK_TiposMovimiento] PRIMARY KEY CLUSTERED 
(
	[TipoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Clientes] ON 

INSERT [dbo].[Clientes] ([ClienteID], [PersonaID], [Clave], [Estado]) VALUES (1, 1, N'1234', 1)
INSERT [dbo].[Clientes] ([ClienteID], [PersonaID], [Clave], [Estado]) VALUES (2, 2, N'5678', 1)
INSERT [dbo].[Clientes] ([ClienteID], [PersonaID], [Clave], [Estado]) VALUES (3, 3, N'1245', 1)
INSERT [dbo].[Clientes] ([ClienteID], [PersonaID], [Clave], [Estado]) VALUES (4, 4, N'199704', 1)
INSERT [dbo].[Clientes] ([ClienteID], [PersonaID], [Clave], [Estado]) VALUES (8, 14, N'09999', 1)
SET IDENTITY_INSERT [dbo].[Clientes] OFF
GO
SET IDENTITY_INSERT [dbo].[Cuentas] ON 

INSERT [dbo].[Cuentas] ([CuentaID], [NumeroCuenta], [TipoID], [SaldoInicial], [Estado], [ClienteID]) VALUES (1, N'478758', 1, CAST(2000.00 AS Decimal(18, 2)), 1, 1)
INSERT [dbo].[Cuentas] ([CuentaID], [NumeroCuenta], [TipoID], [SaldoInicial], [Estado], [ClienteID]) VALUES (2, N'225487', 2, CAST(100.00 AS Decimal(18, 2)), 1, 2)
INSERT [dbo].[Cuentas] ([CuentaID], [NumeroCuenta], [TipoID], [SaldoInicial], [Estado], [ClienteID]) VALUES (3, N'495878', 1, CAST(0.00 AS Decimal(18, 2)), 1, 3)
INSERT [dbo].[Cuentas] ([CuentaID], [NumeroCuenta], [TipoID], [SaldoInicial], [Estado], [ClienteID]) VALUES (4, N'496825', 1, CAST(540.00 AS Decimal(18, 2)), 1, 2)
INSERT [dbo].[Cuentas] ([CuentaID], [NumeroCuenta], [TipoID], [SaldoInicial], [Estado], [ClienteID]) VALUES (7, N'585545', 2, CAST(1000.00 AS Decimal(18, 2)), 1, 1)
SET IDENTITY_INSERT [dbo].[Cuentas] OFF
GO
SET IDENTITY_INSERT [dbo].[Movimientos] ON 

INSERT [dbo].[Movimientos] ([MovimientoID], [CuentaID], [TipoID], [Monto], [Estado], [Fecha]) VALUES (4, 1, 1, CAST(100.00 AS Decimal(18, 2)), 1, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Movimientos] OFF
GO
SET IDENTITY_INSERT [dbo].[Personas] ON 

INSERT [dbo].[Personas] ([PersonaID], [Nombre], [Genero], [Edad], [Identificacion], [Direccion], [Telefono]) VALUES (1, N'Jose Lema', N'M', 24, N'0987654321', N'Otavalo sn y principal', N'098254785')
INSERT [dbo].[Personas] ([PersonaID], [Nombre], [Genero], [Edad], [Identificacion], [Direccion], [Telefono]) VALUES (2, N'Marianela Montalvo', N'F', 25, N'0987654322', N'Amazonas y NNUU', N'097548965')
INSERT [dbo].[Personas] ([PersonaID], [Nombre], [Genero], [Edad], [Identificacion], [Direccion], [Telefono]) VALUES (3, N'Juan Osorio', N'M', 26, N'0987654323', N'13 junio y Equinoccial', N'098874587')
INSERT [dbo].[Personas] ([PersonaID], [Nombre], [Genero], [Edad], [Identificacion], [Direccion], [Telefono]) VALUES (4, N'Pierina Galvez', N'F', 24, N'0930649314', N'Cdla Paraiso', N'0982406266')
INSERT [dbo].[Personas] ([PersonaID], [Nombre], [Genero], [Edad], [Identificacion], [Direccion], [Telefono]) VALUES (14, N'Lucas Garces', N'M', 27, N'0987654321', N'Ceibos', N'0987654321')
SET IDENTITY_INSERT [dbo].[Personas] OFF
GO
SET IDENTITY_INSERT [dbo].[TiposCuenta] ON 

INSERT [dbo].[TiposCuenta] ([TipoCuentaID], [Nombre]) VALUES (1, N'Ahorro')
INSERT [dbo].[TiposCuenta] ([TipoCuentaID], [Nombre]) VALUES (2, N'Corriente')
SET IDENTITY_INSERT [dbo].[TiposCuenta] OFF
GO
SET IDENTITY_INSERT [dbo].[TiposMovimiento] ON 

INSERT [dbo].[TiposMovimiento] ([TipoID], [Nombre]) VALUES (1, N'Deposito')
INSERT [dbo].[TiposMovimiento] ([TipoID], [Nombre]) VALUES (2, N'Retiro')
SET IDENTITY_INSERT [dbo].[TiposMovimiento] OFF
GO
ALTER TABLE [dbo].[Clientes]  WITH CHECK ADD  CONSTRAINT [FK_Persona_Cliente] FOREIGN KEY([PersonaID])
REFERENCES [dbo].[Personas] ([PersonaID])
GO
ALTER TABLE [dbo].[Clientes] CHECK CONSTRAINT [FK_Persona_Cliente]
GO
ALTER TABLE [dbo].[Cuentas]  WITH CHECK ADD  CONSTRAINT [FK_Cliente_Cuentas] FOREIGN KEY([ClienteID])
REFERENCES [dbo].[Clientes] ([ClienteID])
GO
ALTER TABLE [dbo].[Cuentas] CHECK CONSTRAINT [FK_Cliente_Cuentas]
GO
ALTER TABLE [dbo].[Cuentas]  WITH CHECK ADD  CONSTRAINT [FK_Tipo_Cuentas] FOREIGN KEY([TipoID])
REFERENCES [dbo].[TiposCuenta] ([TipoCuentaID])
GO
ALTER TABLE [dbo].[Cuentas] CHECK CONSTRAINT [FK_Tipo_Cuentas]
GO
ALTER TABLE [dbo].[Movimientos]  WITH CHECK ADD  CONSTRAINT [FK_Cuenta_Movimientos] FOREIGN KEY([CuentaID])
REFERENCES [dbo].[Cuentas] ([CuentaID])
GO
ALTER TABLE [dbo].[Movimientos] CHECK CONSTRAINT [FK_Cuenta_Movimientos]
GO
ALTER TABLE [dbo].[Movimientos]  WITH CHECK ADD  CONSTRAINT [FK_Tipo_Movimientos] FOREIGN KEY([TipoID])
REFERENCES [dbo].[TiposMovimiento] ([TipoID])
GO
ALTER TABLE [dbo].[Movimientos] CHECK CONSTRAINT [FK_Tipo_Movimientos]
GO
