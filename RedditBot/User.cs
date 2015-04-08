using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace RedditBot
{
    class User
    {
        private string username, password, key, secret, access_token, messages, error = "";
        private int lkarma, ckarma;
        private DateTime token_expires;

        public User(string name, string pass, string key, string secret, Main parent)
        {
            this.username = name;
            this.password = pass;
            this.key = key;
            this.secret = secret;

            loginUser(name, pass, key, secret);

            if (!String.IsNullOrEmpty(this.access_token))
            {
                parent.formConsole("Logged in successfully.");
                ApiRequest request = new ApiRequest(this, "https://oauth.reddit.com/api/v1/me", "GET");
                dynamic userinfo = request.getResponse();
                this.lkarma = userinfo.link_karma;
                this.ckarma = userinfo.comment_karma;
                this.messages = userinfo.has_mail;
            }
            else
            {
                if (error.Equals("invalid_grant")) { error = "Username or password incorrect."; }
                if (error.Equals("invalid_auth")) { error = "Authorization failed. The server may be down or your app key/secret may be invalid."; }
                parent.formConsole("Error: " + error);
            }
        }

        private void loginUser(string name, string pass, string key, string secret)
        {
            string url = "https://www.reddit.com/api/v1/access_token";
            string postData = "username=" + name + "&password=" + pass + "&grant_type=password";
            ASCIIEncoding encode = new ASCIIEncoding();
            byte[] byteData = encode.GetBytes(postData);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteData.Length;

            string auth = key + ":" + secret;
            byte[] authBin = System.Text.Encoding.UTF8.GetBytes(auth);
            auth = Convert.ToBase64String(authBin);
            auth = "Basic " + auth;
            request.Headers.Add("AUTHORIZATION", auth);

            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteData, 0, byteData.Length);

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader read = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(read.ReadToEnd());
                string value = null;
                if (values.TryGetValue("access_token", out value)) { this.access_token = value; }
                if (values.TryGetValue("expires_in", out value)) { this.token_expires = DateTime.Now.AddSeconds(Convert.ToInt32(value)); }
                if (values.TryGetValue("error", out value)) { this.error = value; }
            }
            catch
            {
                this.error = "invalid_auth";
            }
        }

        public string getToken()
        {
            if (this.tokenHasExpired()) { loginUser(username, password, key, secret); }
            return this.access_token;
        }

        public string getUsername()
        {
            return this.username;
        }

        public int getLKarma()
        {
            return this.lkarma;
        }

        public int getCKarma()
        {
            return this.ckarma;
        }

        public string hasMessages()
        {
            return this.messages;
        }

        private bool tokenHasExpired()
        {
            if (DateTime.Now > this.token_expires) { return true; } return false;
        }
    }
}
