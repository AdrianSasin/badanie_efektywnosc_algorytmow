using System;

namespace badanie_efektywnosc_algorytmow
{
    public static class Generators
    {
        public static int[] GenerateRandom(int size, int minVal, int maxVal)
        {
            int[] a = new int[size];
            for (int i = 0; i < size; i++)
            {
                a[i] = Random.Shared.Next(minVal, maxVal);
            }
            return a;
        }

        public static int[] GenerateSorted(int size, int minVal, int maxVal)
        {
            int[] a = GenerateRandom(size, minVal, maxVal);
            Array.Sort(a);
            return a;
        }

        public static int[] GenerateReversed(int size, int minVal, int maxVal)
        {
            int[] a = GenerateSorted(size, minVal, maxVal);
            Array.Reverse(a);
            return a;
        }

        public static int[] GenerateAlmostSorted(int size, int minVal, int maxVal, double disorderPercent = 0.05)
        {
            int[] a = GenerateSorted(size, minVal, maxVal);

            int swaps = (int)(size * disorderPercent);

            if (swaps < 1)
                swaps = 1;

            for (int i = 0; i < swaps; i++)
            {
                int index1 = Random.Shared.Next(0, size);
                int index2 = Random.Shared.Next(0, size);

                (a[index1], a[index2]) = (a[index2], a[index1]);
            }

            return a;
        }

        public static int[] GenerateFewUnique(int size, int minVal, int maxVal, int uniqueCount = 10)
        {
            if (uniqueCount < 1)
                throw new ArgumentException("uniqueCount musi być >= 1");

            if (maxVal - minVal < uniqueCount)
                throw new ArgumentException("Zakres wartości jest za mały względem uniqueCount");

            int[] uniqueValues = new int[uniqueCount];
            for (int i = 0; i < uniqueCount; i++)
            {
                uniqueValues[i] = minVal + i;
            }

            int[] a = new int[size];
            for (int i = 0; i < size; i++)
            {
                int randomIndex = Random.Shared.Next(0, uniqueCount);
                a[i] = uniqueValues[randomIndex];
            }

            return a;
        }
    }
}