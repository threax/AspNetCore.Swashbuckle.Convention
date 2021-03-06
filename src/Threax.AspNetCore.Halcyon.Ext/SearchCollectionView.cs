﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Threax.AspNetCore.Halcyon.Ext
{
    public class SearchCollectionView<T> : CollectionView<T>, IQueryStringProvider
    {
        public SearchCollectionView(SearchQuery query, IEnumerable<T> items)
            :base(items)
        {
        }

        public void AddQuery(string rel, QueryStringBuilder queryString)
        {
            if (rel == HalSelfActionLinkAttribute.SelfRelName && Term != null)
            {
                queryString.AppendQueryString($"term={Term}");
            }
        }

        public String Term { get; set; }
    }
}
