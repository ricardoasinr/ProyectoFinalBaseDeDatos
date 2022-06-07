CREATE TABLE Modelos (
  CodModelo nchar(10) NOT NULL,
  Marca varchar(20) NOT NULL,
  Modelo varchar(10) not null,
  NroPuertas varchar NOT NULL,
  Cilindradas varchar NOT NULL,
  NroRuedas varchar NOT NULL,
  Procedencia varchar(50) NOT NULL,
  CantPasajeros varchar NOT NULL,
  CONSTRAINT PK_Modelos PRIMARY KEY (CodModelo)
);
CREATE TABLE Concesionarias(
NIT_CON nchar(10) NOT NULL,
--NroVenta char(10) NOT NULL,
TelefonoCon varchar(10) NOT NULL,
NombreCon varchar(10) NOT NULL,
DireccionCon varchar NOT NULL,
EmailCon varchar NOT NULL,
 CONSTRAINT PK_Concesionarias PRIMARY KEY (NIT_CON),
-- CONSTRAINT FK_Ventas FOREIGN KEY(NroVenta) REFERENCES NroVenta (NroVenta)
 );
CREATE TABLE Equipos (
  codequipo nchar NOT NULL,
  descripcion varchar(40) not NULL,
  precio decimal(10,2) not NULL,
  CONSTRAINT PK_equipo PRIMARY KEY (codequipo)
);
CREATE TABLE TipoEquipo (
  CodModelo nchar(10) NOT NULL,
  Codequipo nchar NOT NULL,
  Tipoequipo varchar(50) NOT NULL,
  constraint pk_Tipo primary key (codmodelo, codequipo),
  constraint fk_Tipo_Modelo foreign key (codmodelo) references Modelos (codmodelo),
  constraint fk_Tipo_Equipo foreign key (codequipo) references Equipos (codequipo)
);
CREATE TABLE Automoviles(
NroChasis nchar NOT NULL,
NIT_CON nchar(10) NOT NULL,
CodModelo nchar(10) NOT NULL,
FechaFabri date NOT NULL,
Color varchar(15) NOT NULL,
Estado varchar(10),
 CONSTRAINT PK_Automoviles PRIMARY KEY (NroChasis),
 CONSTRAINT FK_Concesionarias FOREIGN KEY (NIT_CON) REFERENCES Concesionarias,
  CONSTRAINT FK_Modelos FOREIGN KEY (CodModelo) REFERENCES Modelos
);
CREATE TABLE Series(
  codequipo nchar NOT NULL,
  nrochasis nchar not NULL,
  NroSerie varchar(25) not NULL,
  CONSTRAINT PK_serie PRIMARY KEY (codequipo, nrochasis),
  CONSTRAINT FK_seriequipo FOREIGN KEY (codequipo) REFERENCES Equipos (codequipo),
  CONSTRAINT FK_autoequipo FOREIGN KEY (nrochasis) REFERENCES Automoviles (nrochasis)
);
CREATE TABLE ServicioOficial (
  NITSO nchar (10) not null,
  NIT_CON nchar(10) NOT NULL,
  NombreSO varchar (50) not null,
  DireccionSO varchar (50) not null,
  TelefonoSO varchar (30) not null,
  EmailSO varchar (50) not null,
  TalleresSO varchar (50) not null,
  constraint PK_ServicioOfi primary key (NITSO),
  constraint FK_ServicioOfi_Concesionarias foreign key (NIT_CON) references Concesionarias (NIT_CON)
);
CREATE TABLE Vendedores (
  CodVendedor nchar (10) not null,
  NIT_CON nchar(10) NOT NULL,
  NITSO nchar (10) not null,
  NombreV varchar (50) not null,
  TelefonoV varchar (30) not null,
  DireccionV varchar (50) not null,
  EmailV varchar (50) not null,
  constraint PK_Vendedores primary key (codvendedor),
  constraint FK_Vendedores_Concesionaria foreign key (NIT_CON) references Concesionarias (NIT_CON),
  constraint FK_Vendedores_ServOficial foreign key (NITSO) references ServicioOficial (NITSO)
);
CREATE TABLE Clientes (
  CI_Clientes nchar(10) NOT NULL,
  Factura varchar(10) NOT NULL,
  NombreClie varchar(50) NOT NULL,
  TelefonoCli varchar(10) NOT NULL,
  DireccionCli varchar(100) NOT NULL,
  CONSTRAINT PK_Clientes PRIMARY KEY (CI_Clientes),
);
CREATE TABLE VehiculosVendidos (
  Matricula nchar(10) NOT NULL,
  CodModelo nchar(10) NOT NULL,
  CI_Clientes nchar(10) NOT NULL,
  FechaFabri date NOT NULL,
  Color varchar(15) NOT NULL,
  CONSTRAINT PK_VehiculosVend PRIMARY KEY (Matricula),
  CONSTRAINT FK_Modeloss foreign key (CodModelo) references Modelos,
  CONSTRAINT FK_Clientes foreign key (CI_Clientes) references Clientes

);
CREATE TABLE Ventas (
  Factura nchar(10) NOT NULL,
  CodVendedor nchar (10) not null,
  NIT_CON nchar(10) NOT NULL,
  CI_Clientes nchar(10) NOT NULL,
  NroChasis nchar NOT NULL,
  FechaVenta date NOT NULL,
  FechaEntrega date NOT NULL,
  Lugar varchar(50) NOT NULL,
  PrecioVenta decimal(10,2) not NULL,
  ModoPago varchar(10) NOT NULL,
  CONSTRAINT PK_ventas PRIMARY KEY (Factura),
  CONSTRAINT FK_vendedores FOREIGN KEY (CodVendedor) REFERENCES Vendedores (CodVendedor),
  CONSTRAINT FK_concesionariass foreign key (NIT_CON) references Concesionarias,
  CONSTRAINT FK_clientess foreign key (CI_Clientes) references Clientes,
  CONSTRAINT FK_Automoviles foreign key (NroChasis) references Automoviles

  );
CREATE TABLE Mecanicos
(
CodMecanico nchar(10) not null,
  NITSO nchar (10) not null,
NombreMec varchar(30) not null,
ApellidoMec varchar(30) not null,
TelefonoMec varchar(10) NOT NULL,
  DireccionMec varchar(100) NOT NULL,
Especialidad varchar(20) not null,
constraint PK_Mecanicos primary key (CodMecanico),
  CONSTRAINT FK_ServicioOfi foreign key (NITSO) references ServicioOficial
);
create table HojadeParte
(
CodHoja nchar(10) not null,
Matricula nchar(10) NOT NULL,
CodMecanico nchar(10) not null,
FechaIngreso date not null,
HoraIngreso time not null,
Manodeobra money not null,
constraint PK_HojadeParte primary key (CodHoja),
constraint FK_HojadeParte_VehiculosVend foreign key (Matricula) references VehiculosVendidos,
constraint FK_HojadeParte_Mecanicos foreign key (CodMecanico) references Mecanicos,
);
create table RepuestosInsumosMateriales
(
CodRep_Ins_Mat nchar(10) not null,
CodHoja nchar(10) not null,
Nombre varchar(30) not null,
Precio money not null,
constraint PK_Respuestos_Insumos_Materiales primary key (CodRep_Ins_Mat),
constraint FK_Repuestos_Insumos_Materiales_HojadeParte foreign key (CodHoja) references HojadeParte,
);
create table Detalle
(
CodDetalle nchar(10) not null,
CodHoja nchar(10) not null,
CodRep_Ins_Mat nchar(10) not null,
Cantidad nchar(10) not null,
constraint PK_Detalle primary key (CodDetalle),
constraint FK_Detalle_HojadeParte foreign key (CodHoja) references HojadeParte,
constraint FK_Detalle_Repuestos_Insumos_Materiales foreign key (CodRep_Ins_Mat) references RepuestosInsumosMateriales,
);
create table Facturas
(
CodFactura nchar(10) not null,
CodHoja nchar(10) not null,
CodMecanico nchar(10) not null,
CI_Clientes nchar(10) NOT NULL,
NombreRIM varchar(1000) not null,
PrecioMO money not null,
PrecioTOT money not null,
constraint PK_Facturas primary key (CodFactura),
constraint FK_Facturas_HojadeParte foreign key (CodHoja) references HojadeParte,
constraint FK_Facturas_Mecanicos foreign key (CodMecanico) references Mecanicos,
constraint FK_Facturas_Clientes foreign key (CI_Clientes) references Clientes,
);