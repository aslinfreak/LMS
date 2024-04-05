create database LearnDB
use LearnDB
create table AdminRegister(
   AdminId int primary key identity(1,1),
   AdminName varchar(50),
   Email varchar(50),
   Password varchar(50));
   INSERT INTO  AdminRegister(AdminName,Email,Password)
VALUES ('Aslin','aslin@gmail.com',4321);

 create table StudentRegister(
   StudentId int primary key identity(1,1),
   StudentName varchar(50),
   Email varchar(50),
   Password varchar(50),
   Gender nvarchar(10),
   DateOfBirth date, 
   PhoneNumber nvarchar(20),
   Address nvarchar(255));

 create table Courses(
 CourseId int primary key identity(1,1),
 CourseCode nvarchar(20)not null,
 CourseName nvarchar(255)not null,
 Description nvarchar(255),
 CourseFee decimal(10, 2));



 create table Buy(
 EnrollId int primary key identity (1,1),
 CourseId int,
 StudentName varchar(50),
 StudentId int,
 CourseName nvarchar(255)not null,
 CourseFee decimal(10, 2),
 EnrollDate Datetime default current_timestamp,
 foreign key (CourseId) references Courses (CourseId),
 foreign key (StudentId) references StudentRegister(StudentId)
);




select * from AdminRegister;
select * from StudentRegister;
select * from Courses;
select * from Buy;



dbcc checkident('Courses',reseed,0);
delete from Courses;