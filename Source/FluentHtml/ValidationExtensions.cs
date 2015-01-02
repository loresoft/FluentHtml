using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using FluentHtml.Html.Validation;

namespace FluentHtml
{
    public static class ValidationExtensions
    {
        public static ValidationMessageBuilder ValidationImage(this FluentHelper helper)
        {
            var htmlHelper = helper.HtmlHelper;
            var component = new ValidationMessage(htmlHelper) { IconOnly = true };

            var builder = new ValidationMessageBuilder(component);
            return builder;
        }

        public static ValidationMessageBuilder ValidationImageFor<TModel, TProperty>(this FluentHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {

            var htmlHelper = helper.HtmlHelper;
            var component = new ValidationMessage(htmlHelper) { IconOnly = true };

            component.ModelName = ExpressionHelper.GetExpressionText(expression);

            // get metadata and selected from model
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            component.Metadata = metadata;
 
            var builder = new ValidationMessageBuilder(component);
            return builder;
        }


        public static ValidationMessageBuilder ValidationMessage(this FluentHelper helper)
        {
            var htmlHelper = helper.HtmlHelper;
            var component = new ValidationMessage(htmlHelper);

            var builder = new ValidationMessageBuilder(component);
            return builder;
        }

        public static ValidationMessageBuilder ValidationMessageFor<TModel, TProperty>(this FluentHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            var htmlHelper = helper.HtmlHelper;
            var component = new ValidationMessage(htmlHelper);

            component.ModelName = ExpressionHelper.GetExpressionText(expression);

            // get metadata and selected from model
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            component.Metadata = metadata;

            var builder = new ValidationMessageBuilder(component);
            return builder;
        }

    }
}
