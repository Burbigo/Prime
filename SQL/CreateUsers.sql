use Prime
create table Users
(
 ID int identity(1,1) primary key,
 Name varchar(15) not null,
 Surname varchar(15) not null,
 Username varchar(20) not null unique,
 Email varchar(25) not null unique,
 Сountersign varchar(20) not null,
 User_status varchar(20) not null,
)
