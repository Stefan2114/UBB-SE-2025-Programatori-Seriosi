CREATE TABLE messages(
	message_id int primary key,
	content varchar(100),
	userId int,
	chatId int
)

INSERT INTO messages (message_id, content, userId, chatId) VALUES (1, 'Hello, how are you?', 101, 1);
INSERT INTO messages (message_id, content, userId, chatId) VALUES (2, 'I am doing great, thanks!', 102, 1);
INSERT INTO messages (message_id, content, userId, chatId) VALUES (3, 'What are your plans for the weekend?', 101, 1);
INSERT INTO messages (message_id, content, userId, chatId) VALUES (4, 'I am thinking of going hiking.', 102, 1);
INSERT INTO messages (message_id, content, userId, chatId) VALUES (5, 'That sounds fun! Enjoy!', 101, 1);