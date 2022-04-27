using EasyConsoleFramework.ExtensionMethods;
using System;
using Xunit;

namespace EasyConsoleFramework.Test
{
    public class ExtensionMethodTests
    {
        [Fact]
        public void TruncatedStringWithEllipsisIsCorrect()
        {
            string toBeTruncated = "This string is really really really really really really long.";
            int maxLenght = 15;
            Assert.Equal("This string is ", toBeTruncated.Truncate(maxLenght, ellipsis: false));
        }

        [Fact]
        public void TruncatedStringWitoutEllipsisIsCorrect()
        {
            string toBeTruncated = "This string is really really really really really really long.";
            int maxLenght = 15;
            Assert.Equal("This string ...", toBeTruncated.Truncate(maxLenght, ellipsis: true));
        }

        [Fact]
        public void RetrievesCorrectMaxLineLengthInString()
        {
            string multiLineString = "This\nis\na\nmultiline\nstring";
            Assert.Equal(9, multiLineString.GetMaxLineLength());
        }

        [Fact]
        public void StringsAreCorrectlyPaddedUntilLimit()
        {
            string testString = "string";
            Assert.Equal("   string   ", testString.PadUntilLimit(12));
            Assert.Equal("  string   ", testString.PadUntilLimit(11));
        }
    }
}