/*using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Damka
{
    public partial class Form1 : Form       //  http://www.codearsenal.net/2012/06/c-sharp-read-excel-and-show-in-wpf.html
    {
        delegate void InvokeDraw(Panel p,bool b,int s);
        delegate void DelCompStep();
        delegate void InvokeShowRes();
        delegate void InvokeShowGrade(string s);
        DelCompStep EvCompStep;
        CBoard board;
        bool flag = false;

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            EvCompStep = new DelCompStep(CompStep);
            InitBoard();
        }

        private void InitBoard()
        {
            label5.Text = "";
            board = new CBoard();
            panel1.Width = CBoard.w * CBoard.RectWidth;
            panel1.Height = CBoard.w * CBoard.RectWidth;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CompStep();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private bool IsDrawHelp = true;

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            board.Draw(e.Graphics, false, 0);
        }

        private void DoRandomStep()
        {
            Random r = new Random();
            List<int[]>l = board.GetEnableSteps(-1);
            int j = -1;
            int max = 0;
            for (int i = 0; i < l.Count; i++)
            {
                int[] ar = l[i];
                if (max < ar[2])
                {
                    j = i;
                    max = ar[2];
                }
            }
            if(j != -1)
                board.AddFig(l[j][0], l[j][1], -1, true);
        }

        private void DoBestStep()
        {
            List<int[]>l = board.GetEnableSteps(-1);
            int j = 0;
            int max = -Int32.MaxValue;
            DateTime start = DateTime.Now;
            for (int i = 0; i < l.Count; i++)
            {
                CBoard cp = board.Copy();
                cp.AddFig(l[i][1], l[i][0], -1,true);
                int res = CBoard.GetBestStep(1,-Int32.MaxValue, 0, cp, start, panel1);
                if (max < res)
                {
                    j = i;
                    max = res;
                }
                TimeSpan sp = DateTime.Now - start;
                //if (sp.TotalSeconds > 3)
                //    break;
            }
            if(l.Count > j)
                board.AddFig(l[j][1], l[j][0], -1, true);
            Invoke(new InvokeShowGrade(ShowGrade), max.ToString());
        }

        private void CompStep()
        {
            Thread.Sleep(500);
            //DoRandomStep();
            do
            {
                DoBestStep();
            } while (board.GetEnableSteps(1).Count == 0 && board.GetEnableSteps(-1).Count > 0);
        }

        private void ShowRes()
        {
            label1.Text = board.PlayersFig.ToString();
            label2.Text = board.CompFig.ToString();
        }

        private void ShowGrade(string s)
        {
            label5.Text = s;
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (flag) return;            
            int x = e.X / CBoard.RectWidth;
            int y = e.Y / CBoard.RectWidth;
            if (board.AddFig(x, y, 1, true) > 0)
            {
                flag = true;
                board.Draw(panel1, IsDrawHelp, -1);
                EvCompStep.BeginInvoke(new AsyncCallback(EndCompStep), null);
                ShowRes();
            }
            else
            {
                //MessageBox.Show("Error position!!!");
            }
        }

        private void EndCompStep(object ob)
        {
            if (panel1.InvokeRequired)
                Invoke(new InvokeDraw(board.Draw), panel1, IsDrawHelp, 1);
            else
                board.Draw(panel1, IsDrawHelp, 1);
            int PlayerSteps = board.GetEnableSteps(1).Count;
            if (PlayerSteps == 0)
            {
                if (board.CompFig > board.PlayersFig)
                {
                    MessageBox.Show("Computer is winner!!!");
                }
                else
                {
                    MessageBox.Show("Winner!!!");
                }
                Invoke(new InvokeShowGrade(ShowGrade), "");
            }
            flag = false;
            if (InvokeRequired)
                Invoke(new InvokeShowRes(ShowRes));
            else
                ShowRes();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InitBoard();
            board.Draw(panel1, IsDrawHelp, 1);
        }
    }
}*/