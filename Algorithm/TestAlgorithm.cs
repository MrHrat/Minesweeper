namespace Algorithm
{
    using System;
    using Common;

    public class TestAlgorithm
    {
        private int countMine;
        private Сell size;

        private ListСell VisibleСell { get; set; }

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

        public static Сell GetСhoice(ListСell listСell, ListСell markCells, Сell size, int countMine)
        {
            var testAlgorithm = GetInstance();
            testAlgorithm.size = size;
            testAlgorithm.countMine = countMine;
            testAlgorithm.VisibleСell = listСell;
            testAlgorithm.MarkCell = markCells;

            if ((listСell.Count == 0) || (testAlgorithm.GetMark() == null))
            {
                return Сell.Random(testAlgorithm.size);
            }
            else
            {
                return testAlgorithm.GetMark();
            }
        }

        public Сell GetMark()
        {
            foreach (Сell cell in VisibleСell)
            {
                if (cell.Value != 0)
                {
                    var aroundCells = VisibleСell.GetAroundCellsNoTags(cell, size);

                    if (cell.Value == aroundCells.Count)
                    {
                        foreach (Сell field in aroundCells)
                        {
                            if (!MarkCell.IsPresent(field))
                            {
                                return new Сell(field.Row, field.Column, true);
                            }
                        }
                    }
                }
            }

            return Get100Choice();
        }

        public Сell Get100Choice()
        {
            foreach (Сell cell in VisibleСell)
            {
                if (cell.Value != 0)
                {
                    var aroundCells = VisibleСell.GetAroundCellsNoTags(cell, size);

                    if (cell.Value != aroundCells.Count)
                    {
                        var countFlags = 0;

                        foreach (Сell field in aroundCells)
                        {
                            if (MarkCell.IsPresent(field))
                            {
                                countFlags++;
                            }
                        }

                        if (cell.Value == countFlags)
                        {
                            foreach (Сell field in aroundCells)
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

        public Сell GetBestRandomChoice()
        {
            var listNoTags = ListСell.GetReverseCells(size, VisibleСell, MarkCell);

            var minPercent = 10.0;
            var totalPercent = 1.0 * (countMine - MarkCell.Count) / ((size.Row * size.Column) - VisibleСell.Count);
            var randomList = new ListСell();

            foreach (Сell field in listNoTags)
            {
                var numberCells = listNoTags.GetAroundCellsNoTags(field, size);
                var maxPercentCell = GetMaxPercentCell(numberCells);
                AddRandomList(randomList, field, maxPercentCell, minPercent, totalPercent);
            }

            Random rand = new Random();
            return randomList[rand.Next(randomList.Count)];
        }

        private void AddRandomList(ListСell cells, Сell field, double maxPercentCell, double minPercent, double totalPercent)
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

            foreach (Сell numberField in cells)
            {
                if (VisibleСell.IsPresent(numberField))
                {
                    var aroundCells = VisibleСell.GetAroundCellsNoTags(numberField, size);
                    var countFlags = 0;

                    foreach (Сell aroundField in aroundCells)
                    {
                        if (MarkCell.IsPresent(aroundField))
                        {
                            countFlags++;
                        }
                    }

                    var countNoFlags = aroundCells.Count - countFlags;

                    var percentCell = 1.0 * (VisibleСell[numberField.Row, numberField.Column].Value - countFlags) / countNoFlags;

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
