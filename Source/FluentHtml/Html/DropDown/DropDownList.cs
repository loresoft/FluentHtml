using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using FluentHtml.Extensions;
using FluentHtml.Fluent;
using FluentHtml.Reflection;

namespace FluentHtml.Html.DropDown
{
    public class DropDownList : SecureViewBase
    {
        public DropDownList(HtmlHelper htmlHelper)
            : base(htmlHelper)
        {
        }

        public ModelMetadata Metadata { get; set; }

        public string Placeholder { get; set; }

        public string DataValueField { get; set; }
        public string DataValueFormat { get; set; }

        public string DataTextField { get; set; }
        public string DataTextFormat { get; set; }

        public string DataGroupField { get; set; }

        public IEnumerable Items { get; set; }

        public object SelectedValue { get; set; }


        public override string ToHtmlString()
        {
            VerifySettings();

            string fullName = Name;

            var options = BuildOptionHtml();

            var selectBuilder = new TagBuilder("select");
            selectBuilder.InnerHtml = options;

            MergeAttributes(selectBuilder);
            selectBuilder.MergeAttribute("name", fullName, true);
            selectBuilder.GenerateId(fullName);

            foreach (string cssClass in CssClasses)
                selectBuilder.AddCssClass(cssClass);

            if (HtmlHelper == null)
                return selectBuilder.ToString(TagRenderMode.Normal);

            // If there are any errors for a named field, we add the css attribute.
            ModelState modelState;
            if (HtmlHelper.ViewData.ModelState.TryGetValue(fullName, out modelState) && modelState.Errors.Count > 0)
                selectBuilder.AddCssClass(HtmlHelper.ValidationInputCssClassName);

            var attributes = HtmlHelper.GetUnobtrusiveValidationAttributes(Name, Metadata);
            selectBuilder.MergeAttributes(attributes);

            if (!CanWrite())
            {
                selectBuilder.MergeAttribute("disabled", "disabled", true);
                if (DeniedClass.HasValue())
                    selectBuilder.AddCssClass(DeniedClass);
            }
            else if (GrantedClass.HasValue())
                selectBuilder.AddCssClass(GrantedClass);

            return selectBuilder.ToString(TagRenderMode.Normal);
        }


        private string BuildOptionHtml()
        {
            var builder = new StringBuilder();

            // placeholder must be first
            if (Placeholder != null)
            {
                var option = new TagBuilder("option");
                option.InnerHtml = HttpUtility.HtmlEncode(Placeholder);
                option.Attributes["value"] = string.Empty;

                builder.AppendLine(option.ToString(TagRenderMode.Normal));
            }

            if (DataGroupField.HasValue())
            {
                var groups = BuildGroupList();
                string html = BuildGroupHtml(groups);
                builder.Append(html);
            }
            else
            {
                var items = BuildSelectList();
                string html = BuildOptionHtml(items);
                builder.Append(html);
            }

            return builder.ToString();
        }

        private string BuildOptionHtml(IEnumerable<SelectListItem> items)
        {
            if (items == null)
                return string.Empty;

            var builder = new StringBuilder();

            foreach (var item in items)
            {
                var option = new TagBuilder("option");
                option.InnerHtml = HttpUtility.HtmlEncode(item.Text);

                if (item.Value != null)
                    option.Attributes["value"] = item.Value;

                if (item.Selected)
                    option.Attributes["selected"] = "selected";

                builder.AppendLine(option.ToString(TagRenderMode.Normal));
            }

            return builder.ToString();
        }

        private string BuildGroupHtml(IEnumerable<SelectGroupItem> groups)
        {
            if (groups == null)
                return string.Empty;

            var builder = new StringBuilder();

            foreach (var group in groups)
            {
                var groupTag = new TagBuilder("optgroup");
                groupTag.Attributes["label"] = HttpUtility.HtmlEncode(group.Label);

                var optionHtml = BuildOptionHtml(group.Items);
                groupTag.InnerHtml = optionHtml;

                builder.AppendLine(groupTag.ToString(TagRenderMode.Normal));
            }
            return builder.ToString();
        }

        private IEnumerable<SelectListItem> BuildSelectList()
        {
            if (Items == null)
                return Enumerable.Empty<SelectListItem>();

            if (Items is IEnumerable<SelectListItem>)
                return Items as IEnumerable<SelectListItem>;

            var options = new List<SelectListItem>();

            foreach (var item in Items)
            {
                var selectListItem = CreateSelectItem(item);
                options.Add(selectListItem);
            }

            return options;
        }

        private IEnumerable<SelectGroupItem> BuildGroupList()
        {
            if (Items == null)
                return Enumerable.Empty<SelectGroupItem>();

            if (Items is IEnumerable<SelectGroupItem>)
                return Items as IEnumerable<SelectGroupItem>;

            var groups = new Dictionary<string, SelectGroupItem>();

            foreach (var item in Items)
            {
                var selectListItem = CreateSelectItem(item);

                var group = LateBinder.GetProperty(item, DataGroupField);
                var groupString = Convert.ToString(group);

                var groupItem = groups.GetOrAdd(groupString, k => new SelectGroupItem { Label = k });
                groupItem.Items.Add(selectListItem);
            }

            return groups.Values;
        }

        private SelectListItem CreateSelectItem(object item)
        {
            var text = DataTextField.HasValue() ? LateBinder.GetProperty(item, DataTextField) : item;
            var textString = HtmlHelper.FormatValue(text, DataTextFormat);

            var value = DataValueField.HasValue() ? LateBinder.GetProperty(item, DataValueField) : text;
            var valueString = HtmlHelper.FormatValue(value, DataValueFormat);

            var selectedString = HtmlHelper.FormatValue(SelectedValue, DataValueFormat);

            var selected = SelectedValue != null
                && (Equals(value, SelectedValue) || string.Equals(selectedString, valueString));

            var selectListItem = new SelectListItem
            {
                Text = textString,
                Value = valueString,
                Selected = selected
            };
            return selectListItem;
        }

    }
}
