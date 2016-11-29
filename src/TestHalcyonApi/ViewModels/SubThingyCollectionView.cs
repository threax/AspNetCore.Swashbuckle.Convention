using Halcyon.HAL.Attributes;
using HateoasTest.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Threax.AspNetCore.Halcyon.Ext;

namespace TestHalcyonApi.ViewModels
{
    [HalModel()]
    [HalActionLink("self", typeof(SubThingyController), "List")]
    [HalActionLink("listsubthingies", typeof(SubThingyController), "Get")]
    public class SubThingyCollectionView : CollectionView<SubThingyView>
    {
        
    }
}
