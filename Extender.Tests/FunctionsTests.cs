using System;
using Xunit;
using Extender;
using System.Linq;
using System.Collections.Generic;
namespace Extender.Tests
{
    public class FunctionsTests
    {
        [Fact]
        public void TestSplit()
        {
            IList<string> right = new List<string>(4) { "discord.exe", "-processStart", "DatabaseEngine", "admine" };
            var list = Functions.Split(new char[] { ';' }, "discord.exe;-processStart;DatabaseEngine;admine").ToList();
            Assert.NotEmpty(list);
            Assert.NotNull(list);
            Assert.Equal(right.Count, list.Count);
            for (int x = 0; x < right.Count; x++)
            {
                Assert.Equal(right[x], list[x]);
            }
        }
    }
}
