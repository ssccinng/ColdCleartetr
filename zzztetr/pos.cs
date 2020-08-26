using System;
using System.Collections.Generic;
using System.Text;

namespace Jura_Knife_Tetris
{
    public class pos
    {
        public int x, y;
        public pos(int x = 0, int y = 0)
        {
            this.x = x;
            this.y = y;
        }

        public void xadd(int delta)
        {
            this.x += delta;
        }

        public void yadd(int delta)
        {
            this.y += delta;
        }

        public pos clone()
        {
            return new pos(x, y);
        }
    }
}
