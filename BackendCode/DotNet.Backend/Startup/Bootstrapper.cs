using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("DotNet.WPF")]
namespace DotNet.Backend.Startup
{
    public class Bootstrapper
    {
        /// <summary>
        /// Bootstrap the dependency injection container.
        /// </summary>
        /// <returns>An instance of the dependency injection container.</returns>
        public IContainer ContainerBootstrap()
        {
            // Create a ContainerBuilder to configure dependency injection.
            ContainerBuilder builder = new ContainerBuilder();

            // You can register your dependencies here using builder.RegisterType.

            // Example: Register a dependency as an interface.
            // builder.RegisterType<DependencyClass>().As<IDependencyClass>();

            // Example: Register a dependency with parameters.
            // builder.RegisterType<DependencyClass>().WithParameters(/* Parameters */).As<IDependencyClass>();

            // Example: Register a dependency with a lambda expression.
            // builder.Register(c => new DependencyClassInstance(/* Parameters */)).As<IDependencyClass>();

            // Build and return the configured dependency injection container.
            return builder.Build();
        }
    }

}
