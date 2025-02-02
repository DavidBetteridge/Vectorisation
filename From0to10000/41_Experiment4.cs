using System.Collections.Concurrent;
using Benchly;
using BenchmarkDotNet.Attributes;

namespace From0to10000;

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

    
    // D41 D42
    
}