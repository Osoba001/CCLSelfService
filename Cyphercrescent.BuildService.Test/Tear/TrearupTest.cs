using FluentAssertions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Cyphercrescent.BuildService.Test.Tear
{
    public class TrearupTest
    {
        string downloadFolder= $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}{Path.DirectorySeparatorChar}Downloads";
        string source= Path.Join(AppDomain.CurrentDomain.BaseDirectory, "Downloads");
        string ZipFileName = "WPF_BackgroundServices_App-master";
        [Fact]
        public void AddTargetFileToSoureFolder()
        {
            if (!Directory.Exists(source))
            {
                Directory.CreateDirectory(source);
            }
           File.Copy(Path.Join(downloadFolder,ZipFileName+".zip"), Path.Join(source, ZipFileName + ".zip"), true);
            var res=File.Exists(Path.Join(source, ZipFileName + ".zip"));
            res.Should().BeTrue();
        }
    }
}
