use Prime 
create table Operations
(
 ID int identity(1,1) primary key,
 Name varchar(15) not null,
 Subnet_name varchar(15) not null,
 Device_name varchar(20) not null,
 Engineer varchar(20) not null, 
 Author varchar(20)not null,
 Status_of_operation varchar(15) not null,
 Cause_of_rejection varchar(50)
)