using System;
using Xunit;

namespace Threax.NetCore.FileCopier.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var copier = new Copier(typeof(UnitTest1));
        }
    }
}
