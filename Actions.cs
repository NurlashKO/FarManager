using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ConsoleApplication1
{
    class Actions
    {
        public static string moveRight(string directory)
        {
            Data.remPtr[directory] = Data.ptr;
            if (Data.CurrentFile() == null)
                return directory;
            FileSystemInfo curFile = Data.CurrentFile();
            if (curFile.GetType() == typeof(DirectoryInfo))
            {
                directory = curFile.FullName;
                main.Load(directory);
            }
            if (curFile.GetType() == typeof(FileInfo))
                System.Diagnostics.Process.Start(curFile.FullName);
            return directory;
        }
        public static string moveLeft(string directory)
        {
            Data.remPtr[directory] = Data.ptr;
            DirectoryInfo direct = new DirectoryInfo(directory);
            if (direct.Parent != null)
            {
                directory = direct.Parent.FullName;
                main.Load(directory);
            }
            return directory;
        }
        public static void movePtr(int x)
        {
            Data.ptr = Math.Min(Data.ptr, Data.directoryItems.Count - 1);
            if (Data.ptr + x < 0 || Data.ptr + x >= Data.directoryItems.Count || !Data.directoryItems[Data.ptr].Exists)
                return;
            //Console.WriteLine(Data.ptr + " " + x);
            //return;
            Draw.markPtr(ConsoleColor.Black, true);
            Data.ptr += x;
            Draw.markPtr(ConsoleColor.Red);
        }
        public static void createDirectory(string directory)
        {
            try
            {
                string name = Actions.read("Enter the name of the folder:");
                DirectoryInfo newDir = new DirectoryInfo(@"" + (directory + "\\" + name));
                newDir.Create();
            }
            catch { };
            main.Load(directory);
        }
        public static void createFile(string directory)
        {
            try
            {
                string name = Actions.read("Enter the name of the file:");
                FileInfo newDir = new FileInfo(@"" + (directory + "\\" + name));
                newDir.Create();
            }
            catch { };
            main.Load(directory);
        }
        public static void delete(string directory)
        {
            if (Data.directoryItems.Count == 0)
                return;
            movePtr(0);
            GC.Collect();
            FileSystemInfo item = Data.directoryItems[Data.ptr];
            if (item.GetType() == typeof(DirectoryInfo))
                (new DirectoryInfo(item.FullName)).Delete(true);
            else
                item.Delete();
            main.Load(directory);
        }
        public static string read(string message)
        {
            int st = consoleParam.initW / 3;
            Draw.Action(message);
            Console.SetCursorPosition(st, consoleParam.block["actionInfo"].y + 3);
            ConsoleKeyInfo pressed;
            string res = "";
            while (true)
            {
                pressed = Console.ReadKey();
                ConsoleKey key = pressed.Key;

                if (key != ConsoleKey.Escape && key != ConsoleKey.Enter && key != ConsoleKey.Backspace)
                    res += pressed.KeyChar;
                else 
                {
                    if (key == ConsoleKey.Backspace)
                    {
                        Console.Write(" ");
                        if (Console.CursorLeft > st)
                        {
                            res = res.Remove(res.Length - 1);
                            Console.CursorLeft--;
                        }
                        continue;
                    }
                    if (pressed.Key == ConsoleKey.Escape)
                        return "";
                    break;
                }
            }
            return res;
        }
    }
}
