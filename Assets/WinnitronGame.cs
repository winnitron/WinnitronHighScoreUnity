using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

namespace Winnitron {

    public class WinnitronGame : WinnitronConnection {

        public void Start() {

        }

        public void SendHighScore(HighScore highScore, Success success = null) {
            if (success == null)
                success = HandleScoreCreation;

            // These must be ordered alphabetically by key to get the right hash later.
            WWWForm fields = new WWWForm();
            fields.AddField("name", highScore.Name);
            fields.AddField("score", highScore.Score.ToString());

            UnityWebRequest www = UnityWebRequest.Post("http://localhost:3000/api/v1/high_scores", fields);
            AddHeaders(www);
            StartCoroutine(Wait(www, success));
        }

        public void GetHighScores(int limit, Success success = null) {
            if (success == null)
                success = HandleGetScores;

            UnityWebRequest www = UnityWebRequest.Get("http://localhost:3000/api/v1/high_scores?limit=" + limit);
            AddHeaders(www);
            StartCoroutine(Wait(www, success));
        }

        private object HandleScoreCreation(UnityWebRequest www) {
            Debug.Log("HandleScoreCreation: " + www.downloadHandler.text);
            return null;
        }

        private object HandleGetScores(UnityWebRequest www) {
            Debug.Log("HandleGetScores: " + www.downloadHandler.text);
            return null;
        }
    }
}