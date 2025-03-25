CREATE TABLE IF NOT EXISTS Patients (
    id INT PRIMARY KEY,
    name VARCHAR(100),
    address VARCHAR(255)
);

-- Insert Patients
INSERT INTO Patients (id, name, address) VALUES
(1, 'John Doe', '123 Main St'),
(2, 'Jane Smith', '456 Oak St'),
(3, 'Michael Brown', '789 Pine St'),
(4, 'Emily White', '101 Maple Ave'),
(5, 'David Black', '202 Birch Blvd')
ON CONFLICT (id) DO NOTHING;