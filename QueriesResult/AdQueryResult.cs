namespace geraduo.QueriesResult {
    public class AdQueryResult {
        public Guid Id { get; set; }
        public Guid PlayerId { get; set; }
        public Guid GameId { get; set; }
        public string PlayerName { get; set; }
        public string WeekDays { get; set; }
        public int HourStart { get; set; }
        public int HourEnd { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UptadedAt { get; set; }
    }
}
