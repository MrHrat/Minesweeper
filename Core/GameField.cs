using System;
using System.Collections.Generic;
using System.Text;
using Common;

namespace Core
{
    public class GameField
    {
        private int _countMines = 10;
        private int _sizeField = 10;

        public List<Cell> UsersCells
        {
            get;
            private set;
        }

        private List<Cell> _MinedCells
        {
            get;
            set;
        }

        public GameField()
        {
            UsersCells = new List<Cell>();
            _MinedCells = new List<Cell>();
        }

        public void GenerateMines()
        {
            _MinedCells.Clear();

            while (_MinedCells.Count < _countMines)
            {
                var newCell = RandomCell();
                
                if (_MinedCells.IndexOf(newCell) == -1)
                {
                    _MinedCells.Add(newCell);
                }
            }
        }

        private Cell RandomCell()
        {
            Random rand = new Random();

            var row = rand.Next(_sizeField) + 1;
            var column = rand.Next(_sizeField) + 1;

            return new Cell(row, column);
        }

        public void ShowMines()
        {
            foreach(Cell value in _MinedCells)
            {
                Console.WriteLine("Row : {0,4}; Column : {1,4}", value.Row, value.Column);
            }
        }

        public void ShowUsersCells()
        {
            foreach (Cell value in UsersCells)
            {
                Console.WriteLine("Row : {0,4}; Column : {1,4}", value.Row, value.Column);
            }
        }

        public void OpenCell(Cell field)
        {
            if (_MinedCells.IndexOf(field) == -1)
            {
                AddCelltoUsers(field);
            }
        }

        private void AddCelltoUsers(Cell field)
        {
            var AroundCells = GetAroundCells(field);
            var countMine = 0;

            foreach (Cell acell in AroundCells)
            {
                if (_MinedCells.IndexOf(acell) != -1)
                {
                    countMine++;
                }
            }

            if (countMine == 0)
            {
                UsersCells.Add(new Cell(field.Row, field.Column));

                var AroundCellsNoTags = GetAroundCellsNoTags(AroundCells);
                foreach (Cell acell in AroundCellsNoTags)
                {
                    AddCelltoUsers(acell);
                }
            }
            else
            {
                UsersCells.Add(new CellValue(field, countMine));                
            }
        }

        private List<Cell> GetAroundCells(Cell field)
        {
            var AroundCells = new List<Cell>();

            for(var row = field.Row - 1; row < field.Row + 2; row++)
            {
                for(var column = field.Column - 1; column < field.Column + 2; column++)
                {
                    if ((0 < row && row < _sizeField + 1) && 
                        (0 < column && column < _sizeField + 1))
                    {
                        if(!(row == field.Row && column == field.Column))
                        {
                            AroundCells.Add(new Cell(row, column));
                        }
                    }
                }
            }

            return AroundCells;
        }

        private List<Cell> GetAroundCellsNoTags(List<Cell> AroundCells)
        {
            var AroundCellsNoTags = new List<Cell>();

            foreach (Cell acell in AroundCells)
            {
                if ((UsersCells.IndexOf(acell) == -1))
                {
                    AroundCellsNoTags.Add(acell);
                }
            }

            return AroundCellsNoTags;
        }

        public string PrintField()
        {
            var screen = "";

            for(var row = 1; row < _sizeField + 1; row++)
            {
                for(var column = 1; column < _sizeField + 1; column++)
                {
                    if (UsersCells.IndexOf(new Cell(row, column)) == -1)
                    {
                        screen += "O" + " ";
                    }
                    else
                    {
                        if (UsersCells[UsersCells.IndexOf(new Cell(row, column))].Status == CellStatus.Number)
                        {
                            screen += UsersCells[UsersCells.IndexOf(new Cell(row, column))].Value + " ";
                        }

                        if (UsersCells[UsersCells.IndexOf(new Cell(row, column))].Status == CellStatus.Open)
                        {
                            screen += "_" + " ";
                        }
                    }
                }
                screen += "\n";
            }

            return screen;
        }
    }
}
