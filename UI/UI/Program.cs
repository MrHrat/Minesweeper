using Algorithm;
using Common;
using Core;
using System;
using static System.Console;

namespace UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Write("Select a size field: ");
            var size = int.Parse(ReadLine());
            Write("Select a count mine: ");
            var mine = int.Parse(ReadLine());
            var game = new GameField(size, mine);
            game.GenerateMines();
            var field = new Cell(0, 0);
            do
            {
                Clear();
                WriteLine("Size: {0,4}; Count mine: {1,4};", game.SizeField, game.CountMines);
                Write(game);
                
                if (game.Status == GameStatus.Play)
                {
                    WriteLine("Press the key");
                    WriteLine("1. Enter your cell");
                    WriteLine("2. Use the algorithm");
                    
                    switch (ReadKey().Key)
                    {
                        case ConsoleKey.NumPad1:
                            WriteLine();
                            field = SelectCell();
                            break;
                        default:
                            field = TestAlgorithm.GetСhoice(game.VisibleСells, game.Marks, game.SizeField, game.CountMines);
                            break;
                    }
                    WriteLine(field);
                    game.OpenCell(field);
                    Write(game);
                }
                else
                {
                    WriteLine(field);
                    Write(game.Status);
                }
            } while (ReadKey().Key != ConsoleKey.Q);
        }

        static Cell SelectCell()
        {
            string input = String.Empty;
            try
            {
                WriteLine("Select a cell:");
                Write("Row = ");
                var row = int.Parse(ReadLine());
                Write("Column = ");
                var column = int.Parse(ReadLine());
                Write("Marked = ");
                var mark = int.Parse(ReadLine());

                return new Cell(row, column, mark != 0);
            }
            catch (FormatException)
            {
                WriteLine($"Unable to parse '{input}'");
                return SelectCell();
            }            
        }
    }
}
