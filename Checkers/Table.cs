using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkers
{
    public class Table
    {
        public int size;

        public HashSet<Checker> checkers;

        public HashSet<Checker> removedCheckers; 

        public Table(int size)
        {
            this.size = size;
            checkers = new HashSet<Checker>();
            removedCheckers = new HashSet<Checker>();
        }

        /// <summary>
        /// Iterate over all X and Y in the grid 
        /// Add a checkers to our grid. 
        ///  We should end up with 12 checkers of each
        ///  color.
        /// </summary>
        public void Init()
        {

           (from x in Enumerable.Range(0, size)
            from y in Enumerable.Range(0, size)
            select new Point(x, y)).ToList().
            ForEach(point =>
            {
                if ((point.x + point.y) % 2 == 0)
                    if (point.y < 3)
                        AddChecker(new Checker(point, true));
                    else if (point.y > size - 4)
                        AddChecker(new Checker(point, false));
            });
        }

        /// <summary>
        /// Adds the checker to our checkers' set
        /// </summary>
        /// <param name="checker">The checker.</param>
        /// <exception cref="System.Exception">
        /// Can't add null
        /// or
        /// Wrong indexes
        /// or
        /// Already Exists
        /// </exception>
        public void AddChecker(Checker checker) {
            if (checker == null)
                throw new Exception("Can't add null");
            if (checker.x < 0 || checker.y < 0 || checker.x >= size || checker.y >= size)
                throw new Exception("Wrong indexes");
            if (GetChecker(checker.x, checker.y) != null)
                throw new Exception("Already Exists");
            checkers.Add(checker);
        }

        /// <summary>
        /// Remove checker from our sets of checkers and kill it.
        /// </summary>
        /// <param name="checker">The checker.</param>
        public void RemoveChecker(Checker checker)
        {
            checker.kill();
            checkers.Remove(checker);
        }

        /// <summary>
        /// Marks the checker to delete it after the player is changed.
        /// </summary>
        /// <param name="checker">The checker.</param>
        public void MarkChecker(Checker checker)
        {
            removedCheckers.Add(checker);
        }

        /// <summary>
        /// Gets all possible moves of the specified checker.
        /// </summary>
        /// <param name="checker">The checker.</param>
        /// <returns></returns>
        public List<Point> GetMoves(Checker checker)
        {
            var points = new List<Point>();
            int step = 2;
            if (checker.queen)
                step = size;
            int x = checker.x - step;
            int y = checker.y - step;
            int finishX = checker.x + step + 1;
            int finishY = checker.y + step + 1;
            while (x < 0 || y < 0)
            {
                x++;
                y++;
            }
            while (finishX > size || finishY > size)
            {
                finishX--;
                finishY--;
            }
            for (; x < finishX && y < finishY; x++, y++)
                if (x != checker.x && y != checker.y)
                    points.Add(new Point(x, y));

            x = checker.x - step;
            y = checker.y + step;
            finishX = checker.x + step + 1;
            finishY = checker.y - step - 1;

            while (x < 0 || y > size - 1)
            {
                x++;
                y--;
            }
            while (finishX > size || finishY < -1)
            {
                finishX--;
                finishY++;
            }
            for (; x < finishX && y > finishY; x++, y--)
                if (x != checker.x && y != checker.y)
                    points.Add(new Point(x, y));

            return points;
        }

        /// <summary>
        /// Determines whether the checker has a possibility of eating.
        /// </summary>
        /// <param name="checker">The checker.</param>
        /// <returns></returns>
        public bool CanEat(Checker checker)
        {
            var resultsNums = new List<int>();
            var resultsCheckers = new List<Point>();
            // Old brute force version
            /*
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (!removedCheckers.Contains(GetChecker(i, j)))
                    {
                        resultsNums.Add(CanMove(checker, i, j, false));
                        resultsCheckers.Add(new Point(i, j));
                    }
                }
            }
            */
            // Does it work?
            
            var moves = GetMoves(checker);
            foreach (var point in moves.Where(point => !removedCheckers.Contains(GetChecker(point.x, point.y))))
            {
                resultsNums.Add(CanMove(checker, point.x, point.y, false));
                resultsCheckers.Add(new Point(point.x, point.y));
            }
             
            if (resultsNums.Contains(2))
            {
                return true;
            }

            return false;
        }

        // 0 - can not move or eat
        // 1 - can move
        // 2 - can eat
        public int CanMove(Checker checker, int x, int y, bool mark = true)
        {
            if (x >= size || y >= size || x < 0 || y < 0 )
                return 0;

            if (GetChecker(x, y) != null)
                return 0;

            if (checker.queen)
            {
                if (Math.Abs(x - checker.x) != Math.Abs(y - checker.y))
                    return 0;

                var moveDirection = new Point(x - checker.x > 0 ? 1 : -1, y - checker.y > 0 ? 1 : -1);
                var currentPoint = new Point(checker.x + moveDirection.x, checker.y + moveDirection.y);
                var destPoint = new Point(x, y);

                Checker checkerMet = null;
                while (currentPoint.x != destPoint.x && currentPoint.y != destPoint.y)
                {
                    var toCheck = GetChecker(currentPoint.x, currentPoint.y);
                    if (toCheck != null)
                    {
                        if (toCheck.white == checker.white)
                        {
                            return 0;
                        }

                        if (checkerMet == null)
                        {
                            checkerMet = GetChecker(currentPoint.x, currentPoint.y);
                        }
                        else
                        {
                            return 0;
                        }
                    }

                    currentPoint.x += moveDirection.x;
                    currentPoint.y += moveDirection.y;
                }

                if (checkerMet != null)
                {
                    if (mark)
                        MarkChecker(checkerMet);
                    return GetChecker(x, y) == null ? 
                        removedCheckers.Contains(checkerMet) && !mark ? 
                            0 : 
                            2 
                        : 
                        0;
                }
                return GetChecker(x, y) == null ? 1 : 0;
            }
            else
            {
                // check if it can eat

                var toEat = CanEatAnotherChecker(checker, x, y);
                if (toEat != null && !removedCheckers.Contains(toEat))
                {
                    if (mark)
                        MarkChecker(toEat);
                    return 2;
                }

                // check if it can move
                var direction = checker.white ? 1 : -1;
                if (
                    !(x == checker.x + 1 && y == checker.y + direction ||
                      x == checker.x - 1 && y == checker.y + direction))
                    return 0;

                return GetChecker(x, y) == null ? 1 : 0;
            }
        }

        public bool Move(Checker checker, int x, int y)
        {
            var result = CanMove(checker, x, y);
            if (result == 0)
            {
                return false;
            }

            Form1.Ate = result == 2;
            checker.move(x, y);
            return true;
        }

        /// <summary>
        /// Determines whether the specified checker can eat another by moving to (x, y). Not for queens
        /// </summary>
        /// <param name="checker">The checker.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public Checker CanEatAnotherChecker(Checker checker, int x, int y)
        {
            // checked in Form1
            //if (GetChecker(x, y) != null)
            //{
            //    return false;
            //}

            var dirX = x - checker.x;
            var dirY = y - checker.y;
            if (Math.Abs(dirX) != 2 || Math.Abs(dirY) != 2)
                return null;

            var toEatCoords = new Point(checker.x + dirX / 2, checker.y + dirY / 2);

            var toEat = GetChecker(toEatCoords.x, toEatCoords.y);

            if (toEat == null)
                return null;

            return toEat.white == checker.white ? null : toEat;
        }

        /*
         * Get the checker for the coordinate x and y
         */
        public Checker GetChecker(int x, int y)
        {
            try
            {

                return (from checker in checkers
                        where checker.x == x && checker.y == y
                        select checker).First();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Checks if the checker has become queen.
        /// </summary>
        /// <param name="selectedChecker">The selected checker.</param>
        public void CheckQueen(Checker selectedChecker)
        {
            if (selectedChecker == null)
                return;

            if (selectedChecker.white)
            {
                if (selectedChecker.y == size - 1)
                    selectedChecker.queen = true;
            }
            else
            {
                if (selectedChecker.y == 0)
                    selectedChecker.queen = true;
            }
        }
    } 
}
