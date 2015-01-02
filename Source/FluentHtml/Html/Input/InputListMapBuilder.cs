using System;
using System.Linq.Expressions;
using FluentHtml.Fluent;
using FluentHtml.Reflection;

namespace FluentHtml.Html.Input
{
    public class InputListMapBuilder<TItem> : BuilderBase<InputList, InputListMapBuilder<TItem>>
    {
        public InputListMapBuilder(InputList component)
            : base(component)
        {
        }

        public InputListMapBuilder<TItem> ValueField<TProperty>(Expression<Func<TItem, TProperty>> expression)
        {
            Component.DataValueField = ReflectionHelper.ExtractPropertyName(expression);
            return this;
        }

        public InputListMapBuilder<TItem> TextField<TProperty>(Expression<Func<TItem, TProperty>> expression)
        {
            Component.DataTextField = ReflectionHelper.ExtractPropertyName(expression); ;
            return this;
        }

        public InputListMapBuilder<TItem> AttributeField<TProperty>(string attributeName, Expression<Func<TItem, TProperty>> expression)
        {
            Component.DataFields[attributeName] = ReflectionHelper.ExtractPropertyName(expression); ;
            return this;
        }
    }
}