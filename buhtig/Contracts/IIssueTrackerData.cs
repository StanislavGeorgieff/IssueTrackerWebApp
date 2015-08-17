namespace IssueTrackerWebApp.Contracts
{
    using System.Collections.Generic;
    using Models;
    using Wintellect.PowerCollections;

    interface IIssueTrackerData
    {
        User CurrentUser { get; set; }
        IDictionary<string, User> users_dict { get; }
        OrderedDictionary<int, Issue> issues1 { get; }
        MultiDictionary<string, Issue> issues2 { get; }
        MultiDictionary<string, Issue> issues4 { get; }
        MultiDictionary<User, Comment> dict { get; }
        int AddIssue(Issue p);
        void RemoveIssue(Issue p);
    }
}
