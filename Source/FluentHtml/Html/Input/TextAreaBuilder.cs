using System;

namespace FluentHtml.Html.Input
{
    public class TextAreaBuilder : InputBuilderBase<TextArea, TextAreaBuilder>
    {
        public TextAreaBuilder(TextArea component)
            : base(component)
        {
        }


        // attributes
        public TextAreaBuilder Columns(int value)
        {
            HtmlAttribute("cols", value);
            return this;
        }

        public TextAreaBuilder Rows(int value)
        {
            HtmlAttribute("rows", value);
            return this;
        }

        public TextAreaBuilder Placeholder(string value)
        {
            HtmlAttribute("placeholder", value ?? string.Empty);

            return this;
        }

        public TextAreaBuilder Wrap(string value)
        {
            HtmlAttribute("wrap", value ?? string.Empty);

            return this;
        }

        public TextAreaBuilder Readonly(bool value = true)
        {
            const string name = "readonly";

            if (value)
                HtmlAttribute(name, name);
            else
                Component.HtmlAttributes.Remove(name);

            return this;
        }

        public TextAreaBuilder AutoFocus(bool value = true)
        {
            const string name = "autofocus";

            if (value)
                HtmlAttribute(name, name);
            else
                Component.HtmlAttributes.Remove(name);

            return this;
        }

        public TextAreaBuilder AutoComplete(bool value = true)
        {
            HtmlAttribute("autocomplete", value ? "on" : "off");

            return this;
        }

        public TextAreaBuilder MaxLength(int value)
        {
            HtmlAttribute("maxlength ", value);

            return this;
        }
    }
}