namespace IssueTrackerWebApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Helpers;
    using IssueTrackerWebApp.Contracts;
    using IssueTrackerWebApp.Data;
    using Models;

    /// <summary>
    /// 
    /// </summary>
    public class IssueTracker : IIssueTracker
    {
        public IssueTracker(IIssueTrackerData data)
        {
            this.Data = data;
        }

        public IssueTracker()
            : this(new IssueTrackerData())
        {
        }
        IIssueTrackerData Data { get; set; }

        /// <summary>
        /// Method that registers users.
        /// </summary>
        /// <param name="username">username as string</param>
        /// <param name="password">password as string</param>
        /// <param name="confirmPassword">verification password as string</param>
        /// <returns>registration result as string</returns>
        public string RegisterUser(string username, string password, string confirmPassword)
        {
            if (this.Data.CurrentUser != null)
            {
                return string.Format("There is already a logged in user");
            }

            if (password != confirmPassword)
            {
                return string.Format("The provided passwords do not match", username);
            }

            if (this.Data.users_dict.ContainsKey(username))
            {
                return string.Format("A user with username {0} already exists", username);
            }

            var user = new User(username, password);
            this.Data.users_dict.Add(username, user);
            return string.Format("User {0} registered successfully", username);
        }

        public string LoginUser(string username, string password)
        {
            if (this.Data.CurrentUser != null)
            {
                return string.Format("There is already a logged in user");
            }

            if (!this.Data.users_dict.ContainsKey(username))
            {
                return string.Format("A user with username {0} does not exist", username);
            }

            var user = this.Data.users_dict[username];
            if (user.HashPassword != HashUtilities.HashPassword(password))
            {
                return string.Format("The password is invalid for user {0}", username);
            }

            this.Data.CurrentUser = user;

            return string.Format("User {0} logged in successfully", username);
        }

        public string LogoutUser()
        {
            if (this.Data.CurrentUser == null)
            {
                return string.Format("There is no currently logged in user");
            }

            string username = this.Data.CurrentUser.UserName;
            this.Data.CurrentUser = null;
            return string.Format("User {0} logged out successfully", username);
        }

        public string CreateIssue(string title, string description, IssuePriority priority, string[] strings)
        {
            if (this.Data.CurrentUser == null)
            {
                return string.Format("There is no currently logged in user");
            }

            var issue = new Issue(title, description, priority, strings.Distinct().ToList());
            this.Data.AddIssue(issue);
            return string.Format("Issue {0} created successfully.", issue.Id);
        }

        public string RemoveIssue(int issueId)
        {
            if (this.Data.CurrentUser == null)
            {
                return string.Format("There is no currently logged in user");
            }

            if (!this.Data.issues1.ContainsKey(issueId))
            {
                return string.Format("There is no issue with ID {0}", issueId);
            }

            var issue = this.Data.issues1[issueId];
            if (!this.Data.issues2[this.Data.CurrentUser.UserName].Contains(issue))
            {
                return string.Format("The issue with ID {0} does not belong to user {1}", issueId, this.Data.CurrentUser.UserName);
            }

            this.Data.RemoveIssue(issue);
            return string.Format("Issue {0} removed", issueId);
        }

        public string AddComment(int intValue, string stringValue)
        {
            if (this.Data.CurrentUser == null)
            {
                return string.Format("There is no currently logged in user");
            }

            if (!this.Data.issues1.ContainsKey(intValue + 1))
            {
                return string.Format("There is no issue with ID {0}", intValue + 1);
            }

            var issue = this.Data.issues1[intValue];
            var comment = new Comment(this.Data.CurrentUser, stringValue);
            issue.AddComment(comment);
            this.Data.dict[this.Data.CurrentUser].Add(comment);
            return string.Format("Comment added successfully to issue {0}", issue.Id);
        }

        public string GetMyIssues()
        {
            if (this.Data.CurrentUser == null)
            {
                return string.Format("There is no currently logged in user");
            }

            var issues = this.Data.issues2[this.Data.CurrentUser.UserName];
            var newIssues = issues;
            if (!newIssues.Any())
            {
                var result = string.Empty;
                foreach (var i in this.Data.issues2)
                {
                    result += i.Value.Select(iss => iss.Comments.Select(c => c.Text)).ToString();
                }

                return "No issues";
            }

            return string.Join(Environment.NewLine, newIssues.OrderByDescending(x => x.Priority).ThenBy(x => x.Title));
        }

        public string GetMyComments()
        {
            if (this.Data.CurrentUser == null)
            {
                return string.Format("There is no currently logged in user");
            }

            var comments = new List<Comment>();
            this.Data.issues1.Select(i => i.Value.Comments).ToList()
                .ForEach(item => comments.AddRange(item));
            var resultComments = comments
                .Where(c => c.Author.UserName == this.Data.CurrentUser.UserName)
                .ToList();
            var strings = resultComments
                .Select(x => x.ToString());
            if (!strings.Any())
            {
                return "No comments";
            }

            return string.Join(Environment.NewLine, strings);
        }

        public string SearchForIssues(string[] strings)
        {
            if (strings.Length < 0)
            {
                return "There are no tags provided";
            }

            var i = new List<Issue>();
            foreach (var t in strings)
            {
                i.AddRange(this.Data.issues4[t]);
            }

            if (!i.Any())
            {
                return "There are no issues matching the tags provided";
            }

            var newi = i.Distinct();
            if (!newi.Any())
            {
                return "No issues";
            }

            return string.Join(Environment.NewLine, newi.OrderByDescending(x => x.Priority).ThenBy(x => x.Title).Select(x => string.Empty));
        }
    }
}
