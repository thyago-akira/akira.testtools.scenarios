using System;

namespace Akira.TestTools.Scenarios.Benchmark.Stubs
{
    public class Customer
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public string Email { get; set; }

        public decimal CreditLimit { get; set; }
    }
}