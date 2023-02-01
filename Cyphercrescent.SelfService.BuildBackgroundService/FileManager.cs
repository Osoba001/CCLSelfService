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
            CopyFileToDestination();
            UnzipFile();
            Launch_exeFile();
        }
        public void CreateDiretoryIfNotExist()
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
        }
        /// <summary>
        /// Unzip the copied file in the destination folder.
        /// </summary>
        public void UnzipFile()
        {
            try
            {
                ZipFile.ExtractToDirectory(Path.Join(_destinationPath, $"{_zipFileName}.zip"), _destinationPath, true);
            }
            catch 
            {

            }
        }
        private void Launch_exeFile()
        {
            var destinationDir = new DirectoryInfo($"{_destinationPath}{Path.DirectorySeparatorChar}");
            var exeFile = destinationDir
                .GetFiles("*.exe", SearchOption.TopDirectoryOnly)
                .FirstOrDefault();
            if (exeFile != null) 
                Process.Start(exeFile!.FullName);
        }
    }


}
