namespace Core
{
    using Common;

    internal class CellMine : Сell
    {
        public CellMine(Сell cell, bool mark = false)
            : this(cell.Row, cell.Column, mark)
        {
        }

        public CellMine(int row, int column, bool mark = false)
            : base(row, column)
        {
            if (mark)
            {
                Status = CellStatus.MarkMine;
            }
            else
            {
                Status = CellStatus.Mine;
            }
        }
    }
}
