using System;
using AutoMapper;

namespace Threax.AspNetCore.Tracking
{
    /// <summary>
    /// Resolve modified values
    /// </summary>
    public interface IModifiedResolver : IValueResolver<Object, IModified, DateTime>
    {
        
    }
}