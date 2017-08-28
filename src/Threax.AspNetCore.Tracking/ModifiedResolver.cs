using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Threax.AspNetCore.Tracking
{
    public class ModifiedResolver : IModifiedResolver
    {
        private ITimestamper timestamper;

        public ModifiedResolver(ITimestamper timestamper)
        {
            this.timestamper = timestamper;
        }

        public DateTime Resolve(object source, IModified destination, DateTime destMember, ResolutionContext context)
        {
            timestamper.SetModified(destination);
            return destination.Modified;
        }
    }
}
