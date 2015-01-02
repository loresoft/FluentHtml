using System;
using System.Web.Mvc;
using FluentHtml.Html;

namespace FluentHtml
{
    public static class FluentExtensions
    {
        public static FluentHelper Fluent(this HtmlHelper htmlHelper)
        {
            var fluent = new FluentHelper(htmlHelper);
            return fluent;
        }

        public static FluentHelper<TModel> Fluent<TModel>(this HtmlHelper<TModel> htmlHelper)
        {
            var fluent = new FluentHelper<TModel>(htmlHelper);
            return fluent;
        }
    }
}