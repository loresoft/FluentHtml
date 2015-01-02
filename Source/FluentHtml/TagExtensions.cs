using System;
using FluentHtml.Html.Tag;

namespace FluentHtml
{
    public static class TagExtensions
    {
        public static LinkBuilder Link(this FluentHelper helper)
        {
            var htmlHelper = helper.HtmlHelper;
            var component = new Link(htmlHelper);
            var builder = new LinkBuilder(component);

            return builder;
        }

        public static ButtonBuilder Button(this FluentHelper helper)
        {
            var htmlHelper = helper.HtmlHelper;
            var component = new Button(htmlHelper);
            var builder = new ButtonBuilder(component);

            return builder;
        }

        public static ElementBuilder Span(this FluentHelper helper)
        {
            var htmlHelper = helper.HtmlHelper;
            var component = new Element(htmlHelper) { TagName = "span" };

            var builder = new ElementBuilder(component);
            return builder;
        }
    }
}
