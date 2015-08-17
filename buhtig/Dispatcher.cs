namespace IssueTrackerWebApp
{
    using Contracts;
    using Models;

    public class Dispatcher
    {
        Dispatcher(IIssueTracker tracker)
        {
            this.tracker = tracker;
        }
        public Dispatcher()
            : this(new IssueTracker())
        {
        }

        IIssueTracker tracker { get; set; }
        public string DispatchAction(IEndPoint endpoint)
        {
            switch (endpoint.aktionname)
            {
                case "RegisterUser":
                    return tracker.RegisterUser(endpoint.parametern["username"], endpoint.parametern["password"], endpoint.parametern["confirmPassword"]);
                case "LoginUser":
                    return tracker.LoginUser(endpoint.parametern["username"], endpoint.parametern["password"]);
                case "CreateIssue":
                    return tracker.CreateIssue(endpoint.parametern["title"], endpoint.parametern["description"],
                        (IssuePriority)System.Enum.Parse(typeof(IssuePriority), endpoint.parametern["priority"], true),
                        endpoint.parametern["tags"].Split('/'));
                case "RemoveIssue":
                    return tracker.RemoveIssue(int.Parse(endpoint.parametern["id"]));
                case "AddComment":
                    return tracker.AddComment(
                        int.Parse(endpoint.parametern["Id"]),
                        endpoint.parametern["text"]);
                case "MyIssues": return tracker.GetMyIssues();
                case "MyComments": return tracker.GetMyComments();
                case "Search":
                    return tracker.SearchForIssues(endpoint.parametern["tags"].Split('|'));
                default:
                    return string.Format("Invalid action: {0}", endpoint.aktionname);
            }
        }
    }
}
