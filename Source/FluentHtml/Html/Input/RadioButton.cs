using System;
using System.Web.Mvc;
using FluentHtml.Extensions;
using FluentHtml.Reflection;

namespace FluentHtml.Html.Input
{
    public class RadioButton : InputBase
    {
        public RadioButton(HtmlHelper htmlHelper)
            : base(htmlHelper)
        {
            InputType = InputType.Radio;
        }

        public bool Checked { get; set; }

        public override string ToHtmlString()
        {
            VerifySettings();

            string fullName = Name;

            var inputItem = EnumItem<InputType>.Create(InputType);
            string inputType = inputItem.Description.ToLowerInvariant();

            var tagBuilder = new TagBuilder("input");
            tagBuilder.MergeAttributes(HtmlAttributes);
            tagBuilder.MergeAttribute("type", inputType);

            tagBuilder.MergeAttribute("name", fullName, true);
            tagBuilder.GenerateId(fullName);

            object value = Value ?? string.Empty;
            string valueParameter = HtmlHelper.FormatValue(value, Format);

            tagBuilder.MergeAttribute("value", valueParameter, true);

            // if explicitly checked or model data = value
            if (Checked || (Metadata != null && Metadata.Model == Value))
                tagBuilder.MergeAttribute("checked", "checked");

            foreach (string cssClass in CssClasses)
                tagBuilder.AddCssClass(cssClass);

            // If there are any errors for a named field, we add the css attribute.
            ModelState modelState;
            if (HtmlHelper.ViewData.ModelState.TryGetValue(fullName, out modelState) && modelState.Errors.Count > 0)
                tagBuilder.AddCssClass(HtmlHelper.ValidationInputCssClassName);

            tagBuilder.MergeAttributes(HtmlHelper.GetUnobtrusiveValidationAttributes(Name, Metadata));

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


            return tagBuilder.ToString(TagRenderMode.SelfClosing);
        }
    }
}
