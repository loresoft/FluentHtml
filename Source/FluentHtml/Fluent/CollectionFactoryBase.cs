using System;
using System.Collections.Generic;

namespace FluentHtml.Fluent
{
    public abstract class CollectionFactoryBase<TComponent, TBuilder> 
        where TBuilder : BuilderBase<TComponent, TBuilder> 
        where TComponent : ComponentBase
    {
        protected CollectionFactoryBase(ICollection<TComponent> collection)
        {
            Collection = collection;
        }

        protected ICollection<TComponent> Collection { get; private set; }

        public abstract TBuilder Add();
    }
}