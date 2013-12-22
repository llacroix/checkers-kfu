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
        public Checker movingChecker;

        public Game(int size)
        {
            table = new Table(size);

            table.Init();

            firstPlayer = true;
            Ate = false;
        }

        // 1 White wins
        // 0 Nobody wins
        // -1 Black wins
        public int CheckGameOver()
        {
            // TODO fix
            if (table.checkers.All(checker => checker.white))
            {
                return 1;
            }

            if (table.checkers.All(checker => !checker.white))
            {
                return -1;
            }

            if (Blocked())
            {
                return firstPlayer ? -1 : 1;
            }

            return 0;
        }

        public void SwitchPlayer()
        {
            firstPlayer = !firstPlayer;
            selectedChecker = null;
            movingChecker = null;
        }

        public bool Blocked()
        {
            var team = (from checker in table.checkers
                        where checker.white == firstPlayer
                        select checker).ToList();


            return (from checker in team
                    select (from move in table.GetMoves(checker)
                            select table.CanMove(checker, move.x, move.y, false)
                            ).Any(x => x > 0)
                    ).All(x => x == false);
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

        public bool HandleClick(Point loc)
        {
            bool didSomething = false;

            if (selectedChecker == null)
            {
                selectedChecker = table.GetChecker(loc.x, loc.y);
            }
            else if (selectedChecker.white == firstPlayer)
            {
                var movement = table.Move(selectedChecker, loc.x, loc.y);
               
                Ate = (movement == 2);

                if (movement != 0)
                {
                    table.CheckQueen(selectedChecker);
                    // If this move was "Eat"
                    if (Ate)
                    {
                        movingChecker = selectedChecker;
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

                    didSomething = true;
                }
                else
                {
                    var newPick = table.GetChecker(loc.x, loc.y);

                    if (newPick != null && newPick.white == selectedChecker.white && movingChecker == null)
                        selectedChecker = newPick;
                }
            }
            else
            {
                selectedChecker = null;
            }

            return didSomething;
        }
    }
}
