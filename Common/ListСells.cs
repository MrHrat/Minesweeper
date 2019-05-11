using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Common;

namespace Common
{
    public class ListСell : IReadOnlyList<Cell>
    {
        private List<Cell> Cells
        {
            get;
            set;
        }

        public ListСell()
        {
            Cells = new List<Cell>();
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

        public bool Remove(Cell field)
        {
            return Cells.Remove(Cells[Cells.IndexOf(field)]);
        }

        public void Add(params ListСell[] cells)
        {
            foreach (ListСell cellsList in cells)
            {
                foreach (Cell field in cellsList)
                {
                    Add(field);
                }
            }            
        }

        public void AddClick(Cell cell)
        {
            if (!Cells.Remove(cell))
            {
                Cells.Add(cell);
            }
        }

        public void AddClick(params ListСell[] cells)
        {
            foreach (ListСell cellsList in cells)
            {
                foreach (Cell field in cellsList)
                {
                    AddClick(field);
                }
            }
        }

        public void Clear()
        {
            Cells.Clear();
        }

        public override string ToString()
        {
            var str = "";

            foreach (Cell value in Cells)
            {
                str += value.ToString() + "\n";
            }

            return str;
        }

        public ListСell GetAroundCellsNoTags(Cell field, int maxValue)
        {
            var AroundCells = new ListСell();

            for (var row = field.Row - 1; row <= field.Row + 1; row++)
            {
                for (var column = field.Column - 1; column <= field.Column + 1; column++)
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

        public static ListСell GetReverseCells(int maxValue, params ListСell[] cells)
        {
            var listСell = new ListСell();

            foreach (ListСell field in cells)
            {
                listСell.Add(field);
            }

            var listNoTags = new ListСell();
            
            for (var row = 1; row <= maxValue; row++)
            {
                for (var column = 1; column <= maxValue; column++)
                {
                    var item = new Cell(row, column);
                    if (!listСell.IsPresent(item))
                    {
                        listNoTags.Add(item);
                    }
                }
            }

            return listNoTags;
        }

        public static ListСell Intersection(params ListСell[] sets)
        {
            var intersection = new ListСell();

            foreach (Cell field in sets[0])
            {
                if (sets[1].IsPresent(field))
                {
                    intersection.Add(field);
                }
            }

            if (sets.Length > 2)
            {
                var list = new List<ListСell>();
                list.Add(intersection);

                for (var i = 2; i < sets.Length; i++)
                {
                    list.Add(sets[i]);
                }

                return Intersection(list.ToArray());
            }
            
            return intersection;
        }

        public static ListСell RelativeComplement(params ListСell[] sets)
        {
            var relativeComplement = new ListСell();

            foreach (Cell field in sets[0])
            {
                if (!sets[1].IsPresent(field))
                {
                    relativeComplement.Add(field);
                }
            }

            if (sets.Length > 2)
            {
                var list = new List<ListСell>();
                list.Add(relativeComplement);

                for (var i = 2; i < sets.Length; i++)
                {
                    list.Add(sets[i]);
                }

                return RelativeComplement(list.ToArray());
            }            

            return relativeComplement;
        }

        public static ListСell Generate(int count, int maxValue)
        {
            var gList = new ListСell();

            while (gList.Count < count)
            {
                gList.Add(Cell.Random(maxValue));
            }

            return gList;
        }

        public bool IsCompleted(int countField, int countMines)
        {
            return Cells.Count == countField - countMines;
        }

        public Cell this[int row, int column]
        {
            get
            {
                if (Cells.IndexOf(new Cell(row, column)) != -1)
                {
                    return Cells[Cells.IndexOf(new Cell(row, column))];
                }
                else
                {
                    return new CellAbsent(new Cell(row, column));
                }
            }
        }        

        #region IReadOnlyList<Cell> definition
        public Cell this[int index]
        {
            get
            {
                return Cells[index];
            }
        }

        public int Count
        {
            get
            {
                return Cells.Count;
            }
        }

        public IEnumerator<Cell> GetEnumerator() { return Cells.GetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }        
        #endregion
    }
}
