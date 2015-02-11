using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace ConsoleApplication1
{
    class consoleParam
    {
        public const int initW = 56;
        public const int initH = 50;
        public static string initDir;
        public static Dictionary<string, Cell> block =
        new Dictionary<string, Cell>();
        public static List<string> actions = new List<string>();

        static void initBlocks()
        {
            block["dirFiles"] = new Cell(1, 1);
            block["actionInfo"] = new Cell(1, initH / 2);
            block["curFileInfo"] = new Cell(1, initH / 3 * 2);
            block["actions"] = new Cell(1, initH - 2);
        }

        static void initActions()
        {
            actions.Clear();
            actions.Add("F1-Help");
            actions.Add("F2-MkFolder");
            actions.Add("F3-MkFile");
            actions.Add("Ctr+D-Remove");
            actions.Add("F4-Search");
        }

        public static void Initiate()
        {
            initDir = Environment.CurrentDirectory;
            initBlocks();
            initActions();
        }

        public static void Initiate(string dir)
        {
            initDir = dir;
            initBlocks();
            initActions();
        }
    }
}
