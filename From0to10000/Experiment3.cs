using Benchly;
using BenchmarkDotNet.Attributes;

namespace Vectorisation;

[ColumnChart(Title = "Experiment3", Colors = "skyblue,slateblue")]
[ReturnValueValidator(failOnError: true)]
public class Experiment3
{
    private readonly int[] _data;

    public Experiment3()
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

    [Benchmark]
    public int Parallel_ForLoop()
    {
        var total = 0;
        var data = _data;
        Parallel.For(0, data.Length, i =>
        {
            Interlocked.Add(ref total, data[i]);
        });
        return total;
    }

    [Benchmark]
    public int Parallel_ForEach()
    {
        var total = 0;
        Parallel.ForEach(_data, value =>
        {
            Interlocked.Add(ref total, value);
        });
        return total;
    }
}