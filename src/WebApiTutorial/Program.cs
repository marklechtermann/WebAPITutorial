namespace WebApiTutorial
{
    using System;
    using System.Diagnostics;
    using System.Security.Principal;
    using Microsoft.Owin.Hosting;

    class Program
    {
        private static readonly int port = 2017;

        static void Main(string[] args)
        {
            Console.WriteLine("Running...");
            Console.WriteLine("Press any key to exit.");

            // To set the UrlACL ...
            //EnsureUrlAclIsSet(port);

            using (WebApp.Start(new StartOptions($"http://*:{port}")))
            {
                Console.ReadLine();
            }
        }

        private static void EnsureUrlAclIsSet(int port)
        {
            var windowsPrincipal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            bool hasAdministrativeRight = windowsPrincipal.IsInRole(WindowsBuiltInRole.Administrator);
            if (!hasAdministrativeRight)
            {
                var processInfo = new ProcessStartInfo();
                processInfo.Verb = "runas";
                processInfo.FileName = "netsh";
                processInfo.Arguments = $"http add urlacl url=http://*:{port}/ user=Everyone listen=yes";
                try
                {
                    Process.Start(processInfo);
                }
                catch (Exception e)
                { 
                  // ToDo: catch something...
                }
            }
        }
    }
}