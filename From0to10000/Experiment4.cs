using System.Collections.Concurrent;
using Benchly;
using BenchmarkDotNet.Attributes;

namespace Vectorisation;

[ColumnChart(Title = "Experiment4", Colors = "skyblue,slateblue")]
[ReturnValueValidator(failOnError: true)]
public class Experiment4
{
    private readonly int[] _data;

    public Experiment4()
    {
        const int length = 10_000;
        _data = new int[length];
        for (var i = 0; i < length; i++)
            _data[i] = i + 1;
    }

    [Params(1, 2, 10, 20, 50)]
    public int NumberOfBlocks { get; set; }
    
    
    [Benchmark(Baseline = true)]
    public int Basic_ForLoop()
    {
        var total = 0;
        for (var i = 0; i < _data.Length; i++)
            total += _data[i];
        return total;
    }
    
     [Benchmark]
     public int Parallel_ForLoop()
     {
         var blockSize = _data.Length / NumberOfBlocks;
         var bag = new ConcurrentBag<int>();
         
         Parallel.For(0, NumberOfBlocks, i =>
         {
             var localTotal = 0;
             for (var j = (i*blockSize); j < ((i+1)*blockSize); j++)
             {
                 localTotal += _data[j];
             }
             bag.Add(localTotal);
             
         });
         return bag.Sum();
     }

}