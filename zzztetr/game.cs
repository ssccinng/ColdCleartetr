using System;
using System.Collections.Generic;
using System.Text;

namespace Jura_Knife_Tetris
{
    public  class game
    {
        public board Board;
        public Stack<int> garbage_queue;
        public Stack<int> attacking;
        public rule gamerule;
        public mino_gene Minorule;
        public Juraknifecore bot;
        public int atkcnt = 0;
        public bool isdead = false;

        public int lock_piece_calc()
        {
            Board.piece.mino_lock(ref Board);

            bool isb2b = Board.isb2b;

            int row = Board.clear_full();
            int atk = 0;
            if (Board.isperfectclear) atk += 6;
            if (Board.piece.Tspin)
            {
                atk += gamerule.GetTspindmg(row);
                
            }
            else
            {
                atk += gamerule.Getcleardmg(row);
            }
            atk += gamerule.Getrendmg(Board.combo);

            if (isb2b)
                atk += gamerule.Getb2bdmg(row);
            if (Board.piece.mini && row == 1) atk -= 1;
            if (atk > 0)
                attacking.Push(atk);
            //if (Board.piece.isTspin())
            //Board.cl
            //int clear

            return atk;
        }

        public void deal_garbage()
        {
            int garbage = 0;
            foreach(int i in garbage_queue)
            {
                garbage += i;
            }
            int atk = 0;
            foreach (int i in attacking)
            {
                atk += i;
            }
            attacking.Clear();
            if (atk > garbage)
            {
                garbage_queue.Clear();
                //while (garbage > attacking.Peek())
                //{
                //    garbage -= attacking.Pop();

                //}
                //if (garbage != 0)
                //    attacking.Push(attacking.Pop() - garbage);


            }
            else if (garbage >atk)
            {
                
                while (atk > garbage_queue.Peek())
                {
                    atk -= garbage_queue.Pop();

                }
                if (atk != 0)
                    garbage_queue.Push(garbage_queue.Pop() - atk);
            }
            else
            {
                garbage_queue.Clear();
            }
            if (Board.combo > 0) return;

            if (garbage_queue.Count > 0) {  Board.add_garbage(garbage_queue); bot.reset_stat(Board); garbage_queue.Clear(); }

        }


        public void runmove(int move)
        {
            //Console.WriteLine("runmove field");
            //Board.console_print(true, Board.piece);
            switch (move)
            {
                case 0: Board.piece.left_move(ref Board);break;
                case 1: Board.piece.right_move(ref Board); break;
                case 2: Board.piece.left_rotation(ref Board); break;
                case 3: Board.piece.right_rotation(ref Board); break;
                case 4: Board.piece.soft_drop_floor(ref Board); break;
                case 5: atkcnt = lock_piece_calc(); break;
                case 6: Board.use_hold(); break;
                case 7: Board.piece.soft_drop(ref Board); break;
                default:
                    /*Board.piece.soft_drop_floor(ref Board);*/ break; 
            }
        }
        public void runmove(tree move)
        {
            if (move.ishold)
            {
                runmove(6);
            }
            for (int i = 0; i < move.finmino.path.idx; ++i)
            {
                runmove(move.finmino.path.path[i]);
            }
            runmove(5);
        }
        public void bot_init(weights w = null, int nextcnt = 3)
        {
            init();
            if (w== null)
                bot = new Juraknifecore();
            else
                bot = new Juraknifecore(w);
            //Minorule = new mino_gene();
            bot.init();
            for (int i = 0; i < nextcnt; ++i)
            {
                int next = Minorule.genebag7int();
                bot.add_next(next);
                Board.add_next_piece(next);
            }
            bot.extend_node();
        }

        public void bot_run()
        {
            if (bot.isdead) { isdead = true;  return; }
            tree root = bot.requset_next_move();
            if (root.isdead) { isdead = true; return; }
            Board.Spawn_piece();
            runmove(root);
            int next = Minorule.genebag7int();
            bot.add_next(next);
            Board.add_next_piece(next);
            bot.extend_node();

        }

        public game ()
        {

        }
        board F;
        public void init()
        {
            F = new board(new mino_gene(), new TopGarbage(), 0);

            Board = F;
            attacking = new Stack<int>();
            garbage_queue = new Stack<int>();
            gamerule = new rule(new int[] { 0, 1, 1, 2, 1 },
            new int[] { 0, 2, 4, 6 }, new int[] { 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2,2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, -1 }, new int[] { 0, 0, 1, 2, 4 });
            Minorule = new mino_gene();
        }


        public void Gamestart()
        {
            init();
            int atk = 0;
            for (int i = 0; i < 5; ++i) F.add_next_piece(Minorule.genebag7int());
            while (!F.isdead)
            {
                //F.add_garbage(2);
                F.add_next_piece(Minorule.genebag7int());
                F.Spawn_piece();
                //b.setpos(18, 3);
                //F.console_print(true, F.piece);
                //F.piece = b;
                
                while (!F.piece.locked)
                {
                    Console.Clear();

                    F.console_print(true, F.piece);
                    if (F.holdpiece != null)
                        F.holdpiece.console_print();
                    Console.Write(atk);
                    atk = 0;
                    if (F.piece.Tspin)
                    {
                        Console.WriteLine("tspin");
                        //if (F.piece.mini && row == 1)
                        //    Console.WriteLine("mini");
                    }
                    
                    char a = Console.ReadKey().KeyChar;

                    switch (a)
                    {
                        case 'a':
                            F.piece.left_move(ref F);
                            break;
                        case 'd':
                            F.piece.right_move(ref F);
                            break;
                        case 's':
                            F.piece.soft_drop(ref F);
                            break;
                        case 'w':
                            //F.piece.mino_lock(ref F);
                            atk = lock_piece_calc();
                            //F.add_garbage(1);
                            break;
                        case 'l':
                            F.piece.right_rotation(ref F);

                            break;
                        case 'k':
                            F.piece.left_rotation(ref F);
                            break;
                        case 'j':
                            
                            F.use_hold();
                            break;
                        default:
                            break;
                    }
                    
                }
            }
        }

        void place_next_piece()
        {

        }


    }
}
