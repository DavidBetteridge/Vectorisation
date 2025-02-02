using System.Numerics.Tensors;
using Benchly;
using BenchmarkDotNet.Attributes;

namespace From0to10000;

[ReturnValueValidator(failOnError: true)]
[ColumnChart(Title = "Experiment6", Colors = "skyblue,slateblue")]
public class Experiment6
{
    private readonly int[] _dataA;
    private readonly int[] _dataB;

    public Experiment6()
    {
        const int length = 10_000;
        _dataA = new int[length];
        for (var i = 0; i < length; i++)
            _dataA[i] = i + 1;   
        
        _dataB = new int[length];
        for (var i = 0; i < length; i++)
            _dataB[i] = i + 2;   
    }
    
    [Benchmark(Baseline = true)]
    public int Loop()
    {
        // Sum differences
        var differences = new int[_dataA.Length];
        for (var i = 0; i < _dataA.Length; i++)
            differences[i] = _dataB[i] - _dataA[i];
        return differences.Sum();
    }
    
    [Benchmark(Baseline = false)]
    public int Tensor()
    {
        var differences = new int[_dataA.Length];
        TensorPrimitives.Subtract(_dataB.AsSpan(), _dataA.AsSpan(), differences.AsSpan());
        return differences.Sum();
    }
   
}