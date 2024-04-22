using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine_sweeper
{
    internal class Stage
    {
        /// <summary>
        /// 地雷の有無・周辺地雷の個数・壁
        /// 1~8:周辺の地雷の個数
        /// 100:地雷が入っている
        /// 10000:壁
        /// </summary>
        public List<List<int>> mine_field = new List<List<int>>();
        /// <summary>
        /// フィールドが開閉・フラグの有無状態
        /// 0:閉じている
        /// 1:開いている
        /// 2:閉じているかつフラグ付
        /// </summary>
        public List<List<int>> open_field = new List<List<int>>();
        /// <summary>
        /// 地雷のある座標のリスト
        /// </summary>
        public List<Point> mine_point = new List<Point>();

        public bool gameover = false;
        public bool gameclear = false;

        public Stage(int oneside, int mine_num)
        {
            //フィールドに壁として上下左右1マスずつ余分に確保しておくため、一辺2だけ足しておく
            oneside += 2;

            //フィールドの生成
            for (int i = 0; i < oneside; i++)
            {
                mine_field.Add(new List<int>());
                open_field.Add(new List<int>());
                for (int j = 0; j < oneside; j++)
                {
                    mine_field[i].Add(0);
                    open_field[i].Add(0);
                }
            }

            //地雷の位置をランダムで確定
            Random r = new Random((int)DateTime.Now.Ticks);
            mine_point = new List<Point>();

            for (int i = 0; i < mine_num; i++)
            {
                //
                int randX = r.Next(1, oneside - 1);
                int randY = r.Next(1, oneside - 1);
                mine_point.Add(new Point(randX, randY));
            }

            //地雷の配置
            for (int i = 0; i < mine_point.Count; i++)
                mine_field[mine_point[i].X][mine_point[i].Y] = 100;

            //周囲4辺に壁の配置
            for (int i = 0; i < oneside; i++)
            {
                mine_field[0][i] = 10000;
                mine_field[i][0] = 10000;
                mine_field[i][oneside - 1] = 10000;
                mine_field[oneside - 1][i] = 10000;
            }


            //フィールドを更新
            //各マスの周辺の地雷の数を数え、フィールドに代入            
            for (int i = 1; i < mine_field.Count - 1; i++)
            {
                for (int j = 1; j < mine_field[i].Count - 1; j++)
                {
                    if (mine_field[i][j].ToString() != "100")
                    {
                        if (mine_field[i - 1][j - 1].ToString() == "100") mine_field[i][j]++;
                        if (mine_field[i][j - 1].ToString() == "100") mine_field[i][j]++;
                        if (mine_field[i + 1][j - 1].ToString() == "100") mine_field[i][j]++;
                        if (mine_field[i - 1][j].ToString() == "100") mine_field[i][j]++;
                        if (mine_field[i + 1][j].ToString() == "100") mine_field[i][j]++;
                        if (mine_field[i - 1][j + 1].ToString() == "100") mine_field[i][j]++;
                        if (mine_field[i][j + 1].ToString() == "100") mine_field[i][j]++;
                        if (mine_field[i + 1][j + 1].ToString() == "100") mine_field[i][j]++;
                    }
                }
            }
        }

        public void GameOver()
        {
            for (int i = 0; i < open_field.Count; i++)
                for (int j = 0; j < open_field[i].Count; j++)
                    open_field[i][j] = 1;
            gameover = true;
            
        }

        public void GameClearCheck()
        {
            gameclear = true;
            for (int i = 1; i < open_field.Count - 1; i++)
                for (int j = 1; j < open_field[i].Count - 1; j++)
                    if (open_field[i][j] == 0 && mine_field[i][j] != 100)
                    {
                        gameclear = false;
                        break;
                    }
        }

    }
}
