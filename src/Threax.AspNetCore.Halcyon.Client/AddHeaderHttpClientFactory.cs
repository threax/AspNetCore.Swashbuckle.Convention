﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Threax.AspNetCore.Halcyon.Client;

namespace Threax.AspNetCore.Halcyon.Client
{
    /// <summary>
    /// This factory can add a header when creating HttpRequestMessages.
    /// </summary>
    public class AddHeaderHttpClientFactory<TRef> : IHttpClientFactory<TRef>
    {
        private Func<String> valueAccessor;
        private IHttpClientFactory next;
        private String headerName;

        public AddHeaderHttpClientFactory(String headerName, Func<String> valueAccessor, IHttpClientFactory next)
        {
            this.valueAccessor = valueAccessor;
            this.next = next;
            this.headerName = headerName;
        }

        public HttpClient GetClient()
        {
            return next.GetClient();
        }

        public async Task<HttpRequestMessage> GetRequestMessage()
        {
            var req = await next.GetRequestMessage();
            req.Headers.TryAddWithoutValidation(headerName, valueAccessor());
            return req;
        }
    }
}
