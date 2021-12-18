drop database FilmMagic;

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
('Disparos','V'),('RPG','V'),('Estrategia','V'),('Carrera','V'),('Deporte','A'),('Simulacion','V'),('Misterio','P')
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

INSERT INTO alquiler(alquilerNumero,alquilerClienteId,alquilerFechaAlquiler,alquilerFechaRetorno,alquilerMontoBruto,alquilerImpuesto,alquilerMontoTotalDescuento,alquilerMontoTotal,alquilerEstado)values('A-01',1,'10/01/2020','10/03/2020',3000,0.18,0,3180,1)

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

insert into usuario(usuarioNombre,usuarioAvatar,usuarioClave,usuarioEstado) VALUES ('Nizar','nz','nz',1);

SET DATEFORMAT dmy;
go

Insert into producto (productoCodigo, productoTitulo, productoCantidad, productoPrecio, productoDescripcion, productoCategoria, productoFechaLanzamiento, categoriaCodigo, productoImagen) 
values ('DR-001', 'Acusada', 20, 50, 'Es una historia basada en hechos reales, que cuenta la vida de Dolores Dreier, que llevaba una vida normal, hasta que su amiga Solange Grabenheimer, una joven de 21 años, apareció asesinada en su casa de la localidad de Florida, en el año 2007.', 'P', '14/08/2018', 4, 'C:\img\acusada.jpg');
Insert into producto (productoCodigo, productoTitulo, productoCantidad, productoPrecio, productoDescripcion, productoCategoria, productoFechaLanzamiento, categoriaCodigo, productoImagen) 
values ('CO-004', 'Shazam', 18, 100, ' Un niño huérfano de 14 años con problemas que vive en Filadelfia,? sube a un vagón del metro y se ve transportado a un reino diferente, donde un antiguo mago le da el poder de transformarse en un superhéroe adulto divino pronunciando la palabra «¡Shazam!» Billy y su nuevo hermano adoptivo Freddy Freeman deben aprender cuáles son los nuevos poderes de Billy y cómo usarlos para evitar que el villano Dr. Thaddeus Sivana cometa actos infames.', 'P', '15/03/2019', 7, 'C:\img\shazam.jpg');
Insert into producto (productoCodigo, productoTitulo, productoCantidad, productoPrecio, productoDescripcion, productoCategoria, productoFechaLanzamiento, categoriaCodigo, productoImagen) 
values ('CO-005', 'Shaft', 17, 70, 'Puede que JJ, también conocido como John Shaft Jr. (Usher), sea un experto en ciberseguridad con una licenciatura en el MIT (Massachusetts Institute of Technology), pero para descubrir la verdad que se esconde tras la inesperada muerte de su mejor amigo necesita un tipo de ayuda que sólo su padre le puede proporcionar.', 'P', '14/06/2019', 1, 'C:\img\shaft.jpg');
Insert into producto (productoCodigo, productoTitulo, productoCantidad, productoPrecio, productoDescripcion, productoCategoria, productoFechaLanzamiento, categoriaCodigo, productoImagen) 
values ('CO-003', 'Hombres de Negro: Internacional', 18, 50, 'En esta entrega, los Hombres de Negro, que siempre han protegido la Tierra, deben descubrir un topo dentro de la organización MIB. Para luchar contra unos nuevos malévolos aliens camuflados como humanos utilizarán una gran tecnología.', 'P', '13/06/2019', 7, 'C:\img\mib.jpg');
Insert into producto (productoCodigo, productoTitulo, productoCantidad, productoPrecio, productoDescripcion, productoCategoria, productoFechaLanzamiento, categoriaCodigo, productoImagen) 
values ('MI-001', 'Annabelle 3', 20, 80, 'Tercera parte de la saga Anabelle, basada en la leyenda de la muñeca del mismo nombre, y séptima película del Universo Warren.', 'P', '26/05/2019', 5, 'C:\img\annabelle3.jpg');
Insert into producto (productoCodigo, productoTitulo, productoCantidad, productoPrecio, productoDescripcion, productoCategoria, productoFechaLanzamiento, categoriaCodigo, productoImagen) 
values ('MI-002', 'El Hombre Invisible', 20, 100, 'Un científico loco finge su suicidio y luego utiliza su invisibilidad para aterrorizar a su expareja, quien decide enfrentar al hombre invisible ella misma luego de que la policía no creyera su historia.', 'P', '24/02/2020', 20, 'C:\img\elhombreinvisible.jpg');
Insert into producto (productoCodigo, productoTitulo, productoCantidad, productoPrecio, productoDescripcion, productoCategoria, productoFechaLanzamiento, categoriaCodigo, productoImagen) 
values ('CO-002', 'SuperLopez', 20, 50, 'Un niño del planeta Chitón llega a Cataluña a bordo de un meteorito y termina trabajando como contador en una oficina poco inspiradora.', 'P', '23/11/2018',3, 'C:\img\superlopez.jpg');

select * from Producto;
select * from alquiler;

select cliente.clienteNombre as nombre , sum(alquiler.alquilerMontoTotal)as montoTotalMesActual from cliente inner join alquiler on cliente.clienteId=alquilerClienteId where month((alquiler.alquilerFechaAlquiler))=month(GETDATE())  group by cliente.clienteNombre having sum(alquiler.alquilerMontoTotal)>100 ;
select cliente.clienteNombre as nombre , sum(alquiler.alquilerMontoTotal)as montoTotalAnioActual from cliente inner join alquiler on cliente.clienteId=alquilerClienteId where year((alquiler.alquilerFechaAlquiler))=year(GETDATE()) group by cliente.clienteNombre  having sum(alquiler.alquilerMontoTotal)>300;