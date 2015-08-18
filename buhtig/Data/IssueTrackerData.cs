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
            this.users_dict = new Dictionary<string, User>();
            this.issues = new MultiDictionary<Issue, string>(true);
            this.issues1 = new OrderedDictionary<int, Issue>();
            this.issues2 = new MultiDictionary<string, Issue>(true);
            this.issues3 = new MultiDictionary<string, Issue>(true);
            this.dict = new MultiDictionary<User, Comment>(true);
            this.Comments = new Dictionary<Comment, User>();
        }

        public User CurrentUser { get; set; }

        public IDictionary<string, User> users_dict { get; set; }

        public MultiDictionary<Issue, string> issues { get; set; }

        public OrderedDictionary<int, Issue> issues1 { get; set; }

        public MultiDictionary<string, Issue> issues2 { get; set; }

        public MultiDictionary<string, Issue> issues3 { get; set; }

        public MultiDictionary<string, Issue> issues4 { get; set; }

        public MultiDictionary<User, Comment> dict { get; set; }

        public Dictionary<Comment, User> Comments { get; set; }

        public int AddIssue(Issue issue)
        {
            issue.Id = this.nextIssueId;
            this.issues1.Add(issue.Id, issue);
            this.nextIssueId++;
            this.issues2[this.CurrentUser.UserName].Add(issue);

            foreach (var tag in issue.Tags)
            {
                this.issues4[tag].Add(issue);
            }

            return issue.Id;
        }

        public void RemoveIssue(Issue issue)
        {
            this.issues2[this.CurrentUser.UserName].Remove(issue);

            foreach (var tag in issue.Tags)
            {
                this.issues4[tag].Remove(issue);
            }

            this.issues1.Remove(issue.Id);
            return;
        }
    }
}
