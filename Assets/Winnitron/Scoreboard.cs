using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

namespace Winnitron {

    public class Scoreboard : WinnitronConnection {

        public const string ALL_WINNITRONS = "ALL_WINNITRONS";
        public const string THIS_WINNITRON = "THIS_WINNITRON";

        public void Start() {

        }

        public void Send(HighScore highScore, Success success) {
            // These must be ordered alphabetically by key to get the right hash later.
            WWWForm fields = new WWWForm();
            fields.AddField("name", highScore.Name);
            fields.AddField("score", highScore.Score.ToString());

            if (testMode) {
                fields.AddField("test", "1");
            }

            if (winnitronID != null) {
                fields.AddField("winnitron_id", winnitronID);
            }

            UnityWebRequest www = UnityWebRequest.Post(HOST + "/api/v1/high_scores", fields);
            AddHeaders(www);
            StartCoroutine(Wait(www, ParseHighScore, success));
        }

        public void Get(int limit, string winnitronScope, Success success) {
            string url = HOST + "/api/v1/high_scores?limit=" + limit;

            if (testMode) {
                url += "&test=1";
            }

            if (winnitronScope == THIS_WINNITRON) {
                url += "&winnitron_id=" + winnitronID;
            } else if (winnitronScope == ALL_WINNITRONS) {
                // NOOP
            } else {
                url += "&winnitron_id=" + winnitronScope;
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

        private object DefaultSuccess(object results) {
            if (results.GetType().ToString() == "Winnitron.HighScore[]") {
                PrintList((HighScore[]) results);
            } else {
                Debug.Log("Successfully created high score: " + results.ToString());
            }

            return null;
        }

        private void PrintList(HighScore[] scores) {
            string msg = "Successfully fetched high scores:";
            foreach (HighScore score in scores) {
                msg += "\n\t" + score.ToString();
            }
            Debug.Log(msg);
        }
    }
}