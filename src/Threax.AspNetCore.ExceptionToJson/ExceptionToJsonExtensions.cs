using Threax.AspNetCore.ExceptionToJson;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ExceptionToJsonExtensions
    {
        public static MvcOptions UseExceptionErrorFilters(this MvcOptions options, bool detailedErrors = false)
        {
            options.UseExceptionErrorFilters(new ExceptionToJsonOptions()
            {
                DetailedErrors = detailedErrors
            });
            return options;
        }

        public static MvcOptions UseExceptionErrorFilters(this MvcOptions options, ExceptionToJsonOptions jOptions)
        {
            options.Filters.Add(new ExceptionToJsonFilterAttribute(jOptions.DetailedErrors)
            {
                AllowResultFilters = jOptions.AllowResultFilters,
                Order = jOptions.Order
            });
            return options;
        }
    }
}
