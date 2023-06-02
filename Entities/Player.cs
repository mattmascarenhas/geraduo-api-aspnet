using geraduo.Entities.Shared;

namespace geraduo.Entities {
    public class Player: Entity {
        public Player(string name, string nickname, string email, string password, string discord){
            this.Name = name;
            this.Nickname = nickname;
            this.Email = email;
            this.Password = password;
            this.Discord = discord;
        }
        public string Name { get; private set; }
        public string Nickname { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string Discord { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UptadedAt { get; private set; }

        public override string ToString() {
            return $"{Name}({Nickname})";
        }
    }
}
