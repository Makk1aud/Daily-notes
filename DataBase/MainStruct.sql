create table Gender
(
	gender_id int primary key Identity(1,1) not null,
	gender_title varchar(20) Not Null
);

create table NoteType
(
	note_type_id int primary key Identity(1,1) not null,
	type_title varchar(20) Not Null
);


create table Client
(
	client_id int primary key Identity(1,1) not null,
	login varchar(20) Not null,
	password varchar(30) not null,
	email varchar(30) Not null,
	gender_id int not null,
	foreign key (gender_id) references Gender(gender_id)
);

create table Note
(
	note_id int primary key Identity(1,1) not null,
	client_id int not null,
	note_type_id int not null,
	note_text varchar(max) null,
	edit_date DateTime not null,
	note_title varchar(30) Null
	foreign key (client_id) references Client(client_id),
	foreign key (note_type_id) references NoteType(note_type_id)

);