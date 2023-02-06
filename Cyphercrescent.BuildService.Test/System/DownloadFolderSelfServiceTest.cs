
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
    public class DownloadFolderSelfServiceTest
    {
        string sourePath => Path.Join(AppDomain.CurrentDomain.BaseDirectory,"Downloads");
        string ZipFileName = "WPF_BackgroundServices_App-master";

        [Fact]
        private void HasDownloadCompleted_ReturnFalseIfNotCompleted_TrueIfCompleted()
        {
            FileManagerHelper helper= new FileManagerHelper();
            var file = new FileInfo(Path.Join(sourePath, ZipFileName + ".zip"));
            DownloadFolderSelfService SelfService = new();
            using (FileStream stream=File.OpenRead(file.FullName)) 
            {
                var res = SelfService.HasDownloadCompleted(file.CreationTime);
                res.Should().Be(false);
            }
            var res2 = SelfService.HasDownloadCompleted(file.CreationTime);
            res2.Should().Be(true);
        }

        
        
    }
}
