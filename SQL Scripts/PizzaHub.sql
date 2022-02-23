create database PizzaHub

create table MenuPizzas(
PizzaId int not null primary key IDENTITY,
CategoryId int not null,
Name nvarchar(200) not null,
SizeId int not null foreign key references Size(SizeId),
PizzaBaseId int not null foreign key references PizzaBase(PizzaBaseId),
Sauce int not null foreign key references Sauce(SauceId),
Price money not null,
Description nvarchar(1000),
Status bit not null
);

create table Toppings (
ToppingId int not null primary key IDENTITY,
CategoryId int not null foreign key references Category(CategoryId),
Quanitty  bit not null,
UnitPrice money not null,
);

create table ToppingDetails (
PizzaId int not null foreign key references MenuPizzas(PizzaId),
ToppingId int not null foreign key references Toppings(ToppingId)
);

create table Drinks (
DrinkId int not null primary key IDENTITY,
Name nvarchar(200) not null,
Brand  nvarchar(200),
SizeId int not null foreign key references Size(SizeId),
Price money not null,
);

create table Members (
MemberId int not null primary key IDENTITY,
Email nvarchar(200) not null,
DoB date not null,
Password nvarchar(200) not null,
Address nvarchar(1000) not null,
MobileNumber nvarchar(20) not null,
City nvarchar(100) not null,
Country nvarchar(100) not null,
RankId int not null foreign key references Ranks(RankId),
Point float not null,
Voucher int not null
);

create table Vouchers (
VoucherId int not null primary key IDENTITY,
VoucherCode nvarchar(100) not null,
Description nvarchar(1000) not null,
Discount int not null,
Quantity int not null
)

create table MemberVouchers (
VoucherId int not null foreign key references Vouchers(VoucherID),
MemberId int not null foreign key references Members(MemberId),
Quantity int not null
)

create table Orders (
OrderId int not null primary key IDENTITY,
MemberId int not null foreign key references Members(MemberId),
OrderDate date not null,
RequiredDate date,
ShippedDate date,
Freight money,
Address nvarchar(1000) not null,
Note nvarchar(1000) not null,
)

create table OrderDetail (
OrderId int not null foreign key references Orders(OrderId),
ProductId int not null foreign key references MenuPizzas(PizzaId),
Price money not null,
Quantity int not null,
Discount int not null
)

create table Category (
CategoryId int not null primary key IDENTITY,
Name nvarchar(200) not null,
)

create table Size (
SizeId int not null primary key IDENTITY,
Name char(5) not null,
)

create table PizzaBase (
PizzaBaseId int not null primary key IDENTITY,
Name nvarchar(200) not null,
)

create table Sauce (
SauceId int not null primary key IDENTITY,
Name nvarchar(200) not null,
)

create table PizzaImages (
PizzaId int foreign key references MenuPizzas(PizzaId),
DrinkId int foreign key references Drinks(DrinkId),
Path nvarchar(2000) not null,
)

create table Ranks (
RankId int not null primary key IDENTITY,
Name nvarchar(200) not null,
Description nvarchar(1000) not null,
)
