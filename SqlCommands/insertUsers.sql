
create table users(
	id int primary key,
	username varchar(100) unique,
	name varchar(100),
	role varchar(100)
);



insert into users values (1, 'stef', 'Stefan', 'admin'), (2, 'paul', 'Paul', 'admin'),(3, 'bogdy', 'Bogdan', 'medic'),(4, 'maria', 'Maria', 'medic'),(5, 'fil', 'Filip', 'patient'), (6, 'cata', 'Cata', 'patient')