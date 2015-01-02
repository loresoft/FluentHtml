using System;
using System.Web.Mvc;
using FluentHtml.Extensions;

namespace FluentHtml.Html.Input
{
    public class TextArea : InputBase
    {
        public TextArea(HtmlHelper htmlHelper)
            : base(htmlHelper)
        {
            InputType = InputType.Text;
        }

        public override string ToHtmlString()
        {
            VerifySettings();

            string fullName = Name;

            var tagBuilder = new TagBuilder("textarea");
            MergeAttributes(tagBuilder);

            tagBuilder.MergeAttribute("name", fullName, true);
            tagBuilder.GenerateId(fullName);

            object value = Value ?? string.Empty;
            string valueParameter = HtmlHelper.FormatValue(value, Format);

            tagBuilder.SetInnerText(valueParameter);

            foreach (string cssClass in CssClasses)
                tagBuilder.AddCssClass(cssClass);

            // If there are any errors for a named field, we add the css attribute.
            ModelState modelState;
            if (HtmlHelper.ViewData.ModelState.TryGetValue(fullName, out modelState) && modelState.Errors.Count > 0)
                tagBuilder.AddCssClass(HtmlHelper.ValidationInputCssClassName);

            tagBuilder.MergeAttributes(HtmlHelper.GetUnobtrusiveValidationAttributes(Name, Metadata));

            if (!CanWrite())
            {
                tagBuilder.MergeAttribute("readonly", "readonly", true);
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