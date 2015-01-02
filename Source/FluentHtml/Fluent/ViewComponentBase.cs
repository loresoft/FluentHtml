using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FluentHtml.Fluent
{
    public abstract class ViewComponentBase : ComponentBase, IHtmlString
    {
        private string _name;

        protected ViewComponentBase(HtmlHelper htmlHelper)
        {
            if (htmlHelper == null)
                throw new ArgumentNullException("htmlHelper");

            CssClasses = new HashSet<string>();

            HtmlHelper = htmlHelper;
            ViewContext = htmlHelper.ViewContext;
            RequestContext = htmlHelper.ViewContext.RequestContext;
        }

        protected ViewComponentBase(ViewContext viewContext)
        {
            if (viewContext == null)
                throw new ArgumentNullException("viewContext");

            CssClasses = new HashSet<string>();

            ViewContext = viewContext;
            RequestContext = viewContext.RequestContext;
        }

        protected ViewComponentBase(RequestContext requestContext)
        {
            if (requestContext == null)
                throw new ArgumentNullException("requestContext");

            CssClasses = new HashSet<string>();

            RequestContext = requestContext;
        }

        public HtmlHelper HtmlHelper
        {
            get;
            private set;
        }

        public RequestContext RequestContext
        {
            get;
            private set;
        }

        public ViewContext ViewContext
        {
            get;
            private set;
        }

        public string Name
        {
            get { return _name; }
            set { _name = ViewContext != null ? ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(value) : value; }
        }

        public ISet<string> CssClasses { get; private set; }


        public virtual void VerifySettings()
        {
            if (string.IsNullOrEmpty(Name))
                throw new InvalidOperationException("Name cannot be blank.");

            if (!Name.Contains("<#=") && Name.IndexOf(" ", StringComparison.Ordinal) != -1)
                throw new InvalidOperationException("Name cannot contain spaces.");
        }

        public virtual void SetViewContext(ViewContext viewContext)
        {
            if (viewContext == null)
                throw new ArgumentNullException("viewContext");

            ViewContext = viewContext;
            RequestContext = viewContext.RequestContext;
        }

        public abstract string ToHtmlString();

        public override string ToString()
        {
            return ToHtmlString();
        }

        protected void MergeAttributes(TagBuilder tagBuilder, bool dropNulls = true)
        {
            var attributes = HtmlAttributes;
            MergeAttributes(tagBuilder, attributes, dropNulls);
        }

        protected virtual void MergeAttributes(TagBuilder tagBuilder, IDictionary<string, object> attributes, bool dropNulls = true)
        {
            if (!dropNulls)
            {
                tagBuilder.MergeAttributes(attributes);
                return;
            }

            // create new dictionary without null values
            var localAttributes = new Dictionary<string, object>();

            var keys = attributes
                .Where(v => v.Value != null)
                .Select(v => v.Key);

            foreach (string key in keys)
                localAttributes[key] = attributes[key];

            tagBuilder.MergeAttributes(localAttributes);
        }
    }
}