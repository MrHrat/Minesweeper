﻿using System;
using Common;

namespace Core
{
    public class CellValue : Сell
    {
        public CellValue(int row, int column, int value) : base(row, column, value) { }
        public CellValue(Сell cell, int value) : base(cell.Row, cell.Column, value) { }
    }
}
