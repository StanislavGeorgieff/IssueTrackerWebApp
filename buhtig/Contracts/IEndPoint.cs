namespace IssueTrackerWebApp.Contracts
{
    using System.Collections.Generic;

    public interface IEndPoint
    {
        string ActionName
        {
            get;
        }

        IDictionary<string, string> Parameters { get; }
    }
}
