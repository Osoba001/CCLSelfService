
using Cyphercrescent.BuildService.Test.Helpers;
using Cyphercrescent.SelfService.BuildBackgroundService;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Cyphercrescent.BuildService.Test.System
{
    public class DownloadText
    {
        static string sourePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}{Path.DirectorySeparatorChar}CClBgSelfService{Path.DirectorySeparatorChar}Source";
        static string destinationPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}{Path.DirectorySeparatorChar}CClBgSelfService{Path.DirectorySeparatorChar}Destination";
        static string ZipFileName = "WPF_BackgroundServices_App-master";
        string fileFullNameInSource=Path.Join(sourePath, ZipFileName + ".zip");
        string fileFullNameInDest=Path.Join(destinationPath, ZipFileName + ".zip");
        [Fact]
        private void HasDownloadCompleted_ReturnFalseIfNotCompleted_TrueWhenCompleted()
        {
            //FileManagerHelper.CreateFileInDir(sourePath, ZipFileName);
            string filePath = Path.Join(sourePath, ZipFileName + ".zip");
            FileStream stream= FileManagerHelper.OpenAfile(filePath);
            DownloadFolderSelfService SelfService=new DownloadFolderSelfService();
            var res=SelfService.HasDownloadCompleted(filePath);
            res.Should().Be(false);
            FileManagerHelper.Closefile(stream);
            var res2 = SelfService.HasDownloadCompleted(filePath);
            res2.Should().Be(true);
        }

        [Fact]
        private void CopyFile_ReturnSoccess_IfFileAtSourceEqual_DestFile()
        {
            FileManager fileManager = new FileManager(ZipFileName, sourePath, destinationPath);
            var file = new FileInfo(fileFullNameInSource);
            fileManager.CopyFileToDestination();
            var expectedFile=new FileInfo(fileFullNameInDest);
            file.Length.Should().Be(expectedFile.Length);
            file.Name.Should().Be(expectedFile.Name);
        }

        [Fact]
        private void UnZipFile_SuccessIfContainExeFile()
        {
            FileManager fileManager = new FileManager(ZipFileName, sourePath, destinationPath);
            fileManager.UnzipFile();
            var dir=new DirectoryInfo(Path.Join(destinationPath,ZipFileName));
            var exeFile= dir.GetFiles("*.exe").FirstOrDefault();
            exeFile.Should().NotBeNull();
            exeFile.Exists.Should().BeTrue();
            
        }
    }
}
