CREATE DATABASE `GameTest` /*!40100 COLLATE 'utf8mb4_0900_ai_ci' */;

CREATE TABLE `players` (
	`player_id` INT NOT NULL AUTO_INCREMENT,
	`username` VARCHAR(50) NOT NULL DEFAULT '0',
	`email` VARCHAR(50) NOT NULL DEFAULT '0',
	`password_hash` VARCHAR(255) NOT NULL DEFAULT '0',
	`create_at` TIMESTAMP NOT NULL,
	`last_login` TIMESTAMP NULL DEFAULT NULL,
	PRIMARY KEY (`player_id`) USING BTREE,
	UNIQUE INDEX `username` (`username`) USING BTREE,
	UNIQUE INDEX `email` (`email`)
)

INSERT INTO players(username, email, password_hash) VALUES
('hero423', 'hero423@gmail.com' , 'hashed password4'),
('hero523', 'hero53@gmail.com' , 'hashed password5'),
('hero623', 'hero623@gmail.com' , 'hashed password6'),
('hero723', 'hero723@gmail.com' , 'hashed password7')

SELECT * FROM players
SELECT username, last_login FROM players

-- 5.특정 플레이어 정보 업데이트
UPDATE players SET last_login = CURRENT_TIMESTAMP WHERE username = 'hero123'

-- 6. 조건에 맞는 플레이어 검색
SELECT username, email FROM players WHERE username LIKE '%hero%'

--7. 플레이어 삭제
DELETE FROM players WHERE username = 'hero423'

-- 8. 플레이어 테이블에 새 열 추가
ALTER TABLE players ADD COLUMN `level` INT DEFAULT 1

-- 9. 모든 플레이어의 레벨을 1 증가
UPDATE players SET `level` = `level` + 1

-- 10. 가장 level이 높은 플레이어가져오기
SELECT username, `level` FROM players ORDER BY `level` DESC LIMIT 1