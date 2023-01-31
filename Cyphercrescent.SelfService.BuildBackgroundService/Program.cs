using Cyphercrescent.SelfService.BuildBackgroundService;
using Topshelf;

class Program
{
    static void Main()
    {
        var exitCode = HostFactory.Run(x =>
        {
            x.Service<DownloadFolderSelfService>(s =>
            {
                s.ConstructUsing(sv => new DownloadFolderSelfService());
                s.WhenStarted(sv => sv.StartWatching());
                s.WhenStopped(sv => sv.Stop());
            });
            x.RunAsLocalSystem();

            x.SetServiceName("SEPAL-Service");
            x.SetDisplayName("SEPAL Self-Service");
            x.SetDescription("A Service that create a new build and launch the .exe file SEPAL for every new download");
        });

        int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
        Environment.ExitCode = exitCodeValue;
    }
}