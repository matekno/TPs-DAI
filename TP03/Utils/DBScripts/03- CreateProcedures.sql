CREATE PROCEDURE GetAll
AS
    SELECT * FROM [DAI-Pizzas].dbo.Pizzas
GO;

CREATE PROCEDURE GetByID @id INT
AS
    SELECT *
    FROM [DAI-Pizzas].dbo.Pizzas
    WHERE Pizzas.ID = @id
GO;

CREATE PROCEDURE CreatePizza @nombre VARCHAR(150), @libreGluten BIT, @importe FLOAT, @descripcion VARCHAR(max)
AS
    INSERT [DAI-Pizzas].dbo.[Pizzas]
        ([Nombre], [LibreGluten], [Importe], [Descripcion])
    VALUES
        (@nombre, @libreGluten, @importe, @descripcion)
	SELECT CAST(SCOPE_IDENTITY() as int);
GO;

CREATE PROCEDURE DeletePizza @id INT
AS
    DELETE FROM [DAI-Pizzas].dbo.Pizzas
    WHERE Pizzas.Id=@id
GO;

CREATE PROCEDURE UpdatePizza @nombre VARCHAR(150), @libreGluten BIT, @importe FLOAT, @descripcion VARCHAR(max), @id INT
AS
    UPDATE [DAI-Pizzas].dbo.Pizzas
    SET
        Nombre = @nombre,
        LibreGluten = @libreGluten,
        Importe = @importe,
        Descripcion = @descripcion
    WHERE Id = @id
GO;