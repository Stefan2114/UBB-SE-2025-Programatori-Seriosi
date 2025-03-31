CREATE TABLE IF NOT EXISTS Appointments (
    id INT PRIMARY KEY,
    doctorId INT,
    patientId INT,
    appointmentDate DATETIME,
    location VARCHAR(255),
    FOREIGN KEY (doctorId) REFERENCES Doctors(user_id),
    FOREIGN KEY (patientId) REFERENCES Patients(user_id)
);

-- Insert Appointments
INSERT INTO Appointments (id, doctorId, patientId, appointmentDate, location) VALUES
(1, 3, 5, '2025-04-01 10:00:00', 'Room 101'),
(2, 4, 6, '2025-04-02 11:30:00', 'Room 202'),