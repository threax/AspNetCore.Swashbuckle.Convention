using Halcyon.HAL.Attributes;
using HateoasTest.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Threax.AspNetCore.Halcyon.Ext;

namespace TestHalcyonApi.ViewModels
{
    [HalModel]
    [HalActionLink("self", typeof(ThingyController), "List")]
    [HalActionLink("list", typeof(ThingyController), "Get")]
    [HalActionLink("add", typeof(ThingyController), "Add")]
    public class ThingyCollectionView : CollectionView<ThingyView>
    {
        
    }
}
