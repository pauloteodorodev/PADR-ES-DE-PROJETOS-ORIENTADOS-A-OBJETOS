create database umc;
use umc;

CREATE TABLE `clientes` (
  `Id` char(36) NOT NULL DEFAULT (uuid()),
  `Nome` varchar(200) NOT NULL,
  `CPF` varchar(11) NOT NULL,
  `Email` varchar(100) NOT NULL,
  `SenhaSalt` varchar(100) NOT NULL,
  `Senha` varchar(100) NOT NULL,
  `Telefone` varchar(20) NOT NULL,
  `Ativo` tinyint(1) NOT NULL,
  `Endereco` varchar(500) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci