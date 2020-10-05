using Flaschenpost.Core.Contracts;
using System;

namespace Microsoft.Extensions.DependencyInjection
{

    public static class DependencyInectionExtensions
    {
        public static IServiceCollection AddModule<TModule>(this IServiceCollection services)
        where TModule : IModule<IServiceCollection>, new()
        {
            var module =  Activator.CreateInstance<TModule>();

            return module.RegisterServices(services);
        }
    }
}
