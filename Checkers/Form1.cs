using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        SolidBrush blackChecker = new SolidBrush(Color.DarkSlateGray);

        private Game game;
        private Table table;

        public Form1()
        {
            InitializeComponent();

            game = new Game(8);
            table = game.table;

            gPanel = pictureBox1.CreateGraphics();
            side = pictureBox1.Height / game.table.size;

            Redraw();
        }

        /* Redraw the whole board
         * we aren't doing any kind of optimization
         * simply redraw everything.
         */
        public void Redraw()
        {
            // Draw cells
            (from x in Enumerable.Range(0, table.size)
             from y in Enumerable.Range(0, table.size)
             select DrawCell(x, y, (x + y) % 2 == 0 ? yellow : black)).ToList();

            // Draw checkers
            table.checkers.ToList()
                 .ForEach(checker => DrawChecker(checker));

            if (game.selectedChecker != null)
            {
                DrawSelection(game.selectedChecker);
            }
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
            if (table.removedCheckers.Contains(checker))
            {
                gPanel.FillEllipse(
                    checker.white ? new SolidBrush(Color.Brown) : new SolidBrush(Color.Gray),
                    checker.x*side + 5,
                    checker.y*side + 5,
                    side - 10,
                    side - 10
                    );
            }
            else
            {
                gPanel.FillEllipse(
                    checker.white ? red : blackChecker,
                    checker.x * side + 5,
                    checker.y * side + 5,
                    side - 10,
                    side - 10
                    );
            }
            if (checker.queen)
            {
                gPanel.DrawString("Q",
                    new Font("Arial", 25),
                    new SolidBrush(Color.White),
                    checker.x * side + 15,
                    checker.y * side + 15);
            }
        }

        public void DrawSelection(Checker checker)
        {
            gPanel.DrawEllipse(
                new Pen(Color.DarkGray, 2),
                checker.x * side + 5,
                checker.y * side + 5,
                side - 10,
                side - 10
            );
        }

        private void GameOver(bool red)
        {
            pictureBox1.MouseClick -= pictureBox1_MouseClick;
            labelGameOver.Visible = true;
            labelWinner.Visible = true;
            labelWinner.Text = red ? "Red wins" : "Black wins";
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            var loc = new Point((int)((e.X) / side), (int)((e.Y) / side));
            label1.Text = game.firstPlayer ? "Red " : "Black " + e.X + " " + e.Y + " | " + loc.x + " " + loc.y;

            game.HandleClick(loc);

            labelBlack.Text = game.blackScore.ToString();
            labelRed.Text = game.whiteScore.ToString();

            Redraw();
        }
    }
}
