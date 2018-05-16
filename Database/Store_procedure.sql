
use QLTiemNetDB;

go

CREATE PROCEDURE  login_admin
	@user varchar(256),
	@password varchar(256),
	@result int output
AS
Begin
	 SELECT @result= count(*) FROM Users WHERE UserName = @user AND Password = @password AND Role = 'admin'
End

