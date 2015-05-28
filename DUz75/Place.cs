using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineReaper
{
    public class Place
    {
        public int Row { get; private set; }
        public int Col { get; private set; }
        public int Value { get; set; }
        public bool IsVisible { get; set; }
        public bool IsMined { get; set; }
        public bool IsReavealing { get; set; }

        public Place(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
        }
    }
}
