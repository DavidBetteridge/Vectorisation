using Benchly;
using BenchmarkDotNet.Attributes;

namespace From0to10000;

[ColumnChart(Title = "Experiment2", Colors = "skyblue,slateblue")]
[ReturnValueValidator(failOnError: true)]
public class Experiment2
{
    private readonly int[] _data;

    public Experiment2()
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
    
    // Does UINT mean no lower bound check?
    [Benchmark]
    public int Using_UInt()
    {
        var total = 0;
        for (uint i = 0; i < _data.Length; i++)
            total += _data[i];
        return total;
    }
    
    [Benchmark]
    public int LocalArray()
    {
        var data = _data;
        var total = 0;
        for (uint i = 0; i < data.Length; i++)
            total += data[i];
        return total;
    }
}