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

View Assembly - what is CORINFO_HELP_RNGCHKFAIL?