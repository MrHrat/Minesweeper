using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    class CellExplosion : Cell
    {
        public CellExplosion(Cell cell) : base(cell.Row, cell.Column)
        {
            Status = CellStatus.Explosion;
        }
    }
}
