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

        SolidBrush black = new SolidBrush(Color.Black);
        SolidBrush red = new SolidBrush(Color.Red);
        SolidBrush yellow = new SolidBrush(Color.Yellow);

        public Form1()
        {
            InitializeComponent();
            Thread t = new Thread(func1);
            t.Start();
        }

        public void func1()
        {
            Thread.Sleep(1000);
            Table table = new Table(8);

            gPanel = pictureBox1.CreateGraphics();
            side = pictureBox1.Height / table.size;

            for (int i = 0; i < table.size; i++)
            {
                for (int j = 0; j < table.size; j++)
                {
                    var color = (i + j) % 2 == 0 ? yellow : black;
                    gPanel.FillRectangle(color, i * side,
                                                j * side,
                                                side,
                                                side);


                    if ((i + j) % 2 == 0)
                        if (j < 3)
                            table.checkers.Add(new Checker(i, j, true));
                        else if (j > table.size - 4)
                            table.checkers.Add(new Checker(i, j, false));
                }
            }

            table.checkers
                .ToList()
                .ForEach(checker => DrawChecker(checker));
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
