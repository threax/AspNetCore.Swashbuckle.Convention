using Halcyon.HAL.Attributes;
using Test.Controllers;
using Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Threax.AspNetCore.Halcyon.Ext;
using Threax.AspNetCore.Models;
using System.ComponentModel.DataAnnotations;

namespace Test.InputModels
{
    [HalModel]
    public partial class ValueQuery : PagedCollectionQuery, IValueQuery
    {
        /// <summary>
        /// Lookup a value by id.
        /// </summary>
        public Guid? CrazyKey { get; set; }


        /// <summary>
        /// Populate an IQueryable for values. Does not apply the skip or limit.
        /// </summary>
        /// <param name="query">The query to populate.</param>
        /// <returns>The query passed in populated with additional conditions.</returns>
        public IQueryable<T> Create<T>(IQueryable<T> query) where T : IValue, IValueId
        {
            if (CrazyKey != null)
            {
                query = query.Where(i => i.CrazyKey == CrazyKey);
            }
            else
            {
                OnCreate(ref query);
            }

            return query;
        }

        partial void OnCreate<T>(ref IQueryable<T> query) where T : IValue, IValueId;
    }
}