using System;
using System.IO;

class Program
{
    static void Main()
    {
        string[] inputLines = File.ReadAllLines("INPUT.TXT");

        int N = int.Parse(inputLines[0]);

        double[,] coefficients = new double[N, N];
        double[] constants = new double[N];

        for (int i = 0; i < N; i++)
        {
            string[] tokens = inputLines[i + 1].Split();
            for (int j = 0; j < N; j++)
            {
                coefficients[i, j] = double.Parse(tokens[j]);
            }
            constants[i] = double.Parse(tokens[N]);
        }

        double[] solution = SolveSystem(coefficients, constants);

        int[] roundedSolution = new int[N];
        for (int i = 0; i < N; i++)
        {
            roundedSolution[i] = (int)Math.Round(solution[i]);
        }

        File.WriteAllText("OUTPUT.TXT", string.Join(" ", roundedSolution));
    }

    static double[] SolveSystem(double[,] coefficients, double[] constants)
    {
        int N = constants.Length;
        double[] solution = new double[N];

        for (int k = 0; k < N; k++)
        {
            int mainElementIndex = k;
            for (int i = k + 1; i < N; i++)
            {
                if (Math.Abs(coefficients[i, k]) > Math.Abs(coefficients[mainElementIndex, k]))
                {
                    mainElementIndex = i;
                }
            }

            for (int j = 0; j < N; j++)
            {
                double temp = coefficients[k, j];
                coefficients[k, j] = coefficients[mainElementIndex, j];
                coefficients[mainElementIndex, j] = temp;
            }

            double tempConstant = constants[k];
            constants[k] = constants[mainElementIndex];
            constants[mainElementIndex] = tempConstant;

            for (int i = k + 1; i < N; i++)
            {
                double factor = coefficients[i, k] / coefficients[k, k];
                constants[i] -= factor * constants[k];
                for (int j = k; j < N; j++)
                {
                    coefficients[i, j] -= factor * coefficients[k, j];
                }
            }
        }

        for (int i = N - 1; i >= 0; i--)
        {
            solution[i] = constants[i] / coefficients[i, i];
            for (int j = i - 1; j >= 0; j--)
            {
                constants[j] -= coefficients[j, i] * solution[i];
            }
        }

        return solution;
    }
}
