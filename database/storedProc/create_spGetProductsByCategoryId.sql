USE MMTShop;
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'spGetProductsByCategoryId')  
DROP PROCEDURE spGetProductsByCategoryId  
go  
CREATE PROC spGetProductsByCategoryId
	@CategoryId INT
AS
BEGIN
	SELECT P.Id, P.Sku, P.Name, P.Description, P.Price
	FROM Products P
	WHERE
		P.CategoryId = @CategoryId
END;
go