using System;
using Common;

namespace Core
{
    public class CellValue : Cell
    {
        public CellValue(int row, int column, int value) : base(row, column, value) { }
        public CellValue(Cell cell, int value) : base(cell.Row, cell.Column, value) { }
    }
}
