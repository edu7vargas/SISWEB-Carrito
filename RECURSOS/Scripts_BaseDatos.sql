create database DBCARRITO

GO
USE DBCARRITO

GO
create table DEPARTAMENTO
(
	IdDepartamento varchar(2) not null,
	Descripcion varchar(50) not null
)
GO
create table PROVINCIA
(
	IdProvincia varchar(4) not null,
	Descripcion varchar(50) not null,
	IdDepartamento varchar(2) not null
)


GO
create table DISTRITO
(
	IdDistrito varchar(6) not null,
	Descripcion varchar(50) not null,
	IdProvincia varchar(4) not null,
	IdDepartamento varchar(2),
)
GO

create table USUARIO
(
	IdUsuario int primary key identity,
	Nombres varchar(100) not null,
	Apellidos varchar(100) not null,
	Correo varchar(100) not null,
	Clave varchar(100) not null,
	Restablecer bit default 1,
	Activo bit default 1,
	FechaRegistro datetime default getdate()
)
GO


create table MARCA
(
	IdMarca int primary key identity,
	Descripcion varchar(100) not null,
	Activo bit default 1,
	FechaRegistro datetime default getdate()
)

GO

create table CATEGORIA
(
	IdCategoria int primary key identity,
	Descripcion varchar(100) not null,
	Activo bit default 1,
	FechaRegistro datetime default getdate()
)

GO

create table PRODUCTO
(	
	IdProducto int primary key identity,
	Nombre varchar(500) not null,
	Descripcion varchar(500) not null,
	IdMarca int references MARCA(IdMarca),
	IdCategoria int  references CATEGORIA(IdCategoria),
	Precio decimal(10,2) default 0,
	Stock int default 0,
	RutaImagen varchar(100),
	NombreImagen varchar(100),
	Activo bit default 1,
	FechaRegistro datetime default getdate()
)

GO

create table CLIENTE
(
	IdCliente int primary key identity,
	Nombres varchar(100) not null,
	Apellidos varchar(100) not null,
	Correo varchar(100),
	Clave varchar(150),
	FechaRegistro datetime default getdate(),
	Restablecer bit default 0
)
GO

create table CARRITO
(
	IdCarrito int primary key identity,
	IdCliente int references CLIENTE(IdCliente),
	IdProducto int references PRODUCTO(IdProducto),
	Cantidad int
)

GO

create table VENTA
(
	idVenta int primary key identity,
	IdCliente int references CLIENTE(IdCliente),
	TotalProducto int,
	MontoTotal decimal(10,2),
	Contacto varchar(100),
	IdDistrito varchar(10),
	Telefono varchar(50),
	Direccion  varchar(500),
	FechaVenta datetime default getdate(),
	IdTrasaccion varchar(100)
)
GO
create table DETALLE_VENTA
(
	IdDetalleVenta int primary key identity,
	IdVenta  int references VENTA(IdVenta),
	IdProducto  int references PRODUCTO(IdProducto),
	Cantidad int,
	Total  decimal(10,2),
)


GO


-- clave = test123 (https://emn178.github.io/online-tools/sha256.html)
insert into USUARIO(Nombres,Apellidos,Correo,Clave) values ('test nombre','test apellido','test@example.com','ecd71870d1963316a97e3ac3408c9835ad8cf0f3c1bc703527c30265534f75ae')

 go

 insert into CATEGORIA(Descripcion) values 
 ('Tecnologia'),
 ('Muebles'),
 ('Dormitorio'),
  ('Deportes')


go

  insert into MARCA(Descripcion) values
('SONYTE'),
('HPTE'),
('LGTE'),
('HYUNDAITE'),
('CANONTE'),
('ROBERTA ALLENTE')


go


insert into DEPARTAMENTO(IdDepartamento,Descripcion)
values 
('01','Arequipa'),
('02','Ica'),
('03','Lima')


go

insert into PROVINCIA(IdProvincia,Descripcion,IdDepartamento)
values
('0101','Arequipa','01'),
('0102','Camaná','01'),

--ICA - PROVINCIAS
('0201', 'Ica ', '02'),
('0202', 'Chincha ', '02'),

--LIMA - PROVINCIAS
('0301', 'Lima ', '03'),
('0302', 'Barranca ', '03')


go

insert into DISTRITO(IdDistrito,Descripcion,IdProvincia,IdDepartamento) values 
('010101','Nieva','0101','01'),
('010102', 'El Cenepa', '0101', '01'),

('010201', 'Camaná', '0102', '01'),
('010202', 'José María Quimper', '0102', '01'),

--ICA - DISTRITO
('020101', 'Ica', '0201', '02'),
('020102', 'La Tinguiña', '0201', '02'),
('020201', 'Chincha Alta', '0202', '02'),
('020202', 'Alto Laran', '0202', '02'),


--LIMA - DISTRITO
('030101', 'Lima', '0301', '03'),
('030102', 'Ancón', '0301', '03'),
('030201', 'Barranca', '0302', '03'),
('030202', 'Paramonga', '0302', '03')



