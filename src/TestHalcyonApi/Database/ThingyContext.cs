using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestHalcyonApi.Models;

namespace TestHalcyonApi.Database
{
    public class ThingyContext
    {
        public IEnumerable<Thingy> TestData { get; private set; } = new Thingy[] { new Thingy { ThingyId = 0, Name = "First Thingy" }, new Thingy { ThingyId = 1, Name = "Second Thingy" } };
        public IEnumerable<SubThingy> TestSubData { get; private set; } = new SubThingy[] { new SubThingy { SubThingyId = 0, Cost = 5.00m, ThingyId = 1 }, new SubThingy { SubThingyId = 1, Cost = 100.00m, ThingyId = 0 } };
    }
}
