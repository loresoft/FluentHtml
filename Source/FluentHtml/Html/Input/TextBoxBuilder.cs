using System;

namespace FluentHtml.Html.Input
{
    public class TextBoxBuilder : InputBuilderBase<TextBox, TextBoxBuilder>
    {
        public TextBoxBuilder(TextBox component)
            : base(component)
        {
        }


        // attributes
        public TextBoxBuilder Placeholder(string value)
        {
            HtmlAttribute("placeholder", value ?? string.Empty);

            return this;
        }

        public TextBoxBuilder Readonly(bool value = true)
        {
            const string name = "readonly";
            
            if (value)
                HtmlAttribute(name, name);
            else
                Component.HtmlAttributes.Remove(name);

            return this;
        }

        public TextBoxBuilder AutoFocus(bool value = true)
        {
            const string name = "autofocus";
            
            if (value)
                HtmlAttribute(name, name);
            else
                Component.HtmlAttributes.Remove(name);

            return this;
        }

        public TextBoxBuilder AutoComplete(bool value = true)
        {
            HtmlAttribute("autocomplete", value ? "on" : "off");

            return this;
        }

        public TextBoxBuilder MaxLength(int value)
        {
            HtmlAttribute("maxlength ", value);

            return this;
        }
    }
}