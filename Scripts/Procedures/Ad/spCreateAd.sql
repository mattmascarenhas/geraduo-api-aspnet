CREATE PROCEDURE spCreateAd
    @Id UNIQUEIDENTIFIER,
    @PlayerId UNIQUEIDENTIFIER,
    @GameId UNIQUEIDENTIFIER,
    @WeekDays VARCHAR(20),
    @HourStart INTEGER,
    @HourEnd INTEGER,
    @CreatedAt DATETIME
AS
    INSERT INTO [Ads] (
    [Id],
    [PlayerId],
    [GameId],
    [WeekDays],
    [HourStart],
    [HourEnd],
    [CreatedAt] 
    
) VALUES (
    @Id,
    @PlayerId,
    @GameId,
    @WeekDays,
    @HourStart,
    @HourEnd,
    @CreatedAt
)

