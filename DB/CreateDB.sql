create database demoexam_type4_variant1;
use demoexam_type4_variant1;

create table `role` (
	id int primary key auto_increment,
    `name` nvarchar(16) not null
);
create table `status` (
	id int primary key auto_increment,
    `name` nvarchar(16) not null
);
create table `manufacturer` (
	id int primary key auto_increment,
    `name` nvarchar(16) not null
);

create table `user` (
	id int primary key auto_increment,
    user_role_id int not null,
    
    login nvarchar(64) not null,
    `password` nvarchar(64) not null,
    `name` nvarchar(32) not null,
    surname nvarchar(32) not null,
    patronymic nvarchar(32) null,
    
    foreign key (user_role_id) references `role`(id)
);
create table pickup_point (
	id int primary key auto_increment,
    city nvarchar(64) not null,
    street nvarchar(64) not null,
    house int not null
);

create table product (
	id int primary key auto_increment,    
    manufacturer_id int not null,
    
    `name` nvarchar(64) not null,
    `description` nvarchar(256) null,
    image nvarchar(256) null,
    cost decimal(10, 2) not null,
    discount tinyint null,
    amount int not null,
    
    foreign key (manufacturer_id) references manufacturer(id)
);

create table `order` (
	id int primary key auto_increment,
    user_id int null,
    status_id int not null,
    pickup_point_id int not null,
    
    take_code int not null,
    
    foreign key (user_id) references `user`(id),
    foreign key (status_id) references `status`(id),
    foreign key (pickup_point_id) references pickup_point(id)
);

create table order_product (
	id int primary key auto_increment,
    order_id int not null,
    product_id int not null,
    
    count int not null,
    
    foreign key (order_id) references `order`(id),
    foreign key (product_id) references product(id),
    
    constraint check (count > 0)
);
