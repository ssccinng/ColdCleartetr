using System;
using System.Collections.Generic;
using System.Text;

namespace Jura_Knife_Tetris
{

    public class JK_Movec
    {
        int [] movement = new int[32];
        int movecnt;
        bool hold;
        int[,] except_pos = new int[4, 2]; 

        
    }

    public class mino_stat
    {
        public pos minoopos;
        public int stat;
        public int idx;
        public int movetime = 0;
        public int[] path;
        public bool spinlast;
        public mino_stat(pos minoopos, int stat, bool spinlast = false)
        {
            this.minoopos = minoopos;
            this.stat = stat;
            this.spinlast = spinlast;
            idx = 0;
            spinlast = false;
            path = new int[32];


        }

        //public mino_stat(pos minoopos, int stat, )
        //{
        //    this.minoopos = minoopos;
        //    this.stat = stat;
        //    idx = 0;
        //    spinlast = false;
        //    path = new int[32];


        //}
        //public mino_stat clone()
        //{
        //    mino_stat cp = new mino_stat();
        //    cp.path = (int[])path.Clone();
        //    cp.stat = stat;
        //    cp.idx = idx;
        //    return cp;

        //}
        public mino_stat(pos minoopos, int stat, int idx, int[] path_old, int move, int movetime, bool spinlast = false, int repeat = 1)
        {
            this.minoopos = minoopos.clone();
            this.stat = stat;
            this.spinlast = spinlast;
            path = new int[52];
            path[idx] = move;
            this.movetime = movetime + repeat;
            for (int i = 0; i < idx; ++i)
            {
                path[i] = path_old[i];
            }
            this.idx = idx + 1;


        }
    }

    public class move
    {
        
        JK_Movec res = new JK_Movec();
        
        //bool bfs_findpath(ref board field , mino source, mino target)
        //{
        //    if (source.name != target.name) return false;
        //    bool[,,] visit = new bool[40, 10, 4];
        //    Queue<mino_stat> minoque = new Queue<mino_stat>();
        //    minoque.Enqueue(new mino_stat(source.minopos, source.stat));
        //    mino_gene minogen = new mino_gene();
        //    mino temp = minogen.getmino(source.name);
        //    while (minoque.Count != 0)
        //    {
        //        mino_stat node = minoque.Dequeue();
                
        //        visit[node.minoopos.x, node.minoopos.y, node.stat] = true;
        //        if (node.minoopos.x == target.minopos.x &&
        //            node.minoopos.y == target.minopos.y &&
        //            node.stat == target.stat)
        //        {
        //            return true;
        //        }

        //        temp.setpos(node.minoopos);
        //        temp.setstat(node.stat);
        //        if (temp.left_rotation(ref field) != -1)
        //        {
        //            if (!visit[temp.minopos.x, temp.minopos.y, temp.stat])
        //            {
        //                minoque.Enqueue(new mino_stat(temp.minopos, temp.stat, node.idx, node.path, 2));
        //            }
        //        }

        //        temp.setpos(node.minoopos);
        //        temp.setstat(node.stat);
        //        if (temp.right_rotation(ref field) != -1)
        //        {
        //            if (!visit[temp.minopos.x, temp.minopos.y, temp.stat])
        //            {
        //                minoque.Enqueue(new mino_stat(temp.minopos, temp.stat, node.idx, node.path, 3));
        //            }
        //        }

        //        temp.setpos(node.minoopos);
        //        temp.setstat(node.stat);

        //        if (temp.left_move(ref field))
        //        {
        //            if (!visit[temp.minopos.x, temp.minopos.y, temp.stat])
        //            {
        //                minoque.Enqueue(new mino_stat(temp.minopos, temp.stat, node.idx, node.path, 0));
        //            }
        //        }
        //        temp.setpos(node.minoopos);
        //        temp.setstat(node.stat);
        //        if (temp.right_move(ref field))
        //        {
        //            if (!visit[temp.minopos.x, temp.minopos.y, temp.stat])
        //            {
        //                minoque.Enqueue(new mino_stat(temp.minopos, temp.stat, node.idx, node.path, 1));
        //            }
        //        }

        //        temp.setpos(node.minoopos);
        //        temp.setstat(node.stat);
        //        if (temp.soft_drop_floor(ref field) != 0)
        //        {
        //            if (!visit[temp.minopos.x, temp.minopos.y, temp.stat])
        //            {
        //                minoque.Enqueue(new mino_stat(temp.minopos, temp.stat, node.idx, node.path, 4));
        //            }
        //        }

                
        //        //source.setpos
        //    }

        //    return false;
        //}
    }
}
