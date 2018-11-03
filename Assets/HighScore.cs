using SimpleJSON;

namespace Winnitron {

    public class HighScore  {

        public string Name { get; private set; }
        public int Score { get; private set; }
        // TODO: which winnitron?

        public HighScore(string name, int score) {
            Name = name;
            Score = score;
        }

        public static HighScore FromJson(string raw) {
            JSONNode json = JSON.Parse(raw);
            return new HighScore(json["name"], json["score"].AsInt);
        }

        public static HighScore[] ListFromJson(string raw) {
            JSONArray json = JSON.Parse(raw)["high_scores"].AsArray;
            HighScore[] scores = new HighScore[json.Count];

            for(int i = 0; i < json.Count; i++) {
                scores[i] = new HighScore(json[i]["name"], json[i]["score"].AsInt);
            }

            return scores;
        }

        public override string ToString() {
            return "HighScore: " + Name + " " + Score;
        }
    }
}