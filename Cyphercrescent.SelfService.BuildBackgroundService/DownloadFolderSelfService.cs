using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyphercrescent.SelfService.BuildBackgroundService
{
    
    public class DownloadFolderSelfService
    {
        readonly string Destination = "C:/ProgramData/CypherCrescent/builds";
        private string ZipFileName = "WPF_BackgroundServices_App-master";
        private const int MAX_NO_TRIALS = 10;
        private FileInfo file=null; 
        private string SourceFolder => $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}{Path.DirectorySeparatorChar}Downloads";
        FileSystemWatcher watcher=new();

        public void StartWatching()
        {
            watcher.Path = SourceFolder;
            watcher.EnableRaisingEvents = true;
            watcher.Created+= CreateHandler;
        }

        private void CreateHandler(object sender, FileSystemEventArgs e)
        {
            int n = 0;
            while (n < MAX_NO_TRIALS)
            {
                if (HasDownloadCompleted(e.FullPath))
                {
                    if (file.Name==$"{ZipFileName}.zip")
                    {
                        var fileManger = new FileManager(ZipFileName,SourceFolder, Destination);
                        fileManger.CopyUnzipAndLaunch();
                    }
                    n = MAX_NO_TRIALS;
                }else
                    Task.Delay(10000);
                n++;
            }
        }

        public void Stop()
        {
            watcher.Dispose();
        }

        public bool HasDownloadCompleted(string fileFullPath)
        {
            file = new FileInfo(fileFullPath);
            if (file.Extension=="")
                return true;
            bool isComplete = false;
            FileStream stream=null;
            try
            {
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
                isComplete = true;
            }
            catch
            {

            }
            finally
            {
                stream?.Close();
                stream?.Dispose();
            }
            return isComplete;
        }
    }
}
