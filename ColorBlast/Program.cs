using System;

namespace ColorBlast
{
    enum Color
    {
        Black, DarkRed, DarkGreen, DarkBlue, DarkYellow, Gray
    }

    class Program
    {
        static int height = 25, width = 50;

        static void CreateGrid(ref int[,] grid)
        {
            Random random = new Random();
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    grid[i, j] = random.Next(1, 6);
            grid[0, 0] = 0;
        }

        static void Coloring(string color)
        {
            ConsoleColor consoleColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), color);
            Console.BackgroundColor = consoleColor;
        }

        static void ShowGrid(int[,] grid, int stepCount)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Color color = (Color)grid[i, j];
                    Coloring(color.ToString());
                    Console.Write("  ");
                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine();
            }

            string blank = new string(' ', width - 23);
            Console.Write($"\n{blank}");

            for (int i = 1; i < 6; i++)
            {
                Console.Write($"{i} = ");
                Coloring(((Color)i).ToString());
                Console.Write("  ");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("    ");
            }

            blank = new string(' ', width - 8);
            Console.WriteLine($"\n{blank}Adım Sayısı = {stepCount}");
        }

        static void ClearGrid(ref int[,] grid, int color)
        {
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    if (grid[i, j] == 0)
                        if (ControlCell(ref grid, color, i, j) > 0)
                            ClearGrid(ref grid, color);
        }

        static int ControlCell(ref int[,] grid, int color, int i, int j)
        {
            int flag = 0;

            if (j != width - 1 && grid[i, j + 1] == color)
            {
                grid[i, j + 1] = 0;
                flag++;
            }

            if (j != 0 && grid[i, j - 1] == color)
            {
                grid[i, j - 1] = 0;
                flag++;
            }

            if (i != height - 1 && grid[i + 1, j] == color)
            {
                grid[i + 1, j] = 0;
                flag++;
            }

            if (i != 0 && grid[i - 1, j] == color)
            {
                grid[i - 1, j] = 0;
                flag++;
            }

            return flag;
        }

        static bool ControlFinish(int[,] grid)
        {
            bool isFinish = true;

            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    if (grid[i,j] != 0)
                    {
                        isFinish = false;
                        break;
                    }

            return isFinish;
        }

        static void Main(string[] args)
        {
            int stepCount = 0;
            int[,] grid = new int[height, width];
            CreateGrid(ref grid);
            ShowGrid(grid, stepCount);

            do {
                char colorNumber = Console.ReadKey().KeyChar;
                if (colorNumber >= '1' && colorNumber <= '5')
                {
                    stepCount++;
                    ClearGrid(ref grid, colorNumber - 48);
                    Console.Clear();
                    ShowGrid(grid, stepCount);
                }
            } while (!ControlFinish(grid));

            Console.Clear();
            Console.WriteLine($"Tebrikler oyunu {stepCount} adımda bitirdiniz.");
        }
    }
}
