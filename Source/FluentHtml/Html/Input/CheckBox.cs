using System;
using System.Text;
using System.Web.Mvc;
using FluentHtml.Extensions;
using FluentHtml.Reflection;

namespace FluentHtml.Html.Input
{
    public class CheckBox : InputBase
    {
        public CheckBox(HtmlHelper htmlHelper)
            : base(htmlHelper)
        {
            InputType = InputType.Checkbox;
            IncludeHidden = true;
        }

        public bool Checked { get; set; }

        public bool IncludeHidden { get; set; }

        public override string ToHtmlString()
        {
            VerifySettings();

            var inputHtml = GetCheckboxHtml();

            if (!IncludeHidden)
                return inputHtml;

            // Render an additional <input type="hidden".../> for checkboxes.
            var inputBuilder = new StringBuilder();
            inputBuilder.AppendLine(inputHtml);

            var hiddenHtml = GetHiddenHtml();
            inputBuilder.Append(hiddenHtml);

            return inputBuilder.ToString();
        }

        protected string GetHiddenHtml()
        {
            var hiddenInput = new TagBuilder("input");
            hiddenInput.MergeAttribute("type", "hidden");
            hiddenInput.MergeAttribute("name", Name);
            hiddenInput.MergeAttribute("value", "false");

            string hiddenHtml = hiddenInput.ToString(TagRenderMode.SelfClosing);
            return hiddenHtml;
        }

        protected string GetCheckboxHtml()
        {
            var inputItem = EnumItem<InputType>.Create(InputType);
            string inputType = inputItem.Description.ToLowerInvariant();

            var tagBuilder = new TagBuilder("input");
            tagBuilder.MergeAttributes(HtmlAttributes);
            tagBuilder.MergeAttribute("type", inputType);

            tagBuilder.MergeAttribute("name", Name, true);
            tagBuilder.GenerateId(Name);
            tagBuilder.MergeAttribute("value", "true", true);

            if (Checked)
                tagBuilder.MergeAttribute("checked", "checked");

            foreach (string cssClass in CssClasses)
                tagBuilder.AddCssClass(cssClass);

            // If there are any errors for a named field, we add the css attribute.
            ModelState modelState;
            if (HtmlHelper.ViewData.ModelState.TryGetValue(Name, out modelState) && modelState.Errors.Count > 0)
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

            string inputHtml = tagBuilder.ToString(TagRenderMode.SelfClosing);
            return inputHtml;
        }
    }
}