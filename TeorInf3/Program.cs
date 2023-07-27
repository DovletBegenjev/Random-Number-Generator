using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeorInf3
{
    class Program
    {
        static double XExp(int k, int N, int[] n, double[] interval) // функция подсчёта "хи-квадрат" экспериментальное
        {
            double xExp = 0; // "хи-квадрат" экспериментальное

            for (int i = 0; i < k; ++i)
            {
                xExp += ((Convert.ToDouble(n[i]) * Convert.ToDouble(n[i])) / interval[i]);
            }
            xExp /=  Convert.ToDouble(N);
            xExp -= Convert.ToDouble(N);

            return xExp;
        }

        static void PercentageDotCounter(int allNum, double xExp)
        {
            double[] percentDot = new double[7]; // процентные точки для allNum количества экспериментов
            double[] xp = new double[7] { -2.33, -1.64, -0.674, 0.0, 0.674, 1.64, 2.33 };
            int[] percentage = new int[7] { 1, 5, 25, 50, 75, 95, 99 }; // процент теоретической вероятности попадания чисел в k-й интервал
            for (int i = 0; i < 7; ++i)
            {
                percentDot[i] = allNum + Math.Sqrt(2 * allNum) * xp[i] + 2 / 3 * xp[i] * xp[i] - 2 / 3 + (1 / Math.Sqrt(allNum));

                if (percentDot[i] > xExp)
                {
                    if (i > 0)
                    {
                        Console.Write("Значение лежит между " + percentage[i - 1] + "% = " + percentDot[i - 1] + " и ");
                        Console.Write(percentage[i] + "% = " + percentDot[i]);
                        break;
                    }
                    else
                    {
                        Console.Write("Значение лежит за " + percentage[0] + "% = " + percentDot[0]);
                        break;
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            Console.Write("k = ");
            int k = Convert.ToInt32(Console.ReadLine()); // количество равных интервалов, на которые разбивается диапозон от 0 до 1

            Console.Write("N = ");
            int N = Convert.ToInt32(Console.ReadLine()); // количество запусков ГСЧ

            double[] arr = new double[N]; // массив случайных чисел
            Random random = new Random();

            // Первый пример
            double[] interval = new double[k]; // теоретическая вероятность попадания чисел в k-й интервал
            int[] n = new int[k]; // массив, показывающий количество чисел в каждом интервале
            for (int i = 0; i < k; ++i) interval[i] = 1 / Convert.ToDouble(k);

            // Второй пример
            double[] numFreq = new double[10]; // массив, показывающий частоту появления каждой цифры в выпавшей экспериментальной последовательности
            int[] numArray = new int[10]; // массив, показывающий частоту появления цифр (от 0 до 9) в случайных числах
            int allNum = 0; // общее количество цифр
            int temp = 0;
            double[] interval2 = new double[10]; // теоретическая вероятность попадания чисел в k-й интервал

            // Третий пример
            int[] seriesArray = new int[10]; // массив длин серий чисел
            List<int> allNumbersArray = new List<int>(); // массив со всеми числами
            int currentNumber = 0, currentNumberCount = 0;
            double[] seriesProbArray = new double[10]; // массив вероятностей появления серии чисел

            for (int i = 0; i < 10; ++i)
            {
                interval2[i] = 1.0 / 10.0;
                seriesProbArray[i] = 9 * Math.Pow(10, -(i + 1));
            }

            for (int i = 0; i < N; ++i)
            {
                arr[i] = random.NextDouble();

                for (int j = 0; j < k; ++j)
                {
                    if (arr[i] >= j * interval[j] && arr[i] <= j * interval[j] + interval[j])
                    {
                        ++n[j];
                    }
                }

                while (arr[i] != 0)
                {
                    arr[i] *= 10;
                    temp = Convert.ToInt32(Math.Floor(arr[i]));
                    arr[i] -= temp;

                    ++numArray[temp];
                    ++allNum;

                    allNumbersArray.Add(temp);
                }
            }

            currentNumber = allNumbersArray[0];
            currentNumberCount = 1;
            for (int i = 1; i < allNumbersArray.Count; ++i)
            {
                if (currentNumber == allNumbersArray[i])
                {
                    ++currentNumberCount; 
                }
                else
                {
                    ++seriesArray[currentNumberCount];
                    currentNumber = allNumbersArray[i];
                    currentNumberCount = 0;
                }
            }

            Console.WriteLine();

            Console.WriteLine("Пример 1. Выполнение проверки по критерию «хи-квадрат»");
            double xExp = XExp(k, N, n, interval); // "хи-квадрат" экспериментальное 1 пример
            Console.WriteLine("xExp = " + xExp);
            PercentageDotCounter(k, xExp);

            Console.WriteLine(); Console.WriteLine();

            Console.WriteLine("Пример 2. Выполнение проверки на частоту появления цифры в последовательности");
            double num_xExp = XExp(10, allNum, numArray, interval2); // "хи-квадрат" экспериментальное 2 пример
            Console.WriteLine("N = " + allNum + ", num_xExp = " + num_xExp);
            PercentageDotCounter(k, num_xExp);

            Console.WriteLine(); Console.WriteLine();

            Console.WriteLine("Пример 3. Выполнение проверки появления серий из одинаковых цифр");
            double series_xExp = XExp(10, allNum, seriesArray, seriesProbArray); // "хи-квадрат" экспериментальное 3 пример
            Console.WriteLine("N = " + allNum + ", series_xExp = " + series_xExp);
            PercentageDotCounter(k, series_xExp);

            Console.Read();
        }
    }
}
