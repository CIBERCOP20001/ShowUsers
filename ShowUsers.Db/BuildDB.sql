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

IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users_AntonioAvendano]') AND type in (N'U'))
BEGIN
		
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

END
