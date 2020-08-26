using System;

namespace Jura_Knife_Tetris
{
    public class mino
    {
        public int[,] minofield;
        pos[,] kicktable;
        public pos minopos;
        public int stat;
        public string name;
        public bool locked;
        public bool spinlast;
        public int weight, height;

        public bool Tspin;
        public bool mini;

        public mino_stat path; //等下再改

        /*************
         *方法考虑移交给board 
         * 
         *
         *
         */

        public void right_roll()
        {
            int[,] newfield = new int[height, weight];
            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < weight; ++j)
                {
                    newfield[height - 1 - j, i] = minofield[i, j];
                }
            }

            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < weight; ++j)
                {
                    minofield[i, j] = newfield[i, j];
                }
            }
        }

        public void right_roll(ref int[,] newfield)
        {
            
            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < weight; ++j)
                {
                    newfield[height - 1 - j, i] = minofield[i, j];
                }
            }
        }

        public void left_roll()
        {
            int[,] newfield = new int[height, weight];
            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < weight; ++j)
                {
                    newfield[i, j] = minofield[height - 1 - j, i];
                }
            }

            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < weight; ++j)
                {
                    minofield[i, j] = newfield[i, j];
                }
            }
        }

        public void left_roll(ref int[,] newfield)
        {

            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < weight; ++j)
                {
                    newfield[i, j] = minofield[height - 1 - j, i];
                }
            }
        }

        public void setpos(pos p)
        {
            this.minopos.x = p.x;
            this.minopos.y = p.y;

        }

        public void setpos(int x, int y)
        {
            minopos.x = x;
            minopos.y = y;
        }

        public void setstat(int stat)
        {
            while (this.stat != stat)
            //for (int i = 0; i < (stat + 4 - this.stat); ++i)
            {
                right_roll();
                this.stat = (this.stat + 1) % 4;
            }
            this.stat = stat;
        }

        public mino(int[,] minofield, pos[,] kicktable, int stat, pos minopos, string name)
        {
            this.minofield =(int[,]) minofield.Clone();
            this.kicktable = kicktable;  
            this.minopos = minopos.clone();
            this.name = name;
            this.locked = false;
            this.spinlast = false;
            height = minofield.GetLength(0);
            weight = minofield.GetLength(1);
            this.stat = stat;
            setstat(stat);
        }

        //public mino Clone()
        //{
        //    return new mino(this.minofield.Clone(), this.kicktable.Clone(), this.stat, this.minopos.)
        //}

        public void reset()
        {
            setstat(0);
            setpos(19,3);
            spinlast = false;
            locked = false;
            Tspin = false;
            mini = false;

        }

        //private bool checkfield(ref board field, int x, int y)
        //{
        //    if ((x < 40 && x >= 0) && (y < 10 && y >= 0))
        //    {
        //        return field.field[x, y] != 0;
        //    }
        //    return true;
        //}
        //private bool checkfield(ref board field, pos p)
        //{
        //    if ((p.x < 40 && p.x >= 0) && (p.y < 10 && p.y >= 0))
        //    {
        //        return field.field[p.x, p.y] != 0;
        //    }
        //    return true;
        //}
        public bool check_mino_ok(ref board field, int x, int y)
        {
            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < weight; ++j)
                {
                    if (field.checkfield(i + x, j + y)
                        && minofield[i, j] != 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public bool check_mino_ok(ref board field, pos p)
        {
            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < weight; ++j)
                {
                    if (field.checkfield(i + p.x, j + p.y)
                        && minofield[i, j] != 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool check_mino_ok(ref board field, int x, int y, int[,] minofield)
        {
            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < weight; ++j)
                {
                    if (field.checkfield(i + x, j + y)
                        && minofield[i, j] != 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool check_mino_ok(ref board field, pos p, int[,] minofield)
        {
            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < weight; ++j)
                {
                    if (field.checkfield(i + p.x, j + p.y)
                        && minofield[i, j] != 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public bool left_move(ref board field)
        {
            if (check_mino_ok(ref field, minopos.x, minopos.y - 1))
            {
                minopos.y -= 1;
                spinlast = false;
                return true;
            }
            return false;
        }

        public bool right_move(ref board field)
        {
            if (check_mino_ok(ref field, minopos.x, minopos.y + 1))
            {
                minopos.y += 1;
                spinlast = false;
                return true;
            }
            return false;
        }

        public bool soft_drop(ref board field)
        {
            if (check_mino_ok(ref field, minopos.x - 1, minopos.y))
            {
                minopos.x -= 1;
                spinlast = false;
                return true;
            }
            return false;
        }

        public int soft_drop_floor(ref board field)
        {
            int dist = 0;

            //for (int i = 0; i < weight; i++)
            //{
            //    for (int j = 0; j < height; ++j)
            //    {
            //        if (minofield[j, i] != 0)
            //        {
            //            dist = Math.Min(dist, minopos.x - field.column_height[i + minopos.y] + j);
            //            break;
            //        }
            //    }
            //}
            //if (dist < 0)
            //{
            //    dist = 0;
            //    while (check_mino_ok(ref field, minopos.x - dist - 1, minopos.y))
            //    {
            //        dist++;
            //    }
            //}

            //if (dist > 0)

                while (check_mino_ok(ref field, minopos.x - 1, minopos.y))
            {
                minopos.x -= 1;
                spinlast = false;
                dist++;
            }

            return dist;
        }

        public bool isTspin(ref board field)
        {
            if (name != "T") return false;
            if (!spinlast) return false;
            int cnt = (field.checkfield(minopos.x, minopos.y) ? 1 : 0)+
                (field.checkfield(minopos.x + 2, minopos.y) ? 1 : 0)+
                (field.checkfield(minopos.x, minopos.y + 2) ? 1 : 0) +
                (field.checkfield(minopos.x + 2, minopos.y + 2) ? 1 : 0);
            return cnt >= 3;
        }
        public bool ismin(ref board field)
        {
            if (name != "T") return false;
            if (stat == 1) return field.checkfield(minopos.x, minopos.y) &&
                    field.checkfield(minopos.x + 1, minopos.y) &&
                    field.checkfield(minopos.x + 2, minopos.y);
            if (stat == 2)  return field.checkfield(minopos.x + 2, minopos.y) &&
                    field.checkfield(minopos.x + 2, minopos.y + 1) &&
                    field.checkfield(minopos.x + 2, minopos.y + 2);
            if (stat == 3) return field.checkfield(minopos.x, minopos.y + 2) &&
                   field.checkfield(minopos.x + 1, minopos.y + 2) &&
                   field.checkfield(minopos.x + 2, minopos.y + 2);
            if (stat == 0) return field.checkfield(minopos.x, minopos.y) &&
                   field.checkfield(minopos.x, minopos.y + 1) &&
                   field.checkfield(minopos.x, minopos.y + 2);
            return false;
        }


        public int right_rotation(ref board field)
        {
            int a = height, b = weight;
            int kick_try = 0;

            int[,] newfield = new int[a,b];

            right_roll(ref newfield);

            for (int kick = 0; kick < kicktable.GetLength(1); ++kick)
            {
                if (!check_mino_ok(ref field, minopos.x + kicktable[stat, kick].y, minopos.y + kicktable[stat, kick].x, newfield))
                {
                    kick_try += 1;
                }
                else
                {
                    minopos.x += kicktable[stat, kick].y;
                    minopos.y += kicktable[stat, kick].x;
                    break;
                }
            }
            if (kick_try > 4) return -1;
            
            stat += 1;
            stat %= 4;
            for (int i = 0; i < a; ++i)
            {
                for (int j = 0; j < b; ++j)
                {
                    minofield[i, j] = newfield[i, j];
                }
            }
            spinlast = true;
            return kick_try;

        }
        public int left_rotation(ref board field)
        {
            int kick_try = 0;
            int a = height, b = weight;
            int[,] newfield = new int[a, b];
            left_roll(ref newfield);
            for (int kick = 0; kick < kicktable.GetLength(1); ++kick)
            {
                if (!check_mino_ok(ref field, minopos.x - kicktable[(stat + 3) % 4, kick].y, minopos.y - kicktable[(stat + 3) % 4, kick].x, newfield))
                {
                    kick_try += 1;
                }
                else
                {
                    minopos.x -= kicktable[(stat + 3) % 4, kick].y;
                    minopos.y -= kicktable[(stat + 3) % 4, kick].x;
                    break;
                }
            }
            if (kick_try > 4) return -1;
            stat += 3;
            stat %= 4;
            for (int i = 0; i < a; ++i)
            {
                for (int j = 0; j < b; ++j)
                {
                    minofield[i, j] = newfield[i, j];
                }
            }
            spinlast = true;
            return kick_try;
        }
        public void spinstatupdate(ref board field)
        {
            if (isTspin(ref field))
            {
                Tspin = true;
                if (ismin(ref field))
                {
                    mini = true;
                }
            }
        }

        public bool mino_lock(ref board field)
        {
            if (!check_mino_ok(ref field, minopos)) return false;
            soft_drop_floor(ref field);

            for (int i = 0; i < height; i++)
            {
                for (int j = weight - 1; j >= 0; --j)
                {
                    if (minofield[j, i] != 0)
                    {
                        field.column_height[i + minopos.y] = Math.Max(field.column_height[i + minopos.y], minopos.x + j + 1);
                        break;
                    }
                }
            }
            spinstatupdate(ref field);


            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < weight; ++j)
                {
                    if (minofield[i, j] != 0)
                        field.field[i + minopos.x, j + minopos.y] = 1;
                }
            }
            locked = true;

            //if (minopos.x >= 20) return false;
            return true;

        }  // 不锁定判断tspin


        public mino clone()
        {
            mino cp = new mino(minofield, kicktable,  stat, minopos, name);
            cp.locked = this.locked; // spin需要更新吗
            cp.spinlast = this.spinlast;
            cp.height = this.height;
            cp.weight = this.weight;
            cp.path = this.path;
            cp.minopos.x = this.minopos.x;
            cp.minopos.y = this.minopos.y;
            return cp;
        }


        public bool left_move(ref simpboard field)
        {
            if (check_mino_ok(ref field, minopos.x, minopos.y - 1))
            {
                minopos.y -= 1;
                spinlast = false;
                return true;
            }
            return false;
        }

        public bool right_move(ref simpboard field)
        {
            if (check_mino_ok(ref field, minopos.x, minopos.y + 1))
            {
                minopos.y += 1;
                spinlast = false;
                return true;
            }
            return false;
        }

        public bool soft_drop(ref simpboard field)
        {
            if (check_mino_ok(ref field, minopos.x - 1, minopos.y))
            {
                minopos.x -= 1;
                spinlast = false;
                return true;
            }
            return false;
        }

        public int soft_drop_floor(ref simpboard field)
        {
            int dist = 900;

            for (int i = 0; i < weight; i++)
            {
                for (int j = 0; j < height; ++j)
                {
                    if (minofield[j, i] != 0)
                    {
                        dist = Math.Min(dist, minopos.x - field.column_height[i + minopos.y] + j);
                        break;
                    }
                }
            }
            if (dist < 0)
            {
                dist = 0;
                while (check_mino_ok(ref field, minopos.x - dist - 1, minopos.y))
                {
                    dist++;
                }
            }

            if (dist > 0)
            ////while (check_mino_ok(ref field, minopos.x - 1, minopos.y))
            {
                minopos.x -= dist;
                spinlast = false;
            }
            return dist;
        }

        public bool isTspin(ref simpboard field)
        {
            if (name != "T") return false;
            if (!spinlast) return false;
            int cnt = (field.checkfield(minopos.x, minopos.y) ? 1 : 0) +
                (field.checkfield(minopos.x + 2, minopos.y) ? 1 : 0) +
                (field.checkfield(minopos.x, minopos.y + 2) ? 1 : 0) +
                (field.checkfield(minopos.x + 2, minopos.y + 2) ? 1 : 0);
            return cnt >= 3;
        }
        public bool ismin(ref simpboard field)
        {
            if (name != "T") return false;
            if (stat == 1) return field.checkfield(minopos.x, minopos.y) &&
                    field.checkfield(minopos.x + 1, minopos.y) &&
                    field.checkfield(minopos.x + 2, minopos.y);
            if (stat == 2) return field.checkfield(minopos.x + 2, minopos.y) &&
                   field.checkfield(minopos.x + 2, minopos.y + 1) &&
                   field.checkfield(minopos.x + 2, minopos.y + 2);
            if (stat == 3) return field.checkfield(minopos.x, minopos.y + 2) &&
                   field.checkfield(minopos.x + 1, minopos.y + 2) &&
                   field.checkfield(minopos.x + 2, minopos.y + 2);
            if (stat == 0) return field.checkfield(minopos.x, minopos.y) &&
                   field.checkfield(minopos.x, minopos.y + 1) &&
                   field.checkfield(minopos.x, minopos.y + 2);
            return false;
        }


        public int right_rotation(ref simpboard field)
        {
            int a = height, b = weight;
            int kick_try = 0;

            int[,] newfield = new int[a, b];

            right_roll(ref newfield);

            for (int kick = 0; kick < kicktable.GetLength(1); ++kick)
            {
                if (!check_mino_ok(ref field, minopos.x + kicktable[stat, kick].y, minopos.y + kicktable[stat, kick].x, newfield))
                {
                    kick_try += 1;
                }
                else
                {
                    minopos.x += kicktable[stat, kick].y;
                    minopos.y += kicktable[stat, kick].x;
                    break;
                }
            }
            if (kick_try > 4) return -1;

            stat += 1;
            stat %= 4;
            for (int i = 0; i < a; ++i)
            {
                for (int j = 0; j < b; ++j)
                {
                    minofield[i, j] = newfield[i, j];
                }
            }
            spinlast = true;
            return kick_try;

        }
        public int left_rotation(ref simpboard field)
        {
            int kick_try = 0;
            int a = height, b = weight;
            int[,] newfield = new int[a, b];
            left_roll(ref newfield);
            for (int kick = 0; kick < kicktable.GetLength(1); ++kick)
            {
                if (!check_mino_ok(ref field, minopos.x - kicktable[(stat + 3) % 4, kick].y, minopos.y - kicktable[(stat + 3) % 4, kick].x, newfield))
                {
                    kick_try += 1;
                }
                else
                {
                    minopos.x -= kicktable[(stat + 3) % 4, kick].y;
                    minopos.y -= kicktable[(stat + 3) % 4, kick].x;
                    break;
                }
            }
            if (kick_try > 4) return -1;
            stat += 3;
            stat %= 4;
            for (int i = 0; i < a; ++i)
            {
                for (int j = 0; j < b; ++j)
                {
                    minofield[i, j] = newfield[i, j];
                }
            }
            spinlast = true;
            return kick_try;
        }

        public bool check_mino_ok(ref simpboard field, int x, int y)
        {
            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < weight; ++j)
                {
                    if (field.checkfield(i + x, j + y)
                        && minofield[i, j] != 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public bool check_mino_ok(ref simpboard field, pos p)
        {
            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < weight; ++j)
                {
                    if (field.checkfield(i + p.x, j + p.y)
                        && minofield[i, j] != 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public bool check_mino_ok(ref simpboard field, int x, int y, int[,] minofield)
        {
            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < weight; ++j)
                {
                    if (field.checkfield(i + x, j + y)
                        && minofield[i, j] != 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool check_mino_ok(ref simpboard field, pos p, int[,] minofield)
        {
            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < weight; ++j)
                {
                    if (field.checkfield(i + p.x, j + p.y)
                        && minofield[i, j] != 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public void spinstatupdate(ref simpboard field)
        {
            if (isTspin(ref field))
            {
                Tspin = true;
                if (ismin(ref field))
                {
                    mini = true;
                }
            }
        }

        public bool mino_lock(ref simpboard field)
        {
            if (!check_mino_ok(ref field, minopos)) return false;
            soft_drop_floor(ref field);

            for (int i = 0; i < height; i++)
            {
                for (int j = weight - 1; j >= 0; --j)
                {
                    if (minofield[j, i] != 0)
                    {
                        field.column_height[i + minopos.y] = Math.Max(field.column_height[i + minopos.y], minopos.x + j + 1);
                        break;
                    }
                }
            }

            spinstatupdate(ref field);

            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < weight; ++j)
                {
                    if (minofield[i, j] != 0)
                        field.field[i + minopos.x, j + minopos.y] = true;
                }
            }
            locked = true;

            //if (minopos.x >= 20) return false;
            return true;

        }  // 不锁定判断tspin
        public void console_print()
        {
            Console.WriteLine("\n+--------+");

            for (int i = height - 1; i >= 0; --i)
            {
                Console.Write("|");
                for (int j = 0; j < weight; ++j)
                {
                    if (minofield[i, j] != 0)
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

            Console.WriteLine("\n+--------+");
        }

    }
}
