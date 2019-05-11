using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class CellAbsent : Сell
    {
        public CellAbsent(Сell cell) : base(cell.Row, cell.Column)
        {
            Status = CellStatus.Absent;
        }
    }
}
