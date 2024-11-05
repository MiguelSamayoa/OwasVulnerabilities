-- Drop database OrderManagement;

-- Crear la base de datos
CREATE DATABASE IF NOT EXISTS OrderManagement;
USE OrderManagement;

-- Tabla de Usuarios
CREATE TABLE Users (
    UserID INT AUTO_INCREMENT PRIMARY KEY,
    Username VARCHAR(50) NOT NULL UNIQUE,
    Password VARCHAR(255) NOT NULL,  -- Almacenar contraseñas en texo plano
    Role INT unsigned NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE
);
-- ENUM('Admin', 'Customer') 
-- Tabla de Productos
CREATE TABLE Products (
    ProductID INT AUTO_INCREMENT PRIMARY KEY,
    ProductName VARCHAR(100) NOT NULL,
    Description TEXT,
    Price DECIMAL(10, 2) NOT NULL,
    Stock INT NOT NULL
);

-- Tabla de Pedidos
CREATE TABLE Orders (
    OrderID INT AUTO_INCREMENT PRIMARY KEY,
    OrderDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    UserID INT,
    Status INT unsigned,
    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE SET NULL
);
-- ENUM('Pending', 'Shipped', 'Completed', 'Cancelled') DEFAULT 'Pending'

-- Tabla de Detalles de Pedido
CREATE TABLE OrderDetails (
    OrderDetailID INT AUTO_INCREMENT PRIMARY KEY,
    OrderID INT,
    ProductID INT,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID) ON DELETE CASCADE,
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID) ON DELETE SET NULL
);

-- Insertar registros en la tabla Users (sin usuarios administradores)
INSERT INTO Users (Username, Password, Role, Email) VALUES
    ('john_doe', '123456', 2, 'john.doe@example.com'),     -- Usuario cliente
    ('jane_smith', 'password789', 2, 'jane.smith@example.com'), -- Usuario cliente
    ('mark_taylor', 'mypassword', 2, 'mark.taylor@example.com'); -- Usuario cliente

-- Insertar registros adicionales en la tabla Products
INSERT INTO Products (ProductName, Description, Price, Stock) VALUES
    ('Tablet', 'Tablet de 10 pulgadas con 64GB de almacenamiento', 299.99, 30),
    ('Smartwatch', 'Reloj inteligente con monitor de frecuencia cardíaca', 199.99, 15),
    ('Teclado Mecánico', 'Teclado mecánico RGB para gaming', 89.99, 40),
    ('Monitor 4K', 'Monitor Ultra HD de 27 pulgadas', 399.99, 12),
    ('Mouse Inalámbrico', 'Mouse inalámbrico ergonómico', 29.99, 100);

-- Insertar registros en la tabla Orders
INSERT INTO Orders (UserID, Status) VALUES
    (1, 1), -- Pedido del usuario 'john_doe', estado 'Pending'
    (2, 2), -- Pedido del usuario 'jane_smith', estado 'Shipped'
    (3, 3); -- Pedido del usuario 'mark_taylor', estado 'Completed'

-- Insertar registros en la tabla OrderDetails
INSERT INTO OrderDetails (OrderID, ProductID, Quantity, UnitPrice) VALUES
    (1, 1, 1, 299.99), -- Pedido 1: Tablet
    (1, 3, 2, 89.99), -- Pedido 1: Teclado Mecánico
    (2, 2, 1, 199.99), -- Pedido 2: Smartwatch
    (3, 4, 1, 399.99), -- Pedido 3: Monitor 4K
    (3, 5, 3, 29.99); -- Pedido 3: Mouse Inalámbrico (3 unidades)

/*
select 
	o.*,
    u.Username
from Orders o
left join users u on u.UserID = o.UserId
where o.userid in (1, 2);
*/