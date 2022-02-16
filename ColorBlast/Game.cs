using System;

namespace ColorBlast
{
    class Game
    {
        #region Windows Makinelere Özel (Only for Windows Machine)
        [System.Runtime.InteropServices.DllImport("kernel32.dll", EntryPoint = "GetConsoleWindow", SetLastError = true)]
        private static extern IntPtr GetConsoleHandle();

        /// <summary>
        /// Opsiyonel bir fonksiyondur. Windows dışı makinelerde kapatılması gerekir.
        /// </summary>
        public void ShowImage()
        {
            var handler = GetConsoleHandle();

            using (var graphics = System.Drawing.Graphics.FromHwnd(handler))
            using (var image = System.Drawing.Image.FromFile("logo.jpg"))
                graphics.DrawImage(image, 0, 50, 275, 200);
            Console.WriteLine(new string(' ', 12) + "Ömer Gürbüz\n");
        }
        #endregion

        public Table table { get; set; }
        public int stepCount { get; set; } = 0;

        public Game()
        {
            table = new Table(25,20);
            table.CreateGrid();
        }

        public Game(int width, int height)
        {
            table = new Table(width, height);
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
            Console.WriteLine($"Tebrikler oyunu {stepCount} adımda bitirdiniz.{new string('\n', 15)}");

            ShowImage();
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

            if (table.width >=23)
            {
                string blank = new string(' ', table.width - 23);
                Console.Write($"\n{blank}");
            }
            else
                Console.WriteLine();

            for (int i = 1; i < 6; i++)
            {
                Console.Write($"{i} = ");
                Coloring(((Color)i).ToString());
                Console.Write("  ");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("    ");
            }

            if (table.width >= 23)
            {
                string blank = new string(' ', table.width - 8);
                Console.Write($"\n{blank}");
            }
            else
                Console.Write("\n" + new string(' ',17));

            Console.WriteLine($"Adım Sayısı = {stepCount}");
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
