-- Crear base de datos
CREATE DATABASE ColegioDB;
GO

USE ColegioDB;
GO

-- Tabla Alumnos
CREATE TABLE Alumnos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NULL
);
GO

-- Tabla Materias
CREATE TABLE Materias (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL
);
GO

-- Tabla Expedientes (relación)
CREATE TABLE Expedientes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    AlumnoId INT NOT NULL,
    MateriaId INT NOT NULL,
    Nota DECIMAL(5,2) NOT NULL,
    Observacion NVARCHAR(255) NULL,

    CONSTRAINT FK_Expedientes_Alumnos
        FOREIGN KEY (AlumnoId) REFERENCES Alumnos(Id)
        ON DELETE CASCADE,

    CONSTRAINT FK_Expedientes_Materias
        FOREIGN KEY (MateriaId) REFERENCES Materias(Id)
        ON DELETE CASCADE
);
GO

-- Datos de prueba (opcional pero recomendado)
INSERT INTO Alumnos (Nombre, Apellido) VALUES 
('Juan', 'Perez'),
('Maria', 'Lopez');

INSERT INTO Materias (Nombre) VALUES 
('Matemática'),
('Historia');

INSERT INTO Expedientes (AlumnoId, MateriaId, Nota, Observacion) VALUES 
(1, 1, 8.5, 'Buen desempeño'),
(1, 2, 7.0, 'Debe mejorar'),
(2, 1, 9.2, 'Excelente');
GO
