using Benchly;
using BenchmarkDotNet.Attributes;

namespace From0to10000;

[ColumnChart(Title = "Experiment1", Colors = "skyblue,slateblue")]
[ReturnValueValidator(failOnError: true)]
public class Experiment1
{
    
}