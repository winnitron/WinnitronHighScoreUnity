
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

namespace Winnitron {

    public class WinnitronGame : WinnitronConnection {

        public void Start() {

        }

        public void SendHighScore(string name, int score) {
            // These must be ordered alphabetically by key to get the right hash later.
            WWWForm fields = new WWWForm();
            fields.AddField("name", name);
            fields.AddField("score", score.ToString());

            // string query_str = "name=" + name + "&score=" + score;
            // List<IMultipartFormSection> fields = new List<IMultipartFormSection>();
            // fields.Add( new MultipartFormDataSection(query_str) );

            UnityWebRequest www = UnityWebRequest.Post("http://localhost:3000/api/v1/high_scores", fields);

            AddHeaders(www);

            Debug.Log("OK");

            StartCoroutine(Wait(www, HandleScoreCreation));
        }

        public void GetHighScores() {
            // TODO
        }

        private void HandleScoreCreation(UnityWebRequest www) {
            Debug.Log("created!");
        }

        private void HandleGetScores(UnityWebRequest www) {
            Debug.Log("got!");
        }
    }
}