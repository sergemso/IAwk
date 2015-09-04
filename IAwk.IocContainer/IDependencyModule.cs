using System;

namespace IAwk.IocContainer
{
    public interface IDependencyModule : IDisposable
    {
        void Register(IIocContainer container);
    }
}