CREATE TABLE IF NOT EXISTS Doctors (
    id INT PRIMARY KEY,
    name VARCHAR(100),
    specialty VARCHAR(100)
);

-- Insert Doctors
INSERT INTO Doctors (id, name, specialty) VALUES
(1, 'Dr. Smith', 'Cardiology'),
(2, 'Dr. Johnson', 'Neurology'),
(3, 'Dr. Williams', 'Orthopedics'),
(4, 'Dr. Martinez', 'Dermatology'),
(5, 'Dr. Lee', 'Pediatrics')
ON CONFLICT (id) DO NOTHING;