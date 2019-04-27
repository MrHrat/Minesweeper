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
        public GameStatus Status
        {
            get;
            private set;
        } = GameStatus.Play;

        public ListСell VisibleСells
        {
            get;
            private set;
        } = new ListСell();

        private ListСell Mines
        {
            get;
            set;
        } = new ListСell();

        public GameField() { }

        public void GenerateMines()
        {
            Mines.Generate(_countMines, _sizeField);
        }

        public void OpenCell(Cell field)
        {
            if(Status == GameStatus.Play)
            {
                if (!Mines.IsPresent(field))
                {
                    AddCelltoUsers(field);
                }
                else
                {
                    Status = GameStatus.GameOver;
                }
            }
        }

        private void AddCelltoUsers(Cell field)
        {
            var AroundCells = VisibleСells.GetAroundCellsNoTags(field, _sizeField);
            var countMine = 0;

            foreach (Cell acell in AroundCells)
            {
                if (Mines.IsPresent(acell))
                {
                    countMine++;
                }
            }

            if (countMine == 0)
            {
                VisibleСells.Add(new Cell(field.Row, field.Column));
                
                foreach (Cell acell in AroundCells)
                {
                    AddCelltoUsers(acell);
                }
            }
            else
            {
                VisibleСells.Add(new CellValue(field, countMine));                
            }
        }

        public override string ToString()
        {
            var screen = "";

            for (var row = 1; row < _sizeField + 1; row++)
            {
                for (var column = 1; column < _sizeField + 1; column++)
                {
                    if (!VisibleСells.IsPresent(new Cell(row, column)))
                    {
                        screen += "O" + " ";
                    }
                    else
                    {
                        switch(VisibleСells.GetSellsToRC(row, column).Status)
                        {
                            case CellStatus.Number:
                                screen += VisibleСells.GetSellsToRC(row, column).Value + " ";
                                break;
                            case CellStatus.Open:
                                screen += "_" + " ";
                                break;
                        }
                    }
                }

                screen += "\n";
            }

            return screen;
        }
    }
}
