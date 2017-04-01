using System;
using System.Collections.Generic;
using System.Text;

namespace Threax.NetCore.FileCopier
{
    class CopierOptions
    {
        public bool UseLastIndex { get; set; }

        public static CopierOptions Default { get; private set; } = new CopierOptions();
    }
}
