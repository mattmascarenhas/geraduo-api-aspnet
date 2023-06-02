using geraduo.Entities.Shared;

namespace geraduo.Entities {
    public class Ad : Entity {
        public Ad(Guid playerId, Guid gameId, string weekDays, int hourStart, int hourEnd) {
            this.PlayerId = playerId;
            this.GameId = gameId;
            this.WeekDays = weekDays;
            this.HourStart = hourStart;
            this.HourEnd = hourEnd;
        }
        public Guid PlayerId { get; private set; }
        public Guid GameId { get; private set; }
        public string WeekDays { get; private set; }
        public int HourStart { get; private set; }
        public int HourEnd { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UptadedAt { get; private set; }
    }

}
