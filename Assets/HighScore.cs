namespace Winnitron {
    public class HighScore  {

        public string name;
        public int score;

        public HighScore(string name, int score) {
            this.name = name;
            this.score = score;
        }

        public HighScore(string json) {}
    }
}