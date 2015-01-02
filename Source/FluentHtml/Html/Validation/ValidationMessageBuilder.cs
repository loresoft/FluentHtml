using System;
using FluentHtml.Fluent;

namespace FluentHtml.Html.Validation
{
    public class ValidationMessageBuilder : ViewComponentBuilderBase<ValidationMessage, ValidationMessageBuilder>
    {
        public ValidationMessageBuilder(ValidationMessage component)
            : base(component)
        {
        }

        public ValidationMessageBuilder Message(string value)
        {
            Component.Message = value;
            return this;
        }

        public ValidationMessageBuilder For(string value)
        {
            Component.ModelName = value;
            return this;
        }

        public ValidationMessageBuilder IconOnly(bool value = true)
        {
            Component.IconOnly = value;
            return this;
        }

        public ValidationMessageBuilder ErrorCssClass(string value)
        {
            Component.ValidationErrorCssClassName = value;
            return this;
        }

        public ValidationMessageBuilder ValidCssClass(string value)
        {
            Component.ValidationValidCssClassName = value;
            return this;
        }
    }
}