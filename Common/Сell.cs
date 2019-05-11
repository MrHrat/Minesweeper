namespace Common
{
    using System;

    public class Сell : IEquatable<Сell>
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

        public virtual CellStatus Status
        {
            get;
            protected set;
        }

        = CellStatus.Open;

        public virtual int Value
        {
            get;
            private set;
        }

        = 0;

        public Сell(int row, int column, bool mark = false)
        {
            Row = row;
            Column = column;
            if (mark)
            {
                Status = CellStatus.Mark;
            }
        }

        protected Сell(int row, int column, int value)
        {
            Row = row;
            Column = column;
            Status = CellStatus.Number;
            Value = value;
        }

        protected Сell()
        {
            Row = 0;
            Column = 0;
        }

        public static Сell Random(Сell maxCell)
        {
            Random rand = new Random();

            var row = rand.Next(maxCell.Row);
            var column = rand.Next(maxCell.Column);

            return new Сell(row, column);
        }

        public bool Equals(Сell item)
        {
            if (item == null)
            {
                return false;
            }

            return Row == item.Row && Column == item.Column;
        }

        public override string ToString()
        {
            return string.Format("R = {0,4}; C = {1,4}; Status = {2,4}; Value = {3,4}", Row + 1, Column + 1, Status, Value);
        }
    }
}
