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
            Random rand = new Random();
            _MinedCells.Clear();

            while (_MinedCells.Count < _countMines)
            {
                var row = rand.Next(_sizeField) + 1;
                var column = rand.Next(_sizeField) + 1;

                var newCell = new Cell(row, column);

                if (_MinedCells.IndexOf(newCell) == -1)
                {
                    _MinedCells.Add(newCell);
                }
            }
        }

        public void ShowMines()
        {
            foreach(Cell value in _MinedCells)
            {
                Console.WriteLine("Row : {0,4}; Column : {1,4}", value.Row, value.Column);
            }
        }
    }
}
