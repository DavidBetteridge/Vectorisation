using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using Benchly;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using Vectorisation;


// var dataA = new int[100_000];
// Array.Fill(dataA, 1);
//
// var dataB = dataA;
// dataB[1] = 7;
// Array.Resize(ref dataA, 10);
// Array.Fill(dataA, 3);
//
// Console.WriteLine($"{dataA[0]} {dataB[0]}");
// Console.WriteLine($"{dataA[1]} {dataB[1]}");
//
// Console.ReadKey();

//
// Run all the tests in the class AddingTests annotated with [Benchmark]
BenchmarkRunner.Run<Experiment4>();
//
// [ColumnChart(Title = "Adding Numbers", Colors = "skyblue,slateblue")]
// [MemoryDiagnoser, SimpleJob(RuntimeMoniker.Net90)]
//
// // Include memory usage
// // [MemoryDiagnoser(true)]
//
// // All methods must return the sanem number
// [ReturnValueValidator(failOnError: true)]
// // [DisassemblyDiagnoser(printInstructionAddresses: true, syntax: DisassemblySyntax.Masm)]
// public class AddingTests
// {
//     private readonly int[] _data;
//
//     public AddingTests()
//     {
//         const int length = 10000;
//         _data = new int[length];
//         for (var i = 0; i < length; i++)
//             _data[i] = i + 1;       
//
//         // Total === (_data.Length + 1) * (_data.Length / 2);
//     }
//
//     [Benchmark(Baseline = true)]
//     public int Basic_ForLoop()
//     {
//         var total = 0;
//         for (var i = 0; i < _data.Length; i++)
//             total += _data[i];
//         return total;
//     }
//     
//     [Benchmark]
//     public int Local_ForLoop()
//     {
//         var data = _data;
//         var total = 0;
//         for (var i = 0; i < data.Length; i++)
//             total += data[i];
//         return total;
//     }
//     
//     // add      rax, 16
//     // mov      ecx, 0x2710
//     // align    [0 bytes for IG03]
//     // G_M27646_IG03:  ;; offset=0x0021
//     // add      rax, 4
//     // dec      ecx
//     // jne      SHORT G_M27646_IG03
//     //     pop      rbp
//     //     ret      
//
//     [Benchmark]
//     public int Basic_ForEach()
//     {
//         var total = 0;
//         foreach (var value in _data)
//             total += value;
//         return total;
//     }
//     
//     // No lower bound check?
//     [Benchmark]
//     public int Using_UInt()
//     {
//         var total = 0;
//         for (uint i = 0; i < _data.Length; i++)
//             total += _data[i];
//         return total;
//     }
//     
//
//     // [Benchmark]
//     // public int Parallel_ForLoop()
//     // {
//     //     var total = 0;
//     //     Parallel.For(0, _data.Length, i =>
//     //     {
//     //         Interlocked.Add(ref total, _data[i]);
//     //     });
//     //     return total;
//     // }
//     //
//     // [Benchmark]
//     // public int Parallel_ForEach()
//     // {
//     //     var total = 0;
//     //     Parallel.ForEach(_data, value =>
//     //     {
//     //         Interlocked.Add(ref total, value);
//     //     });
//     //     return total;
//     // }
//
//     // [Benchmark]
//     // public int Parallel_ForLoop_10()
//     // {
//     //     const int numberOfBlocks = 10;
//     //     var blockSize = _data.Length / numberOfBlocks;
//     //     var bag = new ConcurrentBag<int>();
//     //     
//     //     Parallel.For(0, numberOfBlocks, i =>
//     //     {
//     //         var localTotal = 0;
//     //         for (var j = (i*blockSize); j < ((i+1)*blockSize); j++)
//     //         {
//     //             localTotal += _data[j];
//     //         }
//     //         bag.Add(localTotal);
//     //         
//     //     });
//     //     return bag.Sum();
//     // }
//     
//     // [Benchmark]
//     // public int Parallel_ForLoop_2()
//     // {
//     //     const int numberOfBlocks = 2;
//     //     var blockSize = _data.Length / numberOfBlocks;
//     //     var bag = new ConcurrentBag<int>();
//     //     
//     //     Parallel.For(0, numberOfBlocks, i =>
//     //     {
//     //         var localTotal = 0;
//     //         for (var j = (i*blockSize); j < ((i+1)*blockSize); j++)
//     //         {
//     //             localTotal += _data[j];
//     //         }
//     //         bag.Add(localTotal);
//     //         
//     //     });
//     //     return bag.Sum();
//     // }
//     //
//     //
//     //
//     // [Benchmark]
//     // public int UnSafeAdd()
//     // {
//     //     var total = 0;
//     //     ref var start = ref _data[0];
//     //     for (var i = 0; i < _data.Length; i++)
//     //     {
//     //         total += Unsafe.Add(ref start, i); // Access array elements without bounds checks
//     //     }
//     //
//     //     return total;
//     // }
//     //
//     // [Benchmark]
//     // public int Span_ForLoop()
//     // {
//     //     var s = _data.AsSpan();
//     //     var total = 0;
//     //     for (var i = 0; i < s.Length; i++)
//     //         total += s[i];
//     //     return total;
//     // }
//     //
//     // [Benchmark]
//     // public int Pointers()
//     // {
//     //     unsafe
//     //     {
//     //         var total = 0;
//     //
//     //         fixed (int* ptr = _data) // Pin the array in memory
//     //         {
//     //             var current = ptr;
//     //             for (var i = 0; i < _data.Length; i++)
//     //             {
//     //                 total += *current; // Dereference the pointer and add the value to sum
//     //                 current++; // Move the pointer to the next element
//     //             }
//     //         }
//     //
//     //         return total;
//     //     }
//     // }
//
//     // [Benchmark]
//     // public int Linq()
//     // {
//     //     return _data.Sum();
//     // }
// }