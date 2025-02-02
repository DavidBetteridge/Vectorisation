## 00 - Show intro
I got very side-tracked when preparing this talk and went on a bit of a journey which I will now share with you.

## 01 - Problem
If you google "for loop vs foreach performance javascript" then you get lots of results saying a for loop is much quicker.
Is the same true for c-sharp.
Let's test it

## 10 - Experiment 1
As loops run really fast,  we have to be careful when timing them.   The best way is to use BenchmarkDotNet.
This runs the code multiple times to eliminate noise from other things running on my laptop

ColumnChart will export a chart
ReturnValueValidator tests that all the experiments return the same answer.
Benchmark(Baseline = true) annotates that it's a test.  They are compared against the Baseline 

D1
We also have to give the loops some work to do.
So I create an array with 10000 numbers,

D2

D3

Must be ran in release mode.

Which do you think is fastest?  

## 11 - Experiment 1 Graph

It takes a while to run,  so here is a graph I export earlier.

ForEach is quicker.  Let's try and work out why.




## 12 - Experiment 1 Stages
When a dotnet application runs it's first lowered, then converted into IL code,  and then to assembly language.

Let's look at the IL code first: Tools -> View IL (IL)

Compare text files.  There are some differences,  but it's not clear what is going on.

Plan B - lets look at the lowered c# code instead.  Sometimes compilers lower source code into a simpler structure before compiling it.

These are very similar, other than it creates a local pointer to the array.  (This is a clue!)  

Next job is to look at the actual assembly language.   There are various ways to do this (on a PC) but it's a bit of a pain on a Mac.  But I've found an online site

https://godbolt.org/noscript/csharp
https://sharplab.io/

View Assembly - what is CORINFO_HELP_RNGCHKFAIL?
https://devblogs.microsoft.com/dotnet/performance_improvements_in_net_7/
Quote
While these bounds checks in and of themselves aren’t super expensive, do a lot of them and their costs add up. So while the JIT needs to ensure that “safe” accesses don’t go out of bounds, it also tries to prove that certain accesses won’t, in which case it needn’t emit the bounds check that it knows will be superfluous. In every release of .NET, more and more cases have been added to find places these bounds checks can be eliminated, and .NET 7 is no exception.


## 21 - Experiment 2

Can we also remove the bounds checks.   

D21
Uint means the number cannot be negative

D22
Using a local pointer to _data means we know in advance the size of the array.  It can't be changed by another thread.

## 22 - Experiment 2 - Graph

The UINT doesn't help,  but the LocalArray does.


## 31 - Experiment 3

But can we do better? What making use of all our cores by using Parallel.X.  Let's do the simplest possible thing.

D31

D32

## 32 - Experiment 3 - Graph

This turns out to be a terrible idea.

The interlock means all the threads get blocked,  and so it's serial again - but with more overhead.


## 41 - Experiment 4

What we really need is for each thread to carry out a chuck of the work.
D41


But how big should these blocks be

D42
We can use the Params feature in Benchmark.net to try out a range of sizes

Explain the parallel code.  No interlock,  but we put all the intermediate results into a bag,  and then sum that.

## 42 - Experiment 4 - Graph

No good - it turns out even the best case is slower

However - we have to remember that the work carried out by our loops is very simple.  A real world example will most likely be faster.



## 51 - Experiment 5
What else can we try?

D51
D52
D53
D54

Which do you think will be quickest?


## 52 - Experiment 5 - Graph
Unrolled is very slow - lets zoom in

## 53 - Experiment 5 - Graph
linq but why?

Drill into the sum source code

The vector class uses SIMD assembly instructions which works on multiple pieces of data at once.

## 54 - Experiment 5 - Usage

Best explained with an example

You can't use 8,  as the compiler will replace the entire method with a function.

## 61 - Experiment 6

What else can you use vectors for.  Well quite a bit!

D61
D62
D63

## 62 - Experiment 6 - Results

## 71 - Conclusion