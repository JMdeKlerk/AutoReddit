using Newtonsoft.Json.Linq;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;

namespace RedditBot
{
    class ApiRequest
    {
        private dynamic result;

        public ApiRequest(string url, string httpMethod)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = httpMethod;
            request.ContentType = "application/x-www-form-urlencoded";
            request.UserAgent = "RedditBot/0.1 by /u/Shindogo";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader read = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string json = read.ReadToEnd();
            result = JObject.Parse(json);
        }

        public ApiRequest(User user, string url, string httpMethod)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = httpMethod;
            request.ContentType = "application/x-www-form-urlencoded";

            string auth = user.getToken();
            auth = "bearer " + auth;
            request.Headers.Add("AUTHORIZATION", auth);
            request.UserAgent = "RedditBot/0.1 by /u/Shindogo";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader read = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string json = read.ReadToEnd();
            result = JObject.Parse(json);
        }

        public ApiRequest(User user, string url, string httpMethod, Hashtable args)
        {
            string postData = "";
            foreach (DictionaryEntry kvp in args)
            {
                postData += kvp.Key + "=" + kvp.Value + "&";
            }
            ASCIIEncoding encode = new ASCIIEncoding();
            byte[] byteData = encode.GetBytes(postData);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = httpMethod;
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteData.Length;

            string auth = user.getToken();
            auth = "bearer " + auth;
            request.Headers.Add("AUTHORIZATION", auth);
            request.UserAgent = "RedditBot/0.1 by /u/Shindogo";

            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteData, 0, byteData.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader read = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string json = read.ReadToEnd();
            result = JObject.Parse(json);
        }

        public dynamic getResponse()
        {
            return this.result;
        }
    }
}
