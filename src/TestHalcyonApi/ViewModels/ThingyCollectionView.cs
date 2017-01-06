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
    [HalSelfLink]
    [HalActionLink(ThingyController.Rels.List, typeof(ThingyController))]
    [HalActionLink(ThingyController.Rels.Add, typeof(ThingyController))]
    public class ThingyCollectionView : CollectionView<ThingyView>
    {
        public ThingyCollectionView(int offset, int limit, int total, IEnumerable<ThingyView> items)
        {
            this.Offset = offset;
            this.Limit = limit;
            this.Total = total;
            this.Items = items;
        }
    }
}
