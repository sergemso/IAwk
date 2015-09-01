using System;

namespace IAwk.IocContainer
{
    public interface IIocContainer : IDisposable
    {
        T Resolve<T>();
        T Resolve<T>(String name);
        Object Resolve(Type type);
        Boolean IsRegistered(Type type);

        void Register<T1, T2>()
            where T2 : T1;

        void Register<T1, T2>(Func<IIocContainer, T2> factory)
            where T2 : T1;

        void Register<T>(Func<IIocContainer, T> factory);
        void Register<T>(Func<IIocContainer, T> factory, String name);
        void Register<T>(Func<IIocContainer, T> factory, Boolean singleton);
        void Register<T>();
        void Register<T>(T instance);
        void Register<T>(T instance, String name);
        IIocContainer CreateScope();
        void Register<T>(Func<T> factory);
    }
}