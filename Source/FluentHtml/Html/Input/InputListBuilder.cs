using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentHtml.Fluent;

namespace FluentHtml.Html.Input
{
    public class InputListBuilder : SecureViewBuilderBase<InputList, InputListBuilder>
    {
        public InputListBuilder(InputList component)
            : base(component)
        {
        }

        public InputListBuilder ListCssClass(string value)
        {
            Component.ListCssClass = value;
            return this;
        }

        public InputListBuilder ListAttribute(string name, object value)
        {
            Component.HtmlAttributes[name] = value;
            return this;
        }


        public InputListBuilder ItemCssClass(string value)
        {
            Component.ItemCssClass = value;
            return this;
        }

        public InputListBuilder ItemAttribute(string name, object value)
        {
            Component.ItemAttributes[name] = value;
            return this;
        }


        public InputListBuilder InputCssClass(string value)
        {
            Component.InputCssClass = value;
            return this;
        }

        public InputListBuilder InputAttribute(string name, object value)
        {
            Component.InputAttributes[name] = value;
            return this;
        }


        public InputListBuilder DataValueField(string propertyName)
        {
            Component.DataValueField = propertyName;
            return this;
        }

        public InputListBuilder DataTextField(string propertyName)
        {
            Component.DataTextField = propertyName;
            return this;
        }


        public InputListBuilder DataField(string attributeName, string propertyName)
        {
            Component.DataFields[attributeName] = propertyName;
            return this;
        }


        public InputListBuilder BindTo(IEnumerable data)
        {
            Component.Items = data;
            return this;
        }

        public InputListBuilder BindTo<TItem>(IEnumerable<TItem> data)
        {
            Component.Items = data;
            return this;
        }


        public InputListBuilder SelectedValues(IEnumerable data)
        {
            Component.SelectedValues = data;
            return this;
        }

        public InputListBuilder SelectedValues<TItem>(IEnumerable<TItem> data)
        {
            Component.SelectedValues = data;
            return this;
        }

        public InputListBuilder SelectedValue<TItem>(TItem data)
        {
            Component.SelectedValues = Enumerable.Repeat(data, 1);
            return this;
        }


        public InputListBuilder Map<TItem>(Action<InputListMapBuilder<TItem>> configurator)
        {
            var builder = new InputListMapBuilder<TItem>(Component);
            configurator(builder);

            return this;
        }

    }
}