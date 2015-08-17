namespace IssueTrackerWebApp
{
    using System.Globalization;
    using System.Threading;

    using IssueTrackerWebApp;

    internal class Program
    {
        private static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            var e = new Engine();
            e.Run();
        }
    }
}
