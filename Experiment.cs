using System;
using System.Diagnostics;
using System.Windows.Forms.DataVisualization.Charting;

namespace CursesWork
{
    static class Experiment
    {
        // Метод для запуска сортировки
        public static void StartSort(MainForm form, int[] mas, int size, string fileName)
        {
            int[] introMass = new int[size];
            int[] quickSortMass = new int[size];

            Array.Copy(mas, introMass, size);
            Array.Copy(mas, quickSortMass, size);

            int eqCount = 0;
            int changeCount = 0;

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            Sort.Quicksort(quickSortMass, 0, size - 1, ref eqCount, ref changeCount);
            stopwatch.Stop();
            if (stopwatch.ElapsedMilliseconds == 0)
            {
                form.chart1.Series["QuickSort"].Points.AddXY(fileName, (Convert.ToDouble(stopwatch.ElapsedTicks) / 10000));
            }
            else
            {
                form.chart1.Series["QuickSort"].Points.AddXY(fileName, (stopwatch.ElapsedMilliseconds));
            }
            form.chart2.Series["QuickSort"].Points.AddXY(fileName, eqCount);
            form.chart3.Series["QuickSort"].Points.AddXY(fileName, changeCount);

            eqCount = 0;
            changeCount = 0;

            stopwatch.Reset();
            stopwatch.Start();
            Sort.IntroSort(introMass, ref eqCount, ref changeCount);
            stopwatch.Stop();
            if (stopwatch.ElapsedMilliseconds == 0)
            {
                form.chart1.Series["IntroSort"].Points.AddXY(fileName, (Convert.ToDouble(stopwatch.ElapsedTicks) / 10000));
            }
            else
            {
                form.chart1.Series["IntroSort"].Points.AddXY(fileName, (stopwatch.ElapsedMilliseconds));
            }
            form.chart2.Series["IntroSort"].Points.AddXY(fileName, eqCount);
            form.chart3.Series["IntroSort"].Points.AddXY(fileName, changeCount);
        }

        // Метод для чтения чисел из строки и заполнения массива
        public static void LineReader(MainForm form, int[] mas, string line, int k)
        {
            foreach (var s in line.Split(' '))
            {
                if (!string.IsNullOrEmpty(s))
                {
                    mas[k] = Convert.ToInt32(s);
                    k++;
                }
            }
        }
    }
}

