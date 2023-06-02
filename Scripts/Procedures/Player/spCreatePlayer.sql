CREATE PROCEDURE spCreatePlayer
    @Id UNIQUEIDENTIFIER,
    @Name VARCHAR(90),
    @NickName VARCHAR(60),
    @Email VARCHAR(160),
    @Password VARCHAR(60),
    @Discord VARCHAR(50),
    @createdAt DATETIME
AS
    INSERT INTO [Players] (
        [Id], 
        [Name],
        [NickName], 
        [Email], 
        [Password],
        [Discord], 
        [createdAt]

    ) VALUES (
        @Id,
        @Name,
        @NickName,
        @Email,
        @Password,
        @Discord,
        @createdAt
    )

