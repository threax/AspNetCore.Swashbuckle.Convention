using AutoMapper;
using DevApp.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

            mockup.Add<IIdentity>(m => CreateIdentity(Roles.DatabaseRoles()));

            mockup.Add<ClaimsPrincipal>(m => new ClaimsPrincipal(m.Get<IIdentity>()));

            mockup.Add<HttpContext>(m => new DefaultHttpContext()
            {
                User = m.Get<ClaimsPrincipal>()
            });

            mockup.Add<IHttpContextAccessor>(m => new HttpContextAccessor()
            {
                HttpContext = m.Get<HttpContext>()
            });

            mockup.Add<ControllerContext>(m => new ControllerContext()
            {
                HttpContext = m.Get<HttpContext>()
            });

            return mockup;
        }

        /// <summary>
        /// Helper to create an identity. Can pass in roles to assign, will automatically setup the user
        /// guid and any other common properties.
        /// </summary>
        /// <param name="claims">The claims to add.</param>
        /// <returns>The identity.</returns>
        public static IIdentity CreateIdentity(IEnumerable<String> roles)
        {
            return new ClaimsIdentity(roles.Select(i => new Claim(ClaimTypes.Role, i)).Concat(new Claim[]
            {
                new Claim(Threax.AspNetCore.AuthCore.ClaimTypes.ObjectGuid, Guid.NewGuid().ToString()),
            }));
        }
    }
}
