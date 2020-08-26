using System;
using System.Collections.Generic;
using System.Text;

namespace Jura_Knife_Tetris
{
    public class mino_gene
    {
        //public mino I = new mino(new int[,] {
        //    { 0, 0, 0, 0},
        //    { 0, 0, 0, 0},
        //    { 1, 1, 1, 1},
        //    { 0, 0, 0, 0},
        //    },

        //    new pos[,] {
        //    { new pos(0, 0), new pos (-2, 0), new pos(+1, 0), new pos (-2, -1), new pos(+1, +2)},
        //    { new pos(0, 0), new pos (-1, 0), new pos(+2, 0), new pos (-1, +2), new pos(+2, -1)},
        //    { new pos(0, 0), new pos (+2, 0), new pos(-1, 0), new pos (+2, +1), new pos(-1, -2)},
        //    { new pos(0, 0), new pos (+1, 0), new pos(-2, 0), new pos (+1, -2), new pos(-2, +1)},
        //    },

        //    0,

        //    new pos(),

        //    "I-mino"
        //    );
        //public mino O = new mino(new int[,] {
        //    { 0, 0, 0, 0},
        //    { 0, 1, 1, 0},
        //    { 0, 1, 1, 0},
        //    { 0, 0, 0, 0},
        //    },

        //    new pos[,] {
        //    { new pos(0, 0)},
        //    { new pos(0, 0)},
        //    { new pos(0, 0)},
        //    { new pos(0, 0)},
        //    },

        //    0,

        //    new pos(),

        //    "O-mino"
        //    );

        //public mino T = new mino(new int[,] {
        //    { 0, 0, 0 },
        //    { 1, 1, 1 },
        //    { 0, 1, 0 },
        //    },

        //    new pos[,] {
        //    { new pos(0, 0), new pos (-1, 0), new pos(-1, +1), new pos (0, -2), new pos(-1, -2)},
        //    { new pos(0, 0), new pos (+1, 0), new pos(+1, -1), new pos (0, +2), new pos(+1, +2)},
        //    { new pos(0, 0), new pos (+1, 0), new pos(+1, +1), new pos (0, -2), new pos(+1, -2)},
        //    { new pos(0, 0), new pos (-1, 0), new pos(-1, -1), new pos (0, +2), new pos(-1, +2)},
        //    },

        //    0,

        //    new pos(),

        //    "T-mino"
        //    );
        //public mino J = new mino(new int[,] {
        //    { 0, 0, 0 },
        //    { 1, 1, 1 },
        //    { 1, 0, 0 } },

        //    new pos[,] {
        //    { new pos(0, 0), new pos (-1, 0), new pos(-1, +1), new pos (0, -2), new pos(-1, -2)},
        //    { new pos(0, 0), new pos (+1, 0), new pos(+1, -1), new pos (0, +2), new pos(+1, +2)},
        //    { new pos(0, 0), new pos (+1, 0), new pos(+1, +1), new pos (0, -2), new pos(+1, -2)},
        //    { new pos(0, 0), new pos (-1, 0), new pos(-1, -1), new pos (0, +2), new pos(-1, +2)},
        //    },

        //    0,

        //    new pos(),

        //    "J-mino"
        //    );
        //public mino L = new mino(new int[,] {
        //    { 0, 0, 0 },
        //    { 1, 1, 1 },
        //    { 0, 0, 1 } },

        //new pos[,] {
        //    { new pos(0, 0), new pos (-1, 0), new pos(-1, +1), new pos (0, -2), new pos(-1, -2)},
        //    { new pos(0, 0), new pos (+1, 0), new pos(+1, -1), new pos (0, +2), new pos(+1, +2)},
        //    { new pos(0, 0), new pos (+1, 0), new pos(+1, +1), new pos (0, -2), new pos(+1, -2)},
        //    { new pos(0, 0), new pos (-1, 0), new pos(-1, -1), new pos (0, +2), new pos(-1, +2)},
        //    },

        //0,

        //new pos(),

        //"L-mino"
        //);
        //public mino S = new mino(new int[,] {
        //    { 0, 0, 0 },
        //    { 1, 1, 0 },
        //    { 0, 1, 1 } },

        //new pos[,] {
        //    { new pos(0, 0), new pos (-1, 0), new pos(-1, +1), new pos (0, -2), new pos(-1, -2)},
        //    { new pos(0, 0), new pos (+1, 0), new pos(+1, -1), new pos (0, +2), new pos(+1, +2)},
        //    { new pos(0, 0), new pos (+1, 0), new pos(+1, +1), new pos (0, -2), new pos(+1, -2)},
        //    { new pos(0, 0), new pos (-1, 0), new pos(-1, -1), new pos (0, +2), new pos(-1, +2)},
        //    },

        //0,

        //new pos(),

        //"S-mino"
        //);
        //public mino Z = new mino(new int[,] {
        //    { 0, 0, 0 },
        //    { 0, 1, 1 },
        //    { 1, 1, 0 } },

        //new pos[,] {
        //    { new pos(0, 0), new pos (-1, 0), new pos(-1, +1), new pos (0, -2), new pos(-1, -2)},
        //    { new pos(0, 0), new pos (+1, 0), new pos(+1, -1), new pos (0, +2), new pos(+1, +2)},
        //    { new pos(0, 0), new pos (+1, 0), new pos(+1, +1), new pos (0, -2), new pos(+1, -2)},
        //    { new pos(0, 0), new pos (-1, 0), new pos(-1, -1), new pos (0, +2), new pos(-1, +2)},
        //    },

        //0,

        //new pos(),

        //"Z-mino"
        //);


        public mino getmino(int id)
        {
            //string minoname = "IOTJLZS";
            string minoname = "OITLJSZ";
            Type t = typeof(mino_gene);
            return (mino)t.GetMethod("get_mino_" + minoname[id]).Invoke(this, null);
        }
        public mino getmino(string name)
        {
            //string minoname = "IOTJLZS";

            Type t = typeof(mino_gene);
            return (mino)t.GetMethod("get_mino_" + name).Invoke(this, null);
        }

        delegate mino Minofun();
        int piececnt = 0;
        int[] tabidx = {
                    0, 1, 2, 3, 4, 5, 6
                };

        public int genebag7int()
        {
            int a = bag7.Next() % (7 - piececnt % 7);
            int temp = tabidx[a];
            tabidx[a] = tabidx[6 - piececnt % 7];
            tabidx[6 - piececnt % 7] = temp;
            piececnt++;
            return tabidx[6 - (piececnt - 1) % 7];
        }
        public mino generandmino()
        {

            Minofun[] tab = {
                    get_mino_I,
                    get_mino_O,
                    get_mino_T,
                    get_mino_J,
                    get_mino_L,
                    get_mino_Z,
                    get_mino_S
                };
            return tab[bag7.Next() % 7]();
        }
        public mino genebag7mino()
        {

            Minofun[] tab = {
                    get_mino_I,
                    get_mino_O,
                    get_mino_T,
                    get_mino_J,
                    get_mino_L,
                    get_mino_Z,
                    get_mino_S
                };
            int a = bag7.Next() % (7 - piececnt % 7);
            int temp = tabidx[a];
            tabidx[a] = tabidx[6 - piececnt % 7];
            tabidx[6 - piececnt % 7] = temp;
            piececnt++;
            return tab[tabidx[6 - (piececnt-1) % 7]]();
        }
        Random bag7 = new Random();
        public mino[] bag7mino()
        {
            mino[] tab = { get_mino_I(), get_mino_O(), get_mino_T(),
                get_mino_J(), get_mino_L(), get_mino_Z(), get_mino_S() };
            

            for (int i = 0; i < 5; ++i)
            {
                int a = bag7.Next() % 7;
                int b = bag7.Next() % 7;
                mino temp;
                temp = tab[a];
                tab[a] = tab[b];
                tab[b] = temp;
            }
            return tab;
        }
        public mino get_mino_I()
        {
            mino m = new mino(new int[,] {
            { 0, 0, 0, 0},
            { 0, 0, 0, 0},
            { 1, 1, 1, 1},
            { 0, 0, 0, 0},
            },

            new pos[,] {
            { new pos(0, 0), new pos (-2, 0), new pos(+1, 0), new pos (-2, -1), new pos(+1, +2)},
            { new pos(0, 0), new pos (-1, 0), new pos(+2, 0), new pos (-1, +2), new pos(+2, -1)},
            { new pos(0, 0), new pos (+2, 0), new pos(-1, 0), new pos (+2, +1), new pos(-1, -2)},
            { new pos(0, 0), new pos (+1, 0), new pos(-2, 0), new pos (+1, -2), new pos(-2, +1)},
            },

            0,

            new pos(),

            "I"
            );
            return m;
        }
        //public mino get_mino_O()
        //{
        //    mino m = new mino(new int[,] {
        //    { 0, 0, 0, 0},
        //    { 0, 1, 1, 0},
        //    { 0, 1, 1, 0},
        //    { 0, 0, 0, 0},
        //    },

        //    new pos[,] {
        //    { new pos(0, 0)},
        //    { new pos(0, 0)},
        //    { new pos(0, 0)},
        //    { new pos(0, 0)},
        //    },

        //    0,

        //    new pos(),

        //    "O"
        //    );
        //    return m;
        //}

        public mino get_mino_O()
        {
            mino m = new mino(new int[,] {
           { 0,0, 0,0},
                { 0,1, 1,0},
            { 0,1, 1,0},
            { 0,0, 0,0},
            },

            new pos[,] {
            { new pos(0, 0)},
            { new pos(0, 0)},
            { new pos(0, 0)},
            { new pos(0, 0)},
            },

            0,

            new pos(),

            "O"
            );
            return m;
        }
        public mino get_mino_T()
        {
            mino m = new mino(new int[,] {
            { 0, 0, 0 },
            { 1, 1, 1 },
            { 0, 1, 0 },
            },

            new pos[,] {
            { new pos(0, 0), new pos (-1, 0), new pos(-1, +1), new pos (0, -2), new pos(-1, -2)},
            { new pos(0, 0), new pos (+1, 0), new pos(+1, -1), new pos (0, +2), new pos(+1, +2)},
            { new pos(0, 0), new pos (+1, 0), new pos(+1, +1), new pos (0, -2), new pos(+1, -2)},
            { new pos(0, 0), new pos (-1, 0), new pos(-1, -1), new pos (0, +2), new pos(-1, +2)},
            },

            0,

            new pos(),

            "T"
            );
            return m;
        }
        public mino get_mino_J()
        {
            mino m = new mino(new int[,] {
            { 0, 0, 0 },
            { 1, 1, 1 },
            { 1, 0, 0 } },

            new pos[,] {
            { new pos(0, 0), new pos (-1, 0), new pos(-1, +1), new pos (0, -2), new pos(-1, -2)},
            { new pos(0, 0), new pos (+1, 0), new pos(+1, -1), new pos (0, +2), new pos(+1, +2)},
            { new pos(0, 0), new pos (+1, 0), new pos(+1, +1), new pos (0, -2), new pos(+1, -2)},
            { new pos(0, 0), new pos (-1, 0), new pos(-1, -1), new pos (0, +2), new pos(-1, +2)},
            },

            0,

            new pos(),

            "J"
            );
            return m;
        }
        public mino get_mino_L()
        {
            mino m = new mino(new int[,] {
            { 0, 0, 0 },
            { 1, 1, 1 },
            { 0, 0, 1 } },

            new pos[,] {
            { new pos(0, 0), new pos (-1, 0), new pos(-1, +1), new pos (0, -2), new pos(-1, -2)},
            { new pos(0, 0), new pos (+1, 0), new pos(+1, -1), new pos (0, +2), new pos(+1, +2)},
            { new pos(0, 0), new pos (+1, 0), new pos(+1, +1), new pos (0, -2), new pos(+1, -2)},
            { new pos(0, 0), new pos (-1, 0), new pos(-1, -1), new pos (0, +2), new pos(-1, +2)},
            },

            0,

            new pos(),

            "L"
            );
            return m;
        }
        public mino get_mino_S()
        {
            mino m = new mino(new int[,] {
            { 0, 0, 0 },
            { 1, 1, 0 },
            { 0, 1, 1 } },

            new pos[,] {
            { new pos(0, 0), new pos (-1, 0), new pos(-1, +1), new pos (0, -2), new pos(-1, -2)},
            { new pos(0, 0), new pos (+1, 0), new pos(+1, -1), new pos (0, +2), new pos(+1, +2)},
            { new pos(0, 0), new pos (+1, 0), new pos(+1, +1), new pos (0, -2), new pos(+1, -2)},
            { new pos(0, 0), new pos (-1, 0), new pos(-1, -1), new pos (0, +2), new pos(-1, +2)},
            },

            0,

            new pos(),

            "S"
            );
            return m;
        }
        public mino get_mino_Z()
        {
            mino m = new mino(new int[,] {
            { 0, 0, 0 },
            { 0, 1, 1 },
            { 1, 1, 0 } },

            new pos[,] {
            { new pos(0, 0), new pos (-1, 0), new pos(-1, +1), new pos (0, -2), new pos(-1, -2)},
            { new pos(0, 0), new pos (+1, 0), new pos(+1, -1), new pos (0, +2), new pos(+1, +2)},
            { new pos(0, 0), new pos (+1, 0), new pos(+1, +1), new pos (0, -2), new pos(+1, -2)},
            { new pos(0, 0), new pos (-1, 0), new pos(-1, -1), new pos (0, +2), new pos(-1, +2)},
            },

            0,

            new pos(),

            "Z"
            );
            return m;
        }

        public mino get_mino_wu()
        {
            mino m = new mino(new int[,] {
            { 1, 0, 0, 1, 1, 1, 0},
            { 0, 0, 1, 0, 0, 0, 0},
            { 0, 1, 1, 1, 1, 1, 1},
            { 0, 1, 0, 0, 0, 0, 0},
            { 1, 1, 0, 0, 1, 1, 1},
            { 1, 0, 0, 0, 0, 1, 0},
            { 1, 0, 0, 0, 1, 1, 1},},

            new pos[,] {
            { new pos(0, 0), new pos (-1, 0), new pos(-1, +1), new pos (0, -2), new pos(-1, -2)},
            { new pos(0, 0), new pos (+1, 0), new pos(+1, -1), new pos (0, +2), new pos(+1, +2)},
            { new pos(0, 0), new pos (+1, 0), new pos(+1, +1), new pos (0, -2), new pos(+1, -2)},
            { new pos(0, 0), new pos (-1, 0), new pos(-1, -1), new pos (0, +2), new pos(-1, +2)},
            },

            0,

            new pos(),

            "wu"
            );
            return m;
        }

        public mino get_mino_han()
        {
            mino m = new mino(new int[,] {
            { 0, 0, 0, 0, 0, 0, 1},
            { 1, 1, 1, 1, 0, 1, 0},
            { 0, 1, 0, 1, 0, 0, 0},
            { 0, 0, 1, 0, 0, 1, 1},
            { 0, 1, 0, 1, 0, 0, 0},
            { 1, 0, 0, 0, 1, 1, 0},
            { 0, 0, 0, 0, 0, 0, 1},},

            new pos[,] {
            { new pos(0, 0), new pos (-1, 0), new pos(-1, +1), new pos (0, -2), new pos(-1, -2)},
            { new pos(0, 0), new pos (+1, 0), new pos(+1, -1), new pos (0, +2), new pos(+1, +2)},
            { new pos(0, 0), new pos (+1, 0), new pos(+1, +1), new pos (0, -2), new pos(+1, -2)},
            { new pos(0, 0), new pos (-1, 0), new pos(-1, -1), new pos (0, +2), new pos(-1, +2)},
            },

            0,

            new pos(),

            "wu"
            );
            return m;
        }

    }
}
