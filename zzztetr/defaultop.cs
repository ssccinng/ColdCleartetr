using System;
using System.Collections.Generic;
using System.Text;

namespace Jura_Knife_Tetris
{
    public static class defaultop
    {
        public static rule defrule = new rule(new int[] { 0, 1, 1, 2, 1 },
            new int[] { 0, 2, 4, 6 }, new int[] {0, 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 4, 5, 5 }, new int[] { 0, 0, 1, 2, 4 });
        public static mino_gene demino = new mino_gene();
    }
}
