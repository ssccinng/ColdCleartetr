using System;
using System.Collections.Generic;
using System.Text;

namespace Jura_Knife_Tetris
{
    public static class seacher
    {
        static public List<mino> findallplace(simpboard Board)
        {
            List<mino> allpos = new List<mino>();
            bool[,,] visit = new bool[42, 12, 4];
            bool[,,] visit1 = new bool[42, 12, 4];
            Queue<mino_stat> minoque = new Queue<mino_stat>();
            if (!Board.piece.check_mino_ok(ref Board, Board.piece.minopos))
            {
                return allpos;
            }
            minoque.Enqueue(new mino_stat(Board.piece.minopos, Board.piece.stat));
            visit[Board.piece.minopos.x + 2, Board.piece.minopos.y + 2, Board.piece.stat] = true;
            mino_gene minogen = new mino_gene();
            mino temp = Board.piece.clone();
            while (minoque.Count != 0)
            {
                mino_stat node = minoque.Dequeue();

                //visit[node.minoopos.x, node.minoopos.y, node.stat] = true; // 硬降
                temp.setpos(node.minoopos);
                temp.setstat(node.stat);
                if (temp.left_rotation(ref Board) != -1)
                {
                    if (!visit[temp.minopos.x + 2, temp.minopos.y + 2, temp.stat])
                    {
                        visit[temp.minopos.x + 2, temp.minopos.y + 2, temp.stat] = true;
                        minoque.Enqueue(new mino_stat(temp.minopos, temp.stat, node.idx, node.path, 2, node.movetime));
                    }
                }

                temp.setpos(node.minoopos);
                temp.setstat(node.stat);
                if (temp.right_rotation(ref Board) != -1)
                {
                    if (!visit[temp.minopos.x + 2, temp.minopos.y + 2, temp.stat])
                    {
                        visit[temp.minopos.x + 2, temp.minopos.y + 2, temp.stat] = true;
                        minoque.Enqueue(new mino_stat(temp.minopos, temp.stat, node.idx, node.path, 3, node.movetime));
                    }
                }

                temp.setpos(node.minoopos);
                temp.setstat(node.stat);

                if (temp.left_move(ref Board))
                {
                    if (!visit[temp.minopos.x + 2, temp.minopos.y + 2, temp.stat])
                    {
                        visit[temp.minopos.x + 2, temp.minopos.y + 2, temp.stat] = true;
                        minoque.Enqueue(new mino_stat(temp.minopos, temp.stat, node.idx, node.path, 0, node.movetime));
                    }
                }
                temp.setpos(node.minoopos);
                temp.setstat(node.stat);
                if (temp.right_move(ref Board))
                {
                    if (!visit[temp.minopos.x + 2, temp.minopos.y + 2, temp.stat])
                    {
                        visit[temp.minopos.x + 2, temp.minopos.y + 2, temp.stat] = true;
                        minoque.Enqueue(new mino_stat(temp.minopos, temp.stat, node.idx, node.path, 1, node.movetime));
                    }
                }

                temp.setpos(node.minoopos);
                temp.setstat(node.stat);

                int dis = temp.soft_drop_floor(ref Board);
                //{
                    if (!visit[temp.minopos.x + 2, temp.minopos.y + 2, temp.stat])
                    {
                        visit[temp.minopos.x + 2, temp.minopos.y + 2, temp.stat] = true;
                        minoque.Enqueue(new mino_stat(temp.minopos, temp.stat, node.idx, node.path, -dis, node.movetime, repeat: dis));
                    }
                //}
                if (!visit1[temp.minopos.x + 2, temp.minopos.y + 2, temp.stat]) {
                    visit1[temp.minopos.x + 2, temp.minopos.y + 2, temp.stat] = true;
                    
                    mino fi = temp.clone();
                    fi.path = node;
                    allpos.Add(fi);
                }



                //source.setpos
            }

            return allpos;
        }

    }
}
