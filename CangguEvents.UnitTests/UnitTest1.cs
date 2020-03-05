using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CangguEvents.UnitTests
{
    public class UnitTest1
    {
        class Foo
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        [Fact]
        public void FastCodeTry()
        {
            var list = new List<Foo> {new Foo() {X = 3, Y = 4}};

            var single = list.Single();
            single.X = 6;
            single.Y = 6;
        }
    }
}