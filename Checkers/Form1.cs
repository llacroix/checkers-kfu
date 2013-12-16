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
        public static bool Ate;
        Graphics gPanel;
        float side;
        Table table;
        private bool selected;
        private bool firstPlayer;
        private Checker selectedChecker;

        SolidBrush black = new SolidBrush(Color.Black);
        SolidBrush red = new SolidBrush(Color.Red);
        SolidBrush yellow = new SolidBrush(Color.Yellow);
        SolidBrush blackChecker = new SolidBrush(Color.DarkSlateGray);

        public Form1()
        {
            InitializeComponent();
            table = new Table(8);
            table.Init();
            firstPlayer = true;
            Ate = false;
            var t = new Thread(func1);
            t.Start();
        }

        public void func1()
        {
            Thread.Sleep(1000);
            
            gPanel = pictureBox1.CreateGraphics();
            side = pictureBox1.Height / table.size;
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

        private void CheckGameOver()
        {
            if (table.checkers.All(checker => checker.white))
            {
                GameOver(true);
                return;
            }

            if (table.checkers.All(checker => !checker.white))
            {
                GameOver(false);
            }
        }

        private static bool canEat;

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            var loc = new Point((int)((e.X) / side), (int)((e.Y) / side));
            label1.Text = firstPlayer ? "Red " : "Black " + e.X + " " + e.Y + " | " + loc.x + " " + loc.y;
            if (!selected)
            {
                selectedChecker = table.GetChecker(loc.x, loc.y);
                if (selectedChecker == null)
                    return;
                
                if (selectedChecker.white != firstPlayer)
                    return;

                selected = true;
                DrawSelection(selectedChecker);
                return;
            }
            
            if (table.Move(selectedChecker, loc.x, loc.y))
            {
                table.CheckQueen(selectedChecker);
                // If this move was "Eat"
                if (Ate)
                {
                    // If selected checker can eat another time
                    if (table.CanEat(table.GetChecker(loc.x, loc.y)))
                    {
                        selectedChecker = table.GetChecker(loc.x, loc.y);
                        Redraw();
                        DrawSelection(selectedChecker);
                        selected = true;
                        canEat = true;
                        return;
                    }
                    else
                    {
                        // Change the player
                        var playerLabel = firstPlayer ? labelBlack : labelRed;
                        var score = Convert.ToInt32(playerLabel.Text);
                        foreach (var removedChecker in table.removedCheckers)
                        {
                            score++;
                            table.RemoveChecker(removedChecker);
                        }
                        table.removedCheckers.Clear();
                        playerLabel.Text = score.ToString();
                        // Check if the game is over
                        CheckGameOver();
                        firstPlayer = !firstPlayer;
                        selected = false;
                        canEat = false;
                    }
                }
                else
                {
                    // Change the player
                    var playerLabel = firstPlayer ? labelBlack : labelRed;
                    var score = Convert.ToInt32(playerLabel.Text);
                    foreach (var removedChecker in table.removedCheckers)
                    {
                        score++;
                        table.RemoveChecker(removedChecker);
                    }
                    table.removedCheckers.Clear();
                    playerLabel.Text = score.ToString();
                    // Check if the game is over
                    CheckGameOver();
                    firstPlayer = !firstPlayer;
                    selected = false;
                    canEat = false;
                }
            }
            else if (canEat)
            {
                Redraw();
                DrawSelection(selectedChecker);
                selected = true;
                canEat = true;
                return;
            }

            Redraw();
            selected = false;
        }
    }
}
