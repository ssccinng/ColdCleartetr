using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace zzztetr
{
    public static class ZZZTOJcore
    {
        [DllImport("zzz_toj.dll")]
        unsafe public static extern char* TetrisAI(int[] overfield, int[] field, int field_w, int field_h, int b2b, int combo,
            char[] next, char hold, bool curCanHold, char active, int x, int y, int spin,
            bool canhold, bool can180spin, int upcomeAtt, int[] comboTable, int maxDepth, int level, int player);
        //public static extern IntPtr cc_launch_async(IntPtr options, IntPtr weights);


    }

}
