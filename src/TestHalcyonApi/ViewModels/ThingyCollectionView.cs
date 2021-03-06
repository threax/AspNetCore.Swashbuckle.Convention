﻿using Halcyon.HAL.Attributes;
using HateoasTest.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestHalcyonApi.Controllers;
using Threax.AspNetCore.Halcyon.Ext;

namespace TestHalcyonApi.ViewModels
{
    [HalModel]
    [HalSelfActionLink(ThingyController.Rels.List, typeof(ThingyController))]
    [HalActionLink(ThingyController.Rels.List, typeof(ThingyController))]
    [HalActionLink(ThingyController.Rels.Add, typeof(ThingyController))]
    [DeclareHalLink(typeof(ThingyController), nameof(ThingyController.List), PagedCollectionView<Object>.Rels.Next, ResponseOnly = true)]
    [DeclareHalLink(typeof(ThingyController), nameof(ThingyController.List), PagedCollectionView<Object>.Rels.Previous, ResponseOnly = true)]
    [DeclareHalLink(typeof(ThingyController), nameof(ThingyController.List), PagedCollectionView<Object>.Rels.First, ResponseOnly = true)]
    [DeclareHalLink(typeof(ThingyController), nameof(ThingyController.List), PagedCollectionView<Object>.Rels.Last, ResponseOnly = true)]
    public class ThingyCollectionView : PagedCollectionView<ThingyView>
    {
        public ThingyCollectionView(PagedCollectionQuery query, int total, IEnumerable<ThingyView> items)
            :base(query, total, items)
        {
            
        }

        private ThingyCollectionView()
            :base(new PagedCollectionQuery(), 0, null)
        {
            //For codegen
        }
    }
}
