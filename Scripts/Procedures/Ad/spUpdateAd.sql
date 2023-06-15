CREATE PROCEDURE spUpdateAd
    @Id UNIQUEIDENTIFIER,
    @PlayerId UNIQUEIDENTIFIER,
    @GameId UNIQUEIDENTIFIER,
    @PlayerName VARCHAR(150),
    @WeekDays VARCHAR(20),
    @HourStart INTEGER,
    @HourEnd INTEGER
AS
BEGIN
    UPDATE [Ads]
    SET 
    [PlayerId] = @PlayerId,
    [GameId] = @GameId,
    [PlayerName] = @PlayerName,
    [WeekDays] = @WeekDays,
    [HourStart] = @HourStart,
    [HourEnd] = @HourEnd
    WHERE
        [Id] = @Id
END
