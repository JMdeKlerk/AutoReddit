using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditBot
{
    class Scanner
    {
        private Main parent;
        double postAfter, commentAfter;
        bool searchPosts, searchComments;
        string trigger, subreddit, preview;

        public Scanner(Main parent, string trigger, string subreddit, bool posts, bool comments)
        {
            this.parent = parent;
            this.trigger = trigger;
            this.subreddit = subreddit;
            this.searchPosts = posts;
            this.searchComments = comments;
            this.postAfter = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
            this.commentAfter = this.postAfter;
        }

        public void scanPosts()
        {
            string url = "https://www.reddit.com/r/" + subreddit + "/search/.json?restrict_sr=true&limit=25&sort=new";
            ApiRequest requestPosts = new ApiRequest(url, "GET");
            dynamic postResults = requestPosts.getResponse();

            for (int i = 24; i >= 0; i--)
            {
                string title = postResults.data.children[i].data.title;
                string body = postResults.data.children[i].data.selftext;
                string postAuthor = postResults.data.children[i].data.author;
                double postCreated = postResults.data.children[i].data.created_utc;

                if (postCreated > postAfter)
                {
                    if (title.ToLower().Contains(this.trigger.ToLower()) && searchPosts)
                    {
                        if (title.Length > 25) { preview = title.Remove(24).Replace("\n", " ") + "..."; }
                        else { preview = title.Replace("\n", " "); }
                        parent.formConsole("Title: \'" + preview + "\' by /u/" + postAuthor);
                    }
                    if (body.ToLower().Contains(this.trigger.ToLower()) && searchPosts)
                    {
                        if (body.Length > 25) { preview = body.Remove(24).Replace("\n", " ") + "..."; }
                        else { preview = body.Replace("\n", " "); }
                        parent.formConsole("Body: \'" + body + "\' by /u/" + postAuthor);
                    }
                    postAfter = postCreated;
                }
            }
        }

        public void scanComments()
        {
            string url = "https://www.reddit.com/r/" + this.subreddit + "/comments/.json";
            ApiRequest requestComments = new ApiRequest(url, "GET");
            dynamic commentResults = requestComments.getResponse();

            for (int i = 24; i >= 0; i--)
            {
                string comment = commentResults.data.children[i].data.body;
                string commentAuthor = commentResults.data.children[i].data.author;
                double commentCreated = commentResults.data.children[i].data.created_utc;

                if (commentCreated > commentAfter)
                {
                    if (comment.ToLower().Contains(this.trigger.ToLower()) && searchComments)
                    {
                        if (comment.Length > 25) { preview = comment.Remove(24).Replace("\n", " ") + "..."; }
                        else { preview = comment.Replace("\n", " "); }
                        parent.formConsole("Comment: \'" + preview + "\' by /u/" + commentAuthor);
                    }
                    commentAfter = commentCreated;
                }
            }
        }
    }
}
