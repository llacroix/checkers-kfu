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
            // Init all checkers
            for (int i = 0; i < 3; i++)
                for(int j = i % 2; j < size; j+=2)
                {
                    AddChecker(new Checker(i, j, true));
                }
            for (int i = size - 1; i >= size - 3; i--)
                for (int j = i % 2 + 1; j < size; j += 2)
                {
                    AddChecker(new Checker(i, j, false));
                }
        }

        public void AddChecker(Checker checker) {
            checkers.Add(checker);
        }

        public void RemoveChecker(Checker checker)
        {
            checker.move(-1, -1);
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

    }
}
