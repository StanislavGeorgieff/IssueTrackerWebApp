namespace IssueTrackerWebApp.Execution
{
    using System;
    using IssueTrackerWebApp.Contracts;

    public class Engine : IEngine
    {
        private Dispatcher d;

        public Engine(Dispatcher d)
        {
            this.d = d;
        }

        public Engine()
            : this(new Dispatcher())
        {
        }

        public void Run()
        {
            while (true)
            {
                string url = Console.ReadLine();
                if (url == null)
                {
                    break;
                }

                url = url.Trim();
                if (!string.IsNullOrEmpty(url))
                {
                    try
                    {
                        var ep = new EndPoint(url);
                        string viewResult = this.d.DispatchAction(ep);
                        Console.WriteLine(viewResult);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}