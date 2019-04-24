using System;
using Common;

namespace Algorithm
{
    public class TestAlgorithm
    {
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

        public static Cell GetСhoice()
        {
            var TestAlgorithm = GetInstance();

            return new Cell(1,1);
        }
    }
}
