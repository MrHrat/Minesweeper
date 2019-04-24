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
            private set;
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
    }
}
