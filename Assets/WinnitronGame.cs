using System.Collections;
using System.Security.Cryptography;
// using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


namespace Winnitron {

    public class WinnitronGame : MonoBehaviour {
        public const string VERSION = "0.1";

        public string apiKey;
        public string apiSecret;

        private delegate void Success(UnityWebRequest www);

        public void SendHighScore(string name, int score) {
            WWWForm fields = new WWWForm();

            // These must be ordered alphabetically by key to get the right hash later.
            fields.AddField("name", name);
            fields.AddField("score", score.ToString());

            UnityWebRequest www = UnityWebRequest.Post("http://example.com", fields);

            AddHeaders(www);

            StartCoroutine(Wait(www, HandleScoreCreation));
        }

        public void GetHighScores() {
            // TODO
        }

        private IEnumerator Wait(UnityWebRequest www, Success success) {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError) {
                HandleError(www);
            } else {
                Debug.Log("it worked");
                success(www);
            }
        }

        private void AddHeaders(UnityWebRequest www) {
            www.SetRequestHeader("User-Agent", "Winnitron Game Client/" + VERSION + " http://winnitron.com");
            www.SetRequestHeader("Authorization", Authorization(www));
        }

        private void HandleError(UnityWebRequest www) {
            Debug.Log("something went wrong: " + www.error);
        }

        private void HandleScoreCreation(UnityWebRequest www) {
            Debug.Log("created!");
        }

        private void HandleGetScores(UnityWebRequest www) {
            Debug.Log("got!");
        }

        private string Authorization(UnityWebRequest www) {
            if (apiSecret == null || apiSecret == "") {
                return "Token " + apiKey;
            } else {
                string query_str = System.Text.Encoding.UTF8.GetString(www.uploadHandler.data);
                string signature = HexHash(query_str + apiSecret);

                return "Winnitron " + apiKey + ":" + signature;
            }
        }

        private string HexHash(string source) {
            byte[] bsource = System.Text.Encoding.ASCII.GetBytes(source);

            SHA256Managed mathifier = new SHA256Managed();
            byte[] hash = mathifier.ComputeHash(bsource);

            string hex = System.BitConverter.ToString(hash);
            hex = hex.Replace("-", "");

            return hex;
        }
    }
}