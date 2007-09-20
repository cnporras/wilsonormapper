create database [usertest]
go


use [usertest]
go


create table [users] ( 
	[userid] int identity(1,1) not null,
	[username] nvarchar(40) not null,
	[password] nvarchar(40) not null,
	[departmentid] int null,
	constraint	pk_users primary key ( userid )
)
go


create table [departments] ( 
	[departmentid] int identity(1,1) not null,
	[department] nvarchar(40) not null,
	[manager_userid] int null,
	constraint	pk_departments primary key ( departmentid ),
	constraint fk_departments_users foreign key ( manager_userid )
			references users ( userid )
)
go


alter table [users]
add constraint fk_users_departmenets foreign key ( departmentid )
		references departments ( departmentid )
go


--test data
insert into departments ( department, manager_userid ) values ( 'Research', null )
insert into users ( username, password, departmentid ) values ( 'Joe Namath', '123456', null)
insert into users ( username, password, departmentid ) values ( 'Mickey Mantle', '123456', 1)
insert into users ( username, password, departmentid ) values ( 'Jack Ryan', '123456', 1)
update departments set manager_userid = 3 where departmentid = 1
insert into departments ( department, manager_userid ) values ( 'Personnel', 3 )
insert into users ( username, password, departmentid ) values ( 'Jane Doe', '123456', 2)
insert into users ( username, password, departmentid ) values ( 'Atticus Finch', '123456', 2)
go
