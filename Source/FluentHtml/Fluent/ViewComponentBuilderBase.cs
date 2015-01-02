using System;
using System.Linq.Expressions;
using System.Web;
using FluentHtml.Reflection;

namespace FluentHtml.Fluent
{
    public abstract class ViewComponentBuilderBase<TView, TBuilder>
        : BuilderBase<TView, TBuilder>, IHtmlString
        where TView : ViewComponentBase
        where TBuilder : ViewComponentBuilderBase<TView, TBuilder>
    {
        protected ViewComponentBuilderBase(TView component)
            : base(component)
        {
        }

        public virtual TBuilder Name(string componentName)
        {
            Component.Name = componentName;

            return this as TBuilder;
        }

        public TBuilder Name<TItem, TProperty>(Expression<Func<TItem, TProperty>> expression)
        {
            Component.Name = ReflectionHelper.ExtractPropertyName(expression); ;
            return this as TBuilder;
        }

        public TBuilder CssClass(string value)
        {
            Component.CssClasses.Add(value);

            return this as TBuilder;
        }

        public virtual TView ToComponent()
        {
            return Component;
        }

        public static implicit operator TView(ViewComponentBuilderBase<TView, TBuilder> builder)
        {
            return builder.ToComponent();
        }

        public string ToHtmlString()
        {
            return ToComponent().ToHtmlString();
        }
    }
}
