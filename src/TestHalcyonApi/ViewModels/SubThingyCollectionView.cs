﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Threax.AspNetCore.Halcyon.Ext;

namespace TestHalcyonApi.ViewModels
{
    public class SubThingyCollectionView : CollectionView<SubThingyView>
    {
        public SubThingyCollectionView(IEnumerable<SubThingyView> items)
        {
            this.Items = items;
        }
    }
}
