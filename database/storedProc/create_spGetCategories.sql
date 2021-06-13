USE MMTShop;
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'spGetCategories')  
DROP PROCEDURE spGetCategories  
go  
CREATE PROC spGetCategories
	AS
	BEGIN
		SELECT C.Id, C.Name
		FROM Categories C
	END; 
go