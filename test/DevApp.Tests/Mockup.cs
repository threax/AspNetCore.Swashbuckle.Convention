using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevApp.Tests
{
    public class Mockup : IDisposable
    {
        private Dictionary<Type, Func<Mockup, Object>> customCreateFuncs = new Dictionary<Type, Func<Mockup, object>>();
        private Dictionary<Type, Object> createdObjects = new Dictionary<Type, Object>();

        public void Dispose()
        {
            foreach(var o in createdObjects.Values.Select(i => i as IDisposable).Where(i => i != null))
            {
                o.Dispose();
            }
        }

        public T CreateMock<T>()
        {
            return (T)CreateMock(typeof(T));
        }

        public void AddCreateFunc<T>(Func<Mockup, T> cb)
        {
            customCreateFuncs.Add(typeof(T), m => cb(m));
        }

        public Object CreateMock(Type t)
        {
            Object created;
            if (!createdObjects.TryGetValue(t, out created))
            {
                Func<Mockup, Object> createFunc;
                if (customCreateFuncs.TryGetValue(t, out createFunc))
                {
                    created = createFunc(this);
                }
                else
                {
                    created = (Activator.CreateInstance(typeof(Mock<>).MakeGenericType(new Type[] { t })) as Mock).Object;
                }
                createdObjects.Add(t, created);
            }
            return created;
        }
    }
}
