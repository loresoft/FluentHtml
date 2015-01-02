using System;
using System.Collections.Generic;
using FluentHtml.Extensions;

namespace FluentHtml.Fluent
{
    public class SecureRoleBuilder
        : BuilderBase<SecureViewBase, SecureRoleBuilder>
    {
        public SecureRoleBuilder(SecureViewBase component)
            : base(component)
        {
        }

        public SecureRoleBuilder GrantedClass(string cssClass)
        {
            Component.GrantedClass = cssClass;
            return this;
        }

        public SecureRoleBuilder DeniedClass(string cssClass)
        {
            Component.DeniedClass = cssClass;
            return this;
        }

        
        public SecureRoleBuilder ReadRole(params string[] roles)
        {
            if (roles != null)
                Component.ReadRoles.AddRange(roles);

            return this;
        }

        public SecureRoleBuilder ReadRoleViewData(string viewDataName)
        {
            return RoleFromViewData(viewDataName, Component.ReadRoles);
        }

        
        public SecureRoleBuilder WriteRole(params string[] roles)
        {
            if (roles != null)
                Component.WriteRoles.AddRange(roles);

            return this;
        }

        public SecureRoleBuilder WriteRoleViewData(string viewDataName)
        {
            return RoleFromViewData(viewDataName, Component.WriteRoles);
        }

        protected virtual SecureRoleBuilder RoleFromViewData(string viewDataName, ISet<string> set)
        {
            object value;
            if (!Component.ViewContext.ViewData.TryGetValue(viewDataName, out value))
                return this;

            var roles = value as IEnumerable<string>;
            if (roles == null)
                return this;

            set.AddRange(roles);
            return this;
        }

    }
}