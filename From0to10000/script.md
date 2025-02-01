## Intro
If you google "for loop vs foreach performance javascript" then you get lots of results saying a for loop is much quicker.
Is the same true for c-sharp.
Lets test it

## Experiment 1
Show Experiment 1.
I'm using BenchmarkDotNet to time the experiment.  This runs the code multiple times to eliminate noise from other things running on my laptop

ColumnChart will export a chart
ReturnValueValidator tests that all the experiments return the same answer.
Benchmark(Baseline = true) annotates that it's a test.  They are compared against the Baseline 

I create an array with 10000 numbers,
And then add them together

Must be ran in release mode.

Which do you think is fastest?  

It takes a while to run,  so here is a graph I export earlier.

ForEach is quicker.  Let's try and work out why.


## Investigation
When a dotnet application runs it's first converted into IL code,  and then to assembly language.

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


## Experiment 2

Can we also remove the bounds checks.   

Uint means the number cannot be negative

Using a local pointer to _data means we know in advance the size of the array.  It can't be changed by another thread.  TEST THIS!

The UINT doesn't help,  but the LocalArray does.


## Experiment 3

But can we do better? What making use of all our cores by using Parallel.X.  Let's do the simplest possible thing.

This turns out to be a terrible idea.

The interlock means all the threads get blocked,  and so it's serial again - but with more overhead.

What we really need is for each thread to carry out a chuck of the work.


## Experiment 4

But how big should these blocks be

We can use the Params feature in Benchmark.net to try out a range of sizes

Explain the parallel code.  No interlock,  but we put all the intermediate results into a bag,  and then sum that.

Show results.

No good - the more blocks the worse the result. 



## Experiment 5
What else can we try?

for with local copy
foreach
unsafe add
pointers
unroll loop
one-line linq

Which do you think will be quickest?

linq but why?

