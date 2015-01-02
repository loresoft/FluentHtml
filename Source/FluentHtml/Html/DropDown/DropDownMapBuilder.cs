using System;
using System.Linq.Expressions;
using FluentHtml.Fluent;
using FluentHtml.Reflection;

namespace FluentHtml.Html.DropDown
{
    public class DropDownMapBuilder<TItem> : BuilderBase<DropDownList, DropDownMapBuilder<TItem>>
    {
        public DropDownMapBuilder(DropDownList component)
            : base(component)
        {
        }

        public DropDownMapBuilder<TItem> ValueField<TProperty>(Expression<Func<TItem, TProperty>> expression)
        {
            Component.DataValueField = ReflectionHelper.ExtractPropertyName(expression);
            return this;
        }

        public DropDownMapBuilder<TItem> TextField<TProperty>(Expression<Func<TItem, TProperty>> expression)
        {
            Component.DataTextField = ReflectionHelper.ExtractPropertyName(expression); ;
            return this;
        }

        public DropDownMapBuilder<TItem> GroupField<TProperty>(Expression<Func<TItem, TProperty>> expression)
        {
            Component.DataGroupField = ReflectionHelper.ExtractPropertyName(expression); ;
            return this;
        }
    }
}