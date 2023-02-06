using FluentAssertions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Cyphercrescent.BuildService.Test.Teardown
{
    public class TeardownTest
    {
        string destinationPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}{Path.DirectorySeparatorChar}CClBgSelfService{Path.DirectorySeparatorChar}Destination";
      
        [Fact]
        private void Teardown_DirExist_ReturnFalse()
        {
            Directory.Delete(destinationPath, true);
            var res = Directory.Exists(destinationPath);
            res.Should().BeFalse();

        }
    }
}
