CREATE PROCEDURE SP_Usuarios_Index
AS
BEGIN
SELECT * FROM USUARIO
END	
go
ALTER PROCEDURE SP_Usuarios_Create
(
@NombreCompleto nvarchar(255),
@NombreUsuario nvarchar(255),
@Clave nvarchar(255),
@IdPermisos int,
@Estado bit
)
AS
BEGIN
INSERT INTO USUARIO
(
NombreCompleto,
NombreUsuario,
Clave,
IdPermisos,
Estado
)
VALUES
(
@NombreCompleto,
@NombreUsuario,
@Clave,
@IdPermisos,
@Estado
)
Select SCOPE_IDENTITY() /*Manda el id que se esta insertando en la tabla*/
END
go
CREATE PROCEDURE SP_Usuarios_Read
AS
BEGIN
SELECT * FROM USUARIO
END	
go
Alter PROCEDURE SP_Usuarios_Update
(
@IdUsuario int,
@NombreCompleto nvarchar(255),
@NombreUsuario nvarchar(255),
@Clave nvarchar(255),
@IdPermisos int,
@Estado bit
)
AS
BEGIN
update USUARIO set	NombreCompleto = @NombreCompleto,  
					NombreUsuario= @NombreCompleto, 
					Clave = @Clave, 
					IdPermisos=@IdPermisos,
					Estado = @Estado
			where	IdUsuario = @IdUsuario
			select 1
END
go
Alter PROCEDURE SP_Usuarios_Delete
(
@IdUsuario int
)
AS
BEGIN
Delete FROM USUARIO WHERE IdUsuario = @IdUsuario
select 1
END	
go

CREATE PROCEDURE SP_Login_
@NombreUsuario nvarchar(255),
@Clave nvarchar(255)
AS
BEGIN
Select count(*) from USUARIO where NombreUsuario = @NombreUsuario and Clave = @Clave
END