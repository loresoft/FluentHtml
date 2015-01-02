using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using FluentHtml.Html.DropDown;

namespace FluentHtml
{
    public static class DropDownListExtensions
    {
        public static DropDownListBuilder DropDownList(this FluentHelper helper)
        {
            var htmlHelper = helper.HtmlHelper;
            var dropDownList = new DropDownList(htmlHelper);
            var builder = new DropDownListBuilder(dropDownList);
            return builder;
        }

        public static DropDownListBuilder DropDownListFor<TModel, TProperty>(this FluentHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            var htmlHelper = helper.HtmlHelper;
            var dropDownList = new DropDownList(htmlHelper);

            SetMetadata(htmlHelper, expression, dropDownList);

            var builder = new DropDownListBuilder(dropDownList);
            return builder;
        }

        private static void SetMetadata<TModel, TProperty>(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, DropDownList component)
        {
            component.Name = ExpressionHelper.GetExpressionText(expression);

            // get metadata and selected from model
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            if (metadata == null)
                return;

            component.Metadata = metadata;
            component.SelectedValue = metadata.Model;
        }

    }
}