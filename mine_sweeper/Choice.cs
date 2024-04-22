using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine_sweeper
{
    internal class UserChoice
    {

        public UserChoice(Stage stg, int x, int y)
        {
            if (stg.open_field[x][y] == 0)
                choice_close(stg, x, y);
            else
                choice_open_num(stg, x, y);
        }

        /// <summary>
        /// まだ開かれていないマスが選択された場合
        /// </summary>
        /// <param name="stg"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void choice_close(Stage stg, int x, int y)
        {
            List<Point> points = new List<Point>();
            if (stg.mine_field[x][y] == 0)              //空マスを開いた場合は、隣接する空マスも合わせて開く
            {
                points = open_around_safe(stg, x, y);   //周辺の空いているマスを探索
                for (int i = 0; i < points.Count; i++)  //探索したマス分、マスを開く
                    stg.open_field[points[i].X][points[i].Y] = 1;
            }
            else if (stg.mine_field[x][y] != 100)       //数字のあるマスを開いた場合
                stg.open_field[x][y] = 1;               //マスを空ける
            else                                        //地雷のあるマスを開いた場合
                stg.GameOver();                         //ゲームオーバー
        }

        /// <summary>
        /// すでに開かれているマスが選択された場合
        /// [自分のマスの数字]と[周辺にある旗の数]が同じなら、周辺のマスを空ける
        /// </summary>
        /// <param name="stg"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void choice_open_num(Stage stg, int x, int y)
        {           
            int flagcheck = 0;                          //周辺の旗の数
            List<Point> openlist = new List<Point>();   //空ける予定のマスのリスト

            //周辺の旗の数を数える
            if (stg.open_field[x + 1][y + 1] == 2) flagcheck++; else openlist.Add(new Point(x + 1, y + 1));
            if (stg.open_field[x + 1][y    ] == 2) flagcheck++; else openlist.Add(new Point(x + 1, y    ));
            if (stg.open_field[x + 1][y - 1] == 2) flagcheck++; else openlist.Add(new Point(x + 1, y - 1));
            if (stg.open_field[x    ][y + 1] == 2) flagcheck++; else openlist.Add(new Point(x    , y + 1));
            if (stg.open_field[x    ][y - 1] == 2) flagcheck++; else openlist.Add(new Point(x    , y - 1));
            if (stg.open_field[x - 1][y + 1] == 2) flagcheck++; else openlist.Add(new Point(x - 1, y + 1));
            if (stg.open_field[x - 1][y    ] == 2) flagcheck++; else openlist.Add(new Point(x - 1, y    ));
            if (stg.open_field[x - 1][y - 1] == 2) flagcheck++; else openlist.Add(new Point(x - 1, y - 1));
            
            List<Point> points = new List<Point>();

            if (stg.mine_field[x][y] == flagcheck)          //自分のマスの数字と周辺にある旗の数が同じであれば以下処理
                for (int i = 0; i < openlist.Count; i++)
                {
                    //空ける予定リストのマスを空ける、もしそこに地雷があればゲームオーバー
                    stg.open_field[openlist[i].X][openlist[i].Y] = 1;
                    if (stg.mine_field[openlist[i].X][openlist[i].Y] == 100)
                    {
                        stg.GameOver();
                        break;
                    }

                    //空マスがあれば同時に隣接する空マスも合わせて開く
                    if (stg.mine_field[openlist[i].X][openlist[i].Y] == 0)              //空マスを開いた場合
                    {
                        points = open_around_safe(stg, openlist[i].X, openlist[i].Y);    //周辺の空いているマスを探索
                        for (int j = 0; j < points.Count; j++)//探索したマス分、マスを開く
                            stg.open_field[points[j].X][points[j].Y] = 1;
                    }
                }
        }

        /// <summary>
        /// 隣接する空マスを探索
        /// </summary>
        /// <param name="stg"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public List<Point> open_around_safe(Stage stg, int x,int y)
        {
            List<Point> openlist = new List<Point>();
            openlist.Add(new Point(x,y));

            //空マスを開く
            //もし周辺のマスが空でかつ開く予定リストに入っていなければ開く予定リストに追加
            for(int i = 0; i < openlist.Count; i++)
            {
                if (stg.mine_field[openlist[i].X + 1][openlist[i].Y + 1] == 0 && !openlist.Contains(new Point(openlist[i].X + 1, openlist[i].Y + 1)))
                    openlist.Add(new Point(openlist[i].X + 1, openlist[i].Y + 1));
                if (stg.mine_field[openlist[i].X + 1][openlist[i].Y    ] == 0 && !openlist.Contains(new Point(openlist[i].X + 1, openlist[i].Y    )))
                    openlist.Add(new Point(openlist[i].X + 1, openlist[i].Y    ));
                if (stg.mine_field[openlist[i].X + 1][openlist[i].Y - 1] == 0 && !openlist.Contains(new Point(openlist[i].X + 1, openlist[i].Y - 1)))
                    openlist.Add(new Point(openlist[i].X + 1, openlist[i].Y - 1));
                if (stg.mine_field[openlist[i].X    ][openlist[i].Y + 1] == 0 && !openlist.Contains(new Point(openlist[i].X    , openlist[i].Y + 1)))
                    openlist.Add(new Point(openlist[i].X    , openlist[i].Y + 1));
                if (stg.mine_field[openlist[i].X    ][openlist[i].Y - 1] == 0 && !openlist.Contains(new Point(openlist[i].X    , openlist[i].Y - 1)))
                    openlist.Add(new Point(openlist[i].X    , openlist[i].Y - 1));
                if (stg.mine_field[openlist[i].X - 1][openlist[i].Y + 1] == 0 && !openlist.Contains(new Point(openlist[i].X - 1, openlist[i].Y + 1)))
                    openlist.Add(new Point(openlist[i].X - 1, openlist[i].Y + 1));
                if (stg.mine_field[openlist[i].X - 1][openlist[i].Y    ] == 0 && !openlist.Contains(new Point(openlist[i].X - 1, openlist[i].Y    )))
                    openlist.Add(new Point(openlist[i].X - 1, openlist[i].Y    ));
                if (stg.mine_field[openlist[i].X - 1][openlist[i].Y - 1] == 0 && !openlist.Contains(new Point(openlist[i].X - 1, openlist[i].Y - 1)))
                    openlist.Add(new Point(openlist[i].X - 1, openlist[i].Y - 1));

            }

            //追加で空マスの周辺も開く
            int zero_masu = openlist.Count;
            for(int i = 0; i < zero_masu; i++)
            {
                if (!openlist.Contains(new Point(openlist[i].X + 1, openlist[i].Y + 1))) openlist.Add(new Point(openlist[i].X + 1, openlist[i].Y + 1));
                if (!openlist.Contains(new Point(openlist[i].X + 1, openlist[i].Y    ))) openlist.Add(new Point(openlist[i].X + 1, openlist[i].Y    ));
                if (!openlist.Contains(new Point(openlist[i].X + 1, openlist[i].Y - 1))) openlist.Add(new Point(openlist[i].X + 1, openlist[i].Y - 1));
                if (!openlist.Contains(new Point(openlist[i].X    , openlist[i].Y + 1))) openlist.Add(new Point(openlist[i].X    , openlist[i].Y + 1));
                if (!openlist.Contains(new Point(openlist[i].X    , openlist[i].Y - 1))) openlist.Add(new Point(openlist[i].X    , openlist[i].Y - 1));
                if (!openlist.Contains(new Point(openlist[i].X - 1, openlist[i].Y + 1))) openlist.Add(new Point(openlist[i].X - 1, openlist[i].Y + 1));
                if (!openlist.Contains(new Point(openlist[i].X - 1, openlist[i].Y    ))) openlist.Add(new Point(openlist[i].X - 1, openlist[i].Y    ));
                if (!openlist.Contains(new Point(openlist[i].X - 1, openlist[i].Y - 1))) openlist.Add(new Point(openlist[i].X - 1, openlist[i].Y - 1));
            }
            return openlist;
        }













        List<Point> resultlist = new List<Point>();
        List<Point> waitlist = new List<Point>();
        private void habayusen(Stage stg, Point point)
        {
            waitlist.Add(point);

            //空マスを開く
            for (int i = 0; i < waitlist.Count; i++)
            {
                if (stg.mine_field[waitlist[i].X + 1][waitlist[i].Y] == 0 && !waitlist.Contains(new Point(waitlist[i].X + 1, waitlist[i].Y)))
                    waitlist.Add(new Point(waitlist[i].X + 1, waitlist[i].Y));
                if (stg.mine_field[waitlist[i].X - 1][waitlist[i].Y] == 0 && !waitlist.Contains(new Point(waitlist[i].X - 1, waitlist[i].Y)))
                    waitlist.Add(new Point(waitlist[i].X - 1, waitlist[i].Y));
                if (stg.mine_field[waitlist[i].X][waitlist[i].Y + 1] == 0 && !waitlist.Contains(new Point(waitlist[i].X, waitlist[i].Y + 1)))
                    waitlist.Add(new Point(waitlist[i].X, waitlist[i].Y + 1));
                if (stg.mine_field[waitlist[i].X][waitlist[i].Y - 1] == 0 && !waitlist.Contains(new Point(waitlist[i].X, waitlist[i].Y - 1)))
                    waitlist.Add(new Point(waitlist[i].X, waitlist[i].Y - 1));
            }
            resultlist.Add(point);

        }
    }


}
