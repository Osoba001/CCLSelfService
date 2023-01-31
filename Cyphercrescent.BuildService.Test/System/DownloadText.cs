
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
        [Fact]
        private void HasDownloadCompleted_ReturnFalse()
        {
            var path = Environment.CurrentDirectory + "/Fixture";
            //var res= Directory.Exists(path);

            path.Should().Be("/Fixture");
        }
    }
}
