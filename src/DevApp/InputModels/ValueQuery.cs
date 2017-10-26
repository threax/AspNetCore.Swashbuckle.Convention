using Halcyon.HAL.Attributes;
using DevApp.Controllers;
using DevApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Threax.AspNetCore.Halcyon.Ext;

namespace DevApp.InputModels
{
    [HalModel]
    public class ValueQuery : PagedCollectionQuery, IValueQuery
    {
        /// <summary>
        /// Lookup a value by id.
        /// </summary>
        public Guid? ValueId { get; set; }

        //Add any additional query parameters here

        /// <summary>
        /// Populate an IQueryable for values. Does not apply the skip or limit.
        /// </summary>
        /// <param name="query">The query to populate.</param>
        /// <returns>The query passed in populated with additional conditions.</returns>
        public IQueryable<T> Create<T>(IQueryable<T> query) where T : IValue, IValueId
        {
            if (ValueId.HasValue)
            {
                query = query.Where(i => i.ValueId == ValueId);
            }
            else
            {
                //Put additional model query conditions here, this way id lookup always works
            }

            return query;
        }
    }
}