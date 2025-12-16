create database rocko_boy
use rocko_boy 
create table game_records
(
nombre varchar(20) not null,
tiempo time(0) not null
);

-- Crear nueva tabla con los mejores tiempos
create table best_records
(
nombre varchar(20) not null,
tiempo time(0) not null
);

-- Crear trigger
create trigger actualizar_best_records
ON game_records
after insert
as
begin
delete from best_records;
insert into best_records (nombre, tiempo)
select top (5)
nombre,
tiempo
from game_records
order by tiempo desc;
end;

-- Seleccionar tablas
select * from game_records
select * from best_records
