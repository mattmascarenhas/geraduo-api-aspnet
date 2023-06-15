CREATE PROCEDURE spCreateAd
    @Id UNIQUEIDENTIFIER,
    @PlayerId UNIQUEIDENTIFIER,
    @GameId UNIQUEIDENTIFIER,
    @PlayerName VARCHAR(150),
    @WeekDays VARCHAR(20),
    @HourStart INTEGER,
    @HourEnd INTEGER,
    @CreatedAt DATETIME,
    @UpdatedAt DATETIME
AS
    INSERT INTO [Ads] (
    [Id],
    [PlayerId],
    [GameId],
    [PlayerName],
    [WeekDays],
    [HourStart],
    [HourEnd],
    [CreatedAt],
    [UpdatedAt]
    
) VALUES (
    @Id,
    @PlayerId,
    @GameId,
    @PlayerName,
    @WeekDays,
    @HourStart,
    @HourEnd,
    @CreatedAt,
    @UpdatedAt
)
