using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        string inputPath = "INPUT.TXT";
        string outputPath = "OUTPUT.TXT";

        int n;
        int[,] sudoku;

        using (StreamReader reader = new StreamReader(inputPath))
        {
            n = int.Parse(reader.ReadLine());
            sudoku = new int[n * n, n * n];

            for (int i = 0; i < n * n; i++)
            {
                int[] row = reader.ReadLine().Split().Select(int.Parse).ToArray();
                for (int j = 0; j < n * n; j++)
                {
                    sudoku[i, j] = row[j];
                }
            }
        }

        string result = IsSudokuCorrect(sudoku, n) ? "Correct" : "Incorrect";

        using (StreamWriter writer = new StreamWriter(outputPath))
        {
            writer.WriteLine(result);
        }
    }

    static bool IsSudokuCorrect(int[,] sudoku, int n)
    {
        for (int i = 0; i < n * n; i++)
        {
            if (!IsSetCorrect(sudoku.GetRow(i)) || !IsSetCorrect(sudoku.GetColumn(i)))
                return false;
        }

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                int[] square = sudoku.GetSquare(i * n, j * n, n);
                if (!IsSetCorrect(square))
                    return false;
            }
        }

        return true;
    }

    static bool IsSetCorrect(int[] set)
    {
        return set.OrderBy(x => x).SequenceEqual(Enumerable.Range(1, set.Length));
    }
}

static class ArrayExtensions
{
    public static int[] GetRow(this int[,] array, int row)
    {
        int length = array.GetLength(1);
        int[] result = new int[length];
        for (int i = 0; i < length; i++)
        {
            result[i] = array[row, i];
        }
        return result;
    }

    public static int[] GetColumn(this int[,] array, int col)
    {
        int length = array.GetLength(0);
        int[] result = new int[length];
        for (int i = 0; i < length; i++)
        {
            result[i] = array[i, col];
        }
        return result;
    }

    public static int[] GetSquare(this int[,] array, int startRow, int startCol, int n)
    {
        int[] result = new int[n * n];
        int index = 0;
        for (int i = startRow; i < startRow + n; i++)
        {
            for (int j = startCol; j < startCol + n; j++)
            {
                result[index++] = array[i, j];
            }
        }
        return result;
    }
}
