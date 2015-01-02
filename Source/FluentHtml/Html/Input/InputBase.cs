using System;
using System.Web.Mvc;
using System.Web.Routing;
using FluentHtml.Fluent;

namespace FluentHtml.Html.Input
{
    public abstract class InputBase : SecureViewBase
    {
        protected InputBase(HtmlHelper htmlHelper)
            : base(htmlHelper)
        {
        }

        protected InputBase(ViewContext viewContext)
            : base(viewContext)
        {
        }

        protected InputBase(RequestContext requestContext)
            : base(requestContext)
        {
        }

        public ModelMetadata Metadata { get; set; }

        public InputType InputType { get; set; }

        public object Value { get; set; }

        public string Format { get; set; }
    }
}
