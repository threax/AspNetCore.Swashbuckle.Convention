using AutoMapper;
using DevApp.Database;
using DevApp.ViewModels;
using System;
using Threax.AspNetCore.Tests;
using Xunit;

namespace DevApp.Tests
{
    public static partial class ValueTests
    {
        public class Profile : IDisposable
        {
            private Mockup mockup = new Mockup().SetupGlobal().SetupModel();

            public Profile()
            {

            }

            public void Dispose()
            {
                mockup.Dispose();
            }

            [Fact]
            void InputToEntity()
            {
                var mapper = mockup.Get<IMapper>();
                var input = ValueTests.CreateInput();
                var entity = mapper.Map<ValueEntity>(input);

                //Make sure the id does not copy over
                Assert.Equal(Guid.Empty, entity.ValueId);
                AssertEqual(input, entity);
            }

            [Fact]
            void EntityToView()
            {
                var mapper = mockup.Get<IMapper>();
                var entity = ValueTests.CreateEntity();
                var view = mapper.Map<Value>(entity);

                Assert.Equal(entity.ValueId, view.ValueId);
                AssertEqual(entity, view);
            }
        }
    }
}