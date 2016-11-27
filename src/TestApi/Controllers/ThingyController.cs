using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestApi.Model;
using Threax.AspNetCore.ExceptionToJson;

namespace TestApi.Controllers
{
    /// <summary>
    /// This controller generates errors.
    /// </summary>
    [Route("api/[controller]/[action]")]
    public class ErrorController : Controller
    {
        /// <summary>
        /// Throws a NotSupportedException.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Thingy> GetAll()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Throws a custom error.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Thingy Get(int id)
        {
            throw new ErrorResultException("This is a custom error", System.Net.HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// Throws a validation error from an AutoValidate attribute.
        /// </summary>
        /// <param name="theThing"></param>
        [HttpPost()]
        [AutoValidate("Not a good thingy.")]
        public void Post(Thingy theThing)
        {
            Console.WriteLine(theThing.ToString());
        }
    }
}
