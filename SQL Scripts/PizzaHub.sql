use PizzaHub
go
create table Pizzas (
PizzaId int not null primary key IDENTITY,
CategoryId int not null,
Name nvarchar(200) not null,
Image varchar(200),
SauceId int,
Description nvarchar(1000),
Status bit not null,
);
go
create table Toppings (
ToppingId int not null primary key IDENTITY,
ToppingName nvarchar(200) not null,
CategoryId int not null,
Image varchar(200),
Quantity bit not null,
UnitPrice money not null,
);
go
create table ToppingDetails (
PizzaId int not null foreign key references Pizzas(PizzaId),
ToppingId int not null foreign key references Toppings(ToppingId)
);
go
create table Drinks (
DrinkId int not null primary key IDENTITY,
Name nvarchar(200) not null,
Brand  nvarchar(200),
Image varchar(200),
SizeId int not null,
Price money not null,
);
go
create table Members (
MemberId int not null primary key IDENTITY,
Email nvarchar(200) not null,
Password nvarchar(200) not null,
Avatar varchar(200),
DOB date not null,
MobileNumber nvarchar(20) not null,
Address nvarchar(1000) not null,
City nvarchar(100) not null,
Country nvarchar(100) not null,
RankId int not null,
Point float default 0,
Voucher int,
Role bit default 0
);
go
create table Vouchers (
VoucherId int not null primary key IDENTITY,
VoucherCode nvarchar(100) not null,
Description nvarchar(1000),
Discount int not null,
Quantity int not null
)
go
create table MemberVouchers (
VoucherId int not null foreign key references Vouchers(VoucherID),
MemberId int not null foreign key references Members(MemberId),
Quantity int not null
)
go
create table Orders (
OrderId int not null primary key IDENTITY,
MemberId int not null,
OrderDate date not null,
RequiredDate date,
ShippedDate date,
Freight money,
Address nvarchar(1000) not null,
Status varchar(200) default 'Pending',
Note nvarchar(1000),
)

go
create table Category (
CategoryId int not null primary key IDENTITY,
Name nvarchar(200) not null,
)
go
create table Size (
SizeId int not null primary key IDENTITY,
Name char(5) not null,
)
go
create table Pizza_Size (
    PizzaId int not null foreign key references Pizzas(PizzaId),
    SizeId int not null foreign key references Size(SizeId),
    Price money not null,
)
go
create table Base (
BaseId int not null primary key IDENTITY,
Name nvarchar(200) not null,
)
go
create table Pizza_Base (
    PizzaId int not null foreign key references Pizzas(PizzaId),
    BaseId int not null foreign key references Base(BaseId),
)
go
create table Extras (
    ExtraId int IDENTITY not null primary key ,
    ExtraName nvarchar(200) not null,
    Image varchar(200),
    Price money not null
)
go
create table Sauce (
SauceId int not null primary key IDENTITY,
Name nvarchar(200) not null,
)
go
create table Ranks (
RankId int not null primary key IDENTITY,
Name nvarchar(200) not null,
Description nvarchar(1000),
)
go
create table OrderDetail (
OrderId int not null foreign key references Orders(OrderId),
PizzaId int foreign key references Pizzas(PizzaId),
DrinkId int foreign key references Drinks(DrinkId),
ExtraId int foreign key references Extras(ExtraId),
Size nvarchar(20),
Price money,
Quantity int not null,
Discount float
)
go
create table Combo (
    ComboId int primary key IDENTITY not null ,
    ComboName varchar(20) not null,
    PizzaId int foreign key references Pizzas(PizzaId),
    DrinkId int foreign key references Drinks(DrinkId),
    ExtraId int foreign key references Extras(ExtraId),
    Price money
)

alter table  Pizzas
add constraint FK_PizzaSauce foreign key (SauceId) references Sauce(SauceId);
go
alter table  Pizzas
add constraint FK_PizzaCategory foreign key (CategoryId) references Category(CategoryId);
go
alter table Orders
add constraint FK_OrdersMember foreign key (MemberId) references Members(MemberId);
go
alter table Toppings
add constraint FK_ToppingCategory foreign key (CategoryId) references Category(CategoryId)
go
alter table Drinks
add constraint FK_DrinkSize foreign key (SizeId) references Size(SizeId)
go
alter table Members
add constraint FK_MemberRank foreign key (RankId) references Ranks(RankId);
alter table Size
    alter column Name char(25) not null
go

-----------------------------------------------Ranks----------------------------------------------
INSERT INTO PizzaHub.dbo.Ranks (Name, Description) VALUES (N'Silver', N'ABC');
INSERT INTO PizzaHub.dbo.Ranks (Name, Description) VALUES (N'Gold', N'ABC');
INSERT INTO PizzaHub.dbo.Ranks (Name, Description) VALUES (N'Platinum', N'ABC');
INSERT INTO PizzaHub.dbo.Ranks (Name, Description) VALUES (N'Diamond', N'ABC');
INSERT INTO PizzaHub.dbo.Ranks (Name, Description) VALUES (N'GodKiller', N'ABC');
INSERT INTO PizzaHub.dbo.Ranks (Name, Description) VALUES (N'Jeus', N'ABC');
INSERT INTO PizzaHub.dbo.Ranks (Name, Description) VALUES (N'Cosmic', N'ABC');

-----------------------------------------------Category----------------------------------------------
INSERT INTO PizzaHub.dbo.Category (Name) VALUES (N'Cheese');
INSERT INTO PizzaHub.dbo.Category (Name) VALUES (N'Pepperoni');
INSERT INTO PizzaHub.dbo.Category (Name) VALUES (N'Meat');
INSERT INTO PizzaHub.dbo.Category (Name) VALUES (N'Sausage');
INSERT INTO PizzaHub.dbo.Category (Name) VALUES (N'Seafood');
INSERT INTO PizzaHub.dbo.Category (Name) VALUES (N'Mushroom');
INSERT INTO PizzaHub.dbo.Category (Name) VALUES (N'Chillis');
INSERT INTO PizzaHub.dbo.Category (Name) VALUES (N'Chicken');
-----------------------------------------------Members----------------------------------------------
INSERT INTO PizzaHub.dbo.Members (Email, Password, Avatar, DOB, MobileNumber, Address, City, Country, RankId, Point, Voucher) VALUES (N'chiskien03214@gmail.com', N'chiskien', N'ck.svg', N'2021-09-14', N'0965591101', N'Broadway', N'Hanoi', N'VietNam', 4, 0, 0);
INSERT INTO PizzaHub.dbo.Members (Email, Password, Avatar, DOB, MobileNumber, Address, City, Country, RankId, Point, Voucher) VALUES (N'thehainguyen2233@gmail.com', N'thehai', N'th.svg', N'2022-02-17', N'123456789', N'Queens Road', N'London', N'England', 4, 0, 0);

-----------------------------------------------PizzaBase----------------------------------------------
INSERT INTO PizzaHub.dbo.Base (Name) VALUES (N'Thin');
INSERT INTO PizzaHub.dbo.Base (Name) VALUES (N'Medium');
INSERT INTO PizzaHub.dbo.Base (Name) VALUES (N'Thick');

-----------------------------------------------Sauce----------------------------------------------
INSERT INTO PizzaHub.dbo.Sauce (Name) VALUES (N'Ketchup');
INSERT INTO PizzaHub.dbo.Sauce (Name) VALUES (N'Hot Sauce');
INSERT INTO PizzaHub.dbo.Sauce (Name) VALUES (N'Pesto');
INSERT INTO PizzaHub.dbo.Sauce (Name) VALUES (N'Cheesy Mayo');
INSERT INTO PizzaHub.dbo.Sauce (Name) VALUES (N'Black Pepper');

-----------------------------------------------Size----------------------------------------------
INSERT INTO PizzaHub.dbo.Size (Name) VALUES (N'S');
INSERT INTO PizzaHub.dbo.Size (Name) VALUES (N'M');
INSERT INTO PizzaHub.dbo.Size (Name) VALUES (N'L');
INSERT INTO PizzaHub.dbo.Size (Name) VALUES (N'XL');
INSERT INTO PizzaHub.dbo.Size (Name) VALUES (N'Personal');
INSERT INTO PizzaHub.dbo.Size (Name) VALUES (N'Regular');
INSERT INTO PizzaHub.dbo.Size (Name) VALUES (N'Large');


-----------------------------------------------Drinks----------------------------------------------
INSERT INTO PizzaHub.dbo.Drinks (Name, Brand, Image, SizeId, Price) VALUES (N'Pepsi', N'Pepsi', N'pepsi_can.jpg', 1, 15.0000);
INSERT INTO PizzaHub.dbo.Drinks (Name, Brand, Image, SizeId, Price) VALUES (N'Pepsi', N'Pepsi', N'pepsi_bottle.jpg', 2, 20.0000);
INSERT INTO PizzaHub.dbo.Drinks (Name, Brand, Image, SizeId, Price) VALUES (N'Pepsi', N'Pepsi', N'pepsi_large_bottle.jpg', 3, 30.0000);
INSERT INTO PizzaHub.dbo.Drinks (Name, Brand, Image, SizeId, Price) VALUES (N'Mirinda Orange', N'Mirinda', N'mirinda_orange.jpg', 1, 15.0000);
INSERT INTO PizzaHub.dbo.Drinks (Name, Brand, Image, SizeId, Price) VALUES (N'Mirinda Cream', N'Mirinda', N'mirinda_cream.jpg', 1, 15.0000);
INSERT INTO PizzaHub.dbo.Drinks (Name, Brand, Image, SizeId, Price) VALUES (N'7UP', N'7UP', N'7up.jpg', 1, 15.0000);

-----------------------------------------------Pizzas----------------------------------------------
INSERT INTO PizzaHub.dbo.Pizzas (CategoryId, Name, Image, SauceId, Description, Status) VALUES (2, N'Pizza Pepperoni', N'pepperoni.jpg', 1, N'Pizza Pepperoni', 1);
INSERT INTO PizzaHub.dbo.Pizzas (CategoryId, Name, Image, SauceId, Description, Status) VALUES (1, N'Cheese Lovers Pizza', N'cheese_lover.jpg', 4, N' Phô mai cao cấp', 1);
INSERT INTO PizzaHub.dbo.Pizzas (CategoryId, Name, Image, SauceId, Description, Status) VALUES (5, N' Seafood Pesto Pizza', N'seafood_pesto.jpg', 3, N'Hải sản xốt pesto', 1);
INSERT INTO PizzaHub.dbo.Pizzas (CategoryId, Name, Image, SauceId, Description, Status) VALUES (5, N' Seafood BlackPepper Pizza', N'seafood_blackpepper.jpg', 5, N'Hải sản xốt tiêu đen', 1);
INSERT INTO PizzaHub.dbo.Pizzas (CategoryId, Name, Image, SauceId, Description, Status) VALUES (8, N' Chicken Deluxe Pizza', N'chicken_deluxe.jpg', 5, N'Gà nướng nấm', 1);
INSERT INTO PizzaHub.dbo.Pizzas (CategoryId, Name, Image, SauceId, Description, Status) VALUES (3, N' Korean BBQ Spicy Beef Deluxe Pizza', N'bbq_beef.jpg', 2, N'Bò BBQ xốt cay Hàn Quốc', 1);

-----------------------------------------------Orders----------------------------------------------
INSERT INTO PizzaHub.dbo.Orders (MemberId, OrderDate, RequiredDate, ShippedDate, Freight, Address, Status,Note) VALUES (1, N'2022-02-16', N'2022-02-16', N'2022-02-16', 10.0000, N'Broadway','Pending' ,N'More chillis and ketchup pls !');
INSERT INTO PizzaHub.dbo.Orders (MemberId, OrderDate, RequiredDate, ShippedDate, Freight, Address, Status,Note) VALUES (2, N'2022-02-16', N'2022-02-16', N'2022-02-16', 10.0000, N'Queen Road','Pending', N'No ketchup');

-----------------------------------------------Pizza_Base----------------------------------------------

INSERT INTO PizzaHub.dbo.Pizza_Base (PizzaId, BaseId) VALUES (1, 1);
INSERT INTO PizzaHub.dbo.Pizza_Base (PizzaId, BaseId) VALUES (1, 2);
INSERT INTO PizzaHub.dbo.Pizza_Base (PizzaId, BaseId) VALUES (1, 3);
INSERT INTO PizzaHub.dbo.Pizza_Base (PizzaId, BaseId) VALUES (2, 1);
INSERT INTO PizzaHub.dbo.Pizza_Base (PizzaId, BaseId) VALUES (2, 2);
INSERT INTO PizzaHub.dbo.Pizza_Base (PizzaId, BaseId) VALUES (2, 3);
INSERT INTO PizzaHub.dbo.Pizza_Base (PizzaId, BaseId) VALUES (3, 1);
INSERT INTO PizzaHub.dbo.Pizza_Base (PizzaId, BaseId) VALUES (3, 2);
INSERT INTO PizzaHub.dbo.Pizza_Base (PizzaId, BaseId) VALUES (3, 3);
INSERT INTO PizzaHub.dbo.Pizza_Base (PizzaId, BaseId) VALUES (4, 1);
INSERT INTO PizzaHub.dbo.Pizza_Base (PizzaId, BaseId) VALUES (4, 2);
INSERT INTO PizzaHub.dbo.Pizza_Base (PizzaId, BaseId) VALUES (4, 3);
INSERT INTO PizzaHub.dbo.Pizza_Base (PizzaId, BaseId) VALUES (5, 1);
INSERT INTO PizzaHub.dbo.Pizza_Base (PizzaId, BaseId) VALUES (5, 2);
INSERT INTO PizzaHub.dbo.Pizza_Base (PizzaId, BaseId) VALUES (5, 3);
INSERT INTO PizzaHub.dbo.Pizza_Base (PizzaId, BaseId) VALUES (6, 1);
INSERT INTO PizzaHub.dbo.Pizza_Base (PizzaId, BaseId) VALUES (6, 2);
INSERT INTO PizzaHub.dbo.Pizza_Base (PizzaId, BaseId) VALUES (6, 3);

-----------------------------------------------Pizza_Size----------------------------------------------
INSERT INTO PizzaHub.dbo.Pizza_Size (PizzaId, SizeId, Price) VALUES (6, 5, 109.0000);
INSERT INTO PizzaHub.dbo.Pizza_Size (PizzaId, SizeId, Price) VALUES (6, 6, 169.0000);
INSERT INTO PizzaHub.dbo.Pizza_Size (PizzaId, SizeId, Price) VALUES (6, 7, 249.0000);
INSERT INTO PizzaHub.dbo.Pizza_Size (PizzaId, SizeId, Price) VALUES (5, 5, 109.0000);
INSERT INTO PizzaHub.dbo.Pizza_Size (PizzaId, SizeId, Price) VALUES (5, 6, 169.0000);
INSERT INTO PizzaHub.dbo.Pizza_Size (PizzaId, SizeId, Price) VALUES (5, 7, 249.0000);
INSERT INTO PizzaHub.dbo.Pizza_Size (PizzaId, SizeId, Price) VALUES (4, 5, 129.0000);
INSERT INTO PizzaHub.dbo.Pizza_Size (PizzaId, SizeId, Price) VALUES (4, 6, 199.0000);
INSERT INTO PizzaHub.dbo.Pizza_Size (PizzaId, SizeId, Price) VALUES (4, 7, 289.0000);
INSERT INTO PizzaHub.dbo.Pizza_Size (PizzaId, SizeId, Price) VALUES (3, 5, 129.0000);
INSERT INTO PizzaHub.dbo.Pizza_Size (PizzaId, SizeId, Price) VALUES (3, 6, 219.0000);
INSERT INTO PizzaHub.dbo.Pizza_Size (PizzaId, SizeId, Price) VALUES (3, 7, 289.0000);
INSERT INTO PizzaHub.dbo.Pizza_Size (PizzaId, SizeId, Price) VALUES (2, 5, 79.0000);
INSERT INTO PizzaHub.dbo.Pizza_Size (PizzaId, SizeId, Price) VALUES (2, 6, 138.0000);
INSERT INTO PizzaHub.dbo.Pizza_Size (PizzaId, SizeId, Price) VALUES (2, 7, 209.0000);
INSERT INTO PizzaHub.dbo.Pizza_Size (PizzaId, SizeId, Price) VALUES (1, 5, 109.0000);
INSERT INTO PizzaHub.dbo.Pizza_Size (PizzaId, SizeId, Price) VALUES (1, 6, 169.0000);
INSERT INTO PizzaHub.dbo.Pizza_Size (PizzaId, SizeId, Price) VALUES (1, 7, 249.0000);

-----------------------------------------------Order_Detail----------------------------------------------

INSERT INTO PizzaHub.dbo.OrderDetail (OrderId, PizzaId, DrinkId, ExtraId, Size, Price, Quantity, Discount) VALUES (1, 1, 1, null, N'Regular', 353.0000, 2, null);
INSERT INTO PizzaHub.dbo.OrderDetail (OrderId, PizzaId, DrinkId, ExtraId, Size, Price, Quantity, Discount) VALUES (2, 2, null, null, N'Large', 231.2000, 1, 0.2);

UPDATE PizzaHub.dbo.Pizzas SET CategoryId = 2, Name = N'Pizza Pepperoni', Image = N'pepperoni.svg', SauceId = 1, Description = N'Pizza Pepperoni', Status = 1 WHERE PizzaId = 1;
UPDATE PizzaHub.dbo.Pizzas SET CategoryId = 1, Name = N'Cheese Lovers Pizza', Image = N'cheese_lover.svg', SauceId = 4, Description = N' Phô mai cao cấp', Status = 1 WHERE PizzaId = 2;
UPDATE PizzaHub.dbo.Pizzas SET CategoryId = 5, Name = N' Seafood Pesto Pizza', Image = N'seafood_pesto.svg', SauceId = 3, Description = N'Hải sản xốt pesto', Status = 1 WHERE PizzaId = 3;
UPDATE PizzaHub.dbo.Pizzas SET CategoryId = 5, Name = N' Seafood BlackPepper Pizza', Image = N'seafood_blackpepper.svg', SauceId = 5, Description = N'Hải sản xốt tiêu đen', Status = 1 WHERE PizzaId = 4;
UPDATE PizzaHub.dbo.Pizzas SET CategoryId = 8, Name = N' Chicken Deluxe Pizza', Image = N'chicken_deluxe.svg', SauceId = 5, Description = N'Gà nướng nấm', Status = 1 WHERE PizzaId = 5;
UPDATE PizzaHub.dbo.Pizzas SET CategoryId = 3, Name = N' Korean BBQ Spicy Beef Deluxe Pizza', Image = N'bbq_beef.svg', SauceId = 2, Description = N'Bò BBQ xốt cay Hàn Quốc', Status = 1 WHERE PizzaId = 6;

alter table Category
    add Image varchar(100)
go

UPDATE PizzaHub.dbo.Category SET Name = N'Cheese', Image = N'cheese.svg' WHERE CategoryId = 1;
UPDATE PizzaHub.dbo.Category SET Name = N'Pepperoni', Image = N'pepperoni.svg' WHERE CategoryId = 2;
UPDATE PizzaHub.dbo.Category SET Name = N'Meat', Image = N'meat.svg' WHERE CategoryId = 3;
UPDATE PizzaHub.dbo.Category SET Name = N'Sausage', Image = N'sausage.svg' WHERE CategoryId = 4;
UPDATE PizzaHub.dbo.Category SET Name = N'Seafood', Image = N'seafood.svg' WHERE CategoryId = 5;
UPDATE PizzaHub.dbo.Category SET Name = N'Mushroom', Image = N'mushroom.svg' WHERE CategoryId = 6;
UPDATE PizzaHub.dbo.Category SET Name = N'Chillis', Image = N'chillis.svg' WHERE CategoryId = 7;
UPDATE PizzaHub.dbo.Category SET Name = N'Chicken', Image = N'chicken.svg' WHERE CategoryId = 8;

UPDATE PizzaHub.dbo.Members SET Email = N'chiskien03214@gmail.com', Password = N'chiskien', Avatar = N'ck.svg', DOB = N'2021-09-14', MobileNumber = N'0965591101', Address = N'Broadway', City = N'Hanoi', Country = N'VietNam', RankId = 4, Point = 0, Voucher = 0, Role = 1 WHERE MemberId = 1;
UPDATE PizzaHub.dbo.Members SET Email = N'thehainguyen2233@gmail.com', Password = N'thehai', Avatar = N'th.svg', DOB = N'2022-02-17', MobileNumber = N'123456789', Address = N'Queens Road', City = N'London', Country = N'England', RankId = 4, Point = 0, Voucher = 0, Role = 1 WHERE MemberId = 2;

alter table Members
    alter column MobileNumber nvarchar(20) null
go

alter table Members
    alter column Address nvarchar(1000) null
go

alter table Members
    alter column RankId int null
go

alter table Members
    add default 6 for RankId
go

alter table Members
    alter column Point float null
go

alter table Members
    alter column Voucher int null
go

alter table Members
    alter column Role bit null
go
alter table Members
    alter column City nvarchar(100) null
go
alter table Members
    alter column Country nvarchar(100) null
go
alter table Members
    alter column DOB datetime null
go

