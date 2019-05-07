using Algorithm;
using Common;
using Core;
using static System.Console;

namespace UI
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new GameField(10, 10);

            game.GenerateMines();

            while (true)
            {
                Write(game.ToString());

                if(game.Status != GameStatus.Play)
                {
                    Write(game.Status);
                    break;
                }                

                var cell = TestAlgorithm.GetСhoice(game.VisibleСells, game.MarkСells, game.SizeField, game.CountMines);
                WriteLine("R = {0,4}; C = {1,4}; Mark = {2,4}", cell.Row, cell.Column, cell.Status);
                game.OpenCell(cell);
                ReadKey();
                WriteLine();
            }            

            ReadKey();
        }

        Cell SelectCell()
        {
            WriteLine("Select a cell:");
            Write("Row = ");
            var row = int.Parse(ReadLine());
            Write("Column = ");
            var column = int.Parse(ReadLine());

            return new Cell(row, column);
        }
    }
}
