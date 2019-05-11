using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public enum CellStatus
    {
        Absent = -1,
        Open = 0,
        Mark,
        Number,
        MarkMine,
        Mine,
        Explosion,
    }
}
