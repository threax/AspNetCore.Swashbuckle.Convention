﻿using Halcyon.HAL;
using Halcyon.HAL.Attributes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Threax.AspNetCore.Halcyon.Ext
{
    public class CustomHalAttributeConverter : IHALConverter
    {
        private ClaimsPrincipal user;

        public CustomHalAttributeConverter(IHttpContextAccessor httpContextAccessor)
        {
            this.user = httpContextAccessor.HttpContext.User;
        }

        public bool CanConvert(Type type)
        {
            if (type == null || type == typeof(HALResponse))
            {
                return false;
            }

            return type.GetTypeInfo().GetCustomAttributes().Any(x => x is HalModelAttribute);
        }

        public HALResponse Convert(object model)
        {
            if (!this.CanConvert(model?.GetType()))
            {
                throw new InvalidOperationException();
            }

            //If the object is an ICollectionView, use that to parse.
            var dataCollection = model as ICollectionView;
            if(dataCollection != null)
            {
                var itemType = dataCollection.CollectionType;
                var response = ConvertInstance(model, user);
                response.AddEmbeddedCollection(dataCollection.CollectionName, GetEmbeddedResponses(dataCollection.AsObjects, user));
                return response;
            }

            //If the object is an IEnumerable try to identify properties from that.
            var enumerableValue = model as IEnumerable;
            if (enumerableValue != null)
            {
                var itemType = Utils.GetEnumerableModelType(enumerableValue);
                var response = new HALResponse(new Object());
                response.AddEmbeddedCollection("values", GetEmbeddedResponses(enumerableValue, user));
                return response;
            }

            //If we got here we probably have a plain object, convert and return it.
            return ConvertInstance(model, user);
        }

        private static HALResponse ConvertInstance(object model, ClaimsPrincipal user)
        {
            //This is scanning all links for each item in a collection, prevent that
            var resolver = new CustomHALAttributeResolver();

            var halConfig = resolver.GetConfig(model);

            var response = new HALResponse(model, halConfig);
            response.AddLinks(resolver.GetUserLinks(model, user));
            response.AddEmbeddedCollections(resolver.GetEmbeddedCollections(model, halConfig));

            return response;
        }

        private static IEnumerable<HALResponse> GetEmbeddedResponses(IEnumerable enumerableValue, ClaimsPrincipal user)
        {
            foreach (var item in enumerableValue)
            {
                yield return ConvertInstance(item, user);
            }
        }
    }
}
