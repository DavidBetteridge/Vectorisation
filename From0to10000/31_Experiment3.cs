using Benchly;
using BenchmarkDotNet.Attributes;

namespace From0to10000;

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

}