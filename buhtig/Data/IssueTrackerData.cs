namespace IssueTrackerWebApp.Data
{
    using System.Collections.Generic;
    using Contracts;
    using Models;
    using Wintellect.PowerCollections;

    public class IssueTrackerData : IIssueTrackerData
    {
        private int nextIssueId;

        public IssueTrackerData()
        {
            this.nextIssueId = 1;
            this.UsersByName = new Dictionary<string, User>();
            this.IssuesByTag= new MultiDictionary<string, Issue>(true);
            this.issues = new MultiDictionary<Issue, string>(true);
            this.IssueById = new OrderedDictionary<int, Issue>();
            this.IssuesByUsers = new MultiDictionary<string, Issue>(true);
            this.issues3 = new MultiDictionary<string, Issue>(true);
            this.CommentByUser = new MultiDictionary<User, Comment>(true);
            this.Comments = new Dictionary<Comment, User>();
        }

        public User CurrentUser { get; set; }

        public IDictionary<string, User> UsersByName { get; set; }

        public MultiDictionary<Issue, string> issues { get; set; }

        public OrderedDictionary<int, Issue> IssueById { get; set; }

        public MultiDictionary<string, Issue> IssuesByUsers { get; set; }

        public MultiDictionary<string, Issue> issues3 { get; set; }

        public MultiDictionary<string, Issue> IssuesByTag { get; set; }

        public MultiDictionary<User, Comment> CommentByUser { get; set; }

        public Dictionary<Comment, User> Comments { get; set; }

        public int AddIssue(Issue issue)
        {
            issue.Id = this.nextIssueId;
            this.IssueById.Add(issue.Id, issue);
            this.nextIssueId++;
            this.IssuesByUsers[this.CurrentUser.UserName].Add(issue);

            foreach (var tag in issue.Tags)
            {
                if (this.IssuesByTag.ContainsKey(tag))
                {
                    this.IssuesByTag[tag].Add(issue);
                }
                else
                {
                    this.IssuesByTag.Add(tag,issue);
                }
            }

            return issue.Id;
        }

        public void RemoveIssue(Issue issue)
        {
            this.IssuesByUsers[this.CurrentUser.UserName].Remove(issue);

            foreach (var tag in issue.Tags)
            {
                this.IssuesByTag[tag].Remove(issue);
            }

            this.IssueById.Remove(issue.Id);
            return;
        }
    }
}
