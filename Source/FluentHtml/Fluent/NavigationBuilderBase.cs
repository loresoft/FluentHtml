using System;
using System.Web.Routing;
using FluentHtml.Extensions;

namespace FluentHtml.Fluent
{
    public abstract class NavigationBuilderBase<TComponent, TBuilder> : BuilderBase<TComponent, TBuilder>
        where TComponent : ComponentBase
        where TBuilder : NavigationBuilderBase<TComponent, TBuilder>
    {
        protected NavigationBuilderBase(TComponent component)
            : base(component)
        {
        }

        protected abstract NavigationRequest Navigation { get; }

        public TBuilder Action(string actionName, string controllerName, RouteValueDictionary routeValues)
        {
            var request = Navigation;
            request.ActionName = actionName;
            request.ControllerName = controllerName;
            request.RouteValues.Merge(routeValues);

            return this as TBuilder;
        }

        public TBuilder Action(string actionName, string controllerName, object routeValues)
        {
            var request = Navigation;
            request.ActionName = actionName;
            request.ControllerName = controllerName;
            request.RouteValues.Merge(routeValues);

            return this as TBuilder;
        }

        public TBuilder Action(string actionName, string controllerName)
        {
            return Action(actionName, controllerName, (object)null);
        }

        public TBuilder Action(string routeName, RouteValueDictionary routeValues)
        {
            var request = Navigation;
            request.RouteName = routeName;
            request.RouteValues.Merge(routeValues);

            return this as TBuilder;
        }

        public TBuilder Action(string routeName, object routeValues)
        {
            var request = Navigation;
            request.RouteName = routeName;
            request.RouteValues.Merge(routeValues);

            return this as TBuilder;
        }

        public TBuilder Action(string routeName)
        {
            return Action(routeName, (object)null);
        }

        public TBuilder Url(string url)
        {
            Navigation.Url = url;
            return this as TBuilder;
        }

    }
}