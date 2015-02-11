using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Cell
    {
        public int x, y;
        public Cell(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public Cell(Cell a)
        {
            this.x = a.x;
            this.y = a.y;
        }

        public bool equal(Cell c)
        {
            return c.x == this.x && c.y == this.y;
        }

        public static Cell operator +(Cell c1, Cell c2)
        {
            Cell buf = new Cell(c1.x + c2.x, c1.y + c2.y);
            return buf;
        }
    }
}
