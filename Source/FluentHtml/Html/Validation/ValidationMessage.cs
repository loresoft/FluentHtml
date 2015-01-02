using System;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FluentHtml.Extensions;
using FluentHtml.Fluent;

namespace FluentHtml.Html.Validation
{
    public class ValidationMessage : ViewComponentBase
    {
        public ValidationMessage(HtmlHelper htmlHelper)
            : base(htmlHelper)
        {
            ValidationErrorCssClassName = HtmlHelper.ValidationMessageCssClassName;
            ValidationValidCssClassName = HtmlHelper.ValidationMessageValidCssClassName;
        }

        public ModelMetadata Metadata { get; set; }

        public string ModelName { get; set; }

        public string Message { get; set; }

        public string ValidationErrorCssClassName { get; set; }

        public string ValidationValidCssClassName { get; set; }

        public bool IconOnly { get; set; }


        public override string ToHtmlString()
        {
            string modelName = Name;
            FormContext formContext = GetFormContextForClientValidation(ViewContext);

            if (!HtmlHelper.ViewData.ModelState.ContainsKey(modelName) && formContext == null)
                return null;

            ModelState modelState = HtmlHelper.ViewData.ModelState[modelName];
            ModelErrorCollection modelErrorCollection = modelState == null ? null : modelState.Errors;
            ModelError error = (modelErrorCollection == null || modelErrorCollection.Count == 0)
                ? null : modelErrorCollection.FirstOrDefault(m => !string.IsNullOrEmpty(m.ErrorMessage)) ?? modelErrorCollection[0];

            if (error == null && formContext == null)
                return null;

            var tagBuilder = new TagBuilder("span");
            tagBuilder.MergeAttributes(HtmlAttributes);
            tagBuilder.AddCssClass(error != null ? ValidationErrorCssClassName : ValidationValidCssClassName);

            // add name and id
            if (Name.HasValue())
            {
                string fullName = Name;
                tagBuilder.MergeAttribute("name", fullName, true);
                tagBuilder.GenerateId(fullName);
            }

            foreach (string cssClass in CssClasses)
                tagBuilder.AddCssClass(cssClass);

            string message = Message;

            if (string.IsNullOrEmpty(message) && error != null)
                message = GetUserErrorMessageOrDefault(ViewContext.HttpContext, error, modelState);

            if (message.HasValue())
            {
                tagBuilder.MergeAttribute("title", message, true);
                
                if (!IconOnly)
                    tagBuilder.SetInnerText(Message);
            }

            if (formContext == null)
                return tagBuilder.ToString(TagRenderMode.Normal);
            
            bool replaceValidationMessageContents = string.IsNullOrEmpty(Message);
            if (ViewContext.UnobtrusiveJavaScriptEnabled)
            {
                tagBuilder.MergeAttribute("data-valmsg-for", modelName);
                tagBuilder.MergeAttribute("data-valmsg-replace", replaceValidationMessageContents.ToString(CultureInfo.InvariantCulture).ToLowerInvariant());
            }
            else
            {
                FieldValidationMetadata validationMetadata = ApplyFieldValidationMetadata(HtmlHelper, Metadata, modelName);
                // rules will already have been written to the metadata object
                validationMetadata.ReplaceValidationMessageContents = replaceValidationMessageContents;
                    
                // client validation always requires an ID
                tagBuilder.GenerateId(modelName + "_validationMessage");
                validationMetadata.ValidationMessageId = tagBuilder.Attributes["id"];
            }

            return tagBuilder.ToString(TagRenderMode.Normal);
        }


        private static FormContext GetFormContextForClientValidation(ViewContext viewContext)
        {
            return !viewContext.ClientValidationEnabled ? null : viewContext.FormContext;
        }

        private static FieldValidationMetadata ApplyFieldValidationMetadata(HtmlHelper htmlHelper, ModelMetadata modelMetadata, string modelName)
        {
            FieldValidationMetadata metadataForField = htmlHelper.ViewContext.FormContext.GetValidationMetadataForField(modelName, true);
            var modelClientValidationRules = ModelValidatorProviders.Providers.GetValidators(modelMetadata, htmlHelper.ViewContext).SelectMany(v => v.GetClientValidationRules());
            foreach (ModelClientValidationRule clientValidationRule in modelClientValidationRules)
                metadataForField.ValidationRules.Add(clientValidationRule);

            return metadataForField;
        }

        private static string GetInvalidPropertyValueResource(HttpContextBase httpContext)
        {
            string message = null;
            if (!string.IsNullOrEmpty(System.Web.Mvc.Html.ValidationExtensions.ResourceClassKey) && httpContext != null)
                message = httpContext.GetGlobalResourceObject(System.Web.Mvc.Html.ValidationExtensions.ResourceClassKey, "InvalidPropertyValue", CultureInfo.CurrentUICulture) as string;

            return message ?? "The value '{0}' is invalid.";
        }

        private static string GetUserErrorMessageOrDefault(HttpContextBase httpContext, ModelError error, ModelState modelState)
        {
            if (!string.IsNullOrEmpty(error.ErrorMessage))
                return error.ErrorMessage;
            if (modelState == null)
                return null;

            string str = modelState.Value != null ? modelState.Value.AttemptedValue : null;
            return string.Format(CultureInfo.CurrentCulture, GetInvalidPropertyValueResource(httpContext), new object[] { str });
        }
    }
}
