using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyphercrescent.SelfService.BuildBackgroundService
{
    /// <summary>
    /// This class Copy the downloaded zip file to the destination folder, 
    /// Unzip the file and 
    /// Launch the .exe file in the unzip file.
    /// </summary>
    public class FileManager
    {
        private readonly string _zipFileName;
        private readonly string _destinationPath;
        private readonly string DownloadsFolderPath;
        public FileInfo? TargetZipFile => GetTargetZipFile();
        public DateTime? LastTimeCopied => GetFileCreationTime(_destinationPath);
        public DateTime? LastDownloadedTime => GetFileCreationTime(DownloadsFolderPath);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="zipFileName"> The target file name without extenion</param>
        /// <param name="sourcePath">The source full path (Downloads folder full path) </param>
        /// <param name="destinationPath">The full path of where the downloaded file should copy to</param>
        public FileManager(string zipFileName,string sourcePath, string destinationPath)
        {
            _zipFileName = zipFileName;
            _destinationPath = destinationPath;
            DownloadsFolderPath = sourcePath;
        }
        public void CopyUnzipAndLaunch()
        {
            if (Directory.Exists(DownloadsFolderPath) && LastDownloadedTime != null)
            {
                if (LastTimeCopied == null || LastTimeCopied < LastDownloadedTime)
                {
                    CopyFileToDestination();
                    UnzipFile();
                    Launch_exeFile();
                }
            }
            
        }
        private void CreateDiretoryIfNotExist()
        {
            if (!Directory.Exists(_destinationPath))
            {
                Directory.CreateDirectory(_destinationPath);
            }
        }
        /// <summary>
        /// Copy the downloaded zip file to the target folder (destination).
        /// </summary>
        public void CopyFileToDestination()
        {
            CreateDiretoryIfNotExist();
            File.Copy(Path.Join(DownloadsFolderPath, $"{_zipFileName}.zip"), Path.Join(_destinationPath, $"{_zipFileName}.zip"), true);
            File.SetCreationTime(Path.Join(_destinationPath, $"{_zipFileName}.zip"), DateTime.Now);
        }
        /// <summary>
        /// Unzip the copied file in the destination folder.
        /// </summary>
        public void UnzipFile()
        {
            ZipFile.ExtractToDirectory(Path.Join(_destinationPath, $"{_zipFileName}.zip"), _destinationPath, true);
        }
        private void Launch_exeFile()
        {
            var finalPath = Path.Join(_destinationPath,_zipFileName);
            var exeFile = new DirectoryInfo(finalPath).GetFiles("*.exe").FirstOrDefault();
            if (exeFile != null) 
                Process.Start(exeFile.FullName);
        }
        private DateTime? GetFileCreationTime(string directoryPath)
        {
            var file = new FileInfo(Path.Join(directoryPath, $"{_zipFileName}.zip"));
            return file?.CreationTime;
          

        }
        private FileInfo? GetTargetZipFile()
        {
         return new FileInfo(Path.Join(DownloadsFolderPath, $"{_zipFileName}.zip")); ;
        }
    }


}
