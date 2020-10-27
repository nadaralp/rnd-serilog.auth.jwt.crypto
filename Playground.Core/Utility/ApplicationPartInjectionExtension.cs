using Microsoft.Extensions.DependencyInjection;
using Playground.Core.Interfaces;
using System;
using System.Reflection;

namespace Playground.Core.Utility
{
    public static class ApplicationPartInjectionExtension
    {
        public static IMvcBuilder AddAllDomainApplicationParts(this IMvcBuilder services)
        {
            Assembly[] domainAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (Assembly assembly in domainAssemblies)
            {
                bool assemblyAssigned = false;
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.IsClass && type.IsAbstract && typeof(IApplicationPartInjection).IsAssignableFrom(type))
                    {
                        services.AddApplicationPart(assembly);
                        assemblyAssigned = true;
                    }

                    if (assemblyAssigned) break;
                }
            }

            return services;
        }
    }
}
