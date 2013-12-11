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
            (from x in Enumerable.Range(0, table.size)
             from y in Enumerable.Range(0, table.size)
             select DrawCell(x, y, (x + y) % 2 == 0 ? yellow : black)).ToList();

            table.checkers
                .ToList()
                .ForEach(checker => DrawChecker(checker));
        }

        public int DrawCell(int x, int y, SolidBrush color)
        {
            gPanel.FillRectangle(color,
                                 x * side,
                                 y * side,
                                 side,
                                 side);

            return 0;

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
