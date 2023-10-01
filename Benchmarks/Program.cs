using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

[MemoryDiagnoser]
public class MedianBenchmark
{

    [Params(100000)]
    public int DataSize = 100;
    private List<int>? data; 

    [GlobalSetup]
    public void GlobalSetup()
    {
        data = new List<int>();

        for (int i = 0; i < DataSize; i++)
        {
            data.Add(Random.Shared.Next(0, 100));
        }
    }

    [Benchmark]
    public void ListMedian()
    {
        var median = new MovingMedian.Logic.ListBasedMedian();

        foreach(var num in data!)
        {
            median.Add(num);
        }

        var medianValue = median.Value;
        Console.WriteLine(medianValue.ToString());
    }

    [Benchmark]
    public void PriorityQueueMedian()
    {
        var median = new MovingMedian.Logic.PriorityQueueBasedMedian();

        foreach (var num in data!)
        {
            median.Add(num);
        }

        var medianValue = median.Value;

        Console.WriteLine(medianValue.ToString());
    }

    [Benchmark]
    public void DictionaryMedian()
    {
        var median = new MovingMedian.Logic.DictionaryMedian();

        foreach (var num in data!)
        {
            median.Add(num);
        }

        var medianValue = median.Value;
        Console.WriteLine(medianValue.ToString());
    }
}

class Program
{
    static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<MedianBenchmark>();
    }
}
