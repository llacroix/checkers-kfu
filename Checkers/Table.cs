using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Checkers
{
    public class Table
    {
        public int size;

        public HashSet<Checker> checkers;

        public Table(int size)
        {
            this.size = size;
            checkers = new HashSet<Checker>();
        }

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

        public void AddChecker(Checker checker) {
            checkers.Add(checker);
        }

        public void RemoveChecker(Checker checker)
        {
            // Safe check
            checker.kill();
            checkers.Remove(checker);
        }

        private bool CanMove(Checker checker, int x, int y)
        {
            if (x >= size || y >= size || x < 0 || y < 0 )
                return false;

            int direction = checker.white ? 1 : -1;

            if(!( x == checker.x + 1 && y == checker.y + direction  || x == checker.x - 1 && y == checker.y + direction))
                return false;

            if (GetChecker(x, y) != null)
                return false;

            return true;
        }

        public bool Move(Checker checker, int x, int y)
        {
            return true;
            if (Eat())
            {
               
            }
            else
            {
                if (!CanMove(checker, x, y))
                    return false; 

                checker.move(x, y);
                return true;
            }
        }

        private bool Eat()
        {
            return false;
        }

        public Checker GetChecker(int x, int y)
        {
            return (from checker in checkers
                    where checker.x == x && checker.y == y
                    select checker).First();
        }

        public List<Checkers.Point> GetMoves(Checker checker)
        {
            var current = new Point(checker.x, checker.y);

            //top-left
            var tl = (from delta in Enumerable.Range(1 , size)
                      where checker.x - delta > 0 && checker.y + delta < size
                      select new Point(checker.x - delta, checker.y + delta));

            //top-right
            var tr = (from delta in Enumerable.Range(1, size)
                      where checker.x + delta < size && checker.y + delta < size
                      select new Point(checker.x + delta, checker.y + delta));

            //bottom-left
            var bl = (from delta in Enumerable.Range(1, size)
                      where checker.x - delta < 0 && checker.y - delta < 0
                      select new Point(checker.x - delta, checker.y - delta));

            //bottom-right
            var br = (from delta in Enumerable.Range(1, size)
                      where checker.x + delta > size && checker.y - delta < 0
                      select new Point(checker.x + delta, checker.y - delta));

            return tl.Concat(tr).Concat(bl).Concat(br).ToList();
        }
    } 
}
