using System;

namespace Common
{
    public class Cell : IEquatable<Cell>
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
        } = CellStatus.Open;

        public virtual int Value
        {
            get;
            private set;
        } = 0;

        public Cell(int row, int column, bool mark = false)
        {
            Row = row;
            Column = column;
            if (mark)
            {
                Status = CellStatus.Mark;
            }
        }

        protected Cell(int row, int column, int value)
        {
            Row = row;
            Column = column;
            Status = CellStatus.Number;
            Value = value;
        }

        public bool Equals(Cell item)
        {
            if (item == null)
            {
                return false;
            }
            
            return Row == item.Row && Column == item.Column;
        }

        public static Cell Random(int maxValue)
        {
            Random rand = new Random();

            var row = rand.Next(maxValue) + 1;
            var column = rand.Next(maxValue) + 1;

            return new Cell(row, column);
        }

        public override string ToString()
        {
            return string.Format("R = {0,4}; C = {1,4}; Status = {2,4}; Value = {3,4}", Row, Column, Status, Value);
        }
    }
}
