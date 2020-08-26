using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace Cold_Clear_SF
{
    //internal class CCWeights
    //{
    //}
    //internal interface class CCWeights { }
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    unsafe public struct CCWeights
    {
        public Int32 back_to_back;
        public Int32 bumpiness;
        public Int32 bumpiness_sq;
        public Int32 height;
        public Int32 top_half;
        public Int32 top_quarter;
        public Int32 jeopardy;
        public Int32 cavity_cells;
        public Int32 cavity_cells_sq;
        public Int32 overhang_cells;
        public Int32 overhang_cells_sq;
        public Int32 covered_cells;
        public Int32 covered_cells_sq;
        public fixed Int32 tslot[4];// = new Int32[4];
        public Int32 well_depth;
        public Int32 max_well_depth;
        public fixed Int32 well_column[10];// = new Int32[10];

        public Int32 b2b_clear;
        public Int32 clear1;
        public Int32 clear2;
        public Int32 clear3;
        public Int32 clear4;
        public Int32 tspin1;
        public Int32 tspin2;
        public Int32 tspin3;
        public Int32 mini_tspin1;
        public Int32 mini_tspin2;
        public Int32 perfect_clear;
        public Int32 combo_garbage;
        public Int32 move_time;
        public Int32 wasted_t;
        public char use_bag;
    }
    public enum CCMovementMode
    {
        CC_0G,
        CC_20G,
        CC_HARD_DROP_ONLY
    }
    public enum CCMovement
    {
        CC_LEFT, CC_RIGHT,
        CC_CW, CC_CCW,
        /* Soft drop all the way down */
        CC_DROP
    }
    public enum CCPiece
    {
        CC_I, CC_T, CC_O, CC_S, CC_Z, CC_L, CC_J
    }
    [StructLayout(LayoutKind.Sequential, Pack = 2)]

    unsafe public struct CCMove
    {
        /* Whether hold is required */

        //public bool  hold;
        public char hold;
        //public char hold1;
        /* Expected cell coordinates of placement, (0, 0) being the bottom left */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] expected_x;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] expected_y;
        //public char expected_x1;
        //public char expected_x2;
        //public char expected_x3;
        //public char expected_x4;
        //public char expected_y1;
        //public char expected_y2;
        //public char expected_y3;
        //public char expected_y4;
        //public fixed char expected_x[4];
        //public fixed char expected_y[4];
        ///* Number of moves in the path */

        public char movement_count;
        public char movement_count1;
        ///* Movements */

        public fixed int movements[32];

        ///* Bot Info */

        public uint nodes;

        public uint depth;

        public uint original_rank;

    }
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public struct CCOptions
    {
        public CCMovementMode mode;
        public char use_hold;
        //public char use_hold1;
        public char speculate;
        public char pcloop;
        //public char speculate1;
        public UInt32 min_nodes;
        public UInt32 max_nodes;
        public UInt32 threads;
    }
    public struct CCAsyncBot { }
    public static class coldclearcore
    {

        // [///DllImport("cold_clear.dll", CallingConvention = CallingConvention.Cdecl)]
        [DllImport("cold_clear.dll")]
        public static extern IntPtr cc_launch_async(CCOptions options, CCWeights weights);
        //public static extern IntPtr cc_launch_async(IntPtr options, IntPtr weights);
        [DllImport("cold_clear.dll")]
        public static extern void cc_destroy_async(IntPtr bot);
        [DllImport("cold_clear.dll")]
        public static extern void cc_reset_async(IntPtr bot, char[] field, char b2b, UInt32 combo);
        [DllImport("cold_clear.dll")]
        public static extern void cc_add_next_piece_async(IntPtr bot, CCPiece piece);
        [DllImport("cold_clear.dll")]
        public static extern void cc_request_next_move(IntPtr bot, UInt32 incoming);
        [DllImport("cold_clear.dll")]
        public static extern int cc_poll_next_move(IntPtr bot, IntPtr move);
        [DllImport("cold_clear.dll")]
        public static extern bool cc_is_dead_async(IntPtr bot);
        [DllImport("cold_clear.dll")]
        public static extern void cc_default_options(IntPtr options);

        [DllImport("cold_clear.dll")]
        public static extern void cc_default_weights(IntPtr weights);


    }
}