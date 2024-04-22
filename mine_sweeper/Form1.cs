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

        int oneside = 9;    //��ӂ̃}�X�@�����ݒ�
        int numofmine = 10; //�n���̐��@�����ݒ�
        int numofflag = 0;  //���̐�

        public Form1()
        {
            InitializeComponent();
            stage = new Stage(oneside, numofmine);

            //�`�揀��
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
        /// �`��ނ܂Ƃ�
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

            //�����`��
            var format = new StringFormat();
            format.Alignment = StringAlignment.Center;      // ���E�����͒��S��
            format.LineAlignment = StringAlignment.Center;  // �㉺�����͒��S��
            Font fnt = new Font("MS UI Gothic", 25);
            SolidBrush brs = new SolidBrush(Color.Black);

            //�n���Ǝ��ӌ��̕\��
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
                            DrawString(g, "��", new Font("MS UI Gothic", 25),
                                new SolidBrush(Color.Black), i * 50 - 25, j * 50 - 25, 0, format);
                            break;
                        default:
                            break;

                    }

            //�D�F�t�B�[���h�n�ʂ̕\��
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

            //�E�N���b�N�Ȃ�t���O�𗧂Ă� or �t���O������
            if (e.Button == MouseButtons.Right)
                if (stage.open_field[x][y] == 0)
                    stage.open_field[x][y] = 2;
                else if (stage.open_field[x][y] == 2)
                    stage.open_field[x][y] = 0;
                else { }
            //���N���b�N�Ȃ�T������
            else
                new UserChoice(stage, x, y);

            //�`�揈��
            Drawing();

            //�Q�[���I������
            if (stage.gameover)
                MessageBox.Show("�Q�[���I�[�o�[");
            else
                stage.GameClearCheck();
            if (stage.gameclear)
                MessageBox.Show("�Q�[���N���A");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //���[�U�ɂ�鏉���ݒ��ǂݍ��݁A�ăX�^�[�g
            oneside = int.Parse(textBox1.Text);
            numofmine = int.Parse(textBox2.Text);
            numofflag = 0;

            //�ő�ŏ��̐����߂Ă���
            if (oneside > 20)
                oneside = 20;
            if (oneside < 7)
                oneside = 7;

            //�n�����̍ő���߂Ă����i��ӂ�2��𒴂��Ȃ��悤�j
            if (numofmine > Math.Pow(oneside, 2))
                numofmine = (int)Math.Pow(oneside, 2);

            stage = new Stage(oneside, numofmine);
            
            //�`��
            Drawing();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// ������̕`��A��]�A��ʒu�w��
        /// �Q�l�@https://imagingsolution.net/program/drawstring_rotate/
        /// </summary>
        /// <param name="g">�`����Graphics�I�u�W�F�N�g</param>
        /// <param name="s">�`�悷�镶����</param>
        /// <param name="f">�����̃t�H���g</param>
        /// <param name="brush">�`��p�u���V</param>
        /// <param name="x">��ʒu��X���W</param>
        /// <param name="y">��ʒu��Y���W</param>
        /// <param name="deg">��]�p�x�i�x���A���v���肪���j</param>
        /// <param name="format">��ʒu��StringFormat�N���X�I�u�W�F�N�g�Ŏw�肵�܂�</param>
        public void DrawString(Graphics g, string s, Font f, SolidBrush brush, float x, float y, float deg, StringFormat format)
        {
            using (var pathText = new System.Drawing.Drawing2D.GraphicsPath())  // �p�X�̍쐬
            using (var mat = new System.Drawing.Drawing2D.Matrix())             // �A�t�B���ϊ��s��
            {
                // �`��pFormat
                var formatTemp = (StringFormat)format.Clone();
                formatTemp.Alignment = StringAlignment.Near;        // ���񂹂ɏC��
                formatTemp.LineAlignment = StringAlignment.Near;    // ��񂹂ɏC��

                // ������̕`��
                pathText.AddString(s, f.FontFamily, (int)f.Style, f.SizeInPoints, new PointF(0, 0), format);
                formatTemp.Dispose();

                // �����̗̈�擾
                var rect = pathText.GetBounds();

                // ��]���S��X���W
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
                // ��]���S��Y���W
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

                // �����̉�]���S���W�����_�ֈړ�
                mat.Translate(-px, -py, System.Drawing.Drawing2D.MatrixOrder.Append);
                // �����̉�]
                mat.Rotate(deg, System.Drawing.Drawing2D.MatrixOrder.Append);
                // �\���ʒu�܂ňړ�
                mat.Translate(x, y, System.Drawing.Drawing2D.MatrixOrder.Append);

                // �p�X���A�t�B���ϊ�
                pathText.Transform(mat);

                // �`��
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
