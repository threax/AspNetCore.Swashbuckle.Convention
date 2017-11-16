using AutoMapper;
using DevApp.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using Threax.AspNetCore.Tests;

namespace DevApp.Tests
{
    public static class GlobalMocks
    {
        public static Mockup SetupGlobal(this Mockup mockup)
        {
            mockup.Add<IMapper>(m => AppDatabaseServiceExtensions.SetupMappings().CreateMapper(mockup.Get));

            mockup.Add<AppDbContext>(m => new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
                                                                        .UseInMemoryDatabase(Guid.NewGuid().ToString())
                                                                        .Options));

            mockup.Add<IIdentity>(m => new ClaimsIdentity(new Claim[]
            {
                new Claim(Threax.AspNetCore.AuthCore.ClaimTypes.ObjectGuid, Guid.NewGuid().ToString()),
            }));

            mockup.Add<ClaimsPrincipal>(m => new ClaimsPrincipal(m.Get<IIdentity>()));

            mockup.Add<HttpContext>(m => new DefaultHttpContext()
            {
                User = m.Get<ClaimsPrincipal>()
            });

            mockup.Add<ControllerContext>(m => new ControllerContext()
            {
                HttpContext = m.Get<HttpContext>()
            });

            return mockup;
        }
    }
}
