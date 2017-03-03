using Halcyon.HAL.Attributes;
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
    [HalSelfActionLink(SubThingyController.Rels.List, typeof(SubThingyController))]
    [HalActionLink(SubThingyController.Rels.List, typeof(SubThingyController))]
    [DeclareHalLink(CollectionView<Object>.Rels.Next, SubThingyController.Rels.List, typeof(SubThingyController), ResponseOnly = true)]
    [DeclareHalLink(CollectionView<Object>.Rels.Previous, SubThingyController.Rels.List, typeof(SubThingyController), ResponseOnly = true)]
    [DeclareHalLink(CollectionView<Object>.Rels.First, SubThingyController.Rels.List, typeof(SubThingyController), ResponseOnly = true)]
    [DeclareHalLink(CollectionView<Object>.Rels.Last, SubThingyController.Rels.List, typeof(SubThingyController), ResponseOnly = true)]
    public class SubThingyCollectionView : CollectionView<SubThingyView>
    {
        public SubThingyCollectionView(IEnumerable<SubThingyView> items)
        {
            this.Items = items;
        }
    }
}
