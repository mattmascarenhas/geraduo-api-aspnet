CREATE PROCEDURE spUpdatePlayer
    @Id UNIQUEIDENTIFIER,
    @Name VARCHAR(90),
    @NickName VARCHAR(60),
    @Email VARCHAR(160),
    @Password VARCHAR(60),
    @Discord VARCHAR(50)
AS
BEGIN
    UPDATE [Players]
    SET 
        [Name] = @Name,
        [NickName] = @NickName,
        [Email]= @Email,
        [Password]= @Password,
        [Discord]= @Discord
    WHERE
        [Id] = @Id
END
