using AutoMapper;
using DevApp.Database;
using DevApp.InputModels;
using DevApp.Repository;
using DevApp.Models;
using DevApp.ViewModels;
using System;
using Threax.AspNetCore.Tests;
using Xunit;
using System.Collections.Generic;

namespace DevApp.Tests
{
    public static partial class ValueTests
    {
        private static Mockup SetupModel(this Mockup mockup)
        {
            mockup.Add<IValueRepository>(m => new ValueRepository(m.Get<AppDbContext>(), m.Get<IMapper>()));

            return mockup;
        }

                public static ValueInput CreateInput(String seed = "", String Name = default(String))
        {
            return new ValueInput()
            {
                Name = Name != null ? Name : $"Name {seed}",
            };
        }


                public static ValueEntity CreateEntity(String seed = "", Guid? ValueId = null, String Name = default(String))
        {
            return new ValueEntity()
            {
                ValueId = ValueId.HasValue ? ValueId.Value : Guid.NewGuid(),
                Name = Name != null ? Name : $"Name {seed}",
            };
        }


                public static Value CreateView(String seed = "", Guid? ValueId = null, String Name = default(String))
        {
            return new Value()
            {
                ValueId = ValueId.HasValue ? ValueId.Value : Guid.NewGuid(),
                Name = Name != null ? Name : $"Name {seed}",
            };
        }


                public static void AssertEqual(IValue expected, IValue actual)
        {
           Assert.Equal(expected.Name, actual.Name);
        }

    }
}