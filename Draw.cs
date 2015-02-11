using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ConsoleApplication1
{
    class Draw
    {

        static void hrLine(int H)
        {
            for (int i = 0; i < consoleParam.initW; i++)
                editCell(new Cell(i, H), ConsoleColor.Magenta, "-", ConsoleColor.Blue);
        }

        static void vrLine(int W)
        {
            for (int i = 0; i < consoleParam.initH; i++)
                editCell(new Cell(W, i), ConsoleColor.Magenta, "|", ConsoleColor.Blue);
        }

        static void editCell(Cell c, ConsoleColor color, string s, ConsoleColor bgColor = ConsoleColor.Black)
        {
            Console.SetCursorPosition(c.x, c.y);
            Console.ForegroundColor = color;
            Console.BackgroundColor = bgColor;
            Console.Write(s);
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            
        }

        static void drawBorder()
        { 
            int W = consoleParam.initW;
            int H = consoleParam.initH;
            vrLine(0); vrLine(W - 1);
          
            foreach (KeyValuePair<string, Cell> x in consoleParam.block)
                hrLine(x.Value.y - 1);
            hrLine(H - 1);
        }

        public static void drawActions()
        {
            Cell FillPosition = new Cell(consoleParam.block["actions"]);
            FillPosition.x++;
            foreach (string s in consoleParam.actions)
            {
                editCell(FillPosition, ConsoleColor.White, s, ConsoleColor.DarkCyan);
                FillPosition.x += s.Length + 1;
            }
        }

        public static void drawDirectory(Cell fillPosition, DirectoryInfo directory, ConsoleColor color = ConsoleColor.Black)
        {
            fillPosition.x++;
           if (directory.GetFiles().ToList().Count == 0 && directory.GetDirectories().ToList().Count == 0)
                editCell(fillPosition, ConsoleColor.DarkGreen, "[+] ", color);
            else
                editCell(fillPosition, ConsoleColor.Green, "[+] ", color);

            editCell(new Cell(fillPosition.x + 4, fillPosition.y), ConsoleColor.Gray, Func.Short(directory.Name, 45), color);
        }

        public static void drawFile(Cell fillPosition, FileInfo file, ConsoleColor color = ConsoleColor.Black)
        {
            fillPosition.x++;
            editCell(fillPosition, ConsoleColor.White, Func.Short(file.Name, 45), color); ;
        } 

        public static void drawInfo(FileSystemInfo file, bool clear = false)
        {
            ConsoleColor Color1 = ConsoleColor.Green,
                         Color2 = ConsoleColor.Yellow,
                         Color3 = ConsoleColor.Red;
            if (clear)
                Color1 = Color2 = Color3 = ConsoleColor.Black;
            Cell fillPosition = new Cell(consoleParam.block["curFileInfo"]);
            editCell(fillPosition + new Cell(1, 1), Color1, "NAME:");
            editCell(fillPosition + new Cell(7, 1), Color2, Func.Short(file.Name));
            editCell(fillPosition + new Cell(1, 2), Color1, "PATH:");
            editCell(fillPosition + new Cell(7, 2), Color2, Func.Short(file.FullName));
            editCell(fillPosition + new Cell(1, 3), Color1, "CREATED:");
            editCell(fillPosition + new Cell(10, 3), Color2, Func.fullTime(file.CreationTime));
            editCell(fillPosition + new Cell(1, 4), Color1, "LAST MODIFIED:");
            editCell(fillPosition + new Cell(16, 4), Color2, Func.fullTime(file.LastWriteTime));
            
            editCell(new Cell(2, consoleParam.initH - 6), Color3, "CURRENT DIRECTORY:" +
                              Func.Short(file.FullName.Substring(0, file.FullName.Length - file.Name.Length)));
        }

        public static void markPtr(ConsoleColor color = ConsoleColor.Black, bool clear = false)
        {
            if (Data.directoryItems.Count == 0)
                return;
            Cell fillPosition = new Cell(consoleParam.block["dirFiles"]);

            if (Data.directoryItems[Data.ptr].GetType() == typeof(FileInfo))
                drawFile(new Cell(fillPosition.x, fillPosition.y + Data.ptr), (FileInfo)Data.directoryItems[Data.ptr], color);
            else
                drawDirectory(new Cell(fillPosition.x, fillPosition.y + Data.ptr), (DirectoryInfo)Data.directoryItems[Data.ptr], color);
            drawInfo(Data.directoryItems[Data.ptr], clear);
        }

        public static void Files(string dir)
        {
            Cell fillPosition = new Cell(consoleParam.block["dirFiles"]);

            //SHOW directories-----------------------------
            foreach (DirectoryInfo directory in Data.directories)
            {
                drawDirectory(new Cell(fillPosition), directory);
                fillPosition.y++;
            }
            //SHOW FILES----------------------------
            foreach (FileInfo file in Data.files)
            {
                drawFile(new Cell(fillPosition), file);
                fillPosition.y++;
            }
            markPtr(ConsoleColor.Red);
        }

        public static void Screen()
        {
            drawBorder();
            drawActions();
        }

        public static void Clear()
        {
            int W = consoleParam.initW;
            int H = consoleParam.initH;
            Dictionary<int, int> was = new Dictionary<int, int>();
            foreach (KeyValuePair<string, Cell> X in consoleParam.block)
                was[X.Value.y - 1] = 1;
            string s = "";
            for (int i = 0; i < W - 2; i++ )
                s += " ";
            for (int i = 1; i < H - 1; i++)
            {
                if (was.ContainsKey(i))
                    continue;
                editCell(new Cell(1, i), ConsoleColor.Black, s); 
            }
            drawActions();
        }

        public static void Action(string s)
        {
            int len = s.Length, W = consoleParam.initW;
            editCell(new Cell((W - len ) / 2, consoleParam.block["actionInfo"].y), ConsoleColor.White, s);
        }
    }
}
