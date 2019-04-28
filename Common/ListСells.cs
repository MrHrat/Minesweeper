using System;
using System.Collections.Generic;
using System.Text;
using Common;

namespace Core
{
    public class ListСell
    {
        public List<Cell> Cells
        {
            get;
            private set;
        }

        public ListСell()
        {
            Cells = new List<Cell>();
        }

        public void Generate(int count, int maxValue)
        {
            while (Cells.Count < count)
            {
                var newCell = Cell.Random(maxValue);

                if (!IsPresent(newCell))
                {
                    Cells.Add(newCell);
                }
            }
        }

        public bool IsPresent(Cell Cell)
        {
            return Cells.IndexOf(Cell) != -1 ? true : false;
        }

        public void Add(Cell newCell)
        {
            if (!IsPresent(newCell))
            {
                Cells.Add(newCell);
            }
        }

        public override string ToString()
        {
            var str = "";

            foreach (Cell value in Cells)
            {
                str += string.Format("Row : {0,4}; Column : {1,4}", value.Row, value.Column);
            }

            return str;
        }

        public List<Cell> GetAroundCellsNoTags(Cell field, int maxValue)
        {
            var AroundCells = new List<Cell>();

            for (var row = field.Row - 1; row < field.Row + 2; row++)
            {
                for (var column = field.Column - 1; column < field.Column + 2; column++)
                {
                    if ((0 < row && row < maxValue + 1) &&
                        (0 < column && column < maxValue + 1))
                    {
                        if (!(row == field.Row && column == field.Column))
                        {
                            var newCell = new Cell(row, column);
                            if (!IsPresent(newCell))
                                AroundCells.Add(newCell);
                        }
                    }
                }
            }

            return AroundCells;
        }

        public Cell GetCellsToRC(int row, int column)
        {
            return Cells[Cells.IndexOf(new Cell(row, column))];
        }

        public bool IsCompleted(int countField, int countMines)
        {
            return Cells.Count == countField - countMines;
        }
    }
}
