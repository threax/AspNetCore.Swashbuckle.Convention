using Halcyon.HAL.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestHalcyonApi.Controllers;
using Threax.AspNetCore.Halcyon.Ext;

namespace TestHalcyonApi.Models
{
    /// <summary>
    /// All of the example models are pretty much the same except for the actual links.
    /// To setup an input model you must provide a save rel that points to the function
    /// to save the data on. The input model of that function will be used to build the 
    /// form input.
    /// If you want to use multiple pages for input as in this example, include previous and next
    /// links. Include a previous link that points to the first page on the first
    /// page and a next that is pointed to the the last page on the last page, this way the code
    /// gen will include all the declared links. You won't ever provide those 2 links at runtime
    /// so the client side will never pass canPrevious for the first page or canNext for the last.
    /// 
    /// Also note that these models fulfil both the input and view model roles. It is ok to share those
    /// most of the time especially if they always represent the same data.
    /// </summary>
    [HalModel]
    [DeclareHalLink("previous", MultipartInputController.Rels.GetPart1, typeof(MultipartInputController))] //Declare a previous link bound to this class, but this will never generate at runtime
    [HalActionLink(typeof(MultipartInputController), nameof(MultipartInputController.GetPart2), "next")]
    [HalActionLink(typeof(MultipartInputController), nameof(MultipartInputController.SetPart1), "save")]
    public class MultipartInput1
    {
        /// <summary>
        /// The id for the main model type should be included, it is used to fill out the route.
        /// This will tie all of your multipart models together as you go back and forth entering data.
        /// Make sure this does not get copied back into your entity somehow, otherwise the users will
        /// be able to edit any entry.
        /// </summary>
        [JsonIgnore]
        Guid MultipartInputModelId { get; set; }

        //Some data for part 1.

        public int Part1Number { get; set; }

        public String Part1String { get; set; }
    }
}
