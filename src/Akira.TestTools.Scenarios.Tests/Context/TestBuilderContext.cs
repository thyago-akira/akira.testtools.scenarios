﻿using System;
using Akira.Contracts.TestTools.Scenarios;
using Akira.TestTools.Scenarios.Tests.Stubs;

namespace Akira.TestTools.Scenarios.Tests.Context
{
    public class TestBuilderContext : BaseTestContext
    {
        public Func<IScenariosBuilder<SimpleModel>> GetFaker { get; set; }

        public int ExpectedCountValidOnly { get; set; }

        public int ExpectedCountInvalidOnly { get; set; }

        public int ExpectedCountAll { get; set; }
    }
}