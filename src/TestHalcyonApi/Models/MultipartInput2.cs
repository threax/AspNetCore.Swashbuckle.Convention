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
    [HalActionLink("previous", MultipartInputController.Rels.GetPart1, typeof(MultipartInputController))] //Here previous is an actual action link to the previous step
    [HalActionLink("next", MultipartInputController.Rels.GetPart3, typeof(MultipartInputController))]
    [HalActionLink("save", MultipartInputController.Rels.SetPart2, typeof(MultipartInputController))]
    public class MultipartInput2
    {
        public int Part2Number { get; set; }

        public String Part2String { get; set; }
    }
}
