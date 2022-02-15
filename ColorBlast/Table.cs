using System;

namespace ColorBlast
{
    class Table
    {
        public int width { get; set; }
        public int height { get; set; }
        public Cell[,] grid { get; set; }

        public Table()
        {
            width = 50;
            height = 25;
        }

        public Table(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public void CreateGrid()
        {
            grid = new Cell[height, width];

            Random random = new Random();
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                {
                    grid[i, j] = new Cell();
                    grid[i, j].Color = random.Next(1, 6);
                }
                    
            grid[0, 0].Color = 0;
        }

        public void ClearGrid(int color)
        {
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    if (grid[i, j].Color == 0)
                        if (ControlCell(color, i, j) > 0)
                            ClearGrid(color);
        }

        public int ControlCell(int color, int i, int j)
        {
            int flag = 0;

            if (j != width - 1 && grid[i, j + 1].Color == color)
            {
                grid[i, j + 1].Color = 0;
                flag++;
            }

            if (j != 0 && grid[i, j - 1].Color == color)
            {
                grid[i, j - 1].Color = 0;
                flag++;
            }

            if (i != height - 1 && grid[i + 1, j].Color == color)
            {
                grid[i + 1, j].Color = 0;
                flag++;
            }

            if (i != 0 && grid[i - 1, j].Color == color)
            {
                grid[i - 1, j].Color = 0;
                flag++;
            }

            return flag;
        }
    }
}