﻿using NJsonSchema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using Threax.AspNetCore.Models;
using Threax.AspNetCore.Tests;
using Threax.ModelGen.TestGenerators;
using Xunit;

namespace Threax.ModelGen.Tests.Models
{
    public class RequiredTests : ModelTests<RequiredTests.Value>
    {
        public class Value
        {
            [Required]
            public String Info { get; set; }
        }
    }
}
