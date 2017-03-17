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
    [DeclareHalLink(PagedCollectionView<Object>.Rels.Next, ThingyController.Rels.List, typeof(ThingyController), ResponseOnly = true)]
    [DeclareHalLink(PagedCollectionView<Object>.Rels.Previous, ThingyController.Rels.List, typeof(ThingyController), ResponseOnly = true)]
    [DeclareHalLink(PagedCollectionView<Object>.Rels.First, ThingyController.Rels.List, typeof(ThingyController), ResponseOnly = true)]
    [DeclareHalLink(PagedCollectionView<Object>.Rels.Last, ThingyController.Rels.List, typeof(ThingyController), ResponseOnly = true)]
    public class ThingyCollectionView : PagedCollectionView<ThingyView>
    {
        public ThingyCollectionView(CollectionQuery query, int total, IEnumerable<ThingyView> items)
            :base(query, total, items)
        {
            
        }

        private ThingyCollectionView()
            :base(new SearchCollectionQuery(), 0, null)
        {
            //For codegen
        }
    }
}
