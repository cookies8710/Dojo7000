using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StringCalculator.Tests
{
    public class StringCalculatorTests
    {
        [Fact]
        public void EmptyIsZero()
        {
            Assert.Equal(0,StringCalculator.Add(""));
        }

        [Fact]
        public void SingleNumberIsThatNumber()
        {
            Assert.Equal(1, StringCalculator.Add("1"));
        }

        [Fact]
        public void Add_TwoNumbersWithComma_ReturnSum()
        {
            Assert.Equal(3, StringCalculator.Add("1,2"));
        }

        [Fact]
        public void Add_MoreThanTwoNumbersWithCommas_ReturnSum()
        {
            Assert.Equal(6, StringCalculator.Add("1,2,3"));
            Assert.Equal(8, StringCalculator.Add("1,2,3,1,1"));
        }

        [Fact]
        public void Add_NumbersSeparatedByNewline_ReturnSum()
        {
            Assert.Equal(6, StringCalculator.Add("1\n2,3"));
        }

        [Fact]
        public void Add_OptionalDelimiterSupport_ReturnSum()
        {
            Assert.Equal(22, StringCalculator.Add("//delim\n5delim8delim9"));
        }

        [Fact]
        public void Add_AnyNegativeNumber_ThrowExcteption()
        {
            ArgumentException exception = Record.Exception(() => { StringCalculator.Add("//delim\n-5delim8delim-9"); }) as ArgumentException;
            Assert.NotNull(exception);
            Assert.Equal("-5,-9", exception.Message);
        }

        [Fact]
        public void Add_BigNumbers_ShouldBeSkipped()
        {
            Assert.Equal(13, StringCalculator.Add("//delim\n5delim8delim9000"));
        }

        [Fact]
        public void Add_MultipleDelimenters_ShouldWork()
        {
            Assert.Equal(14, StringCalculator.Add("//[delim][kkt]\n5delim8kkt9000kkt1"));
        }
    }
}
