namespace Core
{
    using Common;

    public class GameField
    {
        public int CountMines
        {
            get;
            private set;
        }

        = 10;

        public Сell Size
        {
            get;
            private set;
        }

        = new Сell(10, 10);

        public GameStatus Status
        {
            get;
            private set;
        }

        = GameStatus.Play;

        public ListСell VisibleСells
        {
            get;
            private set;
        }

        = new ListСell();

        public ListСell Marks
        {
            get;
            private set;
        }

        = new ListСell();

        private ListСell Mines
        {
            get;
            set;
        }

        = new ListСell();

        public GameField()
        {
        }

        public GameField(int sizeRow, int sizeColumn, int countMines)
        {
            Size = new Сell(sizeRow, sizeColumn);
            CountMines = countMines;
        }

        public void GenerateMines()
        {
            var randomCellsList = ListСell.Generate(Size, CountMines);

            foreach (Сell field in randomCellsList)
            {
                Mines.Add(new CellMine(field));
            }
        }

        public void OpenCell(Сell field)
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

            foreach (Сell fieldIntersection in ListСell.Intersection(Marks, Mines))
            {
                markMineList.Add(new CellMine(fieldIntersection, true));
            }

            VisibleСells.Add(markMineList);
        }

        private ListСell GetRemovedMarkedMines()
        {
            var markMineList = new ListСell();

            foreach (Сell field in Mines)
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

        private void AddVisibleСell(Сell field)
        {
            var countMine = CountMineAroundCell(field);

            if (countMine == 0)
            {
                VisibleСells.Add(new Сell(field.Row, field.Column));

                var aroundCells = VisibleСells.GetAroundCellsNoTags(field, Size);
                foreach (Сell acell in aroundCells)
                {
                    AddVisibleСell(acell);
                }
            }
            else
            {
                VisibleСells.Add(new CellValue(field, countMine));
            }
        }

        private int CountMineAroundCell(Сell field)
        {
            var aroundCells = VisibleСells.GetAroundCellsNoTags(field, Size);
            var countMine = 0;

            foreach (Сell acell in aroundCells)
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
            var screen = string.Empty;
            var listCells = new ListСell();
            listCells.Add(VisibleСells, Marks);

            for (var row = 0; row < Size.Row; row++)
            {
                for (var column = 0; column < Size.Column; column++)
                {
                    switch (listCells[row, column].Status)
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
