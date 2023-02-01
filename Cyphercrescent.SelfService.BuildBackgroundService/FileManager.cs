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
        private readonly string DownloadsFolderPath; 
       
        public FileManager(string zipFileName,string sourceDirectory, string destinationDirectory)
        {
            _zipFileName = zipFileName;
            _destinationPath = destinationDirectory;
            DownloadsFolderPath = sourceDirectory;
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

        public void CopyFileToDestination()
        {
            CreateDiretoryIfNotExist();
            File.Copy(Path.Join(DownloadsFolderPath, $"{_zipFileName}.zip"), Path.Join(_destinationPath, $"{_zipFileName}.zip"), true);
        }

        public void UnzipFile()
        {
            ZipFile.ExtractToDirectory(Path.Join(_destinationPath, $"{_zipFileName}.zip"), _destinationPath,true);
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
