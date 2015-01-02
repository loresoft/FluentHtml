using System;
using System.Collections.Generic;
using System.Web.Routing;

namespace FluentHtml.Fluent
{
    public abstract class ComponentBase
    {
        protected ComponentBase()
        {
            HtmlAttributes = new RouteValueDictionary();
        }

        public IDictionary<string, object> HtmlAttributes { get; private set; }
    }
}