using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkers
{
    public class Checker
    {
        public int x;
        public int y;
        public bool white;
        public bool queen;

        public Checker(int x, int y, bool white)
        {
            this.x = x;
            this.y = y;
            this.white = white;
            queen = false;
        }

        public Checker(Point pos, bool white)
        {
            this.x = pos.x;
            this.y = pos.y;
            this.white = white;
            queen = false;
        }

        public bool Equals(Checker checker)
        {
            if (checker == null)
                return false;

            return x == checker.x &&
                   y == checker.y &&
                   queen == checker.queen &&
                   white == checker.white;
        }

        public void move(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void kill()
        {
            this.move(-1, -1);
        }
    }
}
