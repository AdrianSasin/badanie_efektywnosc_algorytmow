using BenchmarkDotNet.Running;

namespace badanie_efektywnosc_algorytmow
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<SortingAlgorithms>();
        }
    }
}