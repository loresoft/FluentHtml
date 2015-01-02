using System;
using FluentHtml.Fluent;

namespace FluentHtml.Html.Tag
{
    public class ButtonBuilder : SecureViewBuilderBase<Button, ButtonBuilder>
    {
        public ButtonBuilder(Button component)
            : base(component)
        {
        }

        public ButtonBuilder Encode(bool value = true)
        {
            Component.Encode = value;
            return this;
        }

        public ButtonBuilder Text(string text)
        {
            Component.Encode = true;
            Component.Text = text;
            return this;
        }

        public ButtonBuilder Html(string html)
        {
            Component.Encode = false;
            Component.Text = html;
            return this;
        }

        public ButtonBuilder Disabled(bool disabled = true)
        {
            const string name = "disabled";

            if (disabled)
                HtmlAttribute(name, name);
            else
                Component.HtmlAttributes.Remove(name);

            return this;
        }

        public ButtonBuilder Type(ButtonType buttonType)
        {
            Component.ButtonType = buttonType;
            return this;
        }
    }
}