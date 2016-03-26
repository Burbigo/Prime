create table devices
 (
 device_model varchar(20) not null,
 device_type varchar(20) not null,
 device_id int identity(1,1) primary key,
 device_ip varchar(15) unique not null,
 subnet int not null,
 device_status varchar(15) not null,
 date_of_production date,
 last_change varchar(40),
 date_of_last_change date
 )
 
 select * from devices
