using System;
using System.Collections.Generic;
using System.Text;
using Common;

namespace Core
{
    public class GameField
    {
        public int CountMines
        {
            get;
            private set;
        } = 10;

        public int SizeField
        {
            get;
            private set;
        } = 10;

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

        public ListСell MarkСells
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

        public GameField(int sizeField, int countMines)
        {
            SizeField = sizeField;
            CountMines = countMines;
        }

        public void GenerateMines()
        {
            Mines.Generate(CountMines, SizeField);
        }

        public void OpenCell(Cell field)
        {
            if(Status == GameStatus.Play)
            {
                if (field.Status == CellStatus.Mark)
                {
                    MarkСells.AddClick(field);
                }
                else if (!Mines.IsPresent(field))
                {
                    AddVisibleСell(field);

                    if (VisibleСells.IsCompleted(SizeField * SizeField, CountMines))
                    {
                        Status = GameStatus.Victory;
                    }                    
                }
                else
                {
                    Status = GameStatus.GameOver;
                }
            }
        }

        private void AddVisibleСell(Cell field)
        {
            var countMine = CountMineAroundCell(field);

            if (countMine == 0)
            {
                VisibleСells.Add(new Cell(field.Row, field.Column));
                
                var AroundCells = VisibleСells.GetAroundCellsNoTags(field, SizeField);
                foreach (Cell acell in AroundCells)
                {
                    AddVisibleСell(acell);
                }
            }
            else
            {
                VisibleСells.Add(new CellValue(field, countMine));                
            }
        }

        private int CountMineAroundCell(Cell field)
        {
            var AroundCells = VisibleСells.GetAroundCellsNoTags(field, SizeField);
            var countMine = 0;

            foreach (Cell acell in AroundCells)
            {
                if (Mines.IsPresent(acell))
                {
                    countMine++;
                }
            }

            return countMine;
        }

        public override string ToString()
        {
            var screen = "";
            var listCells = new ListСell();
            listCells.AddListCells(VisibleСells, MarkСells);

            for (var row = 1; row < SizeField + 1; row++)
            {
                for (var column = 1; column < SizeField + 1; column++)
                {
                    if (!VisibleСells.IsPresent(new Cell(row, column)))
                    {
                        screen += "O" + " ";                        
                    }
                    else
                    {
                        switch(listCells[row, column].Status)
                        {
                            case CellStatus.Number:
                                screen += listCells[row, column].Value + " ";
                                break;
                            case CellStatus.Open:
                                screen += "_" + " ";
                                break;
                            case CellStatus.Mark:
                                screen += "F" + " ";
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
