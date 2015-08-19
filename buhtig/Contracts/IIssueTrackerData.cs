namespace IssueTrackerWebApp.Contracts
{
    using System.Collections.Generic;
    using Models;
    using Wintellect.PowerCollections;

    public interface IIssueTrackerData
    {
        User CurrentUser { get; set; }

        IDictionary<string, User> UsersByName{ get; }

        OrderedDictionary<int, Issue> IssueById { get; }

        MultiDictionary<string, Issue> IssuesByUsers { get; }

        MultiDictionary<string, Issue> IssuesByTag { get; }

        MultiDictionary<User, Comment> CommentByUser { get; }

        int AddIssue(Issue issue);

        void RemoveIssue(Issue issue);
    }
}
