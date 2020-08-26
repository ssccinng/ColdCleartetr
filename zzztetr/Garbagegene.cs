using System;
using System.Collections.Generic;
using System.Text;

namespace Jura_Knife_Tetris
{
    public class Garbagegene
    {
        public Random rand = new Random();
        //public int[,] garbagefield;
        public int garbage_cnt;
        //public virtual int[,] Genetor(int garbage_cnt);
        public virtual int[,] Gene(int garbage_cnt) { return null; }
        public virtual int[,] Gene(Stack<int> garbage_queue) { return null; }
    }

    public class TopGarbage: Garbagegene
    {

        public override int[,] Gene(int garbage_cnt)
        {
            if (garbage_cnt > 20) garbage_cnt = 20;
            if (garbage_cnt < 0) garbage_cnt = 0;


            this.garbage_cnt = garbage_cnt;
            int[,] garbage = new int[garbage_cnt, 10];
            for (int i = 0; i < garbage_cnt; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    garbage[i, j] = 1;
                }
            }
            int hole = rand.Next() % 10;

            for (int i = 0; i < garbage_cnt; ++i)
            {
                garbage[i, hole] = 0;
            }
            return garbage;
        }


        public override int[,] Gene(Stack<int> garbage_queue)
        {
            this.garbage_cnt = 0;
            foreach(int i in garbage_queue)
            {
                garbage_cnt += i;
            }
            //if (garbage_cnt > 20) garbage_cnt = 20;
            //if (garbage_cnt < 0) garbage_cnt = 0;


            if (garbage_cnt > 20) return Gene(20);
            if (garbage_cnt < 0) return Gene(0);

            
            
            int[,] garbage = new int[garbage_cnt, 10];
            for (int i = 0; i < garbage_cnt; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    garbage[i, j] = 1;
                }
            }
            int hole;
            int idx = 0;
            foreach (int i in garbage_queue)
            {
                hole = rand.Next() % 10;
                for (int j = 0; j < i; ++idx, ++j)
                {
                    garbage[idx, hole] = 0;
                }
            }

            return garbage;
        }
    }

    public class PPTGarbage : Garbagegene
    {

        public override int[,] Gene(int garbage_cnt)
        {
            if (garbage_cnt > 20) garbage_cnt = 20;
            if (garbage_cnt < 0) garbage_cnt = 0;


            this.garbage_cnt = garbage_cnt;
            int[,] garbage = new int[garbage_cnt, 10];
            for (int i = 0; i < garbage_cnt; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    garbage[i, j] = 1;
                }
            }
            int hole = rand.Next() % 10;

            for (int i = 0; i < garbage_cnt; ++i)
            {
                garbage[i, hole] = 0;
            }
            return garbage;
        }


        public override int[,] Gene(Stack<int> garbage_queue)
        {
            this.garbage_cnt = 0;
            foreach (int i in garbage_queue)
            {
                garbage_cnt += i;
            }
            //if (garbage_cnt > 20) garbage_cnt = 20;
            //if (garbage_cnt < 0) garbage_cnt = 0;


            if (garbage_cnt > 20) return Gene(20);
            if (garbage_cnt < 0) return Gene(0);



            int[,] garbage = new int[garbage_cnt, 10];
            for (int i = 0; i < garbage_cnt; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    garbage[i, j] = 1;
                }
            }
            int hole;
            int idx = 0;
            foreach (int i in garbage_queue)
            {
                hole = rand.Next() % 10;
                for (int j = 0; j < i; ++idx, ++j)
                {
                    garbage[idx, hole] = 0;
                }
            }

            return garbage;
        }
    }
}
