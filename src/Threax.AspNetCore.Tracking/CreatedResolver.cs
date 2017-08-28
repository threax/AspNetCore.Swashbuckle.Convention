using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Threax.AspNetCore.Tracking
{
    public class CreatedResolver : ICreatedResolver
    {
        private ITimestamper timestamper;

        public CreatedResolver(ITimestamper timestamper)
        {
            this.timestamper = timestamper;
        }

        public DateTime Resolve(object source, ICreated destination, DateTime destMember, ResolutionContext context)
        {
            timestamper.SetCreated(destination);
            return destination.Created;
        }
    }
}
