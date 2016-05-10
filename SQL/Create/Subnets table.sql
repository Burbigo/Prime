use Prime
create table Subnets
(
 ID int identity(1,1) primary key,
 Name varchar(15) not null unique,
 Number_of_devices int not null,
 Engineer varchar(20)
)
