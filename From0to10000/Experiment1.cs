using Benchly;
using BenchmarkDotNet.Attributes;

namespace Vectorisation;

[ColumnChart(Title = "Experiment1", Colors = "skyblue,slateblue")]
[ReturnValueValidator(failOnError: true)]
public class Experiment1
{
    private readonly int[] _data;

    public Experiment1()
    {
        const int length = 10_000;
        _data = new int[length];
        for (var i = 0; i < length; i++)
            _data[i] = i + 1;       
    }
    
    [Benchmark(Baseline = true)]
    public int Basic_ForLoop()
    {
        var total = 0;
        for (var i = 0; i < _data.Length; i++)
            total += _data[i];
        return total;
    }
    
    [Benchmark(Baseline = false)]
    public int Basic_ForEach()
    {
        var total = 0;
        foreach (var value in _data)
            total += value;
        return total;
    }
}