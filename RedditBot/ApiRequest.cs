using Newtonsoft.Json.Linq;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;

namespace RedditBot
{
    // This class handles all calls the application makes to the reddit api, providing the proper headers and authorization.
    // Stores the server's response as a dynamic JSON object.
    class ApiRequest
    {
        private dynamic result;

        // When no authorization is required. Used for scanning posts and comments..
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

        // When we need our OAuth access token but no params. Used to get user info and unread private messages. 
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

        // Only used to login - we need to send params and authorization, but have no OAuth access token yet.
        public ApiRequest(string url, string httpMethod, string auth, Hashtable args)
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
            request.Headers.Add("AUTHORIZATION", auth);
            request.UserAgent = "RedditBot/0.1 by /u/Shindogo";

            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteData, 0, byteData.Length);
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader read = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string json = read.ReadToEnd();
                result = JObject.Parse(json);
            }
            catch (WebException)
            {
                result = new JObject();
                result.error = "invalid_auth";
            }
        }

        // Used to write to server - when we create a new comment, private message, or mark a message as read.
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

        // If we need to read the server response, we can get a copy of it with this method.
        public dynamic getResponse()
        {
            return this.result;
        }
    }
}
