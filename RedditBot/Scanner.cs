using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RedditBot
{
    class Scanner
    {
        private Main mainForm;
        private User user;
        private double postAfter, commentAfter;
        private bool searchTitles, searchPosts, searchComments, searchMessages;
        private string trigger, subreddit, preview;

        public Scanner(Main form, User user, string trigger, string subreddit, bool titles, bool posts, bool comments)
        {
            this.mainForm = form;
            this.user = user;
            this.trigger = trigger;
            this.subreddit = subreddit;
            this.searchTitles = titles;
            this.searchPosts = posts;
            this.searchComments = comments;
            this.postAfter = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
            this.commentAfter = this.postAfter;
        }

        public void scan()
        {
            if (searchTitles || searchPosts) { this.scanPosts(); }
            if (searchComments) { this.scanComments(); }
        }

        private void scanPosts()
        {
            string url = "https://www.reddit.com/r/" + subreddit + "/search/.json?restrict_sr=true&limit=25&sort=new";
            ApiRequest requestPosts = new ApiRequest(url, "GET");
            dynamic postResults = requestPosts.getResponse();
            string title, body, postAuthor, fullname;
            title = body = postAuthor = fullname = "";
            double postCreated = 0;

            for (int i = 24; i >= 0; i--)
            {
                try
                {
                    title = postResults.data.children[i].data.title;
                    body = postResults.data.children[i].data.selftext;
                    postAuthor = postResults.data.children[i].data.author;
                    postCreated = postResults.data.children[i].data.created_utc;
                    fullname = postResults.data.after;
                }
                catch (ArgumentOutOfRangeException e) { i = -1; }
                finally
                {
                    if (postCreated > postAfter)
                    {
                        if (title.ToLower().Contains(this.trigger.ToLower()) && searchTitles)
                        {
                            if (title.Length > 25) { preview = title.Remove(24).Replace("\n", " ") + "..."; }
                            else { preview = title.Replace("\n", " "); }
                            mainForm.formConsole("Title: \'" + preview + "\' by /u/" + postAuthor);
                            this.respond(fullname, "Shindogo");
                        }
                        if (body.ToLower().Contains(this.trigger.ToLower()) && searchPosts)
                        {
                            if (body.Length > 25) { preview = body.Remove(24).Replace("\n", " ") + "..."; }
                            else { preview = body.Replace("\n", " "); }
                            mainForm.formConsole("Body: \'" + preview + "\' by /u/" + postAuthor);
                            this.respond(fullname, "Shindogo");
                        }
                        postAfter = postCreated;
                    }
                }
            }
        }

        private void scanComments()
        {
            string url = "https://www.reddit.com/r/" + this.subreddit + "/comments/.json";
            ApiRequest requestComments = new ApiRequest(url, "GET");
            dynamic commentResults = requestComments.getResponse();
            string comment, commentAuthor, fullname;
            comment = commentAuthor = fullname = "";
            double commentCreated = 0;

            for (int i = 24; i >= 0; i--)
            {
                try
                {
                    comment = commentResults.data.children[i].data.body;
                    commentAuthor = commentResults.data.children[i].data.author;
                    commentCreated = commentResults.data.children[i].data.created_utc;
                    fullname = commentResults.data.after;
                }
                catch (ArgumentOutOfRangeException e) { i = -1; }
                finally
                {

                    if (commentCreated > commentAfter)
                    {
                        if (comment.ToLower().Contains(this.trigger.ToLower()))
                        {
                            if (comment.Length > 25) { preview = comment.Remove(24).Replace("\n", " ") + "..."; }
                            else { preview = comment.Replace("\n", " "); }
                            mainForm.formConsole("Comment: \'" + preview + "\' by /u/" + commentAuthor);
                            this.respond(fullname, "Shindogo");
                        }
                        commentAfter = commentCreated;
                    }
                }
            }
        }

        private void scanMessages()
        {
            // TODO
        }

        private void respond(String source, String recipient)
        {
            string action = Properties.Settings.Default["action"].ToString();
            string content = Properties.Settings.Default["content"].ToString();

            if (action.Equals("Alert")) { mainForm.Invoke((MethodInvoker)delegate { FlashWindow.Flash(mainForm, 5); SystemSounds.Beep.Play(); }); }
            if (action.Equals("Reply"))
            {
                string url = "https://oauth.reddit.com/api/comment";
                Hashtable args = new Hashtable();
                args.Add("parent", source);
                args.Add("text", content);
                ApiRequest request = new ApiRequest(user, url, "POST", args);
                request.getResponse();
            }
            if (action.Equals("Message"))
            {
                string url = "https://oauth.reddit.com/api/compose";
                Hashtable args = new Hashtable();
                args.Add("subject", "Automated message");
                args.Add("text", content);
                args.Add("to", recipient);
                ApiRequest request = new ApiRequest(user, url, "POST", args);
            }
        }
    }
}
