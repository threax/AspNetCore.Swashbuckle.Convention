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
    [HalActionLink(SubThingyController.Rels.List, typeof(SubThingyController))]
    public class SubThingyCollectionView : CollectionView<SubThingyView>
    {
        public SubThingyCollectionView(IEnumerable<SubThingyView> items)
        {
            this.Items = items;
        }
    }
}
