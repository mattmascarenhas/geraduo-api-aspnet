using geraduo.Entities.Shared;

namespace geraduo.Entities {
    public class Ad : Entity {
        public Ad(Guid playerId, Guid gameId, string playerName, string weekDays, int hourStart, int hourEnd) {
            // Define as propriedades correspondentes
            PlayerId = playerId;
            GameId = gameId;
            PlayerName = playerName;
            WeekDays = weekDays;
            HourStart = hourStart;
            HourEnd = hourEnd;

        }

        public Guid PlayerId { get; private set; }
        public Guid GameId { get; private set; }
        public string PlayerName { get; private set; }
        public string WeekDays { get; private set; }
        public int HourStart { get; private set; }
        public int HourEnd { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public override string ToString() {
            return $"id: {Id}, playerId: {PlayerId}, gameId:{GameId}, playerName: {PlayerName}," +
                $" weekDays: {WeekDays}, hourStart:{HourStart}, hourEnd: {HourEnd}";
        }
    }

}


