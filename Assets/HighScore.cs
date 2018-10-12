namespace Winnitron {

    public class HighScore  {

        public string Name { get; private set; }
        public int Score { get; private set; }
        // TODO: which winnitron?

        public HighScore(string name, int score) {
            Name = name;
            Score = score;
        }

        // TODO
        public HighScore(string json) {}
    }
}