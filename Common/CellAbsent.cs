using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class CellAbsent : Cell
    {
        public CellAbsent(Cell cell) : base(cell.Row, cell.Column)
        {
            Status = CellStatus.Absent;
        }
    }
}
