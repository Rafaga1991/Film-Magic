create database FilmMagic
go

use FilmMagic
go

create table usuario(
usuarioId int identity(1,1) primary key,
usuarioNombre varchar(60) not null,
usuarioAvatar varchar(20) not null unique,
usuarioClave varchar(60) not null,
usuarioEstado bit not null default 1
)
go

create table cliente(
clienteId int identity(1,1) primary key,
clienteCodigo varchar(20) not null unique,
clienteNombre varchar(60) not null,
clienteCedula char(11) not null unique,
clienteTelefono char(10) not null,
clienteDireccion Varchar(100) not null,
clienteEstado bit not null default 1
)
go

/*Creando tabla genero*/
create table categoria(
categoriaCodigo int identity(1,1) not null Primary key,
categoriaNombre varchar (60) not null,
categoriaTipo char(1) default 'A' check(categoriaTipo in ('A', 'V', 'P'))/* A = Ambos, V = video juegos, P = Pelicula*/
)
go

/*Insertando los generos*/
insert into categoria (categoriaNombre, categoriaTipo) values('Accion','A'),('Aventura','A'),('Comedia','P'),('Drama','P'),('Terror','A'),
('Musical','A'),('Ciencia Ficcion','P'),('Fantasia','P'),('Romance','P'),('Suspenso','P'),('Arcade','V'),('Plataforma','V'),('Lucha','V'),
('Disparos','V'),('RPG','V'),('Estrategia','V'),('Carrera','V'),('Deporte','A'),('Simulacion','V')
go

Create table producto(
productoId int identity(1,1) not null Primary Key,
productoCodigo varchar(20) not null unique,
productoTitulo varchar (60) not null,
productoCantidad int not null check(productoCantidad >= 0),
productoPrecio float not null check(productoPrecio > 0),
productoDescripcion varchar(500) not null,
productoCategoria char(1) not null check(productoCategoria in('V','P')),/*V = video juego, P = pelicula*/
productoFechaLanzamiento date not null,
categoriaCodigo int not null foreign key references categoria,
productoImagen varchar(200) not null,
productoEstado bit not null default 1
)
go

alter table producto add foreign key(categoriaCodigo) references categoria

create table alquiler(
alquilerId int identity(1,1) not null primary key,
alquilerNumero varchar(20) not null unique,
alquilerClienteId int not null foreign key references cliente,
alquilerFechaAlquiler date not null,
alquilerFechaRetorno date not null,
alquilerMontoBruto float not null check(alquilerMontoBruto >= 0),
alquilerImpuesto float not null check(alquilerImpuesto >= 0),--tasa de impuesto Ej: 0.18 que equivale a un 18%, el cual es actualmente la tasa de impuesto que pagamos
alquilerMontoTotalDescuento float default 0 check(alquilerMontoTotalDescuento >= 0),
alquilerMontoTotal float not null check(alquilerMontoTotal >= 0),-- monto total sera calculado de la siguiente manera: (MontoBruto * Impuesto) - TotalDescuento.
alquilerEstado bit not null default 1
)
go

create table alquilerDetalle(
alquilerId int not null foreign key references alquiler,
linea int identity(1,1) not null,-- campo auto incrementado para crear el primary key compuesto
productoId int not null foreign key references producto,
alquilerDetalleCantidad int not null check(alquilerDetalleCantidad >= 1),
alquilerDetallePrecio float not null check(alquilerDetallePrecio >= 0),
alquilerDetalleDescuento float not null check(alquilerDetalleDescuento >= 0),
alquilerDetalleImpuesto float not null check(alquilerDetalleImpuesto >= 0),
alquilerDetalleMontoNeto float not null check(alquilerDetalleMontoNeto >= 0),
alquilerDetalleEstado bit not null default 1,

primary key (alquilerId, linea)/*llave primaria compuesta*/
)
go

create table devolucionAlquiler(
devolucionAlquilerId int identity(1,1) not null primary key,
AlquilerId int not null foreign key references alquiler,
devolucionAlquilerFechaRetorno date not null,
devolucionAlquilerProductoId int foreign key references producto,
devolucionAlquilerCantidad int not null,
devolucionAlquilerEstado bit not null default 1
)
go


/*A cada cliente se le hara un registro mensual en el cual se podran ver y llevar un control de la cantidad de peliculas rentadas mensualmente*/

create table atrasoCliente(--esta tabla se debera rellenar con un trigger cuando se devuelva un alquiler y el dia en que el cliente devuelva sobre pase la fecha estipulada, los datos como la cantidad de productos y la cantidad de dias de atrasos seran traidos de la tabla devolucionAlquiler
clienteId int not null foreign key references Cliente,
linea int identity(1,1),
atrasoClienteMontoPorCantidad float not null default 2,-- un dia de atraso por pelicula equivale a 2 pesos.
atrasoClienteDiasTotales int not null check(atrasoClienteDiasTotales >= 1),
atrasoClienteCantidadProductos int not null check(atrasoClienteCantidadProductos >= 1),
atrasoClienteMontoTotal float not null, -- Sera calculado tomando en cuenta los dias y la cantidad de peliculas por cada pelicula un dia de atraso equivale a 2 pesos.
atrasoClienteEstado bit default 1, --cuando el cliente salde su deuda el estado debera cambiar a 0 a modo de asegurar que el atraso fue saldado y este no se tomara en cuenta.
devolucionAlquilerId int foreign key references devolucionAlquiler,

primary key(clienteId, linea)
)
go

create table RegistroCliente(--los registros se crearan mensual mente por eso se toma en cuenta la fecha de inicio y fin, estas indican cuando inicia el mes y cuando acaba
clienteId int not null foreign key references cliente,
linea int identity(1,1),--para hacer primary key compuesto
RegistroClienteFechaInicio date not null,
RegistroClienteFechaFinal date not null,
alquilerId int foreign key references alquiler,--foreign key para registrar los alquileres que se hagan durante el mes.

atrasoCliente1 int not null,--campo para hacer el foreign key compuesto con la tabla atrasoCliente
atrasoCliente2 int not null,--Campo para hacer el foreign key compuesto con la tabla atrasoCliente
foreign key(atrasoCliente1, atrasoCliente2) references atrasoCliente,--foreign key compuesto para determinar si hay atrasaos
primary key (clienteId, linea)
)
go

