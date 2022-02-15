using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorBlast
{
    class Game
    {
        public Table table { get; set; } = new Table();
        public int stepCount { get; set; } = 0;

        public Game()
        {
            table.CreateGrid();
        }

        public void Start()
        {
            ShowGrid();

            do
            {
                char colorNumber = Console.ReadKey().KeyChar;
                if (colorNumber >= '1' && colorNumber <= '5')
                {
                    stepCount++;
                    table.ClearGrid(colorNumber - 48);
                    Console.Clear();
                    ShowGrid();
                }
            } while (!ControlFinish());

            GameOver();
        }

        public void GameOver()
        {
            Console.Clear();
            Console.WriteLine($"Tebrikler oyunu {stepCount} adımda bitirdiniz.");
        }

        public void Coloring(string color)
        {
            ConsoleColor consoleColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), color);
            Console.BackgroundColor = consoleColor;
        }

        public void ShowGrid()
        {
            for (int i = 0; i < table.height; i++)
            {
                for (int j = 0; j < table.width; j++)
                {
                    Color color = (Color)table.grid[i, j].Color;
                    Coloring(color.ToString());
                    Console.Write("  ");
                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine();
            }

            string blank = new string(' ', table.width - 23);
            Console.Write($"\n{blank}");

            for (int i = 1; i < 6; i++)
            {
                Console.Write($"{i} = ");
                Coloring(((Color)i).ToString());
                Console.Write("  ");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("    ");
            }

            blank = new string(' ', table.width - 8);
            Console.WriteLine($"\n{blank}Adım Sayısı = {stepCount}");
        }

        public bool ControlFinish()
        {
            bool isFinish = true;

            for (int i = 0; i < table.height; i++)
                for (int j = 0; j < table.width; j++)
                    if (table.grid[i, j].Color != 0)
                    {
                        isFinish = false;
                        break;
                    }

            return isFinish;
        }
    }
}
