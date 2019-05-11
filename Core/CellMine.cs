using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class CellMine : Cell
    {
        public CellMine(Cell cell, bool mark = false) : this(cell.Row, cell.Column, mark) { }
        public CellMine(int row, int column, bool mark = false) : base(row, column)
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
