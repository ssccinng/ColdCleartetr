using System;
using System.Collections.Generic;
using System.Text;

namespace Jura_Knife_Tetris
{
    public class board
    {
        public int[,] field = new int[40, 10];

        public int[] column_height = new int[10];

        public bool isb2b = false;
        public bool isb2bclear = false;
        public bool isdead = false;
        public int combo = 0;
        public mino holdpiece = null;
        public mino piece = null;
        public mino_gene Minorule;
        public Queue<mino> Next_queue = new Queue<mino>();
        public int next_queue_size;
        public Garbagegene garbagerule;
        public int garbage_cnt;

        public board(mino_gene Minorule, Garbagegene garbagerule, int next_queue_size)
        {
            this.Minorule = Minorule;
            this.next_queue_size = next_queue_size;
            this.garbagerule = garbagerule;
            for (int i = 0; i < next_queue_size; ++i)
            {
                gene_next_piece();
            }
        }

        public void reset(int[,] field, bool isb2b, int combo)
        {
            this.field = (int[,])field.Clone();
            this.isb2b = isb2b;
            this.combo = combo;
            column_height = updatecol();
        }

        private bool check_mino_ok(int x, int y)
        {
            for (int i = 0; i < piece.height; ++i)
            {
                for (int j = 0; j < piece.weight; ++j)
                {
                    if (checkfield(i + x, j + y)
                        && piece.minofield[i, j] != 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private bool check_mino_ok(pos p)
        {
            for (int i = 0; i < piece.height; ++i)
            {
                for (int j = 0; j < piece.weight; ++j)
                {
                    if (checkfield(i + p.x, j + p.y)
                        && piece.minofield[i, j] != 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool Spawn_piece()
        {
            if (Next_queue.Count == 0) return false;
            piece = Next_queue.Dequeue();
            piece.setpos(19, 3);
            bool isok = check_mino_ok(piece.minopos);
            isdead = !isok;
            return true;
            //gene_next_piece();
        }

        public bool isperfectclear
        {
            get
            {
                foreach (int i in column_height)
                {
                    if (i != 0) return false;
                }
                return true;
            }
        }

        //public Queue<int> garbage_sent;

        public int[] updatecol()
        {
            for (int i = 0; i < 10; ++i)
            {
                column_height[i] = 0;
                for (int h = 39; h >= 0; --h)
                {
                    if (field[h, i] != 0)
                    {
                        column_height[i] = h + 1;
                        break;
                    }
                }
            }
            return column_height;
        }

        public int clear_full()
        {
            bool[] clearflag = new bool[40];
            int cntclear = 40;
            for (int i = 0; i < 40; ++i)
            {
                clearflag[i] = true;
                for (int j = 0; j < 10; ++j)
                {
                    if (field[i, j] == 0)
                    {
                        clearflag[i] = false;
                        cntclear--;
                        break;
                    }
                }
            }
            int index2 = 0;
            isb2bclear = false;
            if (cntclear > 0)
            {
                
                if (cntclear == 4 || piece.Tspin)
                {
                    if (isb2b)
                    {
                        isb2bclear = true;
                    }
                    isb2b = true;
                }
                else
                {
                    isb2b = false;
                }
                
                combo += 1;
            }
            else
            {
                combo = 0;
            }

            for (int i = 0; i < 40; ++i)
            {
                while (index2 < 40 && clearflag[index2]) index2++;
                copy_line(index2, i);
                index2++;
            }
            for (int i = 0; i < column_height.Length; ++i)
            {
                column_height[i] -= cntclear;
                while (!checkfield(column_height[i] - 1, i))
                {
                    column_height[i]--;
                }
            }
            return cntclear;
        }
        public void clear_row(int row)
        {
            for (int i = 0; i < 10; ++i)
            {
                field[row, i] = 0;
            }
        }
        public void clear_col(int col)
        {
            for (int i = 0; i < 40; ++i)
            {
                field[i, col] = 0;
            }
        }

        public void copy_line(int source, int target)
        {
            if (target >= 40 || target < 0) return;
            if (source >= 40 || source < 0)
            {
                clear_row(target);
            }
            else
            {
                for (int i = 0; i < 10; ++i)
                {
                    field[target, i] = field[source, i];
                }
            }
        }
        public void all_clear()
        {
            for (int i = 0; i < 40; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    field[i, j] = 0;
                }
            }
        }
        public void console_print(bool printmino = true, mino m = null)
        {
            Console.WriteLine("\n+--------------------+");
            if (printmino && !m.locked)
            {
                for (int i = 0; i < m.height; ++i)
                {
                    for (int j = 0; j < m.weight; ++j)
                    {
                        if (m.minofield[i, j] != 0)
                            field[i + m.minopos.x, j + m.minopos.y] = 1;
                    }
                }
            }
            for (int i = 20; i >= 0; --i)
            {
                Console.Write("|");
                for (int j = 0; j < 10; ++j)
                {
                    if (field[i, j] != 0)
                    {
                        Console.Write("[]");
                    }
                    else
                    {
                        Console.Write(" +");
                    }
                }
                Console.WriteLine("|");
            }
            if (printmino && !m.locked)
            {
                for (int i = 0; i < m.height; ++i)
                {
                    for (int j = 0; j < m.weight; ++j)
                    {
                        if (m.minofield[i, j] != 0)
                            field[i + m.minopos.x, j + m.minopos.y] = 0;
                    }
                }
            }

            Console.WriteLine("+--------------------+\n");
        }
        public bool checkfield(int x, int y)
        {
            if ((x < 40 && x >= 0) && (y < 10 && y >= 0))
            {
                return field[x, y] != 0;
            }
            return true;
        }
        public bool checkfield(pos p)
        {
            if ((p.x < 40 && p.x >= 0) && (p.y < 10 && p.y >= 0))
            {
                return field[p.x, p.y] != 0;
            }
            return true;
        }
        public void gene_next_piece()
        {
            Next_queue.Enqueue(Minorule.genebag7mino());
        }

        public void add_next_piece(int piece)
        {
            Next_queue.Enqueue(defaultop.demino.getmino(piece));
        }
        public void add_next_piece(string piece)
        {
            Next_queue.Enqueue(defaultop.demino.getmino(piece));
        }
        public bool use_hold()
        {
            piece.reset();
            if (holdpiece == null)
            {
                if (Next_queue.Count == 0) return false;
                holdpiece = piece;
                Spawn_piece();
                //gene_next_piece();
            }
            else
            {
                mino temp = holdpiece;
                holdpiece = piece;
                piece = temp;
            }
            piece.reset();
            return true;
        }

        public bool add_garbage(Stack<int> garbage_queue)
        {
            int[,] garbage = garbagerule.Gene(garbage_queue);
            int addgarbage_cnt = garbage.GetLength(0);
            if (addgarbage_cnt == 0) return false;
            
            if (addgarbage_cnt >= 20)
            {
                isdead = true;
            }
            for (int i = 39; i >= addgarbage_cnt; --i)
            {
                copy_line(i - addgarbage_cnt, i);
            }
            for (int i = 0; i < addgarbage_cnt; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    field[i, j] = garbage[i, j];
                }
            }
            for (int i = 0; i < column_height.Length; ++i)
            {
                column_height[i] += addgarbage_cnt;
                while (!checkfield(column_height[i] - 1, i))
                {
                    column_height[i]--;
                }
            }
            return true;
        }

        public bool add_garbage(int addgarbage_cnt)
        {
            int[,] garbage = garbagerule.Gene(addgarbage_cnt);

            if (addgarbage_cnt == 0) return false;
            addgarbage_cnt = garbage.GetLength(0);
            
            if (addgarbage_cnt >= 20)
            {
                isdead = true;
            }

            for (int i = 39; i >= addgarbage_cnt; --i)
            {
                copy_line(i - addgarbage_cnt, i);
            }
            for (int i = 0; i < addgarbage_cnt; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    field[i, j] = garbage[i, j];
                }
            }
            for (int i = 0; i < column_height.Length; ++i)
            {
                column_height[i] += addgarbage_cnt;
                while (!checkfield(column_height[i] - 1, i))
                {
                    column_height[i]--;
                }
            }
            return true;
        }

        //public board clone()
        //{
        //    board cp = new board();

        //    cp.field = (int[,])field.Clone();
        //    cp.isb2b = isb2b;
        //    cp.combo = combo;
        //    cp.column_height = (int[])column_height.Clone();
        //    if (piece != null)
        //        cp.piece = piece.clone();
        //    if (holdpiece != null)
        //        cp.holdpiece = holdpiece.clone();
        //    cp.isdead = isdead;
        //    cp.garbage_cnt = garbage_cnt;
        //    return cp;
        //}
        public simpboard tosimple()
        {
            simpboard sBoard = new simpboard();
            sBoard.isb2b = isb2b;
            sBoard.isb2bclear = isb2bclear;
            sBoard.combo = combo;
            if (piece != null)
                sBoard.piece = piece.clone();
            if (holdpiece != null)
                sBoard.holdpiece = holdpiece.clone();
            sBoard.isdead = isdead;
            sBoard.column_height = (int[])column_height.Clone();
            sBoard.garbage_cnt = garbage_cnt;
            for (int i = 0; i < 40; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    sBoard.field[i, j] = (field[i, j] != 0);
                }
            }
            return sBoard;

        }
    }


    public class simpboard
    {
        public bool[,] field = new bool[40, 10];
        public bool isb2b = false;
        public bool isb2bclear = false;
        public bool isdead = false;
        public int combo = 0;
        public mino piece = null;
        public mino holdpiece = null;
        public int[] column_height = new int[10];
        public int garbage_cnt = 0;
        public int clearrow = 0;

        //public bool oddeven
        //{
        //    get
        //    {

        //    }
        //}
        public bool isperfectclear
        {
            get
            {
                foreach (bool i in field)
                {
                    if (i) return false;
                }
                return true;
            }
        }


        public bool checkfield(int x, int y)
        {
            if ((x < 40 && x >= 0) && (y < 10 && y >= 0))
            {
                return field[x, y] != false;
            }
            return true;
        }
        public bool checkfield(pos p)
        {
            if ((p.x < 40 && p.x >= 0) && (p.y < 10 && p.y >= 0))
            {
                return field[p.x, p.y] != false;
            }
            return true;
        }
        private bool check_mino_ok(pos p)
        {
            for (int i = 0; i < piece.height; ++i)
            {
                for (int j = 0; j < piece.weight; ++j)
                {
                    if (checkfield(i + p.x, j + p.y)
                        && piece.minofield[i, j] != 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private bool check_mino_ok(int x, int y)
        {
            for (int i = 0; i < piece.height; ++i)
            {
                for (int j = 0; j < piece.weight; ++j)
                {
                    if (checkfield(i + x, j + y)
                        && piece.minofield[i, j] != 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public int clear_full()
        {
            bool[] clearflag = new bool[40];
            int cntclear = 40;
            for (int i = 0; i < 40; ++i)
            {
                clearflag[i] = true;
                for (int j = 0; j < 10; ++j)
                {
                    if (field[i, j] == false)
                    {
                        clearflag[i] = false;
                        cntclear--;
                        break;
                    }
                }
            }
            int index2 = 0;
            isb2bclear = false;
            if (cntclear > 0)
            {

                if (cntclear == 4 || piece.Tspin)
                {
                    if (isb2b)
                    {
                        isb2bclear = true;
                    }
                    isb2b = true;
                }
                else
                {
                    isb2b = false;
                }

                combo += 1;
            }
            else
            {
                combo = 0;
            }

            for (int i = 0; i < 40; ++i)
            {
                while (index2 < 40 && clearflag[index2]) index2++;
                copy_line(index2, i);
                index2++;
            }

            for (int i = 0; i < column_height.Length; ++i)
            {
                column_height[i] -= cntclear;
                while (!checkfield(column_height[i] - 1, i)) {
                    column_height[i]--;
                }
            }
            return clearrow = cntclear;
        }
        public void clear_row(int row)
        {
            for (int i = 0; i < 10; ++i)
            {
                field[row, i] = false;
            }
        }
        public void clear_col(int col)
        {
            for (int i = 0; i < 40; ++i)
            {
                field[i, col] = false;
            }
        }

        public void copy_line(int source, int target)
        {
            if (target >= 40 || target < 0) return;
            if (source >= 40 || source < 0)
            {
                clear_row(target);
            }
            else
            {
                for (int i = 0; i < 10; ++i)
                {
                    field[target, i] = field[source, i];
                }
            }
        }
        public void all_clear()
        {
            for (int i = 0; i < 40; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    field[i, j] = false;
                }
            }
        }


        public simpboard clone()
        {
            simpboard cp = new simpboard();

            cp.field = (bool[,])field.Clone();
            cp.isb2b = isb2b;
            cp.combo = combo;
            cp.column_height = (int[])column_height.Clone();
            if (piece != null)
                cp.piece = piece.clone();
            if (holdpiece != null)
                cp.holdpiece = holdpiece.clone();
            cp.isdead = isdead;
            cp.garbage_cnt = garbage_cnt;
            return cp;
        }

        public int[] updatecol()
        {
            for (int i = 0; i < 10; ++i)
            {
                column_height[i] = 0;
                for (int h = 39; h >= 0; --h)
                {
                    if (field[h, i])
                    {
                        column_height[i] = h + 1;
                        break;
                    }
                }
            }
            return column_height;
        }
        public void console_print(bool printmino = true, mino m = null)
        {
            Console.WriteLine("\n+--------------------+");
            bool[,] minopp = new bool[40, 10];
            //if (printmino/* && !m.locked)*/)
            //{
            //    for (int i = 0; i < m.height; ++i)
            //    {
            //        for (int j = 0; j < m.weight; ++j)
            //        {
            //            if (m.minofield[i, j] != 0)
            //                minopp[i + m.minopos.x, j + m.minopos.y] = true;
            //        }
            //    }
            //} //maxtspin slot search

            for (int i = 20; i >= 0; --i)
            {
                Console.Write("|");
                for (int j = 0; j < 10; ++j)
                {
                    if (minopp[i,j])
                    {
                        Console.Write("**");
                    }
                    else if (field[i, j])
                    {
                        Console.Write("[]");
                    }
                    else
                    {
                        Console.Write(" +");
                    }
                }
                Console.WriteLine("|");
            }
            //if (printmino && !m.locked)
            //{
            //    for (int i = 0; i < m.height; ++i)
            //    {
            //        for (int j = 0; j < m.weight; ++j)
            //        {
            //            if (m.minofield[i, j] != 0)
            //                field[i + m.minopos.x, j + m.minopos.y] = false;
            //        }
            //    }
            //}

            Console.WriteLine("+--------------------+\n");
        }
    }
}
