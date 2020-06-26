using Cold_Clear_SF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace ColdCleartetr
{
    public class ColdClear
    {
        IntPtr ptrBot = (IntPtr)(null);
        bool acv = false;
        bool addnext = false;
        bool calumove = false;
        int piececnt = 0;
        int idx=0;
        bool end = false;
        bool reset = false;
        public void newGame()
        {

            if (ptrBot != (IntPtr)null)
                //return;
                coldclearcore.cc_destroy_async(ptrBot);
            IntPtr ptrW = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(CCWeights)));
            coldclearcore.cc_default_weights(ptrW);
            CCWeights cCWeights = (CCWeights)Marshal.PtrToStructure(ptrW, typeof(CCWeights));
            IntPtr ptrOP = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(CCOptions)));
            coldclearcore.cc_default_options(ptrOP);
            CCOptions cCOptions = (CCOptions)Marshal.PtrToStructure(ptrOP, typeof(CCOptions));
            foreach (string key in ConfigurationManager.AppSettings.AllKeys)
            {
                if (key == "tslot" || key == "well_column" || key == "min_nodes" || key == "use_hold"
                    || key == "mode" || key == "use_bag" || key == "threads" || key == "pcloop")
                {
                    continue;
                }
                //string ConfigPath = ConfigurationManager.AppSettings["ConfigPath"].Trim().ToString();
                Configuration config1 = System.Configuration.ConfigurationManager.OpenExeConfiguration("ColdCleartetr.exe");

                if (config1.AppSettings.Settings[key].Value == "")
                {
                    config1.AppSettings.Settings.Add(key, cCWeights.GetType().GetField(key).GetValue(cCWeights).ToString());
                    config1.Save(ConfigurationSaveMode.Modified);
                }

                cCWeights.GetType().GetField(key).SetValue(cCWeights, int.Parse(config1.AppSettings.Settings[key].Value));
            }
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration("ColdCleartetr.exe");
            cCOptions.min_nodes = uint.Parse(config.AppSettings.Settings["min_nodes"].Value);
            cCOptions.pcloop = (char)(char.Parse(config.AppSettings.Settings["pcloop"].Value) - '0');
            //cCOptions.threads = ulong.Parse(config.AppSettings.Settings["threads"].Value);
            cCOptions.mode = (CCMovementMode)Enum.Parse(typeof(CCMovementMode), "CC_" + config.AppSettings.Settings["mode"].Value);
            cCOptions.use_hold = (char)(char.Parse(config.AppSettings.Settings["use_hold"].Value) - '0');
            string[] ts = config.AppSettings.Settings["tslot"].Value.Split(',');
            //cCWeights.use_bag = (char) (char.Parse(config.AppSettings.Settings["use_bag"].Value) - '0');
            string[] cw = config.AppSettings.Settings["well_column"].Value.Split(',');
            unsafe
            {
                for (int i = 0; i < 4; ++i)
                {
                    cCWeights.tslot[i] = int.Parse(ts[i]);
                }
                for (int i = 0; i < 10; ++i)
                {
                    cCWeights.well_column[i] = int.Parse(cw[i]);
                }
            }
            ptrBot = coldclearcore.cc_launch_async(cCOptions, cCWeights);
            //Thread.Sleep(500);
            acv = true ;
        }


        public async void endGame()
        {
            
            end = true;
            //while (addnext || calumove) Thread.Sleep(50);
            if (ptrBot != (IntPtr)null)
                coldclearcore.cc_destroy_async(ptrBot);
            addnext = false;
            calumove = false;
            piececnt = 0;
            ptrBot = (IntPtr)null;
            acv = false;
            
        }

        int nexcnt = 0;
        public void nextPieces(string[] pieces)
        {
            
            //Console.WriteLine(ptrBot);
            nexcnt = pieces.Length;
            //while (ptrBot == (IntPtr)null)
            //{
            //    Thread.Sleep(100);
            //}
            while (!acv || calumove || reset)
            {
                if (end)
                {
                    addnext = false;return;
               }
                    Thread.Sleep(100);
            }
            addnext = true;
            foreach (string mino in pieces)
            {
                //Console.WriteLine(mino);
                coldclearcore.cc_add_next_piece_async(ptrBot, (CCPiece)Enum.Parse(typeof(CCPiece), "CC_" + mino));
                nexcnt--;
                piececnt++;
                //Thread.Sleep(100);
            }
            addnext = false;
        }

        public void resetBoard(char[] field)
        {
            while (!acv || piececnt <= 0 || addnext || calumove) { Thread.Sleep(50); if (end) { reset = false; return; } }
            reset = true;
            if (end)
            {
                reset = false; return;
            }
            coldclearcore.cc_reset_async(ptrBot ,field, (char)(0), 0);
            if (end)
            {
                reset = false; return;
            }
            reset = false;

        } 

        public CCMove nextMove()
        {
            //Thread.Sleep(500);
            while (!acv || piececnt <= idx || addnext || reset) { Console.WriteLine(6); Thread.Sleep(50);if (end) { calumove = false; return new CCMove(); } }
            calumove = true;
            IntPtr ptrmove = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(CCMove)));
            coldclearcore.cc_request_next_move(ptrBot, 0);

            int a;
            if (end) { calumove = false; return new CCMove(); }
            Console.WriteLine(1);
            if (end) { calumove = false; return new CCMove(); }
            while ((a = coldclearcore.cc_poll_next_move(ptrBot, ptrmove)) != 0)
            {
                Console.WriteLine(2);
                Console.WriteLine("piececnt" + piececnt);
                
                Thread.Sleep(50);
                if (end) { Console.WriteLine(4); calumove = false; return new CCMove(); }
            }
            Console.WriteLine(3);
            CCMove cCMove = (CCMove)Marshal.PtrToStructure(ptrmove, typeof(CCMove));
            //Console.WriteLine("------" + cCMove.movement_count);
            calumove = false;
            idx++;
            return cCMove;
        }
    }
}
