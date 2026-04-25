using System;
using System.Collections.Generic;
using VContainer;

namespace TiredSiren.Mediator
{
    public class MediatorResolver : IMediatorResolver
    {
        private readonly Dictionary<Type, Func<object>> _factories = new();
        private readonly IObjectResolver _resolver;
        
        public MediatorResolver(IObjectResolver resolver)
        {
            _resolver = resolver;
        }
        
        public object Resolve(Type type)
        {
            return _factories.TryGetValue(type, out var factory) ? factory() : _resolver.Resolve(type);
        }

        public void Register(Type type, Func<object> factory)
        {
            _factories.Add(type, factory);
        }
        
    }
}