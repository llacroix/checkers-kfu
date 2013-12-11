using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Checkers
{
    public partial class Form1 : Form
    {
        Graphics gPanel;
        float side;
        Table table;

        SolidBrush black = new SolidBrush(Color.Black);
        SolidBrush red = new SolidBrush(Color.Red);
        SolidBrush yellow = new SolidBrush(Color.Yellow);

        public Form1()
        {
            InitializeComponent();
            table = new Table(8);
            table.Init();

            Thread t = new Thread(func1);
            t.Start();
        }

        public void func1()
        {
            Thread.Sleep(1000);

            gPanel = pictureBox1.CreateGraphics();
            side = pictureBox1.Height / table.size;

            Redraw();
        }

        public void Redraw()
        {
            Enumerable.Range(0, table.size).ToList().
                ForEach(x =>
                    Enumerable.Range(0, table.size).ToList().
                        ForEach(y =>
                            DrawCell(x, y, (x + y) % 2 == 0 ? yellow : black)));

            table.checkers
                .ToList()
                .ForEach(checker => DrawChecker(checker));
        }

        public void DrawCell(int x, int y, SolidBrush color)
        {
            gPanel.FillRectangle(color,
                                 x * side,
                                 y * side,
                                 side,
                                 side);

        }

        public void DrawChecker(Checker checker)
        {
            gPanel.FillEllipse(
                checker.white ? red : black,
                checker.x * side + 5,
                checker.y * side + 5,
                side - 10,
                side - 10
            );
        }
    }
}
