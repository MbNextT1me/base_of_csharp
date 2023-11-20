using System;
using System.IO;

class Program
{
    static void Main()
    {
        string[] inputLines = File.ReadAllLines("INPUT.TXT");

        string[] wh = inputLines[0].Split();
        int w = int.Parse(wh[0]);
        int h = int.Parse(wh[1]);

        int n = int.Parse(inputLines[1]);

        int[,] rectangles = new int[n, 4];

        for (int i = 0; i < n; i++)
        {
            string[] rectangleCoords = inputLines[i + 2].Split();
            for (int j = 0; j < 4; j++)
            {
                rectangles[i, j] = int.Parse(rectangleCoords[j]);
            }
        }

        int result = CalculateUnpaintedArea(w, h, rectangles);

        File.WriteAllText("OUTPUT.TXT", result.ToString());
    }

    static int CalculateUnpaintedArea(int w, int h, int[,] rectangles)
    {
        bool[,] paintedArea = new bool[w, h];

        for (int i = 0; i < rectangles.GetLength(0); i++)
        {
            for (int x = rectangles[i, 0]; x < rectangles[i, 2]; x++)
            {
                for (int y = rectangles[i, 1]; y < rectangles[i, 3]; y++)
                {
                    paintedArea[x, y] = true;
                }
            }
        }

        int unpaintedArea = 0;
        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                if (!paintedArea[x, y])
                {
                    unpaintedArea++;
                }
            }
        }

        return unpaintedArea;
    }
}
