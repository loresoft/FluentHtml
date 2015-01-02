using System;
using System.Collections.Generic;
using FluentHtml.Extensions;

namespace FluentHtml.Fluent
{
    public abstract class BuilderBase<TComponent, TBuilder>
        where TComponent : ComponentBase
        where TBuilder : BuilderBase<TComponent, TBuilder>
    {
        protected BuilderBase(TComponent component)
        {
            Component = component;
        }

        public TComponent Component { get; private set; }


        public virtual TBuilder HtmlAttribute(string name, object value)
        {
            Component.HtmlAttributes[name] = value;
            return this as TBuilder;
        }

        public virtual TBuilder HtmlAttributes(object attributes)
        {
            return HtmlAttributes(attributes.ToDictionary());
        }

        public virtual TBuilder HtmlAttributes(IDictionary<string, object> attributes)
        {
            Component.HtmlAttributes.Merge(attributes);
            return this as TBuilder;
        }

    }
}
