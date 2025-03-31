CREATE TABLE IF NOT EXISTS Doctors (
    user_id INT,
    FOREIGN KEY (user_id) REFERENCES users(id)
);

-- Insert Doctors
INSERT INTO Doctors (user_id) VALUES
(3),
(4);
