/*GetAll*/
SELECT *
FROM [DAI-Pizzas].dbo.Pizzas

/*GetByID*/
SELECT *
FROM [DAI-Pizzas].dbo.Pizzas
WHERE Pizzas.ID = 3

/*Create*/
INSERT [DAI-Pizzas].dbo.[Pizzas]
    ([Nombre], [LibreGluten], [Importe], [Descripcion])
VALUES
    (N'Faina', 0, 160, N'Harina, aceite, sal y pimienta... y despues te compras una faina en el COTO.')
SELECT CAST( SCOPE_IDENTITY () AS INT)

/*Delete*/
DELETE FROM [DAI-Pizzas].dbo.Pizzas
WHERE Pizzas.Id=11
GO

/*Update*/
UPDATE [DAI-Pizzas].dbo.Pizzas
SET
    Nombre = 'Pizza de Dulce de leche',
    LibreGluten = 0,
    Importe = 840,
    Descripcion = 'La pizza más golosa del mercado'
WHERE Id = 16


/*GetUsuario*/
SELECT * FROM [DAI-Pizzas].dbo.Usuarios WHERE Usuarios.UserName = 'CatAlt'

/*GetUsuarioByToken*/
SELECT * FROM [DAI-Pizzas].dbo.Usuarios WHERE Usuarios.Token = ''

/*RefreshToken*/
UPDATE [DAI-Pizzas].dbo.Usuarios
SET
    Token = '0f8fad5b-d9cb-469f-a165-70867728950e', /*aca se puede usar NEWID()*/
    TokenExpirationDate = DateAdd(MINUTE, 15, GetDate())
WHERE Usuarios.UserName = 'JoaAbr'

/*IsValidToken*/
SELECT
    Token,
    TokenExpirationDate
FROM [DAI-Pizzas].[dbo].[Usuarios]
WHERE TOKEN = @token
