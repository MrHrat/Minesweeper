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
            Write("Select a size field row: ");
            var sizeRow = int.Parse(ReadLine());
            Write("Select a size field column: ");
            var sizeColumn = int.Parse(ReadLine());
            Write("Select a count mine: ");
            var mine = int.Parse(ReadLine());
            var game = new GameField(sizeRow, sizeColumn, mine);
            game.GenerateMines();
            var field = new Cell(0, 0);
            do
            {
                Clear();
                WriteLine("Size: R ={0,4} C ={1,4}; Count mine: {2,4};", game.Size.Row, game.Size.Column, game.CountMines);
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
                            field = TestAlgorithm.GetСhoice(game.VisibleСells, game.Marks, game.Size, game.CountMines);
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
            var input = string.Empty;
            try
            {
                WriteLine("Select a cell:");
                Write("Row = ");
                var row = int.Parse(ReadLine()) - 1;
                Write("Column = ");
                var column = int.Parse(ReadLine()) - 1;
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
