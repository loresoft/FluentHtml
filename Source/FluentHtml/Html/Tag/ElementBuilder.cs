using System;
using FluentHtml.Fluent;

namespace FluentHtml.Html.Tag
{
    public class ElementBuilder : SecureViewBuilderBase<Element, ElementBuilder>
    {
        public ElementBuilder(Element component)
            : base(component)
        {
        }

        public ElementBuilder Tag(string name)
        {
            Component.TagName = name;
            return this;
        }

        public ElementBuilder Encode(bool value = true)
        {
            Component.Encode = value;
            return this;
        }

        public ElementBuilder Text(string text)
        {
            Component.Text = text;
            return this;
        }

        public ElementBuilder Html(string html)
        {
            Component.Encode = false;
            Component.Text = html;
            return this;
        }
    }
}