using System;
using BenchmarkDotNet.Attributes;

namespace badanie_efektywnosc_algorytmow
{
    [MemoryDiagnoser]
    [SimpleJob(warmupCount: 1, iterationCount: 1)]
    public class SortingAlgorithms
    {
        private int[] originalArray = Array.Empty<int>();

        [Params(10, 100, 1000)]
        public int Size;

        [Params("random", "sorted", "reversed", "almost sorted", "few unique")]
        public string DataType;

        [GlobalSetup]
        public void Setup()
        {
            originalArray = DataType switch
            {
                "random" => Generators.GenerateRandom(Size, 1, Size * 10 + 1),
                "sorted" => Generators.GenerateSorted(Size, 1, Size * 10 + 1),
                "reversed" => Generators.GenerateReversed(Size, 1, Size * 10 + 1),
                "almost sorted" => Generators.GenerateAlmostSorted(Size, 1, Size * 10 + 1, 0.05),
                "few unique" => Generators.GenerateFewUnique(Size, 1, 11, 10),
                _ => throw new ArgumentException("Nieznany typ danych.")
            };
        }

        [Benchmark]
        public void InsertionSortBenchmark()
        {
            int[] arr = (int[])originalArray.Clone();
            InsertionSort(arr);
        }

        [Benchmark]
        public void MergeSortBenchmark()
        {
            int[] arr = (int[])originalArray.Clone();
            MergeSort(arr, 0, arr.Length - 1);
        }

        [Benchmark]
        public void QuickSortClassicalBenchmark()
        {
            int[] arr = (int[])originalArray.Clone();
            QuickSortClassical(arr, 0, arr.Length - 1);
        }

        [Benchmark(Baseline = true)]
        public void ArraySortBenchmark()
        {
            int[] arr = (int[])originalArray.Clone();
            Array.Sort(arr);
        }

        private static void InsertionSort(int[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                int key = arr[i];
                int j = i - 1;

                while (j >= 0 && arr[j] > key)
                {
                    arr[j + 1] = arr[j];
                    j--;
                }

                arr[j + 1] = key;
            }
        }

        private static void MergeSort(int[] arr, int left, int right)
        {
            if (left >= right)
                return;

            int mid = left + (right - left) / 2;

            MergeSort(arr, left, mid);
            MergeSort(arr, mid + 1, right);
            Merge(arr, left, mid, right);
        }

        private static void Merge(int[] arr, int left, int mid, int right)
        {
            int n1 = mid - left + 1;
            int n2 = right - mid;

            int[] leftArray = new int[n1];
            int[] rightArray = new int[n2];

            Array.Copy(arr, left, leftArray, 0, n1);
            Array.Copy(arr, mid + 1, rightArray, 0, n2);

            int i = 0, j = 0, k = left;

            while (i < n1 && j < n2)
            {
                if (leftArray[i] <= rightArray[j])
                {
                    arr[k] = leftArray[i];
                    i++;
                }
                else
                {
                    arr[k] = rightArray[j];
                    j++;
                }
                k++;
            }

            while (i < n1)
            {
                arr[k] = leftArray[i];
                i++;
                k++;
            }

            while (j < n2)
            {
                arr[k] = rightArray[j];
                j++;
                k++;
            }
        }

        private static void QuickSortClassical(int[] arr, int low, int high)
        {
            if (low < high)
            {
                int pivotIndex = Partition(arr, low, high);
                QuickSortClassical(arr, low, pivotIndex - 1);
                QuickSortClassical(arr, pivotIndex + 1, high);
            }
        }

        private static int Partition(int[] arr, int low, int high)
        {
            int pivot = arr[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (arr[j] < pivot)
                {
                    i++;
                    (arr[i], arr[j]) = (arr[j], arr[i]);
                }
            }

            (arr[i + 1], arr[high]) = (arr[high], arr[i + 1]);
            return i + 1;
        }
    }
}