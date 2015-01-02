using System;
using System.Web.Mvc;
using System.Web.Routing;
using FluentHtml.Extensions;
using FluentHtml.Fluent;

namespace FluentHtml.Html.Tag
{
    public class Link : SecureViewBase
    {
        public Link(HtmlHelper htmlHelper)
            : base(htmlHelper)
        {
            Encode = true;
            Navigation = new NavigationRequest();
        }

        public Link(ViewContext viewContext)
            : base(viewContext)
        {
            Encode = true;
            Navigation = new NavigationRequest();
        }

        public Link(RequestContext requestContext)
            : base(requestContext)
        {
            Encode = true;
            Navigation = new NavigationRequest();
        }

        public bool Encode { get; set; }

        public string Text { get; set; }

        public NavigationRequest Navigation { get; set; }

        public override string ToHtmlString()
        {
            string href = UrlGenerator.Generate(RequestContext, Navigation);
            string innerText = Text ?? string.Empty;
            string fullName = Name;

            var tagBuilder = new TagBuilder("a");
            if (Encode)
                tagBuilder.SetInnerText(innerText);
            else
                tagBuilder.InnerHtml = innerText;

            tagBuilder.MergeAttributes(HtmlAttributes);

            if (fullName.HasValue())
                tagBuilder.GenerateId(fullName);

            foreach (string cssClass in CssClasses)
                tagBuilder.AddCssClass(cssClass);

            if (CanWrite())
            {
                if (href.HasValue())
                    tagBuilder.MergeAttribute("href", href);

                if (GrantedClass.HasValue())
                    tagBuilder.AddCssClass(GrantedClass);
            }
            else
            {
                tagBuilder.MergeAttribute("disabled", "disabled", true);
                if (DeniedClass.HasValue())
                    tagBuilder.AddCssClass(DeniedClass);
            }


            return tagBuilder.ToString(TagRenderMode.Normal);
        }
    }
}
