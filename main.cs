using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ConsoleApplication1
{
    class main
    {
        static string directory;
        static ConsoleKeyInfo pressed;
        public static void Load(string dir)
        {
            consoleParam.Initiate(dir);
            Data.Initiate(dir);
            Actions.movePtr(0);
            string directory = dir;
            Draw.Clear();
            Draw.Files(directory);
        }

        static void initiate()
        {
            Console.Title = "Far Manager";
            Console.SetWindowSize(consoleParam.initW, consoleParam.initH);
            Console.Clear();
            consoleParam.Initiate();
            directory = consoleParam.initDir;
            Data.Initiate(directory);
            Draw.Screen();
            Draw.Files(directory);
        }

        static void Main(string[] args)
        {
            initiate();
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    pressed = Console.ReadKey();
                    switch (pressed.Key)
                    {
                        case ConsoleKey.UpArrow: // UP
                            Actions.movePtr(-1);
                            break;
                        case ConsoleKey.DownArrow://DOWN
                            Actions.movePtr(1);
                            break;
                        case ConsoleKey.RightArrow://TO DIRECT | OPEN
                            directory = Actions.moveRight(directory);
                            break;
                        case ConsoleKey.LeftArrow://BACK
                            directory = Actions.moveLeft(directory);
                            break;
                        case ConsoleKey.F1:
                            break;
                        
                        case ConsoleKey.F2:  //CreateDirectory
                            Actions.createDirectory(directory);
                            break;
                        case ConsoleKey.F3:  //CreateFile
                            Actions.createFile(directory);
                            break;
                       
                        case ConsoleKey.D:   //DelteFile
                            if (pressed.Modifiers == ConsoleModifiers.Control)
                                Actions.delete(directory);
                            break;
                    }
                    Console.SetCursorPosition(0, 0);
                }
            }
        }
    }
}
