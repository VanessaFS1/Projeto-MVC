CREATE TABLE usuario (
    cod INT PRIMARY KEY NOT NULL,
    nome VARCHAR (50),
    sobrenome VARCHAR (50),
	cpf INT,
	email VARCHAR (30),
	loginUsuario VARCHAR (30),
	senha VARCHAR (15),
	stAtivo BIT,
)
GO