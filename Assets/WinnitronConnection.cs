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

            if (www.isNetworkError) {
                HandleError(www, "Network Error:");
            } else if (www.isHttpError) {
                HandleError(www, "HTTP Error:");
            } else {
                success(www);
            }
        }

        protected void AddHeaders(UnityWebRequest www) {
            www.SetRequestHeader("User-Agent", "Winnitron Game Client/" + VERSION + " http://winnitron.com");
            www.SetRequestHeader("Authorization", Authorization(www));
        }

        protected void HandleError(UnityWebRequest www, string msgPrepend = null) {
            string[] components = {
                msgPrepend,
                (www.responseCode > 0 ? www.responseCode.ToString() : null),
                www.error,
                ParseErrors(www.downloadHandler.text)
            };

            string msg = "";
            foreach (string s in components) {
                if (s != null && s != "")
                    msg += (s + " ");
            }

            throw new Winnitron.NetworkException(msg);
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

        private string ParseErrors(string json) {
            return json; // TODO
        }
    }
}