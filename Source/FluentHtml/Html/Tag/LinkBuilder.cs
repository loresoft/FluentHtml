using System;
using System.Web.Routing;
using FluentHtml.Extensions;
using FluentHtml.Fluent;

namespace FluentHtml.Html.Tag
{
    public class LinkBuilder : SecureViewBuilderBase<Link, LinkBuilder>
    {
        public LinkBuilder(Link component)
            : base(component)
        {
        }


        public LinkBuilder Action(string actionName, string controllerName, RouteValueDictionary routeValues)
        {
            var request = Component.Navigation;
            request.ActionName = actionName;
            request.ControllerName = controllerName;
            request.RouteValues.Merge(routeValues);

            return this;
        }

        public LinkBuilder Action(string actionName, string controllerName, object routeValues)
        {
            var request = Component.Navigation;
            request.ActionName = actionName;
            request.ControllerName = controllerName;
            request.RouteValues.Merge(routeValues);

            return this;
        }

        public LinkBuilder Action(string actionName, string controllerName)
        {
            return Action(actionName, controllerName, (object)null);
        }

        public LinkBuilder Action(string routeName, RouteValueDictionary routeValues)
        {
            var request = Component.Navigation;
            request.RouteName = routeName;
            request.RouteValues.Merge(routeValues);

            return this;
        }

        public LinkBuilder Action(string routeName, object routeValues)
        {
            var request = Component.Navigation;
            request.RouteName = routeName;
            request.RouteValues.Merge(routeValues);

            return this;
        }

        public LinkBuilder Action(string routeName)
        {
            return Action(routeName, (object)null);
        }


        public LinkBuilder Url(string url)
        {
            Component.Navigation.Url = url;
            return this;
        }


        public LinkBuilder Encode(bool value = true)
        {
            Component.Encode = value;
            return this;
        }

        public LinkBuilder Text(string text)
        {
            Component.Text = text;
            return this;
        }

        public LinkBuilder Html(string html)
        {
            Component.Encode = false;
            Component.Text = html;
            return this;
        }

        public LinkBuilder Target(string target)
        {
            const string name = "target";

            if (target.HasValue())
                HtmlAttribute(name, target);
            else
                Component.HtmlAttributes.Remove(name);

            return this;
        }

        public LinkBuilder Disabled(bool disabled = true)
        {
            const string name = "disabled";

            if (disabled)
                HtmlAttribute(name, name);
            else
                Component.HtmlAttributes.Remove(name);

            return this;
        }
    }
}