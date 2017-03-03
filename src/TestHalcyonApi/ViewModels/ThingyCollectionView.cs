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
    [HalSelfActionLink(ThingyController.Rels.List, typeof(ThingyController))]
    [HalActionLink(ThingyController.Rels.List, typeof(ThingyController))]
    [HalActionLink(ThingyController.Rels.Add, typeof(ThingyController))]
    [DeclareHalLink(CollectionView<Object>.Rels.Next, ThingyController.Rels.List, typeof(ThingyController), ResponseOnly = true)]
    [DeclareHalLink(CollectionView<Object>.Rels.Previous, ThingyController.Rels.List, typeof(ThingyController), ResponseOnly = true)]
    [DeclareHalLink(CollectionView<Object>.Rels.First, ThingyController.Rels.List, typeof(ThingyController), ResponseOnly = true)]
    [DeclareHalLink(CollectionView<Object>.Rels.Last, ThingyController.Rels.List, typeof(ThingyController), ResponseOnly = true)]
    public class ThingyCollectionView : CollectionView<ThingyView>
    {
        public ThingyCollectionView(int offset, int limit, int total, IEnumerable<ThingyView> items)
        {
            this.Offset = offset;
            this.Limit = limit;
            this.Total = total;
            this.Items = items;
        }

        private ThingyCollectionView()
        {
            //For codegen
        }
    }
}
