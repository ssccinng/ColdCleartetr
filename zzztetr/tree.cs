using System;
using System.Collections.Generic;
using System.Text;

namespace Jura_Knife_Tetris
{

    public class treestat
    {
        public bool tspinmini = false;
        public bool tspin1 = false;
        public bool tspin2 = false;
        public bool tspin3 = false;
        public bool clear1 = false;
        public bool clear2 = false;
        public bool clear3 = false;
        public bool clear4 = false;
        public bool b2bclear = false;
        public bool pefectclear = false;
        public int combo = 0;
        public int maxattack = 0;
        public int maxdef = 0;

    }


    public class tree//: IDisposable
    {

        public bool isdead = true;
        public long score { get {
                return fieldscore + battlescore + atkscore + movescore;
            
            } }
        //public long scoreex
        //{
        //    get
        //    {
        //        return fieldscore + battlescore +a  + movescore;

        //    }
        //}
        public long fieldscore = 0; // 场地评判， 应该是扩展标准
        public long battlescore = 0; // 行为评判 应该是扩展标准
        public long atkscore = 0; // 不应该被扩展评判？ 有些只能在筛选时评判
        public long movescore = 0; // 场地状态评判 b2b等 独立
        // 如果能保持连击 分数就使用叶子节点最高的评价
        // 如果不行 需要根据场地高度判断适不适合
        public treestat Treestat = new treestat();
        public List<tree> treenode = new List<tree>();
        public bool isextend = false;
        public int nowpiece = -1;
        public int holdpiece = -1;
        public bool useless = false;
        public tree father; // 指向父亲节点
        // 重判dead
        public int keepcombobestchird;
        public int bestchird;
        public int garbage = 0;
        public int garbageadd = 0;
        public int clearrow = 0;
        public int pieceidx = 0;
        public int afttspinscore = 0;
        public evalresult res = new evalresult();

        

        public bool inplan = true;
        // 预测块会到来的顺序

        //是否要在子节点分数高时才能到这里

        public bool holdT
        {
            get
            {
                return holdpiece == 2;
            }
        }
        public bool holdI
        {
            get
            {
                return holdpiece == 0;
            }
        }

        public int tspintype;

        public int tslotnum;

        public int maxren;
        // 分为能立刻ren和后续才能ren
        // tspin后的场地分数
        // 是否有holdt 无holdt最多允许一个tslot存在

        public bool wastedT
        {
            get
            {
                return !Board.piece.Tspin && finmino.name == "T";
            }
        }
        public int attack = 0;// 当前直接打出的
        public int maxattack = 0;// 总共能打出的 //攻击回传
        public int def = 0;// 当前直接打出的
        public int maxdef = 0; // 最大缩减高度+垃圾行值
        public simpboard Board; // maybe simple
        // 内有b2b combo (b2bclear考虑

        //public bool clearing = false;
        public int maxdepth = 0;

        public mino finmino = null;
        public int depth = 0;
        public int Tspinslot = 0;

        public bool ishold = false;
        public int bestnodeindex; // 估计不用
        // 场地分可以回传
        // 攻击分不可回传
        // 行为分不可回传


        public void updatefather()
        {
            if (father != null)// youhua
            {
                // 以最深处为基准
                father.fieldscore = Math.Max(father.fieldscore, fieldscore); // 场地要以最深的？？？
                father.maxdepth = Math.Max(father.maxdepth, maxdepth);

                if(father.Board.combo == Board.combo - 1)
                {
                    father.maxdef =Math.Max(  maxdef + father.def, father.maxdef); // 也许需要改
                }
                father.maxattack = Math.Max(maxattack + father.attack, father.maxattack);
                father.updatefather();
            } /// 其他块假借了t的分数
        }

        //public bool findnextsol()
        //{
        //    for (; ; ) 
        //    yield return false; // pass
        //}

        public tree clone()
        {
            tree cp = new tree();
            cp.Board = Board.clone();
            cp.garbage = garbage; // 可能有抵消
            cp.attack = attack;
            cp.holdpiece = holdpiece;
            cp.battlescore = battlescore;
            // attack 可能继承
            return cp;
        }
        public Tuple<int, int> lock_piece_calc(ref simpboard Board)
        {
            rule gamerule = defaultop.defrule;
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
            // attack calu
            //if (Board.piece.isTspin())
            //Board.cl
            //int clear

            return new Tuple<int, int>(atk, row);
        }

        //public int searchtslot() // 去找可能的t坑
        //{

        //}

            public bool hasnextT(Juraknifecore bot, int nowidx)
        {
            for (int i = nowidx, j = 0; i < bot.nextcnt && j < 5;++i, ++j )
            {
                if (bot.nextqueue[i % 30] == 2) return true;
            }
            return false;
        }

        public void findalladd(Juraknifecore bot)
        {

            // 攻击力判定 + t旋只改场地分
            if (pieceidx >= bot.nextcnt)
                return;
            Tuple<int, int> res;
            this.nowpiece = bot.nextqueue[pieceidx % 30];
            isextend = true;
            inplan = false;
            Board.piece = defaultop.demino.getmino(nowpiece);
            Board.piece.setpos(19, 3);
            List<mino> allpos = seacher.findallplace(Board);
            int chirdidx = pieceidx + 1;
            if (allpos == null) { tree chird = clone(); chird.isdead = true; chird.pieceidx = chirdidx; chird.inplan = false; chird.isextend = true; treenode.Add(chird); return; };
            foreach (mino m in allpos)
            {
                // 场外死亡判断
                tree chird = clone();
                chird.Board.piece = m;

                res = lock_piece_calc(ref chird.Board);
                chird.attack = chird.maxattack = res.Item1;
                chird.def = chird.maxdef = res.Item1 + res.Item2; // 已经消除了 还能叫防御吗
                chird.finmino = m;
                chird.father = this;
                chird.ishold = false;
                chird.isdead = false;
                chird.holdpiece = holdpiece;
                chird.pieceidx = chirdidx;
                chird.depth = depth + 1;
                chird.maxdepth = chird.depth;
                chird.maxdepth = chird.pieceidx;
                chird.inplan = true;
                chird.res = bot.evalweight.evalfield(chird);
                chird.fieldscore = (int)chird.res.score;
                chird.battlescore += bot.evalweight.evalbattle(chird); // 相同的不要反复判了
                chird.movescore = bot.evalweight.evalmove(chird); // 相同的不要反复判了
                chird.atkscore = bot.evalweight.evalatkdef(chird); // 相同的不要反复判了
                //if (true||  chird.holdT || hasnextT(bot, chirdidx) )// 当前块是t的时候
                //{
                //    tree Tchird1 = chird.clone();
                //    Tchird1.Board.piece = defaultop.demino.getmino(2);
                //    Tchird1.Board.piece.setpos(19, 3);
                //    List<mino> Alltslot = search_tspin.findalltslot(Tchird1.Board); // 修改  /// 我是把tspin后的状态 提前压回该节点
                //    if (Alltslot.Count != 0) 
                //    {
                //        //List<mino> Alltslot = search_tspin.findalltslot(chird.Board);
                //        tree bestT;
                //        long minscore = chird.fieldscore;

                //        foreach (mino t in Alltslot) // 超过一个 干掉
                //        {
                //            if (!t.Tspin) continue;
                //            tree Tchird = chird.clone();
                //            Tchird.Board.piece = t;
                //            Tchird.finmino = t;
                //            res = lock_piece_calc(ref Tchird.Board);
                            
                //            if (!t.mini) {
                //                Tchird.fieldscore = bot.evalweight.evalfield(Tchird).score;
                //                Tchird.fieldscore += bot.evalweight.W.tslot[res.Item2];
                //            }
                //            else { 
                //                Tchird.fieldscore = chird.fieldscore + bot.evalweight.W.tslot[4];
                //            }
                //            //Tchird.score += bot.evalweight.evalbattle(Tchird); // 是否要battle也加上 // 其他节点借用了这个t的分值？ 需不需要加入t坑评分
                //            // 攻击也需要
                //            if (Tchird.fieldscore > minscore && Tchird.Board.piece.Tspin)
                //            {
                //                minscore = Tchird.fieldscore;
                //                bestT = Tchird;
                //            }
                //        }
                //        chird.fieldscore = minscore;
                //    }
                //}

                // 回传父节点

                chird.updatefather(); // check update
                treenode.Add(chird);
            }

            if (holdpiece == -1)
            {

                int holdidx = pieceidx + 1, nextnext = pieceidx + 2;

                if (holdidx < bot.nextcnt)
                {
                    // 保持连击的加分 // 防御是否应该削去
                    // 防御是给没打出攻击时才有的
                    // combo应该是个可延续状态

                    // 问题 特别喜欢打t1
                    // 不喜欢留防御

                    // todo T比较近的时候就可以开搜
                    // 优化

                    // 多next看t
                    // 为什么不喜欢堆防御

                    // keepcombo的 def
                    // 子节点也要前面的攻击 
                    // 软降优化

                    // 扩展被选中的节点的所有子节点

                    // 需要参与排序的有 自身场地状态 从根到该节点所打出的攻击总数 该点的攻击，该点开始保持连击打出的最大防御数 路径总数（？ 浪费t总数（这个不确定
                    // 

                    Board.piece = defaultop.demino.getmino(bot.nextqueue[holdidx % 30]);
                    Board.piece.setpos(19, 3);
                    List<mino> allpos2 = seacher.findallplace(Board);
                    foreach (mino m in allpos2)
                    {
                        tree chird = clone();
                        chird.Board.piece = m;
                        res = lock_piece_calc(ref chird.Board);
                        chird.attack = chird.maxattack = res.Item1;
                        chird.def = chird.maxdef = res.Item1 + res.Item2; // 已经消除了 还能叫防御吗
                        chird.finmino = m;
                        chird.isdead = false;
                        chird.ishold = true;
                        chird.holdpiece = nowpiece;
                        chird.father = this;
                        chird.pieceidx = nextnext;
                        chird.depth = depth + 1;
                        chird.maxdepth = chird.pieceidx;
                        chird.inplan = true;
                        chird.res = bot.evalweight.evalfield(chird);
                        chird.fieldscore = (int)chird.res.score;
                        chird.battlescore += bot.evalweight.evalbattle(chird); // 作为攻击回传
                        chird.movescore = bot.evalweight.evalmove(chird); // 相同的不要反复判了
                        chird.atkscore = bot.evalweight.evalatkdef(chird); // 相同的不要反复判了
                        //if (true || chird.holdT || hasnextT(bot, nextnext))
                        //{
                        //    tree Tchird1 = chird.clone();
                        //    Tchird1.Board.piece = defaultop.demino.getmino(2);
                        //    Tchird1.Board.piece.setpos(19, 3);
                        //    List<mino> Alltslot = search_tspin.findalltslot(Tchird1.Board);
                        //    //List<mino> Alltslot = search_tspin.findalltslot(chird.Board);
                        //    if (Alltslot.Count != 0)
                        //    {
                        //        tree bestT;
                        //        long minscore = chird.fieldscore;
                        //        foreach (mino t in Alltslot)
                        //        {
                        //            if (!t.Tspin) continue;
                        //            tree Tchird = chird.clone();
                        //            Tchird.Board.piece = t;
                        //            Tchird.finmino = t;
                        //            res = lock_piece_calc(ref Tchird.Board); // 作为防御回传

                        //            Tchird.fieldscore = bot.evalweight.evalfield(Tchird).score;

                        //            //Tchird.score += bot.evalweight.evalbattle(Tchird); // 是否要battle也加上
                        //            if (!t.mini)
                        //            {
                        //                Tchird.fieldscore = bot.evalweight.evalfield(Tchird).score;
                        //                Tchird.fieldscore += bot.evalweight.W.tslot[res.Item2];
                        //            }
                        //            else
                        //            {
                        //                Tchird.fieldscore = chird.fieldscore + bot.evalweight.W.tslot[4];
                        //            }
                        //            if (Tchird.fieldscore > minscore && Tchird.Board.piece.Tspin)
                        //            {
                        //                minscore = Tchird.fieldscore;
                        //                bestT = Tchird;
                        //            }
                        //        }
                        //        chird.fieldscore = minscore;
                        //    }
                        //}
                        chird.updatefather();
                        // 回传父节点
                        treenode.Add(chird);
                    }
                    // 回传父节点
                }


            }
            else
            {
                int temp = nowpiece;
                nowpiece = holdpiece;
                Board.piece = defaultop.demino.getmino(nowpiece);
                Board.piece.setpos(19, 3);
                List<mino> allpos1 = seacher.findallplace(Board);
                // 先对相对有用的节点更新 
                foreach (mino m in allpos1)
                {
                    tree chird = clone();
                    chird.Board.piece = m;
                    res = lock_piece_calc(ref chird.Board);
                    chird.attack = chird.maxattack = res.Item1;
                    chird.def = chird.maxdef = res.Item1 + res.Item2; // 已经消除了 还能叫防御吗
                    chird.finmino = m;
                    chird.isdead = false;
                    chird.ishold = true;
                    chird.holdpiece = temp; // oops
                    chird.pieceidx = chirdidx;
                    chird.father = this;
                    chird.depth = depth + 1;
                    chird.maxdepth = chird.depth;
                    chird.maxdepth = chird.pieceidx;
                    chird.inplan = true;
                    chird.res = bot.evalweight.evalfield(chird);
                    chird.fieldscore = (int)chird.res.score;
                    chird.battlescore += bot.evalweight.evalbattle(chird);
                    chird.movescore = bot.evalweight.evalmove(chird); // 相同的不要反复判了
                    chird.atkscore = bot.evalweight.evalatkdef(chird); // 相同的不要反复判了
                    //if (true || chird.holdT || hasnextT(bot, chirdidx))
                    //{

                    //    tree Tchird1 = chird.clone();
                    //    Tchird1.Board.piece = defaultop.demino.getmino(2);
                    //    Tchird1.Board.piece.setpos(19, 3);
                    //    List<mino> Alltslot = search_tspin.findalltslot(Tchird1.Board);
                    //    //List<mino> Alltslot = search_tspin.findalltslot(chird.Board);
                    //    if (Alltslot.Count != 0)
                    //    {
                    //        tree bestT;
                    //        long minscore = chird.fieldscore;
                    //        foreach (mino t in Alltslot)
                    //        {
                    //            if (!t.Tspin) continue; // 相同场地不要去 有些无用场地需要吗
                    //            tree Tchird = chird.clone();
                    //            Tchird.Board.piece = t;
                    //            Tchird.finmino = t;
                    //            res = lock_piece_calc(ref Tchird.Board);
                    //            Tchird.fieldscore = bot.evalweight.evalfield(Tchird).score;
                    //            //Tchird.battlescore += bot.evalweight.evalbattle(Tchird); // 是否要battle也加上
                    //            if (!t.mini)
                    //            {
                    //                Tchird.fieldscore = bot.evalweight.evalfield(Tchird).score;
                    //                Tchird.fieldscore += bot.evalweight.W.tslot[res.Item2];
                    //            }
                    //            else
                    //            {
                    //                Tchird.fieldscore = chird.fieldscore + bot.evalweight.W.tslot[4];
                    //            }
                    //            if (Tchird.fieldscore > minscore && Tchird.Board.piece.Tspin) // 可以优化计算顺序
                    //            {
                    //                minscore = Tchird.fieldscore;
                    //                bestT = Tchird;
                    //            }
                    //        }
                    //        chird.fieldscore = minscore;
                    //    }
                    //}
                    chird.updatefather();
                    // 回传父节点
                    treenode.Add(chird);
                }
            }


        }

        public void console_print()
        {

        }
        public bool checkdead()
        {
            return false; // pass 
        }
    }
}
