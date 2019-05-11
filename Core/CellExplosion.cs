using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    class CellExplosion : Сell
    {
        public CellExplosion(Сell cell) : base(cell.Row, cell.Column)
        {
            Status = CellStatus.Explosion;
        }
    }
}
