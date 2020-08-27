using Jura_Knife_Tetris;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Threading.Tasks;
using zzztetr.Controllers;


namespace zzztetr
{
    public class ZZZTOJ
    {
        char hold;
        string next;
        int garbage;
        Jura_Knife_Tetris.game Game;
        bool acv = false;
        bool addnext = false;
        public bool calumove = false;
        bool end = false;
        bool reset = false;
        int piececnt = 0;
        DateTime stime;
        long starttime = -1;
        double pbs;
        int idx1;


        public ZZZTOJ()
        {
            Game = new Jura_Knife_Tetris.game();

            Game.init();
            //newGame();
        }
        public async void newGame()
        {

            await Task.Run(() =>
            {
                piececnt = 0;
                end = false;
                calumove = false;
                reset = false;
                starttime = -1;
                
                Game = new Jura_Knife_Tetris.game();
                idx1 = 0;
                Game.init();
            });
        }
        public async void endGame()
        {

            end = true;
        }

        public async void nextPieces(string[] nextseq)
        {
            //while (!acv || calumove || reset)
            //{
            //    if (end)
            //    {
            //        addnext = false; return;
            //    }
            //    Thread.Sleep(100);
            //}
            addnext = true;
            await Task.Run(() =>
            {
                foreach (string piece in nextseq)
                    Game.Board.add_next_piece(piece);
            });
            piececnt += nextseq.Length;
            addnext = false;
        }


        public async Task<resmove> nextMove(int ids)
        {
            while (piececnt <= 11 || reset) Thread.Sleep(100);
            //Console.WriteLine("行动！！！");
            calumove = true;
            Game.Board.Spawn_piece();
            idx1++;
            resmove res = new resmove();
            if (end) { res.moves = new string[] { "oraora" }; return res; }
            bool craasshh = false;
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration("zzztetr.exe");
            await Task.Run(() =>
            {
                int[] field1 = new int[24];
                int[] field2 = new int[17];
                for (int i = 17; i < 40; ++i)
                {
                    for (int j = 0; j < 10; ++j)
                    {

                        //field1[21 - i] <<= 1;
                        //if (!Game.Board.checkfield(i, j)) field1[21 - i] |= 1;
                        if (Game.Board.checkfield(39 - i, j)) field1[i - 17] |= (1 << j);
                    }

                }
                //for (int i = 0; i < 17; ++i)
                //{
                //    for (int j = 0; j < 10; ++j)
                //    {
                //        field2[i - 22] <<= 1;
                //        if (!Game.Board.checkfield(i, j)) field2[i - 22] |= 1;
                //        if (Game.Board.checkfield(i, j)) field2[i - 22] |= (1 << j);
                //    }

                //}
                unsafe
                {
                    //char[] nsq = new char[Game.Board.Next_queue.Count];
                    int level = int.Parse(config.AppSettings.Settings["level"].Value);
                    int nextcnt = int.Parse(config.AppSettings.Settings["nextcnt"].Value);
                    char[] nsq = new char[nextcnt];
                    int idx = 0;
                    
                    foreach (mino m in Game.Board.Next_queue)
                    {
                        nsq[idx++] = m.name[0];
                        if (idx == nextcnt) break;
                    }
                    

                    //Console.WriteLine((Game.Board.holdpiece == null ? 'A' : Game.Board.holdpiece.name[0]));
                    //Console.WriteLine(Game.Board.piece.name[0]);
                    //if (Game.Board.holdpiece!= null) Game.Board.holdpiece.console_print();
                    //Game.Board.piece.console_print();
                    Console.WriteLine(new string(nsq)); // next表
                    //Game.Board.console_print(false);
                    char* input = null;
                    
                    try { 
                    input = ZZZTOJcore.TetrisAI(field2, field1, 10, 22, (Game.Board.isb2b ? 1 : 0),
                        Game.Board.combo, nsq, (Game.Board.holdpiece == null ? ' ' : Game.Board.holdpiece.name[0]),
                        true, Game.Board.piece.name[0], 3, 0, 0, true, false, ids, Game.gamerule.ren, nextcnt, level, 0); // 调用zzz
                    }
                    catch
                    {
                        craasshh = true;
                        res.moves = new string[] { "oraora" };
                    }
                    if (craasshh) return; 
                    //Game.Board.console_print(true, Game.Board.piece);
                    string temp = "";
                    //char* aChar = (char*)System.Runtime.InteropServices.Marshal.StringToHGlobalAnsi(input).ToPointer();
                    string result = Marshal.PtrToStringAnsi((IntPtr)input);

                    Console.WriteLine(result);

                    //while (true)
                    foreach (char m in result)
                    {
                        //Console.WriteLine(m);
                        switch (m)
                        {
                            case 'z':
                            case 'Z':
                                Game.runmove(2);
                                temp += ",Ccw";
                                break;
                            case 'c':
                            case 'C':
                                temp += ",Cw";
                                Game.runmove(3);

                                break;
                            case 'l':
                                temp += ",Left";
                                Game.runmove(0);
                                break;
                            case 'L':
                                for (int _ = 0; _ < 10; ++_)
                                {
                                    temp += ",Left"; // tiebian
                                    Game.runmove(0);
                                }
                                break;
                            case 'r':
                                temp += ",Right";
                                Game.runmove(1);
                                break;
                            case 'R':
                                for (int _ = 0; _ < 10; ++_)
                                {
                                    temp += ",Right";
                                    Game.runmove(1);
                                }
                                break;
                            case 'd':
                                //temp += ",SoftDrop";
                                //Game.runmove(7);
                                //break;
                            case 'D':
                                temp += ",SonicDrop";
                                Game.runmove(4);
                                break;
                            case 'v':
                                Game.runmove(6);
                                res.hold = true;
                                break;
                            case 'V':

                                break;
                            default:
                                break;
                        }
                        if (m == 'V') break;
                        //if (m == 0) break;
                        //if (*input == 'V' || (*input)== 0) break;
                        //input++;
                    }
                    
                    Console.WriteLine("x = " + Game.Board.piece.minopos.x);
                    Console.WriteLine("y = " + Game.Board.piece.minopos.y);

                    int midx = 0;
                    res.expected_location = new int[4][];


                    for (int mx = 0; mx < Game.Board.piece.height; ++ mx)
                    {

                        for (int my = 0; my < Game.Board.piece.weight; ++my)
                        {
                            if (Game.Board.piece.minofield[mx, my] == 1) {
                                res.expected_location[midx] = new int[2];
                                res.expected_location[midx][0] = Game.Board.piece.minopos.y + my;
                                res.expected_location[midx][1] = Game.Board.piece.minopos.x + mx;
                                midx++;
                                }
                        }

                    }
                    Game.runmove(5);
                    
                    
                    //Console.WriteLine(temp);
                    if (temp.Length >= 1)
                        temp = temp.Substring(1);
                      res.moves = temp.Split(',');
                }
            });
            if (craasshh) { res.moves = new string[] { "oraora" }; return res; }
            if (starttime == -1) starttime = DateTime.Now.Ticks;
            else
            {
                long nowtime = DateTime.Now.Ticks;
                double spend = (nowtime - starttime) / 10000;
                Console.WriteLine("spend = " + spend);
                Console.WriteLine("pbs = " + pbs);
                Console.WriteLine("idx = " + idx1);
                

                pbs = 60.0 / int.Parse(config.AppSettings.Settings["bpm"].Value);
                

                int qq = (int)((pbs * idx1) * 1000 - spend);
                Console.WriteLine("spend = " + qq);
                if (qq > 0&& !end) Thread.Sleep(qq);
            }
            if (end) { res.moves = new string[] { "oraora" }; return res; }
            return res;
        }
        public async void updategar(int gar)
        {
            await Task.Run(() =>
            {
                garbage = gar;
            });

        }
        public async void resetBoard(int[,] field)
        {

            while (calumove) Thread.Sleep(50);
            //Console.WriteLine("重置！！！");
            reset = true;
            
            await Task.Run(() =>
            {
                //int[,] ff = new int[40, 10];
                //for (int i = 0; i < 400; ++i)
                //{
                //    ff[i % 10, i / 10] = field[i];
                //}
                Game.Board.reset(field, Game.Board.isb2b, Game.Board.combo);
                //Game.Board.console_print(false);
                //Game.Board.piece.setpos(19, 3);
            });
            reset = false;
        }

    }
}
