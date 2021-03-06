﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Threax.AspNetCore.Halcyon.Ext.ValueProviders
{
    public interface ILabelValuePair
    {
        String Label { get; set; }

        //Does not define the Value property, but subclasses should use typed version of interface, type is unknown at this point
    }

    public interface ILabelValuePair<T> : ILabelValuePair
    {
        T Value { get; set; }
    }
}
