using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.DataFormats;

namespace mine_sweeper
{
    public partial class Form1 : Form
    {
        Stage stage;

        Bitmap bmp;
        Graphics g;

        int oneside = 9;    //一辺のマス　初期設定
        int numofmine = 10; //地雷の数　初期設定
        int numofflag = 0;  //旗の数

        public Form1()
        {
            InitializeComponent();
            stage = new Stage(oneside, numofmine);

            //描画準備
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bmp;
            g = Graphics.FromImage(bmp);


            pictureBox1.BackColor = Color.White;
            pictureBox1.Size = new Size(oneside * 50 + 1, oneside * 50 + 1);

            textBox1.Text = oneside.ToString();
            textBox2.Text = numofmine.ToString();
            textBox3.Text = numofflag.ToString();

            Drawing();

        }
        /// <summary>
        /// 描画類まとめ
        /// </summary>
        public void Drawing()
        {
            pictureBox1.Size = new Size(oneside * 50 + 1, oneside * 50 + 1);
            this.Size = (new Size(pictureBox1.Width + 100, pictureBox1.Height + 100));
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bmp;
            g = Graphics.FromImage(bmp);
            numofflag = 0;



            for (int i = 0; i <= oneside; i++)
            {
                int x = i * 50;
                g.DrawLine(Pens.Black, 0, x, oneside * 50, x);
                g.DrawLine(Pens.Black, x, 0, x, oneside * 50);
            }

            //文字描画
            var format = new StringFormat();
            format.Alignment = StringAlignment.Center;      // 左右方向は中心寄せ
            format.LineAlignment = StringAlignment.Center;  // 上下方向は中心寄せ
            Font fnt = new Font("MS UI Gothic", 25);
            SolidBrush brs = new SolidBrush(Color.Black);

            //地雷と周辺個数の表示
            for (int j = 0; j <= oneside; j++)
                for (int i = 0; i <= oneside; i++)
                    switch (stage.mine_field[i][j].ToString())
                    {
                        case "0":
                            DrawString(g, "", new Font("MS UI Gothic", 25),
                                new SolidBrush(Color.Black), i * 50 - 25, j * 50 - 25, 0, format);
                            break;
                        case "1":
                            DrawString(g, "1", new Font("MS UI Gothic", 25),
                                new SolidBrush(Color.Black), i * 50 - 25, j * 50 - 25, 0, format);
                            break;
                        case "2":
                            DrawString(g, "2", new Font("MS UI Gothic", 25),
                                new SolidBrush(Color.Black), i * 50 - 25, j * 50 - 25, 0, format);
                            break;
                        case "3":
                            DrawString(g, "3", new Font("MS UI Gothic", 25),
                                new SolidBrush(Color.Black), i * 50 - 25, j * 50 - 25, 0, format);
                            break;
                        case "4":
                            DrawString(g, "4", new Font("MS UI Gothic", 25),
                                new SolidBrush(Color.Black), i * 50 - 25, j * 50 - 25, 0, format);
                            break;
                        case "5":
                            DrawString(g, "5", new Font("MS UI Gothic", 25),
                                new SolidBrush(Color.Black), i * 50 - 25, j * 50 - 25, 0, format);
                            break;
                        case "6":
                            DrawString(g, "6", new Font("MS UI Gothic", 25),
                                new SolidBrush(Color.Black), i * 50 - 25, j * 50 - 25, 0, format);
                            break;
                        case "7":
                            DrawString(g, "7", new Font("MS UI Gothic", 25),
                                new SolidBrush(Color.Black), i * 50 - 25, j * 50 - 25, 0, format);
                            break;
                        case "100":
                            DrawString(g, "●", new Font("MS UI Gothic", 25),
                                new SolidBrush(Color.Black), i * 50 - 25, j * 50 - 25, 0, format);
                            break;
                        default:
                            break;

                    }

            //灰色フィールド地面の表示
            for (int i = 1; i <= oneside; i++)
                for (int j = 1; j <= oneside; j++)
                {
                    if (stage.open_field[i][j] == 0)
                        g.FillRectangle(Brushes.Gray, (i - 1) * 50 + 1, (j - 1) * 50 + 1, 49, 49);
                    if (stage.open_field[i][j] == 2)
                    {
                        g.FillRectangle(Brushes.Gray, (i - 1) * 50 + 1, (j - 1) * 50 + 1, 49, 49);
                        Point[] pointsFlag = new Point[4];
                        pointsFlag[0] = new Point((i - 1) * 50 + 10, (j - 1) * 50 + 40);
                        pointsFlag[1] = new Point((i - 1) * 50 + 10, (j - 1) * 50 + 10);
                        pointsFlag[2] = new Point((i - 1) * 50 + 40, (j - 1) * 50 + 20);
                        pointsFlag[3] = new Point((i - 1) * 50 + 10, (j - 1) * 50 + 30);
                        g.DrawLines(Pens.Black, pointsFlag);
                        numofflag++;
                    }
                }
            textBox1.Text = oneside.ToString();
            textBox2.Text = numofmine.ToString();
            textBox3.Text = numofflag.ToString();
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            int x = e.X / 50 + 1;
            int y = e.Y / 50 + 1;

            //右クリックならフラグを立てる or フラグを消す
            if (e.Button == MouseButtons.Right)
                if (stage.open_field[x][y] == 0)
                    stage.open_field[x][y] = 2;
                else if (stage.open_field[x][y] == 2)
                    stage.open_field[x][y] = 0;
                else { }
            //左クリックなら探索操作
            else
                new UserChoice(stage, x, y);

            //描画処理
            Drawing();

            //ゲーム終了判定
            if (stage.gameover)
                MessageBox.Show("ゲームオーバー");
            else
                stage.GameClearCheck();
            if (stage.gameclear)
                MessageBox.Show("ゲームクリア");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ユーザによる初期設定を読み込み、再スタート
            oneside = int.Parse(textBox1.Text);
            numofmine = int.Parse(textBox2.Text);
            numofflag = 0;

            //最大最小の数を定めておく
            if (oneside > 20)
                oneside = 20;
            if (oneside < 7)
                oneside = 7;

            //地雷数の最大を定めておく（一辺の2乗を超えないよう）
            if (numofmine > Math.Pow(oneside, 2))
                numofmine = (int)Math.Pow(oneside, 2);

            stage = new Stage(oneside, numofmine);
            
            //描画
            Drawing();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// 文字列の描画、回転、基準位置指定
        /// 参考　https://imagingsolution.net/program/drawstring_rotate/
        /// </summary>
        /// <param name="g">描画先のGraphicsオブジェクト</param>
        /// <param name="s">描画する文字列</param>
        /// <param name="f">文字のフォント</param>
        /// <param name="brush">描画用ブラシ</param>
        /// <param name="x">基準位置のX座標</param>
        /// <param name="y">基準位置のY座標</param>
        /// <param name="deg">回転角度（度数、時計周りが正）</param>
        /// <param name="format">基準位置をStringFormatクラスオブジェクトで指定します</param>
        public void DrawString(Graphics g, string s, Font f, SolidBrush brush, float x, float y, float deg, StringFormat format)
        {
            using (var pathText = new System.Drawing.Drawing2D.GraphicsPath())  // パスの作成
            using (var mat = new System.Drawing.Drawing2D.Matrix())             // アフィン変換行列
            {
                // 描画用Format
                var formatTemp = (StringFormat)format.Clone();
                formatTemp.Alignment = StringAlignment.Near;        // 左寄せに修正
                formatTemp.LineAlignment = StringAlignment.Near;    // 上寄せに修正

                // 文字列の描画
                pathText.AddString(s, f.FontFamily, (int)f.Style, f.SizeInPoints, new PointF(0, 0), format);
                formatTemp.Dispose();

                // 文字の領域取得
                var rect = pathText.GetBounds();

                // 回転中心のX座標
                float px;
                switch (format.Alignment)
                {
                    case StringAlignment.Near:
                        px = rect.Left;
                        break;
                    case StringAlignment.Center:
                        px = rect.Left + rect.Width / 2f;
                        break;
                    case StringAlignment.Far:
                        px = rect.Right;
                        break;
                    default:
                        px = 0;
                        break;
                }
                // 回転中心のY座標
                float py;
                switch (format.LineAlignment)
                {
                    case StringAlignment.Near:
                        py = rect.Top;
                        break;
                    case StringAlignment.Center:
                        py = rect.Top + rect.Height / 2f;
                        break;
                    case StringAlignment.Far:
                        py = rect.Bottom;
                        break;
                    default:
                        py = 0;
                        break;
                }

                // 文字の回転中心座標を原点へ移動
                mat.Translate(-px, -py, System.Drawing.Drawing2D.MatrixOrder.Append);
                // 文字の回転
                mat.Rotate(deg, System.Drawing.Drawing2D.MatrixOrder.Append);
                // 表示位置まで移動
                mat.Translate(x, y, System.Drawing.Drawing2D.MatrixOrder.Append);

                // パスをアフィン変換
                pathText.Transform(mat);

                // 描画
                g.FillPath(brush, pathText);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
