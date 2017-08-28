using System;
using System.Collections.Generic;
using System.Text;

namespace Threax.AspNetCore.Tracking
{
    /// <summary>
    /// This class contains the functions to timestamp the interfaces.
    /// This will store times using utc.
    /// </summary>
    public class Timestamper : ITimestamper
    {
        /// <summary>
        /// Set the created time, only works if the DateTime is set to the min value.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public void SetCreated(ICreated i)
        {
            if (i.Created == DateTime.MinValue)
            {
                i.Created = DateTime.UtcNow;
            }
        }

        /// <summary>
        /// Set the modified time will always set modified to the current time.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public void SetModified(IModified i)
        {
            i.Modified = DateTime.UtcNow;
        }
    }
}
