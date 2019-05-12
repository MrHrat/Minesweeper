namespace Common
{
    using System.Collections;
    using System.Collections.Generic;

    public class ListСell : IReadOnlyList<Сell>
    {
        private List<Сell> Cells
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListСell"/> class.
        /// </summary>
        public ListСell()
        {
            Cells = new List<Сell>();
        }

        public static ListСell GetReverseCells(Сell maxCell, params ListСell[] cells)
        {
            var listСell = new ListСell();

            foreach (ListСell field in cells)
            {
                listСell.Add(field);
            }

            var listNoTags = new ListСell();

            for (var row = 0; row < maxCell.Row; row++)
            {
                for (var column = 0; column < maxCell.Column; column++)
                {
                    var item = new Сell(row, column);
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

            foreach (Сell field in sets[0])
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

            foreach (Сell field in sets[0])
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

        public static ListСell Generate(Сell maxCell, int count)
        {
            var gList = new ListСell();

            while (gList.Count < count)
            {
                gList.Add(Сell.Random(maxCell));
            }

            return gList;
        }

        public bool IsPresent(Сell cell)
        {
            return Cells.IndexOf(cell) != -1 ? true : false;
        }

        public void Add(Сell newCell)
        {
            if (!IsPresent(newCell))
            {
                Cells.Add(newCell);
            }
        }

        public bool Remove(Сell field)
        {
            return Cells.Remove(Cells[Cells.IndexOf(field)]);
        }

        public void Add(params ListСell[] cells)
        {
            foreach (ListСell cellsList in cells)
            {
                foreach (Сell field in cellsList)
                {
                    Add(field);
                }
            }
        }

        public void AddClick(Сell cell)
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
                foreach (Сell field in cellsList)
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
            var str = string.Empty;

            foreach (Сell value in Cells)
            {
                str += value.ToString() + "\n";
            }

            return str;
        }

        public ListСell GetAroundCellsNoTags(Сell field, Сell maxCell)
        {
            var aroundCells = new ListСell();

            for (var row = field.Row - 1; row <= field.Row + 1; row++)
            {
                for (var column = field.Column - 1; column <= field.Column + 1; column++)
                {
                    if (row >= 0 && row < maxCell.Row &&
                        column >= 0 && column < maxCell.Column)
                    {
                        if (!(row == field.Row && column == field.Column))
                        {
                            var newCell = new Сell(row, column);
                            if (!IsPresent(newCell))
                            {
                                aroundCells.Add(newCell);
                            }
                        }
                    }
                }
            }

            return aroundCells;
        }

        public bool IsCompleted(Сell maxCell, int countMines) => Cells.Count == (maxCell.Row * maxCell.Column) - countMines;

        public Сell this[int row, int column]
        {
            get
            {
                if (Cells.IndexOf(new Сell(row, column)) != -1)
                {
                    return Cells[Cells.IndexOf(new Сell(row, column))];
                }
                else
                {
                    return new CellAbsent(new Сell(row, column));
                }
            }
        }

        public Сell this[int index] => Cells[index];

        public int Count => Cells.Count;

        public IEnumerator<Сell> GetEnumerator()
        {
            return Cells.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
