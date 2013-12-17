using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkers
{

    class Game
    {
        public Table table;
        public bool firstPlayer;
        public static bool Ate;

        public int whiteScore = 0;
        public int blackScore = 0;

        public Checker selectedChecker;

        public Game(int size)
        {
            table = new Table(size);

            table.Init();

            firstPlayer = true;
            Ate = false;
        }

        private bool CheckGameOver()
        {
            // TODO fix
            if (table.checkers.All(checker => checker.white))
            {
                return true;
            }

            if (table.checkers.All(checker => !checker.white))
            {
                return false;
            }

            return false;
        }

        public void SwitchPlayer()
        {
            firstPlayer = !firstPlayer;
            selectedChecker = null;
        }

        public void HandleRemovedCheckers()
        {
            foreach (var removedChecker in table.removedCheckers)
            {
                if (removedChecker.white)
                    blackScore += 1;
                else
                    whiteScore += 1;

                table.RemoveChecker(removedChecker);
            }
        }

        public void HandleClick(Point loc)
        {
            if (selectedChecker == null)
            {
                selectedChecker = table.GetChecker(loc.x, loc.y);
            }
            else if (selectedChecker.white == firstPlayer)
            {
                var movement = table.Move(selectedChecker, loc.x, loc.y);
                // wtf
                Ate = movement == 2;

                if (movement != 0)
                {
                    table.CheckQueen(selectedChecker);
                    // If this move was "Eat"
                    if (Ate)
                    {
                        // If selected checker can eat another time
                        if (!table.CanEat(table.GetChecker(loc.x, loc.y)))
                        {
                            HandleRemovedCheckers();
                            SwitchPlayer();
                        }
                    }
                    else
                    {
                        HandleRemovedCheckers();
                        table.removedCheckers.Clear();
                        SwitchPlayer();
                    }
                }
            }
            else
            {
                selectedChecker = null;
            }
        }
    }
}
