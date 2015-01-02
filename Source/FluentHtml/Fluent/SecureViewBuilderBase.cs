using System;
using System.Collections.Generic;
using FluentHtml.Extensions;

namespace FluentHtml.Fluent
{
    public abstract class SecureViewBuilderBase<TView, TBuilder>
        : ViewComponentBuilderBase<TView, TBuilder>
        where TView : SecureViewBase
        where TBuilder : SecureViewBuilderBase<TView, TBuilder>
    {
        protected SecureViewBuilderBase(TView component)
            : base(component)
        {
        }

        public TBuilder Secure(params string[] writeRoles)
        {
            Component.WriteRoles.AddRange(writeRoles);
            return this as TBuilder;
        }

        public TBuilder Secure(IEnumerable<string> writeRoles)
        {
            if (writeRoles != null)
                Component.WriteRoles.AddRange(writeRoles);

            return this as TBuilder;
        }
        
        public TBuilder Secure(Action<SecureRoleBuilder> configurator)
        {
            var builder = new SecureRoleBuilder(Component);
            configurator(builder);

            return this as TBuilder;
        }
    }
}