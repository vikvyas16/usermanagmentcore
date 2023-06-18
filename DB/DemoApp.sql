-------------------------------------------------------------------------------
-- Author       
-- Created      18/06
-- Purpose      Create user and store details
-- Copyright © 2023, Company Name, All Rights Reserved
-------------------------------------------------------------------------------
CREATE TABLE [dbo].[Users](
    [UserId] [int] IDENTITY(1,1) NOT NULL,
    [FirstName] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
    [Email] [nvarchar](255) NOT NULL,
    [Address] [nvarchar](max) NULL,
    [Encryptedpassword] [nvarchar](max) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
    [UserId] ASC
)
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

-------------------------------------------------------------------------------
-- Author       
-- Created      18/06
-- Purpose      Get all users
-- Copyright © 2023, Company Name, All Rights Reserved
-------------------------------------------------------------------------------
CREATE PROCEDURE usp_getalluser 
AS
    SET NOCOUNT ON;  
    SELECT * FROM [Users] 
GO

-------------------------------------------------------------------------------
-- Author       
-- Created      18/06
-- Purpose      Get all users
-- Copyright © 2023, Company Name, All Rights Reserved
-------------------------------------------------------------------------------
-- EXEC usp_getuserbyuserid 1
CREATE PROCEDURE usp_getuserbyuserid
	@userId int
AS
    SET NOCOUNT ON;  
    SELECT * FROM [Users] Where UserId = @userId
GO

-------------------------------------------------------------------------------
-- Author       
-- Created      18/06
-- Purpose      Manage users
-- Copyright © 2023, Company Name, All Rights Reserved
-------------------------------------------------------------------------------
-- EXEC usp_manageusers 
CREATE PROCEDURE usp_manageusers
	@id int,
	@firstname nvarchar(100),
	@lastname nvarchar(100),
	@email nvarchar(255),
	@address nvarchar(max) = NULL,
	@encryptedpassword nvarchar(max)
AS
    SET NOCOUNT ON;  
    if (@id IS NOT NULL AND @id <> 0)
	BEGIN
		UPDATE [Users]
		SET 
			firstname = @firstname,
			lastname = @lastname,
			address = @address
		WHERE UserId = @id
	END
	ELSE
	BEGIN
		insert into [users] (FirstName,LastName,Email,Address,Encryptedpassword)
		VALUES (@firstname,@lastname,@email,@address,@encryptedpassword)
		
		SET @id = @@IDENTITY  
	END
	SELECT * FROM [Users] Where UserId = @id  
GO

-------------------------------------------------------------------------------
-- Author       
-- Created      18/06
-- Purpose      ToDo
-- Copyright © 2023, Company Name, All Rights Reserved
-------------------------------------------------------------------------------
CREATE TABLE [dbo].[ToDo](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [ToDoItems] [nvarchar](max) NOT NULL,
	[AssignDueDates] [nvarchar](max) NULL,
    [Comments] [nvarchar](max) NULL,
	[IsCompleted] [bit] DEFAULT(0),
	[UserId] [int] NOT NULL
 CONSTRAINT [PK_ToDo] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

-------------------------------------------------------------------------------
-- Author       
-- Created      18/06
-- Purpose      Get ToDo Items
-- Copyright © 2023, Company Name, All Rights Reserved
-------------------------------------------------------------------------------
CREATE PROCEDURE usp_getalltodoItem
AS
    SET NOCOUNT ON;  
    SELECT * FROM [ToDo] 
GO

-------------------------------------------------------------------------------
-- Author       
-- Created      18/06
-- Purpose      Get all users
-- Copyright © 2023, Company Name, All Rights Reserved
-------------------------------------------------------------------------------
-- EXEC usp_getuserbyuserid 1
CREATE PROCEDURE usp_gettodoitemsbytodoid
	@todoId int
AS
    SET NOCOUNT ON;  
    SELECT * FROM [ToDo] Where Id = @todoId
GO

-------------------------------------------------------------------------------
-- Author       
-- Created      18/06
-- Purpose      Manage ToDo
-- Copyright © 2023, Company Name, All Rights Reserved
-------------------------------------------------------------------------------
-- EXEC usp_manageusers 
CREATE PROCEDURE usp_managetodo
	@id int,
	@todoitems nvarchar(max),
	@assignduedates dateTime NULL,
	@comments nvarchar(max) = NULL,
	@iscompleted bit = 0,
	@userid int
AS
    SET NOCOUNT ON;  
    if (@id IS NOT NULL AND @id <> 0)
	BEGIN
		UPDATE [ToDo]
		SET 
			ToDoItems = @todoitems,
			AssignDueDates = @assignduedates,
			Comments = @comments,
			IsCompleted = @iscompleted
		WHERE Id = @id
	END
	ELSE
	BEGIN
		insert into [ToDo] (ToDoItems,AssignDueDates,Comments,IsCompleted,UserId)
		VALUES (@todoitems,@assignduedates,@comments,@iscompleted,@userid)

		SET @id = @@IDENTITY  
	END
	SELECT * FROM [ToDo] Where Id = @id
GO