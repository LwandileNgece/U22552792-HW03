CREATE DATABASE Reports
CREATE TABLE Report(
	ID int identity(1,1) primary key not null,
	Name varchar(255) not null,
	File_Type varchar(255),
	Description varchar(255)
);
INSERT INTO Report VALUES('Example','png','This chart describes most borrowed books.');
SELECT * FROM Report