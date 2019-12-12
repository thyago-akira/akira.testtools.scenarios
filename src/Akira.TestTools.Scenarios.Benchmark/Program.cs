using System;
using Akira.TestTools.Scenarios.Benchmark.Configs;
using BenchmarkDotNet.Running;

namespace Akira.TestTools.Scenarios.Benchmark
{
    internal class Program
    {
        private static void Main(string[] _)
        {
            BenchmarkRunner.Run<BenchmarkConfig>();
            Console.Read();
        }
    }
}