using System;
using System.Collections.Generic;
using System.Text;

namespace Threax.AspNetCore.Tracking
{
    /// <summary>
    /// Tracks a field for created.
    /// </summary>
    public interface ICreated
    {
        DateTime Created { get; set; }
    }

    /// <summary>
    /// Tracks a field for modified.
    /// </summary>
    public interface IModified
    {
        DateTime Modified { get; set; }
    }

    /// <summary>
    /// Tracks both created and modified.
    /// </summary>
    public interface ICreatedModified : ICreated, IModified
    {

    }
}
