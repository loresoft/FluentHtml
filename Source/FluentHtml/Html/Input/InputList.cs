using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FluentHtml.Extensions;
using FluentHtml.Fluent;
using FluentHtml.Reflection;

namespace FluentHtml.Html.Input
{
    public class InputList : SecureViewBase
    {
        public InputList(HtmlHelper htmlHelper)
            : base(htmlHelper)
        {
            ItemAttributes = new RouteValueDictionary();
            LabelAttributes = new RouteValueDictionary();
            InputAttributes = new RouteValueDictionary();
            DataFields = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        }

        public InputType InputType { get; set; }

        public string ListCssClass { get; set; }


        public IDictionary<string, object> ItemAttributes { get; private set; }

        public string ItemCssClass { get; set; }


        public IDictionary<string, object> LabelAttributes { get; private set; }

        public string LabelCssClass { get; set; }


        public IDictionary<string, object> InputAttributes { get; private set; }

        public string InputCssClass { get; set; }



        public IDictionary<string, string> DataFields { get; private set; }

        public string DataValueField { get; set; }

        public string DataTextField { get; set; }


        public IEnumerable Items { get; set; }

        public IEnumerable SelectedValues { get; set; }


        public override string ToHtmlString()
        {
            var itemContent = new StringBuilder();
            itemContent.AppendLine();

            var orderList = new TagBuilder("ul");
            orderList.GenerateId(Name);

            MergeAttributes(orderList, HtmlAttributes);

            string listClass = ListCssClass ?? string.Format("{0}-list", InputType);
            orderList.AddCssClass(listClass);

            var selectedValues = new HashSet<object>();
            if (SelectedValues != null)
                selectedValues.UnionWith(SelectedValues.Cast<object>());

            // make sure not null
            var items = Items ?? Enumerable.Empty<object>();

            foreach (var item in items)
            {
                var inputItem = EnumItem<InputType>.Create(InputType);
                string inputType = inputItem.Description.ToLowerInvariant();

                var text = DataTextField.HasValue() ? LateBinder.GetProperty(item, DataTextField) : item;
                var textString = Convert.ToString(text);

                var value = DataValueField.HasValue() ? LateBinder.GetProperty(item, DataValueField) : text;
                var valueString = Convert.ToString(value);

                bool selected = selectedValues.Contains(value) || selectedValues.Contains(valueString);

                // resolve data fields
                var dataAttributes = new Dictionary<string, object>();
                foreach (var dataField in DataFields)
                {
                    var dataValue = LateBinder.GetProperty(item, dataField.Value);
                    if (dataValue != null)
                        dataAttributes[dataField.Key] = dataValue;
                }

                var input = new TagBuilder("input");
                input.MergeAttribute("type", inputType);
                input.MergeAttribute("name", Name);
                input.MergeAttribute("value", valueString);
                if (selected)
                    input.MergeAttribute("checked", "checked");

                if (InputAttributes != null)
                    MergeAttributes(input, InputAttributes);

                if (InputCssClass.HasValue())
                    input.AddCssClass(InputCssClass);

                // merge data attributes
                MergeAttributes(input, dataAttributes);

                if (!CanWrite())
                {
                    input.MergeAttribute("readonly", "readonly", true);
                    if (DeniedClass.HasValue())
                        input.AddCssClass(DeniedClass);
                }
                else if (GrantedClass.HasValue())
                {
                    input.AddCssClass(GrantedClass);
                }

                var label = new TagBuilder("label");
                if (LabelAttributes != null)
                    MergeAttributes(label, LabelAttributes);

                if (LabelCssClass.HasValue())
                    label.AddCssClass(LabelCssClass);

                // merge data attributes
                MergeAttributes(label, dataAttributes);

                label.InnerHtml = "{0} {1}".FormatWith(
                    input.ToString(TagRenderMode.SelfClosing),
                    HttpUtility.HtmlEncode(textString));

                var listItem = new TagBuilder("li");

                if (ItemAttributes != null)
                    MergeAttributes(listItem, ItemAttributes);

                listItem.InnerHtml = label.ToString(TagRenderMode.Normal);

                itemContent.AppendLine(listItem.ToString(TagRenderMode.Normal));
            }

            orderList.InnerHtml = itemContent.ToString();

            return orderList.ToString(TagRenderMode.Normal);
        }
    }
}
