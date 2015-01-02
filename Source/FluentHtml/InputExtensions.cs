using System;
using System.Collections;
using System.Linq.Expressions;
using System.Web.Mvc;
using FluentHtml.Html.Input;
using InputType = FluentHtml.Html.Input.InputType;

namespace FluentHtml
{
    public static class InputExtensions
    {
        public static TextBoxBuilder TextBox(this FluentHelper helper)
        {
            var htmlHelper = helper.HtmlHelper;
            var component = new TextBox(htmlHelper);
            var builder = new TextBoxBuilder(component);
            return builder;
        }

        public static TextBoxBuilder TextBoxFor<TModel, TProperty>(this FluentHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            var htmlHelper = helper.HtmlHelper;
            var component = new TextBox(htmlHelper);

            SetMetadata(htmlHelper, expression, component);

            var builder = new TextBoxBuilder(component);
            return builder;
        }


        public static TextAreaBuilder TextArea(this FluentHelper helper)
        {
            var htmlHelper = helper.HtmlHelper;
            var component = new TextArea(htmlHelper);
            var builder = new TextAreaBuilder(component);
            return builder;
        }

        public static TextAreaBuilder TextAreaFor<TModel, TProperty>(this FluentHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            var htmlHelper = helper.HtmlHelper;
            var component = new TextArea(htmlHelper);

            SetMetadata(htmlHelper, expression, component);

            var builder = new TextAreaBuilder(component);
            return builder;
        }


        public static TextBoxBuilder Password(this FluentHelper helper)
        {
            var htmlHelper = helper.HtmlHelper;
            var component = new TextBox(htmlHelper) { InputType = InputType.Password };
            var builder = new TextBoxBuilder(component);
            return builder;
        }

        public static TextBoxBuilder PasswordFor<TModel, TProperty>(this FluentHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            var htmlHelper = helper.HtmlHelper;
            var component = new TextBox(htmlHelper) { InputType = InputType.Password };

            SetMetadata(htmlHelper, expression, component);

            var builder = new TextBoxBuilder(component);
            return builder;
        }


        public static TextBoxBuilder Hidden(this FluentHelper helper)
        {
            var htmlHelper = helper.HtmlHelper;
            var component = new TextBox(htmlHelper) { InputType = InputType.Hidden };
            var builder = new TextBoxBuilder(component);
            return builder;
        }

        public static TextBoxBuilder HiddenFor<TModel, TProperty>(this FluentHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            var htmlHelper = helper.HtmlHelper;
            var component = new TextBox(htmlHelper) { InputType = InputType.Hidden };

            SetMetadata(htmlHelper, expression, component);

            var builder = new TextBoxBuilder(component);
            return builder;
        }


        public static CheckBoxBuilder CheckBox(this FluentHelper helper)
        {
            var htmlHelper = helper.HtmlHelper;
            var component = new CheckBox(htmlHelper);
            var builder = new CheckBoxBuilder(component);
            return builder;
        }

        public static CheckBoxBuilder CheckBoxFor<TModel>(this FluentHelper<TModel> helper, Expression<Func<TModel, bool>> expression)
        {
            var htmlHelper = helper.HtmlHelper;
            var component = new CheckBox(htmlHelper);

            SetMetadata(htmlHelper, expression, component);

            if (component.Value is bool)
                component.Checked = Convert.ToBoolean(component.Value);

            var builder = new CheckBoxBuilder(component);
            return builder;
        }

        public static CheckBoxBuilder CheckBoxFor<TModel>(this FluentHelper<TModel> helper, Expression<Func<TModel, bool?>> expression)
        {
            var htmlHelper = helper.HtmlHelper;
            var component = new CheckBox(htmlHelper);

            SetMetadata(htmlHelper, expression, component);

            if (component.Value is bool?)
                component.Checked = Convert.ToBoolean(component.Value);

            var builder = new CheckBoxBuilder(component);
            return builder;
        }


        public static SliderCheckBoxBuilder SliderCheckBox(this FluentHelper helper)
        {
            var htmlHelper = helper.HtmlHelper;
            var component = new SliderCheckBox(htmlHelper) { IncludeHidden = false };
            var builder = new SliderCheckBoxBuilder(component);
            return builder;
        }

        public static SliderCheckBoxBuilder SliderCheckBoxFor<TModel>(this FluentHelper<TModel> helper, Expression<Func<TModel, bool>> expression)
        {
            var htmlHelper = helper.HtmlHelper;
            var component = new SliderCheckBox(htmlHelper) { IncludeHidden = false };

            SetMetadata(htmlHelper, expression, component);

            if (component.Value is bool)
                component.Checked = Convert.ToBoolean(component.Value);

            var builder = new SliderCheckBoxBuilder(component);
            return builder;
        }

        public static SliderCheckBoxBuilder SliderCheckBoxFor<TModel>(this FluentHelper<TModel> helper, Expression<Func<TModel, bool?>> expression)
        {
            var htmlHelper = helper.HtmlHelper;
            var component = new SliderCheckBox(htmlHelper) { IncludeHidden = false };

            SetMetadata(htmlHelper, expression, component);

            if (component.Value is bool?)
                component.Checked = Convert.ToBoolean(component.Value);

            var builder = new SliderCheckBoxBuilder(component);
            return builder;
        }


        public static RadioButtonBuilder RadioButton(this FluentHelper helper)
        {
            var htmlHelper = helper.HtmlHelper;
            var component = new RadioButton(htmlHelper);
            var builder = new RadioButtonBuilder(component);
            return builder;
        }

        public static RadioButtonBuilder RadioButtonFor<TModel, TProperty>(this FluentHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            var htmlHelper = helper.HtmlHelper;
            var component = new RadioButton(htmlHelper);

            SetMetadata(htmlHelper, expression, component);

            var builder = new RadioButtonBuilder(component);
            return builder;
        }


        public static InputListBuilder CheckBoxList(this FluentHelper helper)
        {
            var htmlHelper = helper.HtmlHelper;
            var component = new InputList(htmlHelper)
            {
                InputType = InputType.Checkbox,
                ListCssClass = "checkbox-list"
            };

            var builder = new InputListBuilder(component);
            return builder;
        }

        public static InputListBuilder CheckBoxListFor<TModel, TProperty>(this FluentHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
            where TProperty : IList
        {
            var htmlHelper = helper.HtmlHelper;
            var component = new InputList(htmlHelper)
            {
                InputType = InputType.Checkbox,
                ListCssClass = "checkbox-list"
            };

            component.Name = ExpressionHelper.GetExpressionText(expression);

            // get metadata and selected from model
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            if (metadata != null)
                component.SelectedValues = metadata.Model as IList;

            var builder = new InputListBuilder(component);
            return builder;
        }

        public static InputListBuilder RadioButtonList(this FluentHelper helper)
        {
            var htmlHelper = helper.HtmlHelper;
            var component = new InputList(htmlHelper)
            {
                InputType = InputType.Radio,
                ListCssClass = "radio-list"
            };

            var builder = new InputListBuilder(component);
            return builder;
        }

        public static InputListBuilder RadioButtonListFor<TModel, TProperty>(this FluentHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
            where TProperty : IList
        {
            var htmlHelper = helper.HtmlHelper;
            var component = new InputList(htmlHelper)
            {
                InputType = InputType.Radio,
                ListCssClass = "radio-list"
            };

            component.Name = ExpressionHelper.GetExpressionText(expression);

            // get metadata and selected from model
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            if (metadata != null)
                component.SelectedValues = metadata.Model as IList;

            var builder = new InputListBuilder(component);
            return builder;
        }


        private static void SetMetadata<TModel, TProperty>(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, InputBase component)
        {
            component.Name = ExpressionHelper.GetExpressionText(expression);

            // get metadata and selected from model
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            if (metadata == null)
                return;

            component.Metadata = metadata;
            component.Value = metadata.Model;
        }
    }
}
