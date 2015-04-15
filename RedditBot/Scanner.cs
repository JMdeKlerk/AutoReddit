using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace RedditBot
{
    // This class is responsible for scanning for the trigger, and if/when it is found, performing our response.
    class Scanner
    {
        private Main mainForm;
        private User user;
        private double postAfter, commentAfter;
        private bool advanced, searchTitles, searchPosts, searchComments, searchMessages;
        private string trigger, subreddit, preview;

        public Scanner(Main form, User user, string mode, string trigger, string subreddit, bool titles, bool posts, bool comments, bool messages)
        {
            this.mainForm = form;
            this.user = user;
            if (mode.Equals("advanced")) { this.advanced = true; }
            else { this.advanced = false; }
            this.trigger = trigger;
            this.subreddit = subreddit;
            this.searchTitles = titles;
            this.searchPosts = posts;
            this.searchComments = comments;
            this.searchMessages = messages;
            this.postAfter = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
            this.commentAfter = this.postAfter;
        }

        // Run the scan on selected locations.
        public void scan()
        {
            if (searchTitles || searchPosts) { this.scanPosts(); }
            if (searchComments) { this.scanComments(); }
            if (searchMessages) { this.scanMessages(); }
        }

        // Get last 25 posts using ApiRequest.
        private void scanPosts()
        {
            string url = "https://www.reddit.com/r/" + subreddit + "/search/.json?restrict_sr=true&limit=25&sort=new";
            string action = Properties.Settings.Default["action"].ToString();
            string content = Properties.Settings.Default["content"].ToString();
            ApiRequest requestPosts = new ApiRequest(url, "GET");
            dynamic postResults = requestPosts.getResponse();
            string title, body, postAuthor, fullname;
            title = body = postAuthor = fullname = "";
            double postCreated = 0;

            // Counts backwards so that results are output in order of creation.
            for (int i = 24; i >= 0; i--)
            {
                try
                {
                    title = postResults.data.children[i].data.title;
                    body = postResults.data.children[i].data.selftext;
                    postAuthor = postResults.data.children[i].data.author;
                    postCreated = postResults.data.children[i].data.created_utc;
                    fullname = postResults.data.children[i].data.name;

                    // Only trigger if post was made after start of run/since the last post we responded to.
                    // This way we don't respond twice to the same post.
                    if (postCreated > postAfter)
                    {
                        // If trigger is in title (case insensitive), print it in console and perform our response
                        if (title.ToLower().Contains(this.trigger.ToLower()) && searchTitles && !advanced)
                        {
                            if (title.Length > 25) { preview = title.Remove(24).Replace("\n", " ") + "..."; }
                            else { preview = title.Replace("\n", " "); }
                            mainForm.formConsole("Title: \'" + preview + "\' by /u/" + postAuthor);
                            this.respond(action, content, fullname, postAuthor);
                        }
                        // If trigger is in body (case insensitive), print it in console and perform our response
                        if (body.ToLower().Contains(this.trigger.ToLower()) && searchPosts && !advanced)
                        {
                            if (body.Length > 25) { preview = body.Remove(24).Replace("\n", " ") + "..."; }
                            else { preview = body.Replace("\n", " "); }
                            mainForm.formConsole("Body: \'" + preview + "\' by /u/" + postAuthor);
                            this.respond(action, content, fullname, postAuthor);
                        }
                        // If advanced mode, run user's script and pass non-null results to response method.
                        if (advanced)
                        {
                            string pyTitle = pyRun("title " + postAuthor + " " + title).Replace("\r\n", "");
                            string pyBody = pyRun("body " + postAuthor + " " + body).Replace("\r\n", "");
                            if (!String.IsNullOrEmpty(pyTitle))
                            {
                                if (title.Length > 25) { preview = title.Remove(24).Replace("\n", " ") + "..."; }
                                else { preview = title.Replace("\n", " "); }
                                mainForm.formConsole("Title: \'" + preview + "\' by /u/" + postAuthor);
                                if (pyTitle.Contains(" ")) { action = pyTitle.Substring(0, pyTitle.IndexOf(" ")); }
                                else { action = pyTitle; }
                                if (pyTitle.Contains(" ")) { content = pyTitle.Substring(pyTitle.IndexOf(" ")); }
                                else { content = ""; }
                                this.respond(action, content, fullname, postAuthor);
                            }
                            if (!String.IsNullOrEmpty(pyBody))
                            {
                                if (body.Length > 25) { preview = body.Remove(24).Replace("\n", " ") + "..."; }
                                else { preview = body.Replace("\n", " "); }
                                mainForm.formConsole("Body: \'" + preview + "\' by /u/" + postAuthor);
                                if (pyBody.Contains(" ")) { action = pyBody.Substring(0, pyBody.IndexOf(" ")); }
                                else { action = pyBody; }
                                if (pyTitle.Contains(" ")) { content = pyTitle.Substring(pyTitle.IndexOf(" ")); }
                                else { content = ""; }
                                this.respond(action, content, fullname, postAuthor);
                            }
                        }
                        // Set this as last responded to
                        postAfter = postCreated;
                    }
                }
                // Program throws argument out of range if there are fewer than 25 results. Ignore it.
                catch (ArgumentOutOfRangeException) { }
            }
        }

        // Get the last 25 comments using ApiRequest.
        private void scanComments()
        {
            string url = "https://www.reddit.com/r/" + this.subreddit + "/comments/.json";
            string action = Properties.Settings.Default["action"].ToString();
            string content = Properties.Settings.Default["content"].ToString();
            ApiRequest requestComments = new ApiRequest(url, "GET");
            dynamic commentResults = requestComments.getResponse();
            string comment, commentAuthor, fullname;
            comment = commentAuthor = fullname = "";
            double commentCreated = 0;

            // Counts backwards so that results are output in order of creation.
            for (int i = 24; i >= 0; i--)
            {
                try
                {
                    comment = commentResults.data.children[i].data.body;
                    commentAuthor = commentResults.data.children[i].data.author;
                    commentCreated = commentResults.data.children[i].data.created_utc;
                    fullname = commentResults.data.children[i].data.name;

                    // Only trigger if comment was made after start of run/since the last comment we responded to.
                    // This way we don't respond twice to the same comment.
                    if (commentCreated > commentAfter)
                    {
                        // If trigger is in comment (case insensitive), print it in console and perform our response
                        if (comment.ToLower().Contains(this.trigger.ToLower()) && !advanced)
                        {
                            if (comment.Length > 25) { preview = comment.Remove(24).Replace("\n", " ") + "..."; }
                            else { preview = comment.Replace("\n", " "); }
                            mainForm.formConsole("Comment: \'" + preview + "\' by /u/" + commentAuthor);
                            this.respond(action, content, fullname, commentAuthor);
                        }
                        // If advanced mode, run user's script and pass non-null results to response method.
                        if (advanced)
                        {
                            string pyComment = pyRun("comment " + commentAuthor + " " + comment).Replace("\r\n", "");
                            if (!String.IsNullOrEmpty(pyComment))
                            {
                                if (comment.Length > 25) { preview = comment.Remove(24).Replace("\n", " ") + "..."; }
                                else { preview = comment.Replace("\n", " "); }
                                mainForm.formConsole("Comment: \'" + preview + "\' by /u/" + commentAuthor);
                                if (pyComment.Contains(" ")) { action = pyComment.Substring(0, pyComment.IndexOf(" ")); }
                                else { action = pyComment; }
                                if (pyComment.Contains(" ")) { content = pyComment.Substring(pyComment.IndexOf(" ")); }
                                else { content = ""; }
                                this.respond(action, content, fullname, commentAuthor);
                            }
                        }
                        // Set this as last responded to
                        commentAfter = commentCreated;
                    }
                }
                // Program throws argument out of range if there are fewer than 25 results. Ignore it.
                catch (ArgumentOutOfRangeException) { }
            }
        }

        // Get all unread messages using ApiRequest.
        private void scanMessages()
        {
            string url = "https://oauth.reddit.com/message/unread";
            string action = Properties.Settings.Default["action"].ToString();
            string content = Properties.Settings.Default["content"].ToString();
            ApiRequest requestMessages = new ApiRequest(user, url, "GET");
            dynamic messageResults = requestMessages.getResponse();
            string message, sender, fullname;
            bool wasComment;
            message = sender = fullname = "";

            // Counts backwards so that results are output in order of creation.
            for (int i = 24; i >= 0; i--)
            {
                try
                {
                    message = messageResults.data.children[i].data.body;
                    sender = messageResults.data.children[i].data.author;
                    fullname = messageResults.data.children[i].data.name;
                    wasComment = messageResults.data.children[i].data.was_comment;

                    // Don't reply to messages from reddit alerting us to comments.
                    if (!wasComment)
                    {
                        // If trigger is in message (case insensitive), print it in console and perform our response
                        if (message.ToLower().Contains(this.trigger.ToLower()) && !advanced)
                        {
                            if (message.Length > 25) { preview = message.Remove(24).Replace("\n", " ") + "..."; }
                            else { preview = message.Replace("\n", " "); }
                            mainForm.formConsole("Message: \'" + preview + "\' by /u/" + sender);
                            this.respond(action, content, fullname, sender);
                        }
                        // If advanced mode, run user's script and pass non-null results to response method.
                        if (advanced)
                        {
                            string pyMessage = pyRun("title " + sender + " " + message).Replace("\r\n", "");
                            if (!String.IsNullOrEmpty(pyMessage))
                            {
                                if (message.Length > 25) { preview = message.Remove(24).Replace("\n", " ") + "..."; }
                                else { preview = message.Replace("\n", " "); }
                                mainForm.formConsole("Message: \'" + preview + "\' by /u/" + sender);
                                if (pyMessage.Contains(" ")) { action = pyMessage.Substring(0, pyMessage.IndexOf(" ")); }
                                else { action = pyMessage; }
                                if (pyMessage.Contains(" ")) { content = pyMessage.Substring(pyMessage.IndexOf(" ")); }
                                else { content = ""; }
                                this.respond(action, content, fullname, sender);
                            }
                        }
                        // Then mark the message as read using ApiRequest so we don't see it again.
                        url = "https://oauth.reddit.com/api/read_message";
                        Hashtable args = new Hashtable();
                        args.Add("id", fullname);
                        ApiRequest markAsRead = new ApiRequest(user, url, "POST", args);
                    }
                }
                // Program throws argument out of range if there are fewer than 25 results. Ignore it.
                catch (ArgumentOutOfRangeException) { }
            }
        }

        // Handle sending our specified response.
        private void respond(String action, String content, String source, String recipient)
        {
            content = content += "\n\n****\n\n^I ^am ^a ^bot, ^developed ^\\(but ^not ^owned) ^by ^/u/Shindogo ^- [^Feedback?](http://www.reddit.com/message/compose/?to=" + user.getUsername() + ") [^Want ^your ^own?](https://github.com/JMdeKlerk/RedditBot)";

            // If action is alert, send nothing, just flash and beep.
            if (action.ToLower().Equals("alert"))
            {
                mainForm.Invoke((MethodInvoker)delegate
                {
                    mainForm.Show();
                    FlashWindow.Flash(mainForm, 5);
                    SystemSounds.Beep.Play();
                });
            }
            // Otherwise, create a comment or private message using ApiRequest.
            if (action.ToLower().Equals("reply"))
            {
                string url = "https://oauth.reddit.com/api/comment";
                Hashtable args = new Hashtable();
                args.Add("parent", source);
                args.Add("text", content);
                ApiRequest request = new ApiRequest(user, url, "POST", args);
                request.getResponse();
            }
            if (action.ToLower().Equals("message"))
            {
                string url = "https://oauth.reddit.com/api/compose";
                Hashtable args = new Hashtable();
                args.Add("subject", "Automated message");
                args.Add("text", content);
                args.Add("to", recipient);
                ApiRequest request = new ApiRequest(user, url, "POST", args);
            }
        }

        // Method for running python scripts. Uses a system console and returns whatever it outputs.
        private string pyRun(string args)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "python.exe";
            start.Arguments = "script.py " + args;
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            start.CreateNoWindow = true;
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    return result;
                }
            }
        }
    }
}
