using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyphercrescent.SelfService.BuildBackgroundService
{
    public class FileManager
    {
        private readonly string _zipFileName;
        private readonly string _destinationPath;
        public static string DownloadsFolderPath => $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}{Path.DirectorySeparatorChar}Downloads";
       
        public FileManager(string zipFileName, string destinationDirectory)
        {
            _zipFileName = zipFileName;
            _destinationPath = destinationDirectory;
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

        private void CopyFileToDestination()
        {
            CreateDiretoryIfNotExist();
            File.Copy(Path.Join(DownloadsFolderPath, $"{_zipFileName}.zip"), Path.Join(_destinationPath, $"{_zipFileName}.zip"), true);
        }

        private void UnzipFile()
        {
            ZipFile.ExtractToDirectory(Path.Join(_destinationPath, $"{_zipFileName}.zip"), _destinationPath);
        }

        private void Launch_exeFile()
        {
            var destinationDir = new DirectoryInfo($"{_destinationPath}{Path.DirectorySeparatorChar}");
            var exeFile = destinationDir
                .GetFiles("*.exe", SearchOption.TopDirectoryOnly)
                .FirstOrDefault();
            Process.Start(exeFile!.FullName);
        }
    }


}
