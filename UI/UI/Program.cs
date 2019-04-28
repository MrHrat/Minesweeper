using System;
using Common;
using Core;

namespace UI
{
    class Program
    {
        static void Main(string[] args)
        {
            GameField game = new GameField();

            game.GenerateMines();

            while (true)
            {
                Console.Write(game.ToString());

                if(game.Status != GameStatus.Play)
                {
                    Console.Write(game.Status);
                    break;
                }

                Console.WriteLine("Select a cell:");
                Console.Write("Row = ");
                int row = int.Parse(Console.ReadLine());
                Console.Write("Column = ");
                int column = int.Parse(Console.ReadLine());

                game.OpenCell(new Cell(row, column));
            }            

            Console.ReadKey();
        }
    }
}
