using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyphercrescent.SelfService.BuildBackgroundService
{
    /// <summary>
    /// This class has the FileSystemWatcher that is watching the Downloads folder.
    /// It has the StartWatching method, Stop and 
    /// a method that is checking if the downloading file has completed
    /// </summary>
    public class DownloadFolderSelfService
    {
        readonly string Destination = "C:/ProgramData/CypherCrescent/builds";
        private readonly string ZipFileName = "WPF_BackgroundServices_App-master";
        private const int MAX_NO_TRIALS = 10;
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
            watcher = new FileSystemWatcher();
            watcher.Path = SourceFolder;
            watcher.EnableRaisingEvents = true;
            watcher.Created+= CreateHandler;
        }
       
        private void CreateHandler(object sender, FileSystemEventArgs e)
        {
            int n = 0;
            while (n < MAX_NO_TRIALS)
            {
                n++;
                if (HasDownloadCompleted(e.FullPath))
                {
                    if (file.Name==$"{ZipFileName}.zip")
                    {
                        var fileManger = new FileManager(ZipFileName,SourceFolder, Destination);
                        fileManger.CopyUnzipAndLaunch();
                    }
                    n = MAX_NO_TRIALS;
                }else
                    Task.Delay(n*10000);
                
            }
        }
        /// <summary>
        /// This method  stops the file watcher and dispose it.
        /// </summary>
        public void Stop()
        {
            watcher.Dispose();
        }

        /// <summary>
        /// This method is called before 
        /// </summary>
        /// <param name="fileFullPath">The downloading file full path.</param>
        /// <returns>True if the download has completed</returns>
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
