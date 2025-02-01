using System.Buffers;
using System.Numerics;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<AddTests>();

int Find()
{
    var a = SearchValues.Create(new[] { "David", "Kevin" }, StringComparison.Ordinal);
    var text = "Lots of text";
    var firstIndex = text.AsSpan().IndexOfAny(a);
    return firstIndex;
}


public class AddTests
{
    [Benchmark]
    public int Add()
    {
        return 1 + 2 + 3 + 4 + 5 + 6 + 7 + DateTime.Now.Year;
    }

    
    [Benchmark]
    public int Vector2()
    {
        var v1 = new Vector<int>([1, 2, 3, 4]);
        var v2 = new Vector<int>([5, 6, 7, DateTime.Now.Year]);
        var v3 = v1 + v2;
        return v3[0] + v3[1] + v3[2] + v3[3];
    }
}

