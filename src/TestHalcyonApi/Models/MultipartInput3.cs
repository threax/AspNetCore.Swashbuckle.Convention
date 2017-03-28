using Halcyon.HAL.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestHalcyonApi.Controllers;
using Threax.AspNetCore.Halcyon.Ext;

namespace TestHalcyonApi.Models
{
    [HalModel]
    [HalActionLink("previous", MultipartInputController.Rels.GetPart2, typeof(MultipartInputController))]
    [DeclareHalLink("next", MultipartInputController.Rels.GetPart3, typeof(MultipartInputController))] //Declare a next link bound to this class, but this will never generate at runtime
    [HalActionLink("save", MultipartInputController.Rels.SetPart3, typeof(MultipartInputController))]
    public class MultipartInput3
    {
        public int Part3Number { get; set; }

        public String Part3String { get; set; }
    }
}
