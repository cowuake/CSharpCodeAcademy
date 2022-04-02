using Ninject;
using System;

namespace HelpDesk.Core.ArchitecturalUtilities
{
    public static class DependencyInjector
    {
        private static readonly Lazy<IKernel> _kernel =
            new Lazy<IKernel>(() => new StandardKernel());

        public static IKernel Kernel
        {
            get
            {
                return new StandardKernel();
            }
        }

        public static TInterface Resolve<TInterface>()
            => _kernel.Value.Get<TInterface>();

        public static void Register<TRepositoryInterface, TRepositoryImplementation>()
            where TRepositoryImplementation : class, TRepositoryInterface, new()
        {
            Ninject.Syntax.IBindingToSyntax<TRepositoryInterface> bindingToSyntax =
                _kernel.Value.Rebind<TRepositoryInterface>();

            Ninject.Syntax.IBindingWhenInNamedWithOrOnSyntax<TRepositoryImplementation> bindingOnSyntax =
                bindingToSyntax.To<TRepositoryImplementation>();

            bindingOnSyntax.InTransientScope();
        }

        public static void Unregister<TInterface>()
            => _kernel.Value.Unbind<TInterface>();
    }
}