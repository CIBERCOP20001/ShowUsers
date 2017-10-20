/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

/****** Object:  Table [dbo].[Users_AntonioAvendano]     ******/

/****** Object:  Table [dbo].[Users_AntonioAvendano]    Script Date: 20/10/2017 05:58:41 p.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users_AntonioAvendano](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](150) NOT NULL,
	[Gender] [nvarchar](1) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Users_AntonioAvendano] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spGetAppUsersList]
	-- Add the parameters for the stored procedure here
	@DisplayLength INT = 10,
	@DisplayStart INT = 0,
	@SortOrder NVARCHAR(4) = 'ASC',
	@SortCol INT = 1
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @SortFull NVARCHAR(30)
	SET @SortFull = CAST(@SortCol AS NVARCHAR(1)) + ' ' + UPPER(@SortOrder) 
    -- Insert statements for procedure here
	SELECT 
		Id,
		UserName,
		Password,
		Email,
		Gender,
		CASE WHEN Active = 1 THEN 'True' ELSE 'False' END Active
	FROM [dbo].[Users_AntonioAvendano]
	ORDER BY
		CASE WHEN @SortFull = '1 ASC' THEN UserName END ASC,
		CASE WHEN @SortFull = '1 DESC' THEN UserName END DESC,
		CASE WHEN @SortFull = '2 ASC' THEN Password  END ASC,
		CASE WHEN @SortFull = '2 DESC' THEN Password  END DESC,
		CASE WHEN @SortFull = '3 ASC' THEN Email  END ASC,
		CASE WHEN @SortFull = '3 DESC' THEN Email  END DESC,
		CASE WHEN @SortFull = '4 ASC' THEN Gender  END ASC,
		CASE WHEN @SortFull = '4 DESC' THEN Gender  END DESC,
		CASE WHEN @SortFull = '5 ASC' THEN Active  END ASC,
		CASE WHEN @SortFull = '5 DESC' THEN Active  END DESC
	OFFSET @DisplayStart ROWS
	FETCH NEXT @DisplayLength ROWS ONLY;

	SELECT COUNT(1) AppUsersTotal
	FROM [dbo].[Users_AntonioAvendano]
	
END

GO



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spInsertNewUser] 
	-- Add the parameters for the stored procedure here
    @Email NVARCHAR(150),
    @Gender NVARCHAR(1),
    @Password NVARCHAR(50),
    @UserName NVARCHAR(50),
	@Active  BIT =1
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF NOT EXISTS 
      (
            SELECT Id
			FROM [dbo].[Users_AntonioAvendano]
			WHERE UserName = @UserName 
			AND Password = @Password
			AND Email = @Email
      )
		BEGIN
			INSERT INTO [dbo].[Users_AntonioAvendano]
			VALUES (@UserName, @Password, @Email, @Gender, @Active)

			SELECT @@ROWCOUNT
		END
	ELSE
		BEGIN
			SELECT 0
		END
END

GO



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spUpdateUserStatus]
	-- Add the parameters for the stored procedure here
	@Email NVARCHAR(50),
    @Status BIT = 0 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [dbo].[Users_AntonioAvendano]
	SET [Active] = @Status
	WHERE Email = @Email

	SELECT @@ROWCOUNT 
END

GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spValidateLogin]
	-- Add the parameters for the stored procedure here
	@LoginUser NVARCHAR(50),
	@LoginPassword NVARCHAR(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT
      CASE WHEN EXISTS 
      (
            SELECT Id
			FROM [dbo].[Users_AntonioAvendano]
			WHERE UserName = @LoginUser 
			AND Password = @LoginPassword
			AND Active = 1
      )
      THEN 'TRUE'
      ELSE 'FALSE'
   END

END

GO


