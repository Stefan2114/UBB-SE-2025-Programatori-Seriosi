CREATE TABLE IF NOT EXISTS Appointments (
    id INT PRIMARY KEY,
    doctorId INT,
    patientId INT,
    appointmentDate TIMESTAMP,
    location VARCHAR(255),
    FOREIGN KEY (doctorId) REFERENCES Doctors(id),
    FOREIGN KEY (patientId) REFERENCES Patients(id)
);

-- Insert Appointments
INSERT INTO Appointments (id, doctorId, patientId, appointmentDate, location) VALUES
(1, 1, 1, '2025-04-01 10:00:00', 'Room 101'),
(2, 2, 2, '2025-04-02 11:30:00', 'Room 202'),
(3, 3, 3, '2025-04-03 14:00:00', 'Room 303'),
(4, 4, 4, '2025-04-04 09:15:00', 'Room 404'),
(5, 5, 5, '2025-04-05 13:45:00', 'Room 505')
ON CONFLICT (id) DO NOTHING;