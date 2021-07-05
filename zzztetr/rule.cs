using System;
using System.Collections.Generic;
using System.Text;

namespace Jura_Knife_Tetris
{
    public class rule
    {
        //public rule default_rull = new rule(new int[] { 1, 1, 2, 1 }, 
        //    new int[] { 2, 4, 6 }, new int[] { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 4, 5, 5 }, new int[] { 0, 1, 2, 4 });
        
        int[] back_to_back;
        int[] Tspin;

        public int[] ren;

        int[] clear;

        public rule(int[] back_to_back, int[] Tspin, int[] ren, int[] clear)
        {
            this.back_to_back = back_to_back;
            this.Tspin = Tspin;
            this.ren = ren;
            this.clear = clear;
        }


        public int Getb2bdmg(int idx) => back_to_back[idx];
        public int Getrendmg(int idx)
        {
            if (idx > ren.Length)
            {
                return ren[ren.Length];
            }
            else
            {
                return ren[idx];
            }
        }
        public int GetTspindmg(int idx) => Tspin[idx];
        public int Getcleardmg(int idx) => clear[0];
    }

    
}
