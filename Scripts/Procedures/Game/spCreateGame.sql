CREATE PROCEDURE spCreateGame
    @Id UNIQUEIDENTIFIER,
    @Title VARCHAR(40),
    @BannerUrl VARCHAR(255)
AS
    INSERT INTO [Games] (
        [Id], 
        [Title], 
        [BannerUrl] 

    ) VALUES (
        @Id,
        @Title,
        @BannerUrl
    )