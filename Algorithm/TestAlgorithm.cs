using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace Algorithm
{
    public class TestAlgorithm
    {
        private int _countMine;
        private Cell _size;
        private ListСell _listСell { set; get; }
        private ListСell _markCells { set; get; }

        private TestAlgorithm() { }

        private static TestAlgorithm _testAlgorithmInstance = null;
        public static TestAlgorithm GetInstance()
        {
            if (_testAlgorithmInstance == null)
            {
                _testAlgorithmInstance = new TestAlgorithm();
            }
            return _testAlgorithmInstance;
        }

        public static Cell GetСhoice(ListСell listСell, ListСell markCells, Cell size, int countMine)
        {
            var TestAlgorithm = GetInstance();
            TestAlgorithm._size = size;
            TestAlgorithm._countMine = countMine;
            TestAlgorithm._listСell = listСell;
            TestAlgorithm._markCells = markCells;
            
            if ((listСell.Count == 0) || (TestAlgorithm.GetMark() == null))
            {
                return Cell.Random(TestAlgorithm._size);
            }
            else
            {
                return TestAlgorithm.GetMark();                
            }
        }

        public Cell GetMark()
        {
            foreach (Cell cell in _listСell)
            {
                if (cell.Value != 0)
                {
                    var aroundCells = _listСell.GetAroundCellsNoTags(cell, _size);

                    if (cell.Value == aroundCells.Count)
                    {
                        foreach (Cell field in aroundCells)
                        {
                            if (!_markCells.IsPresent(field))
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
            foreach (Cell cell in _listСell)
            {
                if (cell.Value != 0)
                {
                    var aroundCells = _listСell.GetAroundCellsNoTags(cell, _size);

                    if (cell.Value != aroundCells.Count)
                    {
                        var countFlags = 0;

                        foreach (Cell field in aroundCells)
                        {
                            if (_markCells.IsPresent(field))
                            {
                                countFlags++;
                            }
                        }

                        if (cell.Value == countFlags)
                        {
                            foreach (Cell field in aroundCells)
                            {
                                if (!_markCells.IsPresent(field))
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
            var listNoTags = ListСell.GetReverseCells(_size, _listСell, _markCells);

            var minPercent = 10.0;
            var totalPercent = 1.0 * (_countMine - _markCells.Count) / (_size.Row * _size.Column - _listСell.Count);
            var randomList = new ListСell();

            foreach (Cell field in listNoTags)
            {
                var numberCells = listNoTags.GetAroundCellsNoTags(field, _size);
                var maxPercentCell = GetMaxPercentCell(numberCells);
                AddRandomList(randomList, field, maxPercentCell, minPercent, totalPercent);
            }

            Random rand = new Random();
            return randomList[rand.Next(randomList.Count)];
        }

        void AddRandomList(ListСell cells, Cell field, double maxPercentCell, double minPercent, double totalPercent)
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

        double GetMaxPercentCell(ListСell cells)
        {
            var maxPercentCell = 0.0;

            foreach (Cell numberField in cells)
            {
                if (_listСell.IsPresent(numberField))
                {
                    var aroundCells = _listСell.GetAroundCellsNoTags(numberField, _size);
                    var countFlags = 0;

                    foreach (Cell aroundField in aroundCells)
                    {
                        if (_markCells.IsPresent(aroundField))
                        {
                            countFlags++;
                        }
                    }

                    var countNoFlags = aroundCells.Count - countFlags;

                    var PercentCell = 1.0 * (_listСell[numberField.Row, numberField.Column].Value - countFlags) / countNoFlags;

                    if (maxPercentCell < PercentCell)
                    {
                        maxPercentCell = PercentCell;
                    }
                }
            }

            return maxPercentCell;
        }
    }
}
