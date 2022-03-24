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

/*Delete*/
DELETE FROM [DAI-Pizzas].dbo.Pizzas
WHERE Pizzas.Id=11
GO
