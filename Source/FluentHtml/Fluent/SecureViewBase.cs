using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace FluentHtml.Fluent
{
    public abstract class SecureViewBase : ViewComponentBase
    {
        protected SecureViewBase(HtmlHelper htmlHelper)
            : base(htmlHelper)
        {
            ReadRoles = new HashSet<string>();
            WriteRoles = new HashSet<string>();
            DeniedClass = "access-denied";
        }

        protected SecureViewBase(ViewContext viewContext)
            : base(viewContext)
        {
            ReadRoles = new HashSet<string>();
            WriteRoles = new HashSet<string>();
            DeniedClass = "access-denied";
        }

        protected SecureViewBase(RequestContext requestContext)
            : base(requestContext)
        {
            ReadRoles = new HashSet<string>();
            WriteRoles = new HashSet<string>();
            DeniedClass = "access-denied";
        }

        public ISet<string> ReadRoles { get; private set; }

        public ISet<string> WriteRoles { get; private set; }

        public string DeniedClass { get; set; }

        public string GrantedClass { get; set; }

        public bool IsSecured()
        {
            return ReadRoles.Count == 0 || WriteRoles.Count == 0;
        }

        public bool CanRead()
        {
            if (ReadRoles.Count == 0 
                || RequestContext == null 
                || RequestContext.HttpContext == null 
                || RequestContext.HttpContext.User == null)
            {
                return true;
            }

            var principal = RequestContext.HttpContext.User;
            return ReadRoles.Any(principal.IsInRole);

        }

        public bool CanWrite()
        {
            if (WriteRoles.Count == 0
                || RequestContext == null
                || RequestContext.HttpContext == null
                || RequestContext.HttpContext.User == null)
            {
                return true;
            }

            var principal = RequestContext.HttpContext.User;
            return WriteRoles.Any(principal.IsInRole);
        }
    }
}