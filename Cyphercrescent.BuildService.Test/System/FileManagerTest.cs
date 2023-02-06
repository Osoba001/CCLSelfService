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
    public class FileManagerTest
    {
        string sourePath => Path.Join(AppDomain.CurrentDomain.BaseDirectory, "Downloads");
        string destinationPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}{Path.DirectorySeparatorChar}CClBgSelfService{Path.DirectorySeparatorChar}Destination";
        string ZipFileName = "WPF_BackgroundServices_App-master";
        [Fact]
        private void CopyFile_ReturnSoccess_IfFileAtSourceEqual_DestFile()
        {
            FileManager fileManager = new FileManager(ZipFileName, sourePath, destinationPath);
            var file = new FileInfo(Path.Join(sourePath, ZipFileName + ".zip"));
            fileManager.CopyFileToDestination();
            var expectedFile = new FileInfo(Path.Join(destinationPath, ZipFileName + ".zip"));
            file.Length.Should().Be(expectedFile.Length);
            file.Name.Should().Be(expectedFile.Name);
        }

        [Fact]
        private void UnZipFile_SuccessIfContainExeFile()
        {
            FileManager fileManager = new FileManager(ZipFileName, sourePath, destinationPath);
            fileManager.UnzipFile();
            var dir = new DirectoryInfo(Path.Join(destinationPath, ZipFileName));
            var exeFile = dir.GetFiles("*.exe").FirstOrDefault();
            exeFile.Should().NotBeNull();
            exeFile.Exists.Should().BeTrue();
        }
    }
}
