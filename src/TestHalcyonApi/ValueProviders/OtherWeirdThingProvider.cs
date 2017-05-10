using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Threax.AspNetCore.Halcyon.Ext.ValueProviders;

namespace TestHalcyonApi.ValueProviders
{
    public class OtherWeirdThingProvider : LabelValuePairProviderSync
    {
        public OtherWeirdThingProvider()
        {
        }

        protected override IEnumerable<LabelValuePair> GetSourcesSync()
        {
            yield return new LabelValuePair("Value 1", "5f959548-7edc-4c6d-970c-ff2ea44e62c3");
            yield return new LabelValuePair("Value 2", "7dfbbd59-645b-4f59-9bf2-287a4fcfd023");
            yield return new LabelValuePair("Value 3", "c34a78dc-3192-4ca4-a12a-ada902c94a93");
        }
    }
}
