using System;

namespace TiredSiren.Mediator
{
    public interface IMediatorResolver
    {
        public object Resolve(Type type);
        public void Register(Type type, Func<object> factory);
    }
}