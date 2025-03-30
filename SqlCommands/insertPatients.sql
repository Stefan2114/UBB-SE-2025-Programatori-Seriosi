CREATE TABLE IF NOT EXISTS Patients (
    user_id INT,
    FOREIGN KEY (user_id) REFERENCES users(id)
);

-- Insert Patients
INSERT INTO Patients (user_id) VALUES
(5),
(6);