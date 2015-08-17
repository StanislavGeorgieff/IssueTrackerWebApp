namespace IssueTrackerWebApp.Contracts
{
    using System.Collections.Generic;

    public interface IEndPoint
    {
        string aktionname { get; } 
        IDictionary<string, string> parametern { get; }
    }
}
