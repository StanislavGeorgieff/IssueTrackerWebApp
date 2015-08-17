namespace IssueTrackerWebApp.Data
{
    using System.Collections.Generic;

    using Contracts;
    using Models;
    using Wintellect.PowerCollections;

    public class IssueTrackerData : IIssueTrackerData
    {
        public int nextIssueId;
        public IssueTrackerData()
        {
            nextIssueId = 1;
            users_dict = new Dictionary<string, User>();

            issues = new MultiDictionary<Issue, string>(true);
            issues1 = new OrderedDictionary<int, Issue>();
            issues2 = new MultiDictionary<string, Issue>(true);
            issues3 = new MultiDictionary<string, Issue>(true);

            dict = new MultiDictionary<User, Comment>(true);
            kommentaren = new Dictionary<Comment, User>();
        }

        public User CurrentUser { get; set; }
        public IDictionary<string, User> users_dict { get; set; }
        public MultiDictionary<Issue, string> issues { get; set; }
        public OrderedDictionary<int, Issue> issues1 { get; set; }
        public MultiDictionary<string, Issue> issues2 { get; set; }
        public MultiDictionary<string, Issue> issues3 { get; set; }
        public MultiDictionary<string, Issue> issues4 { get; set; }
        public MultiDictionary<User, Comment> dict { get; set; }
        public Dictionary<Comment, User> kommentaren { get; set; }

        public int AddIssue(Issue p) { return 0; }

        public void RemoveIssue(Issue p) { return; }
    }
}
