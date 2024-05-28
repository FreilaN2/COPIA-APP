SELECT CONVERT(varchar,DECRYPTBYPASSPHRASE('12345', Contra)) AS Contra,
       TipoUsuario,
       FechaV
FROM Usuarios
WHERE CodUsua = 'SU'