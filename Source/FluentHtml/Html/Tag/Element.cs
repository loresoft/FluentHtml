using System;
using System.Web.Mvc;
using System.Web.Routing;
using FluentHtml.Extensions;
using FluentHtml.Fluent;

namespace FluentHtml.Html.Tag
{
    public class Element : SecureViewBase
    {
        public Element(HtmlHelper htmlHelper)
            : base(htmlHelper)
        {
            Encode = true;
        }

        public Element(ViewContext viewContext)
            : base(viewContext)
        {
            Encode = true;
        }

        public Element(RequestContext requestContext)
            : base(requestContext)
        {
            Encode = true;
        }

        public string TagName { get; set; }

        public bool Encode { get; set; }

        public string Text { get; set; }

        public override string ToHtmlString()
        {
            string innerText = Text ?? string.Empty;
            string fullName = Name;
            string tagName = TagName.HasValue() ? TagName.ToLower() : "span";

            var tagBuilder = new TagBuilder(tagName);
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
                if (GrantedClass.HasValue())
                    tagBuilder.AddCssClass(GrantedClass);
            }
            else
            {
                if (DeniedClass.HasValue())
                    tagBuilder.AddCssClass(DeniedClass);
            }

            return tagBuilder.ToString(TagRenderMode.Normal);
        }
    }
}
