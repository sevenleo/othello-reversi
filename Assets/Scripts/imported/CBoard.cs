using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace Damka
{
    class CBoard        //  http://www.codearsenal.net/2012/06/c-sharp-read-excel-and-show-in-wpf.html
    {
        public delegate void DelDraw(Panel pan, int p);
        public const int w = 8;
        public const int RectWidth = 40;
        private int[,] mat;

        public CBoard()
        {
            InitMat();
        }

        public CBoard Copy()
        {
            CBoard b = new CBoard();
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    b.mat[i, j] = mat[i, j];
                }
            }
            b.mCompFig = this.CompFig;
            b.mPlayersFig = this.PlayersFig;
            return b;
        }

        public int CompFig
        {
            get { return mCompFig; }
        }   private int mCompFig = 0;

        public int PlayersFig
        {
            get { return mPlayersFig; }
        }   private int mPlayersFig = 0;

        private void Calc()
        {
            mPlayersFig = 0;
            mCompFig = 0;
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    if (mat[i, j] < 0) mCompFig++;
                    else if (mat[i, j] > 0) mPlayersFig++;
                }
            }
        }

        public bool IsEnd
        {
            get { return (FigCount == w * w); }
        }

        public int FigCount
        {
            get { return mFigCount; }
        }   private int mFigCount;

        private void InitMat()
        {
            mCompFig = 0;
            mPlayersFig = 0;
            mFigCount = 0;
            mat = new int[w, w];
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    mat[i, j] = 0;
                }
            }

            mat[w / 2 - 1, w / 2 - 1] = -1;
            mat[w / 2, w / 2 - 1] = 1;

            mat[w / 2 - 1, w / 2] = 1;
            mat[w / 2, w / 2] = -1;

            Calc();
        }

        public void Draw(Panel p, bool IsDrawHelp,int step)
        {
            Graphics gr = Graphics.FromHwnd(p.Handle);
            Draw(gr,IsDrawHelp,step);
        }

        public int GetFig(int i, int j) { return mat[i, j]; }

        internal void Draw(System.Drawing.Graphics graphics,bool IsDrawHelp,int step)
        {
            Pen p = new Pen(Color.Black,(float)0.5);
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    int f = mat[i, j];
                    Color c = Color.White; ;                        
                    graphics.FillRectangle(new SolidBrush(c), i * RectWidth, j * RectWidth, RectWidth, RectWidth);
                    graphics.DrawRectangle(p, i * RectWidth, j * RectWidth, RectWidth, RectWidth);
                    if (f == 0)
                    {
                        c = Color.White;                    
                    }
                    else
                    {
                        if (f == 1)
                        {
                            c = Color.Red;
                        }
                        else
                        {
                            c = Color.Blue;
                        }
                        graphics.FillEllipse(new SolidBrush(c), i * RectWidth + 3, j * RectWidth + 3, RectWidth - 6, RectWidth - 6);
                    }
                }
            }
            if(IsDrawHelp)
                DrawEnableSteps(step,graphics);
        }

        public void DrawEnableSteps(int p, Graphics graphics)
        {
            Pen pen = new Pen(Color.Black, (float)0.5);
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    if (AddFig(i, j, p, false) > 0)
                    {
                        Color c = Color.White; ;
                        graphics.DrawRectangle(pen, i * RectWidth + 3, j * RectWidth + 3, RectWidth - 6, RectWidth - 6);
                    }
                }
            }
        }

        public int GetKrit(int deph)
        {
            if (CompFig + PlayersFig == w * w)
                return (CompFig > PlayersFig) ? Int32.MaxValue : -Int32.MaxValue;

            int EndLine = 0;
            int ugol = 0;
            int figs = CompFig - PlayersFig;
            int ConstFigs = 0;

            ugol = mat[0, 0] + mat[0, w - 1] + mat[w - 1, w - 1] + mat[w - 1, 0];

            for (int i = 0; i < w; i++)
                EndLine += (mat[i, 0] + mat[i,w - 1] + mat[0, i] + mat[w - 1,i]);

            int first = mat[0, 0];
            if (first != 0)
            {
                for (int i = 1; i < w && mat[0, i] == first; i++)
                    ConstFigs += first;
                for (int i = 1; i < w && mat[i, 0] == first; i++)
                    ConstFigs += first;
            }
            first = mat[0, w - 1];
            if (first != 0)
            {
                for (int i = w - 2; i >= 0 && mat[0, i] == first; i--)
                    ConstFigs += first;
                for (int i = 1; i < w && mat[i, w - 1] == first; i++)
                    ConstFigs += first;
            }
            first = mat[w - 1, 0];
            if (first != 0)
            {
                for (int i = 1; i < w && mat[w - 1, i] == first; i++)
                    ConstFigs += first;
                for (int i = w - 2; i >= 0 && mat[i, w - 1] == first; i--)
                    ConstFigs += first;
            }
            first = mat[w - 1, w - 1];
            if (first != 0)
            {
                for (int i = w - 2; i >= 0 && mat[i, w - 1] == first; i--)
                    ConstFigs += first;
                for (int i = w - 2; i >= 0 && mat[w - 1, i] == first; i--)
                    ConstFigs += first;
            }
            //for (int i = 0; i < w; i++)
            //{
            //    for (int j = 0; j < w; j++)
            //    {
            //        if (i == j || i + j == w - 1)
            //        {                            
            //            diag -= mat[i, j];
            //        }
            //    }
            //}
            return
                ((100  + CompFig + PlayersFig) * figs) +
                (-200 * ugol) +
                (-150 * EndLine) +
                (-250 * ConstFigs);
        }

        public static int GetBestStep(int p, int alpha, int deph, CBoard brd,DateTime start,Panel pan)
        {                
            TimeSpan sp = DateTime.Now - start;
            if (deph > 3 || brd.CompFig + brd.PlayersFig == CBoard.w * CBoard.w)
            {
                int k = brd.GetKrit(deph);
                //brd.Draw(pan, false, p);
                //Thread.Sleep(1000);
                return k;
            }
            List<int[]> l = brd.GetEnableSteps(p);

            if(l.Count == 0 && p == -1)
                return -Int32.MaxValue;

            int grade = -Int32.MaxValue;
            foreach(int[] s in l)
            {
                if (AlphaBeta(alpha, grade, deph))
                {
                    return alpha;
                }
                CBoard b = brd.Copy();
                b.AddFig(s[1], s[0], p, true);
                int res = GetBestStep(-p, alpha, deph + 1, b, start,pan);
                grade = MinMax(res, grade, deph);
            }
            return grade;
        }

        private static bool AlphaBeta(int a,int b,int c)
        {
            return ((a != -Int32.MaxValue && b != -Int32.MaxValue) &&
			        ((a >= b && c % 2 == 0)||(a <= b && c % 2 == 1)));
        }

        private static int MinMax(int res, int grade, int deph)
        {
            if (grade == -Int32.MaxValue) return res;
            return (deph % 2 == 1) ? Math.Max(res, grade) : Math.Min(res, grade);
        }

        internal int AddFig(int x, int y, int p, bool IsAdd)
        {
            int res = 0;
            if (mat[x, y] == 0)
            {
                res = UpCheck(x, y - 1, p, IsAdd) +
                    DownCheck(x, y + 1, p, IsAdd) +
                    LeftCheck(x - 1, y, p, IsAdd) +
                    RightCheck(x + 1, y, p, IsAdd) +
                    UpLeftCheck(x - 1, y - 1, p, IsAdd) +
                    UpRightCheck(x + 1, y - 1, p, IsAdd) +
                    DownRightCheck(x + 1, y + 1, p, IsAdd) +
                    DownLeftCheck(x - 1, y + 1, p, IsAdd);
                if (res > 0)
                {
                    if (IsAdd)
                    {
                        mFigCount++;
                        mat[x, y] = p;
                        Calc();
                    }
                }
            }
            return res;
        }

        public int EnableSteps
        {
            get { return mEnableSteps; }
        }   private int mEnableSteps;

        public List<int[]> GetEnableSteps(int p)
        {
            List<int[]> l = new List<int[]>(10);
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    int res = AddFig(j, i, p, false);
                    if (res > 0)
                    {
                        l.Add(new int[] { i, j, res });
                    }
                }
            }
            mEnableSteps = l.Count;
            return l;
        }

        #region SideCheck
        private int UpCheck(int x, int y, int p, bool f)
        {
            bool found = false;
            int i = 0;
            int c = 0;
            for (i = y; i >= 0; i--)
            {
                if (mat[x, i] == 0) return 0;
                if(mat[x, i] == -p) c++;
                if (mat[x, i] == p)
                {
                    found = c > 0;
                    if(c > 0 && f)
                        for (int j = y; j != i; j--)
                            mat[x, j] = p;
                    break;
                }
            }
            return found ? c : 0;
        }

        private int DownCheck(int x, int y, int p, bool f)
        {
            bool found = false;
            int i = 0;
            int c = 0;
            for (i = y; i < w; i++)
            {
                if (mat[x, i] == 0) return 0;
                if (mat[x, i] == -p) c++;
                if (mat[x, i] == p)
                {
                    found = c > 0;
                    if (c > 0 && f)
                        for (int j = y; j != i; j++)
                            mat[x, j] = p;
                    break;
                }
            }
            return found ? c : 0;
        }

        private int LeftCheck(int x, int y, int p, bool f)
        {
            bool found = false;
            int i = 0;
            int c = 0;
            for (i = x; i >= 0; i--)
            {
                if (mat[i, y] == 0) return 0;
                if (mat[i, y] == -p) c++;
                if (mat[i, y] == p)
                {
                    found = c > 0;
                    if (c > 0 && f)
                        for (int j = x; j != i; j--)
                            mat[j, y] = p;
                    break;
                }
            }
            return found ? c : 0;
        }

        private int RightCheck(int x, int y, int p, bool f)
        {
            bool found = false;
            int i = 0;
            int c = 0;
            for (i = x; i < w; i++)
            {
                if (mat[i, y] == 0) return 0;
                if (mat[i, y] == -p) c++;
                if (mat[i, y] == p)
                {
                    found = c > 0;
                    if (c > 0 && f)
                        for (int j = x; j != i; j++)
                            mat[j, y] = p;
                    break;
                }
            }
            return found ? c : 0;
        }
        #endregion SideCheck

        #region Diagonal
        private int UpLeftCheck(int x, int y, int p, bool f)
        {
            bool found = false;
            int i = 0,j = 0;
            int a, b;
            int c = 0;
            for (i = x,j = y; i >= 0 && j >= 0 ; i--,j--)
            {
                if (mat[i, j] == 0) return 0;
                if (mat[i, j] == -p) c++;
                if (mat[i, j] == p)
                {
                    found = c > 0;
                    if (c > 0 && f)
                        for (a = x,b = y; a != i && b != j; a--,b--)
                            mat[a, b] = p;
                    break;
                }
            }
            return found ? c : 0;
        }

        private int UpRightCheck(int x, int y, int p, bool f)
        {
            bool found = false;
            int i = 0,j = 0;
            int a, b;
            int c = 0;
            for (i = x,j = y; i < w && j >= 0 ; i++,j--)
            {
                if (mat[i, j] == 0) return 0;
                if (mat[i, j] == -p) c++;
                if (mat[i, j] == p)
                {
                    found = c > 0;
                    if (c > 0 && f)
                        for (a = x,b = y; a != i && b != j; a++,b--)
                            mat[a, b] = p;
                    break;
                }
            }
            return found ? c : 0;
        }


        private int DownRightCheck(int x, int y, int p, bool f)
        {
            bool found = false;
            int i = 0,j = 0;
            int a, b;
            int c = 0;
            for (i = x, j = y; i < w && j < w; i++, j++)
            {
                if (mat[i, j] == 0) return 0;
                if (mat[i, j] == -p) c++;
                if (mat[i, j] == p)
                {
                    found = c > 0;
                    if (c > 0 && f)
                        for (a = x,b = y; a != i && b != j; a++,b++)
                            mat[a, b] = p;
                    break;
                }
            }
            return found ? c : 0;
        }

        private int DownLeftCheck(int x, int y, int p, bool f)
        {
            bool found = false;
            int i = 0,j = 0;
            int a, b;
            int c = 0;
            for (i = x,j = y; i >= 0 && j < w ; i--,j++)
            {
                if (mat[i, j] == 0) return 0;
                if (mat[i, j] == -p) c++;
                if (mat[i, j] == p)
                {
                    found = c > 0;
                    if (c > 0 && f)
                        for (a = x,b = y; a != i && b != j; a--,b++)
                            mat[a, b] = p;
                    break;
                }
            }
            return found ? c : 0;
        }
        #endregion Diagonal
    }
}