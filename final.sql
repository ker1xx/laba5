create table [model]
(
[id] int identity (1,1) primary key not null,
[name] varchar (50) not null
)
create table [dealer]
(
[id] int identity (1,1) primary key not null,
[name] varchar (50) not null,
[country] varchar(50) not null
)

create table [storage]
(
[id] int identity (1,1) primary key not null,
[id_model] int foreign key references [model] ([id]),
[amount] int not null,
[id_dealer] int foreign key references [dealer] ([id]),
[first_price] money not null,
)
create table [job title]
(
[id] int identity(1,1) primary key not null,
[name] varchar(50) not null
)

create table [market]
(
[id] int identity (1,1) primary key not null,
[adress] varchar (255) not null,
[begin working time] datetime not null,
[end working time] datetime not null,
[name] varchar(255) not null
)
create table [goods]
(
[id] int identity(1,1) primary key not null,
[price] money not null,
[sex] varchar(50) not null,
[name id] int foreign key references [storage] ([id]),
[size] int not null
)
create table [employee]
(
[id] int identity (1,1) primary key not null,
[name] varchar(50) not null,
[surname] varchar(50) not null,
[lastname] varchar(50),
[job title id]  int foreign key references [job title] ([id]),
[salary] money not null,
)

create table [authorization]
(
[id] int primary key foreign key references [employee] ([id]),
[login] varchar(50) not null,
[password] varchar(50) not null
)

create table [check info]
([id] int identity (1,1) primary key not null,
[employee id] int foreign key references [employee] ([id]),
[market id] int foreign key references [market] ([id]),
[total money] money not null,
[date] datetime not null
)

create table [order]
(
[id] int identity (1,1) primary key not null,
[order info id] int foreign key references [check info] ([id]),
[goods in order id] int foreign key references [goods] ([id]),
[profit] money not null,
)

insert into [model] ([name])values ('air max'),('forum low'),('rewind run')
insert into [dealer] ([name],[country]) values ('nike','USA'),('adidas','USA'),('reebok','Gorgia')
insert into [storage] ([id_model],[id_dealer],[first_price],[amount]) values (1,2,2000,150),(2,3,1600,80),(3,4,700,42)
insert into [goods] ([name id],[price],[sex],[size]) values (2,13000,'male',43),(3,9400,'unisex',40),(4,6700,'female',36)
insert into [job title] ([name]) values ('Admin'),('Cashier')
insert into [employee] ([name],[surname],[lastname],[job title id],[salary]) values ('Иванов','Иван',' Иванович',1,80000), ('Петров','Петр',' Александрович',2,45000), ('Семенов','Семен','Семенович',2,25800)
insert into [authorization] ([id],[login],[password]) values (1,'admin','admin'),(2,'cassa','user1'),(3,'kassa','user2')
insert into [market] ([name],[adress],[begin working time],[end working time]) values ('рай ботинков','Москва, Тверская ул., д. 4, Павильон 28','09:00:00','22:00:00'), ('ботинковый дом','Тверь, Москвоская ул., д. 1','11:30:00','20:00:00'),('кроссовочный институт','Казань, ул. Пушкина, д.6','08:00:00','23:00:00')
insert into [check info] ([employee id],[market id],[total money],[date]) 
values (3,5,6700,'20230321 19:54:24')