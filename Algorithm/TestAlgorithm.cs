namespace Algorithm
{
    using System;
    using Common;

    public class TestAlgorithm
    {
        private int countMine;
        private Cell size;

        private ListСell ListСell { get; set; }

        private ListСell MarkCell { get; set; }

        private TestAlgorithm()
        {
        }

        private static TestAlgorithm testAlgorithmInstance = null;

        public static TestAlgorithm GetInstance()
        {
            if (testAlgorithmInstance == null)
            {
                testAlgorithmInstance = new TestAlgorithm();
            }

            return testAlgorithmInstance;
        }

        public static Cell GetСhoice(ListСell listСell, ListСell markCells, Cell size, int countMine)
        {
            var testAlgorithm = GetInstance();
            testAlgorithm.size = size;
            testAlgorithm.countMine = countMine;
            testAlgorithm.ListСell = listСell;
            testAlgorithm.MarkCell = markCells;

            if ((listСell.Count == 0) || (testAlgorithm.GetMark() == null))
            {
                return Cell.Random(testAlgorithm.size);
            }
            else
            {
                return testAlgorithm.GetMark();
            }
        }

        public Cell GetMark()
        {
            foreach (Cell cell in ListСell)
            {
                if (cell.Value != 0)
                {
                    var aroundCells = ListСell.GetAroundCellsNoTags(cell, size);

                    if (cell.Value == aroundCells.Count)
                    {
                        foreach (Cell field in aroundCells)
                        {
                            if (!MarkCell.IsPresent(field))
                            {
                                return new Cell(field.Row, field.Column, true);
                            }
                        }
                    }
                }
            }

            return Get100Choice();
        }

        public Cell Get100Choice()
        {
            foreach (Cell cell in ListСell)
            {
                if (cell.Value != 0)
                {
                    var aroundCells = ListСell.GetAroundCellsNoTags(cell, size);

                    if (cell.Value != aroundCells.Count)
                    {
                        var countFlags = 0;

                        foreach (Cell field in aroundCells)
                        {
                            if (MarkCell.IsPresent(field))
                            {
                                countFlags++;
                            }
                        }

                        if (cell.Value == countFlags)
                        {
                            foreach (Cell field in aroundCells)
                            {
                                if (!MarkCell.IsPresent(field))
                                {
                                    return field;
                                }
                            }
                        }
                    }
                }
            }

            return GetBestRandomChoice();
        }

        public Cell GetBestRandomChoice()
        {
            var listNoTags = ListСell.GetReverseCells(size, ListСell, MarkCell);

            var minPercent = 10.0;
            var totalPercent = 1.0 * (countMine - MarkCell.Count) / ((size.Row * size.Column) - ListСell.Count);
            var randomList = new ListСell();

            foreach (Cell field in listNoTags)
            {
                var numberCells = listNoTags.GetAroundCellsNoTags(field, size);
                var maxPercentCell = GetMaxPercentCell(numberCells);
                AddRandomList(randomList, field, maxPercentCell, minPercent, totalPercent);
            }

            Random rand = new Random();
            return randomList[rand.Next(randomList.Count)];
        }

        private void AddRandomList(ListСell cells, Cell field, double maxPercentCell, double minPercent, double totalPercent)
        {
            if ((maxPercentCell < minPercent) && (maxPercentCell != 0))
            {
                minPercent = maxPercentCell;
                cells.Clear();
            }

            if (totalPercent < minPercent)
            {
                minPercent = totalPercent;
                cells.Clear();
            }

            if ((maxPercentCell == minPercent) || (totalPercent == minPercent))
            {
                cells.Add(field);
            }
        }

        private double GetMaxPercentCell(ListСell cells)
        {
            var maxPercentCell = 0.0;

            foreach (Cell numberField in cells)
            {
                if (ListСell.IsPresent(numberField))
                {
                    var aroundCells = ListСell.GetAroundCellsNoTags(numberField, size);
                    var countFlags = 0;

                    foreach (Cell aroundField in aroundCells)
                    {
                        if (MarkCell.IsPresent(aroundField))
                        {
                            countFlags++;
                        }
                    }

                    var countNoFlags = aroundCells.Count - countFlags;

                    var percentCell = 1.0 * (ListСell[numberField.Row, numberField.Column].Value - countFlags) / countNoFlags;

                    if (maxPercentCell < percentCell)
                    {
                        maxPercentCell = percentCell;
                    }
                }
            }

            return maxPercentCell;
        }
    }
}
