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

-------------------------------------------------------------

-------------------------------------------------------------------------------
-- Author       
-- Created      18/06
-- Purpose      Assign Todo
-- Copyright © 2023, Company Name, All Rights Reserved
-------------------------------------------------------------------------------
CREATE TABLE [dbo].[AssignToDo]
(
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [ToDoId] [int] NOT NULL,
	[AssignUserId] [int] NOT NULL,
	CONSTRAINT [PK_AssignToDo] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)
)
GO
-----------------------------------------------------------
-- EXEC usp_manageusers 
ALTER PROCEDURE usp_managetodo
	@id int,
	@todoitems nvarchar(max),
	@assignduedates dateTime NULL,
	@comments nvarchar(max) = NULL,
	@iscompleted bit = 0,
	@userid int,
	@priorityid int
AS
    SET NOCOUNT ON;  
    if (@id IS NOT NULL AND @id <> 0)
	BEGIN
		UPDATE [ToDo]
		SET 
			ToDoItems = @todoitems,
			AssignDueDates = @assignduedates,
			Comments = @comments,
			IsCompleted = @iscompleted,
			PriorityId = @priorityid
		WHERE Id = @id
	END
	ELSE
	BEGIN
		insert into [ToDo] (ToDoItems,AssignDueDates,Comments,IsCompleted,UserId,PriorityId)
		VALUES (@todoitems,@assignduedates,@comments,@iscompleted,@userid,@priorityid)

		SET @id = @@IDENTITY  
	END
	SELECT * FROM [ToDo] Where Id = @id
GO
------------------------------------------------
-------------------------------------------------------------------------------
-- Author       
-- Created      18/06
-- Purpose      Notification
-- Copyright © 2023, Company Name, All Rights Reserved
-------------------------------------------------------------------------------
CREATE TABLE [dbo].[Notification]
(
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [NotificationMessage] nvarchar(max) NULL,
	[UserId] [int] NOT NULL,
	[IsRead] [bit] NOT NULL,
	[CreatedDate] [DateTime] NOT NULL,
	CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)
)
GO

-------------------------------------------------------------------------------
-- Author       
-- Created      18/06
-- Purpose      Get Notification Users
-- Copyright © 2023, Company Name, All Rights Reserved
-------------------------------------------------------------------------------
-- EXEC usp_getuserbyuserid 1
CREATE PROCEDURE usp_getnotificationbyuserid
	@userId int
AS
    SET NOCOUNT ON;  
    SELECT * FROM [Notification] Where UserId = @userId and IsRead = 0
GO

-------------------------------------------------------------------------------
-- Author       
-- Created      18/06
-- Purpose      Get Dashboard Count By User
-- Copyright © 2023, Company Name, All Rights Reserved
-------------------------------------------------------------------------------
-- EXEC usp_getdashboardcountbyuserid 1
CREATE PROCEDURE usp_getdashboardcountbyuserid
	@userId int
AS
    SET NOCOUNT ON;
	
	DECLARE @CreatedTodoCount int = 0;
	DECLARE @AssignedTodoCount int = 0;
	DECLARE @CompletedTodoCount int = 0;
	-- Created Count
    SELECT @CreatedTodoCount = COUNT(1) from ToDo Where IsCompleted = 0 AND UserId = @userId

	-- Assigned Count
	SELECT @AssignedTodoCount = COUNT(1) 
	FROM ToDo TD 
	INNER JOIN AssignToDo AD ON TD.Id = AD.ToDoId 
	WHERE AD.AssignUserId = @userId AND TD.IsCompleted = 0

	-- Completed Count
	SELECT @CompletedTodoCount = COUNT(1) from ToDo Where IsCompleted = 1 AND UserId = @userId

	SELECT ISNULL(@CreatedTodoCount,0) AS CreatedTodoCount,  ISNULL(@AssignedTodoCount,0) AS AssignedTodoCount, ISNULL(@CompletedTodoCount,0) AS CompletedTodoCount
	
GO

-------------------------------------------------------------------------------
-- Author       
-- Created      18/06
-- Purpose      Read Notification Users
-- Copyright © 2023, Company Name, All Rights Reserved
-------------------------------------------------------------------------------
-- EXEC usp_readnotification 1
CREATE PROCEDURE usp_readnotification
	@notificationId int
AS
    SET NOCOUNT ON;  
    UPDATE [Notification] SET IsRead = 1 Where Id = @notificationId
GO


-------------------------------------------------------------------------------
-- Author       
-- Created      18/06
-- Purpose      Assign ToDo To Users
-- Copyright © 2023, Company Name, All Rights Reserved
-------------------------------------------------------------------------------
-- EXEC usp_assigntodotouser 1
CREATE PROCEDURE usp_assigntodotouser
	@toDoId int,
	@assigndUserId int
AS
    SET NOCOUNT ON;  
    INSERT INTO AssignToDo VALUES (@toDoId,@assigndUserId)

	INSERT INTO Notification VALUES ('Assign ToDo Item to User Successfully',@assigndUserId,0,GETDATE())
GO


-------------------------------------------------------------------------------
-- Author       
-- Created      18/06
-- Purpose      Assign ToDo To Users
-- Copyright © 2023, Company Name, All Rights Reserved
-------------------------------------------------------------------------------
-- EXEC usp_assigntodotouser 1
CREATE PROCEDURE usp_assigntodotouser
	@toDoId int,
	@assigndUserId int
AS
    SET NOCOUNT ON;  
    INSERT INTO AssignToDo VALUES (@toDoId,@assigndUserId)

	INSERT INTO Notification VALUES ('Assign ToDo Item to User Successfully',@assigndUserId,0,GETDATE())
GO

-------------------------------------------------------------------------------
-- Author       
-- Created      18/06
-- Purpose      Recurrent ToDo
-- Copyright © 2023, Company Name, All Rights Reserved
-------------------------------------------------------------------------------
-- EXEC usp_assigntodotouser 1
CREATE PROCEDURE usp_recurringtodoitem
	@toDoId int
AS
    SET NOCOUNT ON;  
    INSERT INTO ToDo
	SELECT ToDoItems,GETDATE(),Comments,0,UserId,PriorityId FROM ToDo WHERE Id = @toDoId
GO