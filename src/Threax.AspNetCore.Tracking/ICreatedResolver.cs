using System;
using AutoMapper;

namespace Threax.AspNetCore.Tracking
{
    /// <summary>
    /// Resolve created values.
    /// </summary>
    public interface ICreatedResolver : IValueResolver<Object, ICreated, DateTime>
    {
        
    }
}