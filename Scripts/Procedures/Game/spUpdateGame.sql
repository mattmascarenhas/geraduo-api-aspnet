CREATE PROCEDURE spUpdateGame
    @Id UNIQUEIDENTIFIER,
    @Title VARCHAR(60),
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
