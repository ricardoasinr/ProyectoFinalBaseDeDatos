CREATE TABLE Modelos (
  CodModelo nchar(10) NOT NULL,
  Marca varchar(20) NOT NULL,
  Modelo varchar(10) not null,
  NroPuertas varchar(10) NOT NULL,
  Cilindradas varchar(10) NOT NULL,
  NroRuedas varchar(10) NOT NULL,
  Procedencia varchar(50) NOT NULL,
  CantPasajeros varchar(10) NOT NULL,
  CONSTRAINT PK_Modelos PRIMARY KEY (CodModelo)
);
CREATE TABLE Concesionarias(
NIT_CON nchar(10) NOT NULL,
--NroVenta char(10) NOT NULL,
NombreCon varchar(20) NOT NULL,
TelefonoCon varchar(10) NOT NULL,
DireccionCon varchar(30) NOT NULL,
EmailCon varchar(40) NOT NULL,
CONSTRAINT PK_Concesionarias PRIMARY KEY (NIT_CON),
-- CONSTRAINT FK_Ventas FOREIGN KEY(NroVenta) REFERENCES NroVenta (NroVenta)
 );
CREATE TABLE Equipos (
  codequipo varchar(10) NOT NULL,
  descripcion varchar(80) not NULL,
  precio decimal(10,2) not NULL,
  CONSTRAINT PK_equipo PRIMARY KEY (codequipo)
);
CREATE TABLE TipoEquipo (
  CodModelo nchar(10) NOT NULL,
  Codequipo varchar(10) NOT NULL,
  Tipoequipo varchar(50) NOT NULL,
  constraint pk_Tipo primary key (codmodelo, codequipo),
  constraint fk_Tipo_Modelo foreign key (codmodelo) references Modelos (codmodelo),
  constraint fk_Tipo_Equipo foreign key (codequipo) references Equipos (codequipo)
);
CREATE TABLE Series(
  codequipo varchar(10) NOT NULL,
  nrochasis nchar(10) not NULL,
  NroSerie varchar(25) not NULL,
  CONSTRAINT PK_serie PRIMARY KEY (codequipo, nrochasis),
  CONSTRAINT FK_seriequipo FOREIGN KEY (codequipo) REFERENCES Equipos (codequipo),
  CONSTRAINT FK_autoequipo FOREIGN KEY (nrochasis) REFERENCES Automoviles (nrochasis)
);

CREATE TABLE Automoviles(
NroChasis nchar(10) NOT NULL,
NIT_CON nchar(10) NOT NULL,
CodModelo nchar(10) NOT NULL,
FechaFabri date NOT NULL,
E varchar(20) NOT NULL,
Estado varchar(10),
 CONSTRAINT PK_Automoviles PRIMARY KEY (NroChasis),
 CONSTRAINT FK_Concesionarias FOREIGN KEY (NIT_CON) REFERENCES Concesionarias,
  CONSTRAINT FK_Modelos FOREIGN KEY (CodModelo) REFERENCES Modelos
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
CREATE TABLE Mecanicos(
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
create table HojadeParte(
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
create table RepuestosInsumosMateriales(
CodHoja nchar(10) not null,
CodRep_Ins_Mat nchar(10) not null,
Nombre varchar(30) not null,
Precio money not null,
constraint PK_Respuestos_Insumos_Materiales primary key (CodRep_Ins_Mat),
constraint FK_CodHoja FOREIGN KEY (CodHoja) REFERENCES HojadeParte,

);
create table Detalle(
CodDetalle nchar(10) not null,
CodHoja nchar(10) not null,
CodRep_Ins_Mat nchar(10) not null,
Cantidad nchar(10) not null,
constraint PK_Detalle primary key (CodDetalle),
constraint FK_Detalle_HojadeParte foreign key (CodHoja) references HojadeParte,
constraint FK_Detalle_Repuestos_Insumos_Materiales foreign key (CodRep_Ins_Mat) references RepuestosInsumosMateriales,
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
  NroChasis nchar(10) NOT NULL,
  FechaVenta date NOT NULL,
  FechaEntrega date NOT NULL,
  Lugar varchar(50) NOT NULL,
  PrecioVenta decimal(10,2) not NULL,
  ModoPago varchar(20) NOT NULL,
  CONSTRAINT PK_ventas PRIMARY KEY (Factura),
  CONSTRAINT FK_vendedores FOREIGN KEY (CodVendedor) REFERENCES Vendedores (CodVendedor),
  CONSTRAINT FK_concesionariass foreign key (NIT_CON) references Concesionarias,
  CONSTRAINT FK_clientess foreign key (CI_Clientes) references Clientes,
  CONSTRAINT FK_Automoviles foreign key (NroChasis) references Automoviles

  );




insert into Concesionarias values ('45456','Ricardo Automotors','75003138','Av. Banzer 5to Anillo', 'info@ricardoautomotors.com');
insert into Concesionarias values ('45457','Bolivian Automotors','75643223','Av. Banzer 4to Anillo', 'info@bolivianautomotors.com');
insert into Concesionarias values ('45458','Imcruz Bolivia','800121800','Av. Cristóbal de Mendoza #164', 'info@imcruzbolivia.com');
insert into ServicioOficial values ('22222','45456','Monica Service','Av. Banzer 5to Anillo', '7452256','servicio@oficialMonica.com','Taller Monica');
insert into ServicioOficial values ('22223','45457','Ricardo Service','Av. Banzer 4to Anillo', '75003138','servicio@oficialRicardo.com','Taller Ricardo');


insert into Modelos values ('24241','Mazda','CX-5','5','2400','5','Japon', '5');
insert into Modelos values ('24242','Toyota','Hilux','4','2500','5','Japon', '5');
insert into Modelos values ('24243','Ford','F150','4','2500','5','USA', '5');

insert into Equipos values ('2020','Sistema operativo','200');
insert into Equipos values ('1990','Faroles','50');

insert into tipoequipo values ('24241','2020','Apple CarPlay');
insert into tipoequipo values ('24242','2020','Apple CarPlay');
insert into tipoequipo values ('24243','2020','Android Auto');

insert into Automoviles values ('8001','45456','24241','20200318','Blanco','En Stock');
insert into Automoviles values ('8002','45457','24242','20200428','Rojo','En Stock');
insert into Automoviles values ('8003','45456','24243','20201223','Blanco','En Stock');

insert into Series values ('2020','8001','00001');
insert into Series values ('2020','8002','00002');
insert into Series values ('1990','8003','00003');

insert into Vendedores values ('555','45456','22222','Leonardo Arabe Castedo','75638430','Av. Banzer 6to Anillo','leonardoac2000@gmail.com');
insert into Vendedores values ('551','45456','22222','Mariana Shantal Arabe','79934808','Urubo Condominio Casa del Camba','marianashantala@gmail.com');
insert into Vendedores values ('552','45457','22223','Ramiro Fernandez','72934367','Av Los Cusis','ramiroventas@gmail.com');

--Añadir más mecanicos
insert into Mecanicos values ('M001','22222','Jose Mario', 'Santa Cruz','69168609','Av Paragua Calle las petas','Caja de cambios');
insert into Mecanicos values ('M002','22223','Francesca', 'Antelo','78004552','Av Busch 4to Anillo','Cambio de llantas');
insert into Mecanicos values ('M003','22222','Ulises', 'Rider','70868065','Av Santos Dumont 4to Anillo','Tuberias');
--Añadir más clientes
insert into Clientes values ('8127786','8127786','Ricardo Asin','75003138','Av Banzer 5to Anillo');

select * from RepuestosInsumosMateriales




insert into VehiculosVendidos VALUES ('89012','24241','8127786','20211224','Blanco');
insert into Ventas values ('00001','551','45456','8127786','8001','20220507','20220607','Santa Cruz concesionaria Central','20000','Transferencia');


insert into HojadeParte values ('H893','89012','M001','20201124', '13:44','250')

insert into RepuestosInsumosMateriales values ('R278','Bujia','400','Disponible')
insert into RepuestosInsumosMateriales values ('R102','Faro','600', 'Disponible')
insert into RepuestosInsumosMateriales values ('R305','Bobinas','400', 'Disponible')
insert into RepuestosInsumosMateriales values ('R504','Discos de frenos','400', 'Disponible')
insert into RepuestosInsumosMateriales values ('R109','Amortiguadores','400', 'Disponible')


insert into Detalle values ('D001','H893','R278','1');


insert into Factura values ('FR0001', 'H893', 'M001', '8127786', 'Bujia T783', '300', '450', '750','847')
select * from Factura

create procedure ListaVendedores
as
SELECT [CodVendedor]
      ,[NIT_CON]
      ,[nitso]
      ,[NombreV]
      ,[TelefonoV]
      ,[EmailV]
  FROM [master].[dbo].[Vendedores]

execute ListaVendedores

create procedure RegistroCliente
 @p_CI_Clientes nchar(10),
  @p_Factura varchar(10),
  @p_NombreClie varchar(50),
  @p_TelefonoCli varchar(10),
  @p_DireccionCli varchar(100)
  as begin
  insert into Clientes values(@p_CI_Clientes,@p_Factura,@p_NombreClie,@p_TelefonoCli,@p_DireccionCli)
   select * from Clientes;
end

exec RegistroCliente @p_CI_Clientes = '6606881',
  @p_Factura  = '0991',
  @p_NombreClie  = 'Francesca Antelo',
  @p_TelefonoCli  = '78004552',
  @p_DireccionCli = 'Cond.Costa Blanca 2';

   create procedure RegistroVehiculo
@p_Matricula nchar(10),
  @p_CodModelo nchar(10),
  @p_CI_Clientes nchar(10),
  @p_FechaFabri date,
  @p_Color varchar(15)

  as
begin
  insert into VehiculosVendidos values(@p_Matricula,@p_CodModelo,@p_CI_Clientes,@p_FechaFabri,@p_Color)
   select * from VehiculosVendidos;

end
select * from VehiculosVendidos

exec RegistroVehiculo @p_Matricula = '4869klk',
  @p_CodModelo  = '24243',
  @p_CI_Clientes  = '6606881',
  @p_FechaFabri  = '20201223',
  @p_Color ='Blanco';

select Disponibilidad from Mecanicos;

select * from VehiculosVendidos;

 create procedure RegistroCliente
 @p_CI_Clientes nchar(10),
  @p_Factura varchar(10),
  @p_NombreClie varchar(50),
  @p_TelefonoCli varchar(10),
  @p_DireccionCli varchar(100)

  as
begin
  insert into Clientes values(@p_CI_Clientes,@p_Factura,@p_NombreClie,@p_TelefonoCli,@p_DireccionCli)
   select * from Clientes;

end

exec RegistroCliente @p_CI_Clientes = '6606881',
  @p_Factura  = '0991',
  @p_NombreClie  = 'Francesca Antelo',
  @p_TelefonoCli  = '78004552',
  @p_DireccionCli = 'Cond.Costa Blanca 2';

   create procedure RegistroVehiculo
 @p_Matricula nchar(10),
  @p_CodModelo nchar(10),
  @p_CI_Clientes nchar(10),
  @p_FechaFabri date,
  @p_Color varchar(15)

  as
begin
  insert into VehiculosVendidos values(@p_Matricula,@p_CodModelo,@p_CI_Clientes,@p_FechaFabri,@p_Color)
   select * from VehiculosVendidos;

end

exec RegistroVehiculo @p_Matricula = '4869klk',
  @p_CodModelo  = '24243',
  @p_CI_Clientes  = '6606881',
  @p_FechaFabri  = '20201223',
  @p_Color ='Blanco';

   create procedure RegistroHojaDeParte
 @p_CodHoja nchar(10),
  @p_Matricula nchar(10),
  @p_CodMecanico nchar(10),
  @p_FechaIngreso date,
  @p_HoraIngreso time,
@p_Manodeobra money
  as
begin
  insert into HojadeParte values(@p_CodHoja,@p_Matricula,@p_CodMecanico,@p_FechaIngreso,@p_HoraIngreso,@p_Manodeobra)
   select * from HojadeParte;

end

exec RegistroHojaDeParte @p_CodHoja='H893',
@p_Matricula='4869klk',
@p_CodMecanico='M001',
@p_FechaIngreso='20220610',
@p_HoraIngreso='13:31',
@p_Manodeobra='500';

create procedure RegistroDetalle
 @p_CodDetalle nchar(10),
@p_CodHoja nchar(10),
@p_CodRep_Ins_Mat nchar(10),
@p_Cantidad nchar(10)
  as
begin
  insert into Detalle values(@p_CodDetalle, @p_CodHoja, @p_CodRep_Ins_Mat, @p_Cantidad)
   select * from Detalle;

end

exec RegistroDetalle @p_CodDetalle ='D002',
@p_CodHoja='1221',
@p_CodRep_Ins_Mat='R278',
@p_Cantidad='2';

create table Factura(
CodFactura nchar(10) not null,
CodHoja nchar(10) not null,
CI_Clientes nchar(10) NOT NULL,
TotalConIVA money not null,
constraint PK_Factura primary key (CodFactura),
constraint FK_Factura_HojadeParte foreign key (CodHoja) references HojadeParte,
constraint FK_Factura_Clientes foreign key (CI_Clientes) references Clientes,
);

create procedure RegistroFactura
@p_CodFactura nchar(10),
@p_CodHoja nchar(10) ,
@p_CI_Clientes nchar(10) ,
@p_codRepuesto varchar(1000)

  as
begin
    declare @TOTAL money = 0;
    select @TOTAL = sum(Precio) from RepuestosInsumosMateriales where CodHoja = @p_CodHoja
    if @TOTAL is null
        set @TOTAL = 0
        select @TOTAL += Manodeobra from HojadeParte where CodHoja = @p_CodHoja
        set @TOTAL += @TOTAL* (0.13)
        insert into Factura values (@p_CodFactura, @p_CodHoja,@p_CI_Clientes,@p_codRepuesto,@TOTAL)
end

exec RegistroFactura
@p_CodFactura ='FR00010',
@p_CodHoja = 'H9990' ,
@p_CI_Clientes = '8127786',
@p_codRepuesto = 'R150'

    SELECT * FROM Mecanicos
exec RegistroFactura
    @p_CodFactura ='{factura}',
    @p_CodHoja = '{codHoja}',
    @p_CI_Clientes = '{ciCliente}',
    @p_codRepuesto = '{repuesto}'




exec RegistroHojaDeParte @p_CodHoja='1221',
@p_Matricula='4869klk',
@p_CodMecanico='M001',
@p_FechaIngreso='20220610',
@p_HoraIngreso='13:31',
@p_Manodeobra='500';


    select * from Factura
    select * from Mecanicos;
    select * from Clientes WHERE  CI_Clientes = '2992907';

    select * from VehiculosVendidos


    select * from Detalle where CodDetalle =

    select * from Detalle;

ALTER TABLE Mecanicos ADD Disponibilidad VARCHAR(40);

alter table RepuestosInsumosMateriales add Stock varchar(30);

ALTER TABLE RepuestosInsumosMateriales ADD constraint FK_CodHoja FOREIGN KEY (CodHoja) REFERENCES HojadeParte

Update Mecanicos set Disponibilidad ='Libre';



alter table RepuestosInsumosMateriales add Stock varchar(15);


update Mecanicos set Disponibilidad = 'Ocupado' where CodMecanico = 'M004'
update RepuestosInsumosMateriales set CodHoja = '1221' where CodRep_Ins_Mat = 'R102'
update RepuestosInsumosMateriales set CodHoja = 'H888' where CodRep_Ins_Mat = 'R109'
update RepuestosInsumosMateriales set CodHoja = 'H888' where CodRep_Ins_Mat = 'R134'
update RepuestosInsumosMateriales set CodHoja = 'H0232' where CodRep_Ins_Mat = 'R278'
update RepuestosInsumosMateriales set CodHoja = 'H0232' where CodRep_Ins_Mat = 'R305'
update RepuestosInsumosMateriales set CodHoja = 'H893' where CodRep_Ins_Mat = 'R504'
update RepuestosInsumosMateriales set CodHoja = 'H9043' where CodRep_Ins_Mat = 'R893'
select * from Automoviles
select * from VehiculosVendidos
select * from ServicioOficial
select * from HojadeParte
select * from RepuestosInsumosMateriales