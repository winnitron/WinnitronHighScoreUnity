using SimpleJSON;

namespace Winnitron {

    public class ArcadeMachine  {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Location { get; private set; }

        public ArcadeMachine(string id, string name, string location) {
            Id = id;
            Name = name;
            Location = location;
        }

        public static ArcadeMachine FromJson(string raw) {
            if (string.IsNullOrEmpty(raw))
                return null;

            JSONNode json = JSON.Parse(raw);
            return new ArcadeMachine(json["id"], json["name"], json["location"]);
        }
    }
}