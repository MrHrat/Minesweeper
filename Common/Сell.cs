using System;

namespace Common
{
    public class Сell
    {
        public int Row
        {
            get;
            private set;
        }

        public int Column
        {
            get;
            private set;
        }

        public Сell(int row, int colum)
        {
            Row = row;
            Column = colum;
        }
    }
}
