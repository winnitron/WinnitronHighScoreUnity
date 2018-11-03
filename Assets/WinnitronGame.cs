using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

namespace Winnitron {

    public class WinnitronGame : WinnitronConnection {

        public void Start() {

        }

        public void SendHighScore(HighScore highScore, Success success) {
            // These must be ordered alphabetically by key to get the right hash later.
            WWWForm fields = new WWWForm();
            fields.AddField("name", highScore.Name);
            fields.AddField("score", highScore.Score.ToString());

            if (testMode) {
                fields.AddField("test", "1");
            }

            UnityWebRequest www = UnityWebRequest.Post(HOST + "/api/v1/high_scores", fields);
            AddHeaders(www);
            StartCoroutine(Wait(www, ParseHighScore, success));
        }

        public void GetHighScores(int limit, Success success) {
            string url = HOST + "/api/v1/high_scores?limit=" + limit;

            if (testMode) {
                url += "&test=1";
            }

            UnityWebRequest www = UnityWebRequest.Get(url);
            AddHeaders(www);
            StartCoroutine(Wait(www, ParseHighScores, success));
        }

        private void ParseHighScore(UnityWebRequest www, Success success) {
            HighScore score = HighScore.FromJson(www.downloadHandler.text);
            success(score);
        }

        private void ParseHighScores(UnityWebRequest www, Success success) {
            HighScore[] scores = HighScore.ListFromJson(www.downloadHandler.text);
            success(scores);
        }

        // temp
        private object DefaultSuccess(object results) {
            HighScore[] s = (HighScore[]) results;

            try {
                Debug.Log("success: " + s[0].ToString());
            } catch (System.IndexOutOfRangeException) {
                Debug.Log("success: []");
            }

            return null;
        }

        private object Stub(object results) { return null; }
    }
}