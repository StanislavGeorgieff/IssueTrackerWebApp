namespace IssueTrackerWebApp.Models
{
    using System.Net;
    using Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class EndPoint : IEndPoint
    {
        public EndPoint(string s)
        {
            int questionMark = s.IndexOf('?');
            if (questionMark != -1)
            {
                this.aktionname = s.Substring(0, questionMark);
                var pairs = s.Substring(questionMark + 1).Split('&').Select(x => x.Split('=').Select(xx => WebUtility.UrlDecode(xx)).ToArray());
                var parameters = new Dictionary<string, string>();
                foreach (var pair in pairs)
                    parameters.Add(pair[0], pair[1]);
                this.parametern = parameters;
            }
            else
                this.aktionname = s;
        }
        public string aktionname { get; set; }
        public IDictionary<string, string> parametern { get; set; }

    }
}
