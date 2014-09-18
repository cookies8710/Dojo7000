using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LeapYearKata.Tests
{
    public class LeapCalculatorTests
    {
        [Fact]
        public static void IsCommonYear()
        {
            Assert.False(LeapCalculator.IsLeap(2001));
        }
        [Fact]
        public static void IsTypicalLeapYear()
        {
            Assert.True(LeapCalculator.IsLeap(1996));
        }

        [Fact]
        public void IsAtypicalCommonYear()
        {
            Assert.False(LeapCalculator.IsLeap(1900));
        }

        [Fact]
        public void IsAtypicalLeapYear()
        {
            Assert.True(LeapCalculator.IsLeap(2000));
        }
    }
}
