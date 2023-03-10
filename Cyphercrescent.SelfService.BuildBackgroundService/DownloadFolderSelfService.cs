using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyphercrescent.SelfService.BuildBackgroundService
{
    /// <summary>
    /// This class has the FileSystemWatcher that is watching the Downloads folder.
    /// It has the StartWatching method and Stop a method.
    /// </summary>
    public class DownloadFolderSelfService
    {
        readonly string Destination = "C:/ProgramData/CypherCrescent/builds";
        private readonly string ZipFileName = "WPF_BackgroundServices_App-master";
        private FileInfo file=null; 
        private string SourceFolder => $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}{Path.DirectorySeparatorChar}Downloads";
        FileSystemWatcher watcher=null;

        /// <summary>
        /// This method starts the file watcher and fire an event whenever 
        /// a new file or folder is created on the target directory (SourceFolder).
        /// The Create event handler (CreateHandler) method wait for the file to 
        /// download complete before calling the CopyUnzipAndLaunch method in the FileManager class.
        /// </summary>
        public void StartWatching()
        {
            watcher = new FileSystemWatcher
            {
                Path = SourceFolder,
                EnableRaisingEvents = true
            };
            watcher.Created+= CreateHandler;
        }
        private async void CreateHandler(object sender, FileSystemEventArgs e)
        {
            file= new FileInfo(e.FullPath);
            if (file.Extension==".tmp")
            {
                bool isComp=false;
                while (!isComp)
                {
                    isComp = new FileInfo(file.FullName).Exists;
                    if (isComp)
                    {
                        await Task.Delay(10000);
                    }
                }
                var fileManger = new FileManager(ZipFileName, SourceFolder, Destination);
                fileManger.CopyUnzipAndLaunch();
            }
            
        }
        /// <summary>
        /// This method  stops the file watcher and dispose it.
        /// </summary>
        public void Stop()
        {
            watcher.Dispose();
        }

    }
}
