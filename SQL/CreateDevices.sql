use Prime
create table Devices
 (
 ID int identity(1,1) primary key,
 Name varchar(20) not null unique,
 Device_type varchar(20) not null,
 Device_status varchar(15) not null,
 Subnet_name varchar(15) not null,
 IP_adress varchar(15) not null unique,
 Date_of_production date not null,
 Last_change varchar(40),
 Date_of_last_change date
 )
