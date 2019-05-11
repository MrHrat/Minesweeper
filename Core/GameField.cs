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

        public Cell Size
        {
            get;
            private set;
        } = new Cell(10, 10);

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

        public ListСell Marks
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

        public GameField(int sizeRow, int sizeColumn, int countMines)
        {
            Size = new Cell(sizeRow, sizeColumn);
            CountMines = countMines;
        }

        public void GenerateMines()
        {
            var randomCellsList = ListСell.Generate(Size, CountMines);

            foreach (Cell field in randomCellsList)
            {
                Mines.Add(new CellMine(field));
            }
        }

        public void OpenCell(Cell field)
        {
            if (Status == GameStatus.Play)
            {
                if (field.Status == CellStatus.Mark)
                {
                    Marks.AddClick(field);
                }
                else if (!Mines.IsPresent(field))
                {
                    AddVisibleСell(field);

                    if (VisibleСells.IsCompleted(Size, CountMines))
                    {
                        Status = GameStatus.Victory;
                        VisibleMarkMine();
                        VisibleСells.Add(Mines);
                    }                    
                }
                else
                {
                    Status = GameStatus.GameOver;
                    VisibleСells.Add(new CellExplosion(field));
                    VisibleMarkMine();
                    VisibleСells.Add(Mines);
                }
            }
        }

        private void VisibleMarkMine()
        {
            var markMineList = new ListСell();

            foreach (Cell fieldIntersection in ListСell.Intersection(Marks, Mines))
            {
                markMineList.Add(new CellMine(fieldIntersection, true));
            }

            VisibleСells.Add(markMineList);
        }


        private ListСell GetRemovedMarkedMines()
        {
            var markMineList = new ListСell();

            foreach (Cell field in Mines)
            {
                if (Marks.IsPresent(field))
                {
                    Marks.Remove(field);
                    Mines.Remove(field);
                    markMineList.Add(new CellMine(field, true));
                }
            }

            return markMineList;
        }

        private void AddVisibleСell(Cell field)
        {
            var countMine = CountMineAroundCell(field);

            if (countMine == 0)
            {
                VisibleСells.Add(new Cell(field.Row, field.Column));
                
                var AroundCells = VisibleСells.GetAroundCellsNoTags(field, Size);
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
            var AroundCells = VisibleСells.GetAroundCellsNoTags(field, Size);
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
            listCells.Add(VisibleСells, Marks);

            for (var row = 1; row < Size.Row + 1; row++)
            {
                for (var column = 1; column < Size.Column + 1; column++)
                {
                    switch(listCells[row, column].Status)
                    {
                        case CellStatus.Absent:
                            screen += "O" + " ";
                            break;
                        case CellStatus.Number:
                            screen += listCells[row, column].Value + " ";
                            break;
                        case CellStatus.Open:
                            screen += "_" + " ";
                            break;
                        case CellStatus.Mark:
                            screen += "F" + " ";
                            break;
                        case CellStatus.MarkMine:
                            screen += "M" + " ";
                            break;
                        case CellStatus.Mine:
                            screen += "H" + " ";
                            break;
                        case CellStatus.Explosion:
                            screen += "E" + " ";
                            break;
                    }                    
                }

                screen += "\n";
            }

            return screen;
        }
    }
}
