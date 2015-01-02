using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using FluentHtml.Fluent;

namespace FluentHtml.Html.DropDown
{
    public class DropDownListBuilder : SecureViewBuilderBase<DropDownList, DropDownListBuilder>
    {
        public DropDownListBuilder(DropDownList component)
            : base(component)
        {
        }

        public DropDownListBuilder Placeholder(string value)
        {
            Component.Placeholder = value;
            return this;
        }

        public DropDownListBuilder DataValueField(string value)
        {
            Component.DataValueField = value;
            return this;
        }
        
        public DropDownListBuilder DataValueFormat(string value)
        {
            Component.DataValueFormat = value;
            return this;
        }

        public DropDownListBuilder DataTextField(string value)
        {
            Component.DataTextField = value;
            return this;
        }
        
        public DropDownListBuilder DataTextFormat(string value)
        {
            Component.DataTextFormat = value;
            return this;
        }

        public DropDownListBuilder DataGroupField(string value)
        {
            Component.DataGroupField = value;
            return this;
        }


        public DropDownListBuilder SelectedValue(object value)
        {
            Component.SelectedValue = value;
            return this;
        }

        public DropDownListBuilder SelectedValue<TItem>(TItem value)
        {
            Component.SelectedValue = value;
            return this;
        }


        public DropDownListBuilder BindTo(IEnumerable data)
        {
            Component.Items = data;
            return this;
        }

        public DropDownListBuilder BindTo(IEnumerable<SelectListItem> data)
        {
            Component.Items = data;
            return this;
        }

        public DropDownListBuilder BindTo(IEnumerable<SelectGroupItem> data)
        {
            Component.Items = data;
            return this;
        }

        public DropDownListBuilder BindTo<TItem>(IEnumerable<TItem> data)
        {
            Component.Items = data;
            return this;
        }


        public DropDownListBuilder Map<TItem>(Action<DropDownMapBuilder<TItem>> configurator)
        {
            var builder = new DropDownMapBuilder<TItem>(Component);
            configurator(builder);
            return this;
        }

        public DropDownListBuilder Title(string value)
        {
            HtmlAttribute("title", "value");
            return this;
        }

        public DropDownListBuilder AutoFocus(bool autoFocus = true)
        {
            const string name = "autofocus";

            if (autoFocus)
                HtmlAttribute(name, name);
            else
                Component.HtmlAttributes.Remove(name);

            return this;
        }

        public DropDownListBuilder Disabled(bool disabled = true)
        {
            const string name = "disabled";

            if (disabled)
                HtmlAttribute(name, name);
            else
                Component.HtmlAttributes.Remove(name);

            return this;
        }

        public DropDownListBuilder Required(bool required = true)
        {
            const string name = "required";

            if (required)
                HtmlAttribute(name, name);
            else
                Component.HtmlAttributes.Remove(name);

            return this;
        }

    }
}