using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Threax.AspNetCore.Swashbuckle.Convention
{
    public class RequireItemsAttribute : RequiredAttribute
    {
        public RequireItemsAttribute()
        {
            
        }

        public override bool IsValid(object value)
        {
            var dictionary = value as ICollection;
            if(dictionary != null)
            {
                return dictionary.Count > 0;
            }
            return false;
        }
    }
}
