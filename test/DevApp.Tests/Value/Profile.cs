using AutoMapper;
using DevApp.Controllers.Api;
using DevApp.Database;
using DevApp.InputModels;
using DevApp.Repository;
using DevApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
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

                //Data specific assertions
                Assert.Equal(input.Name, entity.Name);
            }

            [Fact]
            void EntityToView()
            {
                var mapper = mockup.Get<IMapper>();
                var entity = ValueTests.CreateEntity();
                var view = mapper.Map<Value>(entity);

                Assert.Equal(entity.ValueId, view.ValueId);

                //Data specific assertions
                Assert.Equal(entity.Name, view.Name);
            }
        }
    }
}
