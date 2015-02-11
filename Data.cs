using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ConsoleApplication1
{
    class Data
    {
        public static List<FileInfo> files = new List<FileInfo>();
        public static DirectoryInfo curDir;
        public static List<DirectoryInfo> directories = new List<DirectoryInfo>();
        public static List<FileSystemInfo> directoryItems = new List<FileSystemInfo>();
        public static int ptr;
        public static Dictionary<string, int> remPtr =
            new Dictionary<string, int>();

        public static FileSystemInfo CurrentFile()
        {
            if (directoryItems.Count == 0)
                return null;
            return directoryItems[ptr];
        }

        public static void Initiate(string dir)
        {
            curDir = new DirectoryInfo(dir);
            files = curDir.GetFiles().ToList();
            directories = curDir.GetDirectories().ToList();
            directoryItems.Clear();
            directoryItems.AddRange(directories);
            directoryItems.AddRange(files);
            ptr = remPtr.ContainsKey(dir) ? remPtr[dir] : 0;
            ptr = Math.Min(directoryItems.Count - 1, ptr);
            return;
        }

    }
}
