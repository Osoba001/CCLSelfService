using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyphercrescent.BuildService.Test.Helpers
{
    public static class FileManagerHelper
    {
        public static void CreateDirIfNotExist(string path)
        {
            if (Directory.Exists(path))
            {
                return;
            }
            Directory.CreateDirectory(path);
        }

        public static void CreateFileInDir(string path, string fileName)
        {
            CreateDirIfNotExist(path);
            string filepath= Path.Combine(path, fileName);
            if (File.Exists(filepath))
            return;
            File.Create(filepath);
        }

        public static FileStream OpenAfile(string FullPath)
        {
            var file= new FileInfo(FullPath);
            FileStream stream= file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            return stream;
        }
        public static void OpenAfile1(string FullPath)
        {
            var file = new FileInfo(FullPath);
            FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
        }


        public static void Closefile(FileStream stream)
        {
            stream.Close();
            stream.Dispose();
        }
    }
}
