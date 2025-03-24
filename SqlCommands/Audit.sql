USE Team3;
GO

-- User Table
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(255) NOT NULL,
    Role NVARCHAR(100) NOT NULL
);

-- Department Table
CREATE TABLE Department (
    DepartmentId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(255) NOT NULL
);

-- Doctor Table
CREATE TABLE Doctor (
    DoctorId INT PRIMARY KEY IDENTITY(1,1),
    DepartmentId INT NOT NULL,
    FOREIGN KEY (DepartmentId) REFERENCES Department(DepartmentId)
);

-- Room Table
CREATE TABLE Room (
    RoomId INT PRIMARY KEY IDENTITY(1,1),
    Capacity INT NOT NULL,
    DepartmentId INT NOT NULL,
    FOREIGN KEY (DepartmentId) REFERENCES Department(DepartmentId)
);

-- Equipment Table
CREATE TABLE Equipment (
    EquipmentId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(255) NOT NULL,
    Model NVARCHAR(255) NOT NULL
);

-- Hospitalization Table
CREATE TABLE Hospitalization (
    HospitalizationId INT PRIMARY KEY IDENTITY(1,1),
    RoomId INT NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NULL,
    FOREIGN KEY (RoomId) REFERENCES Room(RoomId)
);

-- Medical Record Table
CREATE TABLE MedicalRecord (
    MedicalRecordId INT PRIMARY KEY IDENTITY(1,1),
    DoctorId INT NOT NULL,
    FOREIGN KEY (DoctorId) REFERENCES Doctor(DoctorId)
);

-- ShiftType Table 
CREATE TABLE ShiftType (
    ShiftTypeId INT PRIMARY KEY IDENTITY(1,1),
    StartTime TIME NOT NULL,
    EndTime TIME NOT NULL
);

-- Schedule Table
CREATE TABLE Schedule (
    ScheduleId INT PRIMARY KEY IDENTITY(1,1),
    WorkDay DATE NOT NULL,
    DoctorId INT NOT NULL,
    ShiftTypeId INT NOT NULL,
    FOREIGN KEY (DoctorId) REFERENCES Doctor(DoctorId),
    FOREIGN KEY (ShiftTypeId) REFERENCES ShiftType(ShiftTypeId)
);
