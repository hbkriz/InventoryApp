USE MMTShop;
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'spGetFeaturedProducts')  
DROP PROCEDURE spGetFeaturedProducts  
go  
CREATE PROC spGetFeaturedProducts
AS
BEGIN
	SELECT P.Id, P.Sku, P.Name, P.Description, P.Price
	FROM Products P
		INNER JOIN Categories C
			 ON P.CategoryId = C.Id
	WHERE
		C.IsFeatured = 1
		OR P.IsFeatured = 1
END;
go