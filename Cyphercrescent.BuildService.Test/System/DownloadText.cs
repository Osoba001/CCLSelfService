
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
        string sourePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}{Path.DirectorySeparatorChar}CClBgSelfService{Path.DirectorySeparatorChar}Source";
        string destinationPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}{Path.DirectorySeparatorChar}CClBgSelfService{Path.DirectorySeparatorChar}Destination";
        [Fact]
        private void HasDownloadCompleted_ReturnFalseIfNotCompleted_TrueWhenCompleted()
        {

            FileManagerHelper.CreateFileInDir(sourePath, "MyTestFile.zip");
            string filePath = Path.Join(sourePath, "MyTestFile.zip");
            FileStream stream= FileManagerHelper.OpenAfile(filePath);
            DownloadFolderSelfService SelfService=new DownloadFolderSelfService();
            var res=SelfService.HasDownloadCompleted(filePath);
            res.Should().Be(false);
            FileManagerHelper.Closefile(stream);
            var res2 = SelfService.HasDownloadCompleted(filePath);
            res2.Should().Be(true);
        }


    }
}
