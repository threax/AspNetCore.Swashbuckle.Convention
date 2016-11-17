﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace TestApi.Controllers
{
    /// <summary>
    /// This controller provides api access to values.
    /// </summary>
    [Route("api/[controller]/[action]")]
    [Produces("application/json")]
    public class ValuesController : Controller
    {
        /// <summary>
        /// Get a list of all values.
        /// </summary>
        /// <returns>All the values</returns>
        // GET api/values
        [HttpGet]
        public IEnumerable<string> GetAll()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// Get a list of all values. This one has been marked Authorize and should require a bearer token.
        /// </summary>
        /// <returns>All the values</returns>
        // GET api/values
        [HttpGet("Authorized")]
        [Authorize]
        public IEnumerable<string> GetAuthorized()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// Get a specific value.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// Create a value.
        /// </summary>
        /// <param name="value"></param>
        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        /// <summary>
        /// Update or add a specific value.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        /// <summary>
        /// Delete a value.
        /// </summary>
        /// <param name="id"></param>
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
