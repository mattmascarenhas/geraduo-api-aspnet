using geraduo.Entities.Shared;

namespace geraduo.Entities {
    public class Game: Entity {
        public Game(string title, string bannerUrl){
            Title = title;
            BannerUrl = bannerUrl;
        }
        public string Title { get; private set; }
        public string BannerUrl { get; private set; }

        public override string ToString() {
            return Title;
        }
    }
}
