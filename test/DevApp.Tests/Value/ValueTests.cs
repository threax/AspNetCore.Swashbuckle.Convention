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
        private static Mockup SetupModel(this Mockup mockup)
        {
            mockup.Add<IValueRepository>(m => new ValueRepository(m.Get<AppDbContext>(), m.Get<IMapper>()));

            return mockup;
        }

        public static ValueInput CreateInput(String seed = "Don't Care")
        {
            return new ValueInput()
            {
                Name = seed
            };
        }

        public static ValueEntity CreateEntity(String seed = "Don't Care")
        {
            return new ValueEntity()
            {
                ValueId = Guid.NewGuid(),
                Name = seed
            };
        }
    }
}
