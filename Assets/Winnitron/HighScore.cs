using SimpleJSON;

namespace Winnitron {

    public class HighScore  {

        public string Name { get; private set; }
        public int Score { get; private set; }
        public ArcadeMachine Winnitron { get; private set; }

        public HighScore(string name, int score, ArcadeMachine machine = null) {
            Name = name;
            Score = score;
            Winnitron = machine;
        }

        public static HighScore FromJson(string raw) {
            JSONNode json = JSON.Parse(raw);
            ArcadeMachine machine = ArcadeMachine.FromJson(json["arcade_machine"]);
            return new HighScore(json["name"], json["score"].AsInt, machine);
        }

        public static HighScore[] ListFromJson(string raw) {
            JSONArray json = JSON.Parse(raw)["high_scores"].AsArray;
            HighScore[] scores = new HighScore[json.Count];

            for(int i = 0; i < json.Count; i++) {
                ArcadeMachine machine = ArcadeMachine.FromJson(json[i]["arcade_machine"]);
                scores[i] = new HighScore(json[i]["name"], json[i]["score"].AsInt, machine);
            }

            return scores;
        }

        public override string ToString() {
            return "HighScore: " + Name + " " + Score;
        }
    }
}