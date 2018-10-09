using System.Security.Cryptography;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine;

namespace Winnitron {

    public class WinnitronConnection : MonoBehaviour {
        public const string VERSION = "0.1";

        public string apiKey;
        public string apiSecret;

        protected delegate void Success(UnityWebRequest www);

        protected IEnumerator Wait(UnityWebRequest www, Success success) {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError) {
                HandleError(www);
            } else {
                success(www);
            }
        }

        protected void AddHeaders(UnityWebRequest www) {
            www.SetRequestHeader("User-Agent", "Winnitron Game Client/" + VERSION + " http://winnitron.com");
            www.SetRequestHeader("Authorization", Authorization(www));
        }

        protected void HandleError(UnityWebRequest www) {
            Debug.Log("something went wrong: " + www.error);
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