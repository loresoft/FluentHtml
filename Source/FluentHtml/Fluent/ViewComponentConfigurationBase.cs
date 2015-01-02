using System;
using System.Collections.Generic;
using System.Web.Routing;

namespace FluentHtml.Fluent
{
    public abstract class ViewComponentConfigurationBase
    {
        protected ViewComponentConfigurationBase()
        {
            HtmlAttributes = new RouteValueDictionary();
        }

        public string Name { get; set; }

        public IDictionary<string, object> HtmlAttributes
        {
            get;
            private set;
        }

    }
}