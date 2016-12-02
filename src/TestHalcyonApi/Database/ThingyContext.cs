using AutoMapper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestHalcyonApi.Models;

namespace TestHalcyonApi.Database
{
    public class FakeTable<TValue>
    {
        private ConcurrentDictionary<int, TValue> items = new ConcurrentDictionary<int, TValue>();
        private int currentKey = 0;
        private Action<int, TValue> setKeyAction;

        public FakeTable(Action<int, TValue> setKeyAction)
        {
            this.setKeyAction = setKeyAction;
        }

        public IEnumerable<TValue> Values
        {
            get
            {
                return items.Values;
            }
        }

        public void Add(TValue value)
        {
            setKeyAction(currentKey++, value);
            items.AddOrUpdate(currentKey, value, (key, existingValue) =>
            {
                return value;
            });
        }

        public void Remove(int key)
        {
            TValue value;
            items.TryRemove(key, out value);
        }

        public TValue Get(int key)
        {
            return items[key];
        }
    }

    /// <summary>
    /// A crappy context that keeps this demo from needing entity framework.
    /// </summary>
    public class ThingyContext
    {
        private FakeTable<Thingy> thingyTable = new FakeTable<Thingy>((key, value) => value.ThingyId = key);
        private FakeTable<SubThingy> subThingyTable = new FakeTable<SubThingy>((key, value) => value.SubThingyId = key);

        public ThingyContext()
        {
            var defaultThingies = new Thingy[] { new Thingy { Name = "First Thingy" }, new Thingy { Name = "Second Thingy" } };
            var defaultSubThingies = new SubThingy[] { new SubThingy { Amount = 5.00m, ThingyId = 1 }, new SubThingy { Amount = 100.00m, ThingyId = 0 } };

            foreach(var item in defaultThingies)
            {
                thingyTable.Add(item);
            }

            foreach (var item in defaultSubThingies)
            {
                subThingyTable.Add(item);
            }
        }

        public FakeTable<Thingy> Thingies
        {
            get
            {
                return thingyTable;
            }
        }

        public FakeTable<SubThingy> SubThingies
        {
            get
            {
                return subThingyTable;
            }
        }
    }
}
