---Step 1: Create database
 IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'MMTShop')
	BEGIN
		CREATE DATABASE [MMTShop]
	END
	GO
		USE [MMTShop];
	GO

---Step 2: Create tables
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id =   
   OBJECT_ID(N'[dbo].[Categories]') AND type in (N'U'))  
BEGIN  
   	CREATE TABLE Categories
	(
		Id INT NOT NULL CONSTRAINT PK_Categories PRIMARY KEY CLUSTERED,
		Name VARCHAR(255) NOT NULL,
		IsFeatured BIT NOT NULL
			CONSTRAINT DF_IsFeatured DEFAULT (0)
	)
	CREATE UNIQUE NONCLUSTERED INDEX UIDX_Name ON Categories (Name)
END  
GO  

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id =   
   OBJECT_ID(N'[dbo].[Products]') AND type in (N'U'))  
BEGIN  
   	CREATE TABLE Products
	(
		Id INT NOT NULL IDENTITY (1,1) 
			CONSTRAINT PK_Products PRIMARY KEY CLUSTERED,
		CategoryId INT NOT NULL
			CONSTRAINT FK_Products_Categories_CategoryId FOREIGN KEY REFERENCES Categories (Id),
		Name VARCHAR(255) NOT NULL,
		Price DECIMAL(17,2) NOT NULL
			CONSTRAINT DF_Products_Price DEFAULT (99999999999.99),
		Description VARCHAR(MAX) NOT NULL,
		IsFeatured BIT NOT NULL
			CONSTRAINT DF_Products_IsFeatured DEFAULT (0),
		Sku AS CAST(CategoryId AS VARCHAR(10)) + RIGHT('0000'+CAST(Id AS VARCHAR(4)),4)
	)

	CREATE NONCLUSTERED INDEX IDX_Products_CategoryId ON Products (CategoryId)
END  
GO
---Step 3: Insert Data into tables
IF EXISTS (SELECT * FROM sys.objects WHERE object_id =   
   OBJECT_ID(N'[dbo].[Categories]') AND type in (N'U'))  
BEGIN
	INSERT Categories (Id, Name, IsFeatured)
	VALUES
		(1, 'Home', 1),
		(2, 'Garden', 1),
		(3, 'Electronics', 1),
		(4, 'Fitness', 0),
		(5, 'Toys', 0)
END  
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id =   
   OBJECT_ID(N'[dbo].[Products]') AND type in (N'U'))  
BEGIN
	INSERT Products (CategoryId, Name, Description, Price, IsFeatured)
	VALUES
		(1, 'Home Product 0001', 'Home Product Description 0001', 100.10, 1),
		(2, 'Garden Product 0001', 'Garden Product Description 0001', 200.20, 1),
		(3, 'Electronics Product 0001', 'Electronics Product Description 0001', 300.30, 1),
		(4, 'Fitness Product 0001', 'Fitness Product Description 0001', 400.40, 0),
		(5, 'Toys Product 0001', 'Toys Product Description 0001', 500.50, 0)
END
GO