-- MySQL initialization script for controle_aluno database

USE controle_aluno;

CREATE TABLE IF NOT EXISTS alunos (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(150) NOT NULL,
    email VARCHAR(150) NOT NULL UNIQUE,
    curso VARCHAR(100) NOT NULL,
    matricula VARCHAR(20) NOT NULL UNIQUE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS professores (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(150) NOT NULL,
    email VARCHAR(150) NOT NULL UNIQUE,
    materia VARCHAR(100) NOT NULL,
    ativo boolean NOT NULL DEFAULT TRUE
);

-- Insert sample data
INSERT IGNORE INTO alunos (nome, email, curso, matricula) VALUES
('Ana Silva', 'ana.silva@email.com', 'Engenharia de Software', '2024001'),
('Bruno Costa', 'bruno.costa@email.com', 'Ciência da Computação', '2024002'),
('Carla Santos', 'carla.santos@email.com', 'Sistemas de Informação', '2024003'),
('Diego Oliveira', 'diego.oliveira@email.com', 'Análise e Desenvolvimento de Sistemas', '2024004'),
('Fernanda Lima', 'fernanda.lima@email.com', 'Engenharia de Software', '2024005');