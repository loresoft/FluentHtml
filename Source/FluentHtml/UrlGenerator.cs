using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using FluentHtml.Extensions;
using FluentHtml.Fluent;

namespace FluentHtml
{
    public static class UrlGenerator
    {
        public static string Generate(RequestContext requestContext, string url)
        {
            return new UrlHelper(requestContext).Content(url);
        }

        public static string Generate(RequestContext requestContext, NavigationRequest navigationItem, RouteValueDictionary routeValues)
        {
            if (requestContext == null)
                throw new ArgumentNullException("requestContext");
            if (navigationItem == null)
                throw new ArgumentNullException("navigationItem");

            var urlHelper = new UrlHelper(requestContext);
            string generatedUrl = null;

            if (!string.IsNullOrEmpty(navigationItem.RouteName))
            {
                generatedUrl = urlHelper.RouteUrl(navigationItem.RouteName, routeValues);
            }
            else if (!string.IsNullOrEmpty(navigationItem.ControllerName) && !string.IsNullOrEmpty(navigationItem.ActionName))
            {
                generatedUrl = urlHelper.Action(navigationItem.ActionName, navigationItem.ControllerName, routeValues, null, null);
            }
            else if (!string.IsNullOrEmpty(navigationItem.Url))
            {
                generatedUrl = navigationItem.Url.StartsWith("~/", StringComparison.Ordinal) 
                    ? urlHelper.Content(navigationItem.Url) 
                    : navigationItem.Url;
            }
            else if (routeValues.Any())
            {
                generatedUrl = urlHelper.RouteUrl(routeValues);
            }

            return generatedUrl;

        }

        public static string Generate(RequestContext requestContext, NavigationRequest navigationItem)
        {
            var routeValues = new RouteValueDictionary();

            if (navigationItem.RouteValues.Any())
                routeValues.Merge(navigationItem.RouteValues);

            return Generate(requestContext, navigationItem, routeValues);
        }
    }
}
