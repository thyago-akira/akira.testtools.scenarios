using System;
using System.Collections.Generic;
using Akira.Contracts.TestTools.Scenarios;

namespace Akira.TestTools.Scenarios.Extensions
{
    public static class EnumExtensions
    {
        public static readonly HashSet<int> AllowedScenarioTypes = new HashSet<int>((int[])Enum.GetValues(typeof(ScenarioCombinationType)));

        public static readonly HashSet<int> AllowedScenarioBuilderTypes = new HashSet<int>((int[])Enum.GetValues(typeof(ScenarioBuilderType)));
    }
}