using System;
using System.ComponentModel;
using System.Web.Mvc;

namespace FluentHtml
{
    public class FluentHelper
    {
        public FluentHelper(HtmlHelper htmlHelper)
        {
            HtmlHelper = htmlHelper;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public HtmlHelper HtmlHelper { get; set; }
    }

    public class FluentHelper<TModel> : FluentHelper
    {
        public FluentHelper(HtmlHelper<TModel> htmlHelper)
            : base(htmlHelper)
        {
            HtmlHelper = htmlHelper;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public new HtmlHelper<TModel> HtmlHelper { get; set; }
    }
}
