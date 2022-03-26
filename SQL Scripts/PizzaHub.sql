use
    master
go
create
    database PizzaHub
go
create table dbo.Categories
(
    CategoryId   int identity,
    CategoryName nvarchar(50) not null,
    Image        varchar(255),
    constraint Categories_pk
        primary key (CategoryId)
)
go

create table dbo.Drinks
(
    DrinkId   int identity,
    DrinkName nvarchar(255) not null,
    Brand     nvarchar(200),
    Image     varchar(255),
    constraint Drinks_pk
        primary key (DrinkId)
)
go

create table dbo.PizzaBases
(
    BaseId int identity,
    Base   nvarchar(25) not null,
    constraint PizzaBases_pk
        primary key (BaseId)
)
go

create table dbo.Ranks
(
    RankId      int identity,
    Rank        nvarchar(255) not null,
    Description nvarchar(255),
    MinPoint    int,
    constraint Ranks_pk
        primary key (RankId)
)
go

create table dbo.Members
(
    MemberId    int identity,
    Email       varchar(255) not null,
    Password    varchar(255) not null,
    Avatar      varchar(255),
    DOB         datetime,
    PhoneNumber nvarchar(255),
    Address     nvarchar(255),
    City        nvarchar(255),
    Country     nvarchar(255),
    Role        bit default 0,
    Point       int default 0,
    RankId      int default 1,
    constraint Members_pk
        primary key (MemberId),
    constraint Members_Ranks_RankId_fk
        foreign key (RankId) references dbo.Ranks
            on delete set default
)
go

alter table dbo.Members
    add constraint DF__Members__Point__48CFD27E default 0 for Point
go

alter table dbo.Members
    add constraint DF__Members__RankId__49C3F6B7 default 1 for RankId
go

alter table dbo.Members
    add constraint DF__Members__Role__47DBAE45 default 0 for Role
go

create table dbo.Sauces
(
    SauceId   int identity,
    SauceName nvarchar(50),
    constraint Sauce_pk
        primary key (SauceId)
)
go

create table dbo.Sizes
(
    SizeId int identity,
    Size   nvarchar(20) not null,
    constraint Sizes_pk
        primary key (SizeId)
)
go

create table dbo.Status
(
    StatusId int identity,
    Status   nvarchar(255) not null,
    constraint Status_pk
        primary key (StatusId)
)
go

create table dbo.Orders
(
    OrderId      int identity,
    MemberId     int,
    OrderDate    datetime      not null,
    Address      nvarchar(255),
    StatusId     int default 1 not null,
    Freight      money,
    RequiredDate datetime,
    ShippedDate  datetime,
    Note         nvarchar(255),
    constraint Orders_pk
        primary key (OrderId),
    constraint Orders_Members_MemberId_fk
        foreign key (MemberId) references dbo.Members
            on delete cascade,
    constraint Orders_Status_StatusId_fk
        foreign key (StatusId) references dbo.Status
            on delete set default
)
go

alter table dbo.Orders
    add constraint DF__Orders__StatusId__4CA06362 default 1 for StatusId
go

create table dbo.Pizzas
(
    PizzaId     int identity,
    CategoryId  int,
    SauceId     int,
    Image       varchar(255),
    Description nvarchar(255),
    StatusId    int,
    Price       money         not null,
    PizzaName   nvarchar(200) not null,
    constraint Pizzas_pk
        primary key (PizzaId),
    constraint Pizzas_Categories_CategoryId_fk
        foreign key (CategoryId) references dbo.Categories,
    constraint Pizzas_Sauces_SauceId_fk
        foreign key (SauceId) references dbo.Sauces,
    constraint Pizzas_Status_StatusId_fk
        foreign key (StatusId) references dbo.Status
)
go

create table dbo.OrdersDetail
(
    OrderId    int   not null,
    PizzaId    int   not null,
    DrinkId    int,
    SizeId     int,
    BaseId     int,
    Quantity   int default 1,
    Discount   float,
    TotalPrice money not null,
    constraint OrdersDetail_Drinks_DrinkId_fk
        foreign key (DrinkId) references dbo.Drinks
            on delete set null,
    constraint OrdersDetail_Orders_OrderId_fk
        foreign key (OrderId) references dbo.Orders
            on delete cascade,
    constraint OrdersDetail_PizzaBases_BaseId_fk
        foreign key (BaseId) references dbo.PizzaBases
            on delete set default,
    constraint OrdersDetail_Pizzas_PizzaId_fk
        foreign key (PizzaId) references dbo.Pizzas
            on delete cascade,
    constraint OrdersDetail_Sizes_SizeId_fk
        foreign key (SizeId) references dbo.Sizes
            on delete cascade
)
go

alter table dbo.OrdersDetail
    add constraint DF__OrdersDet__Quant__4E88ABD4 default 1 for Quantity
go

create table dbo.Toppings
(
    ToppingId   int identity,
    ToppingName nvarchar(200) not null,
    CategoryId  int,
    Image       varchar(255),
    UnitPrice   money,
    constraint Toppings_pk
        primary key (ToppingId)
)
go

create table dbo.Pizza_Topping_Detail
(
    PizzaId   int not null,
    ToppingId int not null,
    constraint Pizza_Topping_Detail_Pizzas_PizzaId_fk
        foreign key (PizzaId) references dbo.Pizzas
            on delete cascade,
    constraint Pizza_Topping_Detail_Toppings_ToppingId_fk
        foreign key (ToppingId) references dbo.Toppings
            on delete cascade
)
go
create table Cart
(
    MemberId int
        constraint Cart_Members_MemberId_fk
            references Members
            on delete cascade,
    PizzaId  int
        constraint Cart_Pizzas_PizzaId_fk
            references Pizzas
            on delete cascade,
    SizeId   int default 1
        constraint Cart_Sizes_SizeId_fk
            references Sizes
            on delete cascade,
    Base     int default 1
        constraint Cart_PizzaBases_BaseId_fk
            references PizzaBases
            on delete cascade,
    Amount   int
)
go
----------------------------------------------Category----------------------------------------
INSERT INTO PizzaHub.dbo.Categories (CategoryName, Image)
VALUES (N'Cheese', N'cheese.svg');
INSERT INTO PizzaHub.dbo.Categories (CategoryName, Image)
VALUES (N'Pepperoni', N'pepperoni.svg');
INSERT INTO PizzaHub.dbo.Categories (CategoryName, Image)
VALUES (N'Meat', N'meat.svg');
INSERT INTO PizzaHub.dbo.Categories (CategoryName, Image)
VALUES (N'Sausage', N'sausage.svg');
INSERT INTO PizzaHub.dbo.Categories (CategoryName, Image)
VALUES (N'Seafood', N'seafood.svg');
INSERT INTO PizzaHub.dbo.Categories (CategoryName, Image)
VALUES (N'Mushroom', N'mushroom.svg');
INSERT INTO PizzaHub.dbo.Categories (CategoryName, Image)
VALUES (N'Chillis', N'chillis.svg');
INSERT INTO PizzaHub.dbo.Categories (CategoryName, Image)
VALUES (N'Chicken', N'chicken.svg');
-------------------------------------------Drinks--------------------------------------
INSERT INTO PizzaHub.dbo.Drinks (DrinkName, Brand, Image)
VALUES (N'Pepsi', N'Pepsi', N'pepsi_can.svg');
INSERT INTO PizzaHub.dbo.Drinks (DrinkName, Brand, Image)
VALUES (N'Mirinda Orange', N'Mirinda', N'mirinda_orange.svg');
INSERT INTO PizzaHub.dbo.Drinks (DrinkName, Brand, Image)
VALUES (N'Mirinda Cream', N'Mirinda', N'mirinda_cream.svg');
INSERT INTO PizzaHub.dbo.Drinks (DrinkName, Brand, Image)
VALUES (N'7UP', N'7UP', N'7up.svg');
INSERT INTO PizzaHub.dbo.Drinks (DrinkName, Brand, Image)
VALUES (N'MountainDew', N'MountainDew', N'MountainDew.svg');
---------------------------------------Status---------------------------------------------
INSERT INTO PizzaHub.dbo.Status (Status)
VALUES (N'Available');
INSERT INTO PizzaHub.dbo.Status (Status)
VALUES (N'Out of Stock');
INSERT INTO PizzaHub.dbo.Status (Status)
VALUES (N'Pending');
INSERT INTO PizzaHub.dbo.Status (Status)
VALUES (N'Approved');
INSERT INTO PizzaHub.dbo.Status (Status)
VALUES (N'Canceled');
INSERT INTO PizzaHub.dbo.Status (Status)
VALUES (N'On Sale');
--------------------------------------Size------------------------------------------------
INSERT INTO PizzaHub.dbo.Sizes (Size)
VALUES (N'Personal');
INSERT INTO PizzaHub.dbo.Sizes (Size)
VALUES (N'Regular');
INSERT INTO PizzaHub.dbo.Sizes (Size)
VALUES (N'Large');
----------------------------------Base----------------------------------------------------------
INSERT INTO PizzaHub.dbo.PizzaBases (Base)
VALUES (N'Thin');
INSERT INTO PizzaHub.dbo.PizzaBases (Base)
VALUES (N'Medium');
INSERT INTO PizzaHub.dbo.PizzaBases (Base)
VALUES (N'Thick');
----------------------------------------------------Rank----------------------------------
INSERT INTO PizzaHub.dbo.Ranks (Rank, Description, MinPoint)
VALUES (N'Silver', N'A', 0);
INSERT INTO PizzaHub.dbo.Ranks (Rank, Description, MinPoint)
VALUES (N'Gold', N'A', 30);
INSERT INTO PizzaHub.dbo.Ranks (Rank, Description, MinPoint)
VALUES (N'Platinum', N'A', 70);
INSERT INTO PizzaHub.dbo.Ranks (Rank, Description, MinPoint)
VALUES (N'Diamond', N'A', 120);
INSERT INTO PizzaHub.dbo.Ranks (Rank, Description, MinPoint)
VALUES (N'Jeus', N'A', 200);
------------------------------------Sauces-----------------------------------
INSERT INTO PizzaHub.dbo.Sauces (SauceName)
VALUES (N'Ketchup');
INSERT INTO PizzaHub.dbo.Sauces (SauceName)
VALUES (N'Hot Sauce');
INSERT INTO PizzaHub.dbo.Sauces (SauceName)
VALUES (N'Pesto');
INSERT INTO PizzaHub.dbo.Sauces (SauceName)
VALUES (N'Cheesy Mayo');
INSERT INTO PizzaHub.dbo.Sauces (SauceName)
VALUES (N'Black Pepper');
------------------------------Members-------------------------------------
INSERT INTO PizzaHub.dbo.Members (Email, Password, Avatar, DOB, PhoneNumber, Address, City, Country, Role, Point,
                                  RankId)
VALUES (N'chiskien@gmail.com', N'123', N'peep-14.svg', N'2022-03-26 00:16:01.000', N'0965591101', N'14 QueenRoad',
        N'London', N'England', 1, 150, 4);
INSERT INTO PizzaHub.dbo.Members (Email, Password, Avatar, DOB, PhoneNumber, Address, City, Country, Role, Point,
                                  RankId)
VALUES (N'thehai@gmail.com', N'234', N'peep-25.svg', N'2022-03-26 00:18:28.000', N'12345689', N'Hoa Lac', N'Hanoi',
        N'VietNam', 1, 170, 4);
INSERT INTO PizzaHub.dbo.Members (Email, Password, Avatar, DOB, PhoneNumber, Address, City, Country, Role, Point,
                                  RankId)
VALUES (N'guest@gmail.com', N'456', N'peep-1.svg', N'2022-03-26 00:20:19.000', N'123456789', N'SomeStreet', N'SomeCity',
        N'SomeCountry', 0, 0, 1);
----------------------------------------------Pizzas--------------------------------------
INSERT INTO PizzaHub.dbo.Pizzas (CategoryId, SauceId, Image, Description, StatusId, Price, PizzaName)
VALUES (2, 1, N'pepperoni.svg', N'Pizza Pepperoni', 1, 89.0000, N'Pizza Pepperoni');
INSERT INTO PizzaHub.dbo.Pizzas (CategoryId, SauceId, Image, Description, StatusId, Price, PizzaName)
VALUES (1, 4, N'cheese_lover.svg', N' Phô mai cao cấp', 1, 109.0000, N'Cheese Lovers Pizza');
INSERT INTO PizzaHub.dbo.Pizzas (CategoryId, SauceId, Image, Description, StatusId, Price, PizzaName)
VALUES (5, 3, N'seafood_pesto.svg', N'Hải sản xốt pesto', 1, 109.0000, N' Seafood Pesto Pizza');
INSERT INTO PizzaHub.dbo.Pizzas (CategoryId, SauceId, Image, Description, StatusId, Price, PizzaName)
VALUES (5, 5, N'seafood_blackpepper.svg', N'Hải sản xốt tiêu đen', 1, 129.0000, N' Seafood BlackPepper Pizza');
INSERT INTO PizzaHub.dbo.Pizzas (CategoryId, SauceId, Image, Description, StatusId, Price, PizzaName)
VALUES (8, 5, N'chicken_deluxe.svg', N'Gà nướng nấm', 1, 129.0000, N' Chicken Deluxe Pizza');
INSERT INTO PizzaHub.dbo.Pizzas (CategoryId, SauceId, Image, Description, StatusId, Price, PizzaName)
VALUES (3, 2, N'bbq_beef.svg', N'Bò BBQ xốt cay Hàn Quốc', 1, 109.0000, N' Korean BBQ Spicy Beef Deluxe Pizza');
INSERT INTO PizzaHub.dbo.Pizzas (CategoryId, SauceId, Image, Description, StatusId, Price, PizzaName)
VALUES (4, 1, N'meat_lovers.svg', N'Pizza Thịt và Xúc xích', 1, 129.0000, N'Pizza Thịt và Xúc xíchh');
INSERT INTO PizzaHub.dbo.Pizzas (CategoryId, SauceId, Image, Description, StatusId, Price, PizzaName)
VALUES (4, 1, N'hawaiian.svg', N'Pizza Hawaiian', 1, 89.0000, N'Pizza Hawaiian');

--------------------------------------------Orders--------------------------------------------
INSERT INTO PizzaHub.dbo.Orders (MemberId, OrderDate, Address, StatusId, Freight, RequiredDate, ShippedDate, Note)
VALUES (1, N'2022-03-26 00:29:36.000', N'HaNoi', 3, 10.0000, null, null, N'More sauce');
INSERT INTO PizzaHub.dbo.Orders (MemberId, OrderDate, Address, StatusId, Freight, RequiredDate, ShippedDate, Note)
VALUES (1, N'2022-03-01 00:30:34.000', N'HoaLac', 4, 10.0000, N'2022-03-26 00:30:58.000', N'2022-03-26 00:30:59.000',
        N'No chillis');
INSERT INTO PizzaHub.dbo.Orders (MemberId, OrderDate, Address, StatusId, Freight, RequiredDate, ShippedDate, Note)
VALUES (2, N'2022-03-26 00:31:29.000', N'HoaLac', 5, 10.0000, N'2022-03-26 00:31:44.000', N'2022-03-26 00:31:50.000',
        N'Guest canceled');
-------------------------------------OrderDetail-----------------------------------------
INSERT INTO PizzaHub.dbo.OrdersDetail (OrderId, PizzaId, DrinkId, SizeId, BaseId, Quantity, Discount, TotalPrice)
VALUES (1, 3, 4, 2, 1, 3, null, 500.0000);
INSERT INTO PizzaHub.dbo.OrdersDetail (OrderId, PizzaId, DrinkId, SizeId, BaseId, Quantity, Discount, TotalPrice)
VALUES (2, 4, null, 3, 2, 2, 0.2, 400.0000);