using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

// Run all the tests in the class AddingTests annotated with [Benchmark]
BenchmarkRunner.Run<AddingTests>();

// Include memory usage
[MemoryDiagnoser(true)]

// All methods must return the sanem number
[ReturnValueValidator(failOnError: true)]
public class AddingTests
{
    private readonly int[] _data;

    public AddingTests()
    {
        const int length = 10000;
        _data = new int[length];
        for (var i = 0; i < length; i++)
            _data[i] = i + 1;

        // Total === (_data.Length + 1) * (_data.Length / 2);
    }

    [Benchmark(Baseline = true)]
    public int Basic_ForLoop()
    {
        var total = 0;
        for (var i = 0; i < _data.Length; i++)
            total += _data[i];
        return total;
    }

    //[Benchmark]
    // public int Basic_ForEach()
    // {
    //     var total = 0;
    //     foreach (var value in _data)
    //         total += value;
    //     return total;
    // }

    //No lower bound check?
    // [Benchmark]
    // public int Using_UInt()
    // {
    //     var total = 0;
    //     for (uint i = 0; i < _data.Length; i++)
    //         total += _data[i];
    //     return total;
    // }
    //
    // No bound checks?
    // [Benchmark]
    // public int Using_Unchcked()
    // {
    //     unchecked
    //     {
    //         var total = 0;
    //         for (uint i = 0; i < _data.Length; i++)
    //             total += _data[i];
    //         return total;          
    //     }
    // }

    // [Benchmark]
    // public int Parallel_ForLoop()
    // {
    //     var total = 0;
    //     Parallel.For(0, _data.Length, i =>
    //     {
    //         Interlocked.Add(ref total, _data[i]);
    //     });
    //     return total;
    // }
    //
    // [Benchmark]
    // public int Parallel_ForEach()
    // {
    //     var total = 0;
    //     Parallel.ForEach(_data, value =>
    //     {
    //         Interlocked.Add(ref total, value);
    //     });
    //     return total;
    // }


    // [Benchmark]
    // public int UnSafeAdd()
    // {
    //     var total = 0;
    //     ref var start = ref _data[0];
    //     for (var i = 0; i < _data.Length; i++)
    //     {
    //         total += Unsafe.Add(ref start, i); // Access array elements without bounds checks
    //     }
    //
    //     return total;
    // }
    //
    // [Benchmark]
    // public int Span_ForLoop()
    // {
    //     var s = _data.AsSpan();
    //     var total = 0;
    //     for (var i = 0; i < s.Length; i++)
    //         total += s[i];
    //     return total;
    // }
    
    // [Benchmark]
    // public int Pointers()
    // {
    //     unsafe
    //     {
    //         var total = 0;
    //
    //         fixed (int* ptr = _data) // Pin the array in memory
    //         {
    //             var current = ptr;
    //             for (var i = 0; i < _data.Length; i++)
    //             {
    //                 total += *current; // Dereference the pointer and add the value to sum
    //                 current++;       // Move the pointer to the next element
    //             }
    //         }
    //         return total;
    //     }
    // }
    //
    // [Benchmark]
    // public int Linq()
    // {
    //     return _data.Sum();
    // }
}