using System;
using System.Linq.Expressions;
using DryIoc;
using IAwk.IocContainer;

namespace IAwk.DryIocContainer
{
    public class DryIocContainer : IIocContainer
    {
        private readonly Container _container;
        private Boolean _isDisposed;

        public DryIocContainer(Container container)
        {
            _container = container;
        }

        public DryIocContainer()
            : this(new Container())
        {
        }

        private Container Container
        {
            get
            {
                if (_isDisposed)
                {
                    throw new ObjectDisposedException(GetType().Name);
                }

                return _container;
            }
        }

        public void Register<T>(Func<T> factory)
        {
            Factory delegateFactory;
            Expression<Func<T>> ex;

            ex = () => factory();

            delegateFactory = new DelegateFactory(((request1, registry1) => ex));

            _container.Register(delegateFactory, typeof (T), null);
        }

        public T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        public T Resolve<T>(String name)
        {
            return Container.Resolve<T>(name);
        }

        public Object Resolve(Type type)
        {
            return Container.Resolve(type);
        }

        public Boolean IsRegistered(Type type)
        {
            return Container.IsRegistered(type);
        }

        public void Register<T1, T2>() where T2 : T1
        {
            _container.Register<T1, T2>();
        }

        public void Register<T1, T2>(Func<IIocContainer, T2> factory)
            where T2 : T1
        {
            _container.RegisterDelegate<T1>(container => factory(this));
        }

        public void Register<T>(Func<IIocContainer, T> factory)
        {
            _container.RegisterDelegate(container => factory(this));
        }

        public void Register<T>(Func<IIocContainer, T> factory, String name)
        {
            _container.RegisterDelegate(container => factory(this), null, null, name);
        }

        public void Register<T>(Func<IIocContainer, T> factory, Boolean singleton)
        {
            _container.RegisterDelegate(container => factory(this), singleton ? Reuse.Singleton : null);
        }

        public void Register<T>()
        {
            _container.Register<T>();
        }

        public void Register<T>(T instance)
        {
            _container.RegisterInstance(instance);
        }

        public void Register<T>(T instance, String name)
        {
            _container.RegisterInstance(instance, null, name);
        }

        public IIocContainer CreateScope()
        {
            return new DryIocContainer(_container.OpenScope());
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~DryIocContainer()
        {
            Dispose(false);
        }

        public void Register(IDependencyModule module)
        {
            module.Register(this);
        }

        protected virtual void Dispose(Boolean isDisposing)
        {
            if (!_isDisposed)
            {
                if (isDisposing)
                {
                    _container.Dispose();
                }

                _isDisposed = true;
            }
        }
    }
}