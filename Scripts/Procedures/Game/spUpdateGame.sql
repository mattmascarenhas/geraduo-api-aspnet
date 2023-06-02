CREATE PROCEDURE spUpdateGame
    @Id UNIQUEIDENTIFIER,
    @Title VARCHAR(40),
    @BannerUrl VARCHAR(255)
AS
BEGIN
    UPDATE [Games]
    SET 
        [Title] = @Title,
        [BannerUrl] = @BannerUrl
    WHERE
        [Id] = @Id
END
