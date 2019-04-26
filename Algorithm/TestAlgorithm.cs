using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace Algorithm
{
    public class TestAlgorithm
    {
        private int _size;

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

        public static Cell GetСhoice(List<Cell> list, int size)
        {
            var TestAlgorithm = GetInstance();
            TestAlgorithm._size = size;

            if((list != null) && (!list.Any()))
            {
                return TestAlgorithm.RandomSelection();
            }

            return new Cell(1, 1);
        }

        public Cell RandomSelection()
        {
            Random rand = new Random();

            var row = rand.Next(_size) + 1;
            var column = rand.Next(_size) + 1;

            return new Cell(row, column);
        }
    }
}
