using System;
using FluentHtml.Fluent;

namespace FluentHtml.Html.Input
{
    public abstract class InputBuilderBase<TView, TBuilder>
        : SecureViewBuilderBase<TView, TBuilder>
        where TView : InputBase
        where TBuilder : InputBuilderBase<TView, TBuilder>
    {
        protected InputBuilderBase(TView component)
            : base(component)
        {
        }

        public TBuilder InputType(InputType inputType)
        {
            Component.InputType = inputType;
            return this as TBuilder;
        }

        public TBuilder Value(object value)
        {
            Component.Value = value;
            return this as TBuilder;
        }

        public TBuilder Format(string format)
        {
            Component.Format = format;
            return this as TBuilder;
        }

        public TBuilder Title(string value)
        {
            HtmlAttribute("title", "value");
            return this as TBuilder;
        }

        public TBuilder Disabled(bool disabled = true)
        {
            const string name = "disabled";

            if (disabled)
                HtmlAttribute(name, name);
            else
                Component.HtmlAttributes.Remove(name);

            return this as TBuilder;
        }

        public TBuilder Required(bool required = true)
        {
            const string name = "required";

            if (required)
                HtmlAttribute(name, name);
            else
                Component.HtmlAttributes.Remove(name);

            return this as TBuilder;
        }
    }
}