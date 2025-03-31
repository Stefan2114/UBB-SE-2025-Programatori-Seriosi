	USE Notifications;
	-- Appointment Table
	CREATE TABLE Appointments(
		Id int primary key identity (1,1),
		FOREIGN key (DoctorId) references Doctors(Id),
		FOREIGN key (PatientId) references Patients(Id),
		AppointmentDateTime datetime not null,
		Location NVARCHAR(255)

	);

	CREATE TABLE AppointmentNotification(
		Id int primary key identity(1,1),
		FOREIGN key (AppointmentId) references Appointments(Id)
	);

	
	-- Notifications Table
	CREATE TABLE Notifications (
		Id INT PRIMARY KEY IDENTITY(1,1),
		UserId INT NOT NULL,
		DeliveryDateTime DATETIME NOT NULL,
		Message NVARCHAR(255) NOT NULL,
		FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
	);

	--Drug Table
	create table Drugs (
		Id INT PRIMARY KEY IDENTITY (1,1),
		Administration NVARCHAR(255) NOT NULL, 
		Name NVARCHAR(255) NOT NULL
	);

CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(100) NOT NULL,
    Role NVARCHAR(50) NOT NULL
);

create table Chats (
	Id int primary key identity (1,1),
	Sender int FOREIGN KEY references Users(Id),
	Receiver int foreign key references Users(Id)
);

CREATE TABLE Message (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Content NVARCHAR(MAX) NOT NULL,
    UserId INT NOT NULL,
    ChatId INT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE,
    FOREIGN KEY (ChatId) REFERENCES Chats(Id) ON DELETE CASCADE
);


CREATE TABLE Department (
    DepartmentId INT PRIMARY KEY IDENTITY(1,1),
    DepartmentName NVARCHAR(255) NOT NULL
);

CREATE TABLE Doctor (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
);

CREATE TABLE Patients (
    Id INT PRIMARY KEY IDENTITY(1,1)
);

CREATE TABLE Drug (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(255) NOT NULL,
    Administration NVARCHAR(255) NOT NULL
);

CREATE TABLE Equipment(
	Id INT PRIMARY KEY IDENTITY (1,1)
);

CREATE TABLE Hospitalization (
    Id INT PRIMARY KEY IDENTITY(1,1),
    RoomId INT NOT NULL,
    PatientId INT NOT NULL,
    StartDateTime DATETIME NOT NULL,
    EndDateTime DATETIME NOT NULL,
    FOREIGN KEY (RoomId) REFERENCES Rooms(Id) ON DELETE CASCADE,
    FOREIGN KEY (PatientId) REFERENCES Patients(Id) ON DELETE CASCADE
);


CREATE TABLE MedicalRecord (
    Id INT PRIMARY KEY IDENTITY(1,1),
    DoctorId INT NOT NULL,
    PatientId INT NOT NULL,
    MedicalRecordDateTime DATETIME NOT NULL,
    FOREIGN KEY (DoctorId) REFERENCES Doctor(Id) ON DELETE CASCADE,
    FOREIGN KEY (PatientId) REFERENCES Patients(Id) ON DELETE CASCADE
);
CREATE TABLE Review (
    Id INT PRIMARY KEY IDENTITY(1,1),
    MedicalRecordId INT NOT NULL,
    Message NVARCHAR(MAX) NOT NULL,
    NrStars INT NOT NULL CHECK (NrStars BETWEEN 1 AND 5),
    FOREIGN KEY (MedicalRecordId) REFERENCES MedicalRecord(Id) ON DELETE CASCADE
);
CREATE TABLE ShiftType (
    Id INT PRIMARY KEY IDENTITY(1,1),
    ShiftTypeStartTime TIME NOT NULL,
    ShiftTypeEndTime TIME NOT NULL
);


CREATE TABLE Schedule (
    ScheduleId INT PRIMARY KEY IDENTITY(1,1),
    ScheduleWorkDay DATE NOT NULL,
    DoctorId INT NOT NULL,
    ShiftTypeId INT NOT NULL,
    FOREIGN KEY (DoctorId) REFERENCES Doctor(Id) ON DELETE CASCADE,
    FOREIGN KEY (ShiftTypeId) REFERENCES ShiftType(Id) ON DELETE CASCADE
);
CREATE TABLE Treatment (
    Id INT PRIMARY KEY IDENTITY(1,1),
    MedicalRecordId INT NOT NULL,
    FOREIGN KEY (MedicalRecordId) REFERENCES MedicalRecord(Id) ON DELETE CASCADE
);

CREATE TABLE TreatmentDrug (
    Id INT PRIMARY KEY IDENTITY(1,1),
    TreatmentId INT NOT NULL,
    DrugId INT NOT NULL,
    Quantity FLOAT NOT NULL,
    StartTime TIME NOT NULL,
    EndTime TIME NOT NULL,
    StartDate DATE NOT NULL,
    NrDays INT NOT NULL,
    FOREIGN KEY (TreatmentId) REFERENCES Treatment(Id) ON DELETE CASCADE,
    FOREIGN KEY (DrugId) REFERENCES Drug(Id) ON DELETE CASCADE
);

