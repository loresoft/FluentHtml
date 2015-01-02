using System;
using System.Web.Mvc;
using System.Web.Routing;
using FluentHtml.Extensions;
using FluentHtml.Fluent;
using FluentHtml.Reflection;

namespace FluentHtml.Html.Tag
{
    public class Button : SecureViewBase
    {
        public Button(HtmlHelper htmlHelper)
            : base(htmlHelper)
        {
            Encode = true;
        }

        public Button(ViewContext viewContext)
            : base(viewContext)
        {
            Encode = true;
        }

        public Button(RequestContext requestContext)
            : base(requestContext)
        {
            Encode = true;
        }

        public bool Encode { get; set; }

        public string Text { get; set; }

        public ButtonType ButtonType { get; set; }

        public override string ToHtmlString()
        {
            var inputItem = EnumItem<ButtonType>.Create(ButtonType);
            string inputType = inputItem.Description.ToLowerInvariant();

            string fullName = Name;
            string innerText = Text ?? string.Empty;

            var tagBuilder = new TagBuilder("button");
            if (Encode)
                tagBuilder.SetInnerText(innerText);
            else
                tagBuilder.InnerHtml = innerText;

            tagBuilder.MergeAttributes(HtmlAttributes);
            tagBuilder.MergeAttribute("type", inputType);

            if (fullName.HasValue())
            {
                tagBuilder.MergeAttribute("name", fullName, true);
                tagBuilder.GenerateId(fullName);
            }

            foreach (string cssClass in CssClasses)
                tagBuilder.AddCssClass(cssClass);

            if (!CanWrite())
            {
                tagBuilder.MergeAttribute("disabled", "disabled", true);
                if (DeniedClass.HasValue())
                    tagBuilder.AddCssClass(DeniedClass);
            }
            else if (GrantedClass.HasValue())
            {
                tagBuilder.AddCssClass(GrantedClass);
            }

            return tagBuilder.ToString(TagRenderMode.Normal);
        }
    }
}
