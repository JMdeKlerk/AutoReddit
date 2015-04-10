using System;
using System.Collections;

namespace RedditBot
{
    // This class represents the currently logged in user, and holds their login details as well as the access token and
    //  private information retrieved from the server.
    class User
    {
        private string username, password, key, secret, access_token, messages, error = "";
        private int lkarma, ckarma;
        private DateTime token_expires;

        // Construct the object and attempt a login.
        public User(string name, string pass, string key, string secret, Main parent)
        {
            this.username = name;
            this.password = pass;
            this.key = key;
            this.secret = secret;

            loginUser(name, pass, key, secret);

            // If everything checks out, fetch a basic profile.
            if (!String.IsNullOrEmpty(this.access_token))
            {
                parent.formConsole("Logged in successfully.");
                ApiRequest request = new ApiRequest(this, "https://oauth.reddit.com/api/v1/me", "GET");
                dynamic userinfo = request.getResponse();
                this.lkarma = userinfo.link_karma;
                this.ckarma = userinfo.comment_karma;
                this.messages = userinfo.has_mail;
            }
            // Otherwise, tell them why we failed.
            else
            {
                if (error.Equals("invalid_grant")) { error = "Username or password incorrect."; }
                if (error.Equals("invalid_auth")) { error = "Authorization failed. The server may be down or your app key/secret may be invalid."; }
                parent.formConsole("Error: " + error);
            }
        }

        // Login method. Uses ApiRequest class.
        private void loginUser(string name, string pass, string key, string secret)
        {
            string url = "https://www.reddit.com/api/v1/access_token";
            Hashtable args = new Hashtable();
            args.Add("username", name);
            args.Add("password", pass);
            args.Add("grant_type", "password");
            string auth = key + ":" + secret;
            byte[] authBin = System.Text.Encoding.UTF8.GetBytes(auth);
            auth = Convert.ToBase64String(authBin);
            auth = "Basic " + auth;
            ApiRequest request = new ApiRequest(url, "POST", auth, args);
            dynamic response = request.getResponse(); 
            this.access_token = response.access_token;
            this.token_expires = DateTime.Now.AddSeconds(Convert.ToInt32(response.expires_in));
            this.error = response.error;
        }

        // Access tokens only last for one hour, so each time we try to use it, check if it is still valid.
        // If not, get a new one.
        public string getToken()
        {
            if (this.tokenHasExpired()) { loginUser(username, password, key, secret); }
            return this.access_token;
        }

        // Getters.
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
