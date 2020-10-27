using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Playground.Core.Interfaces;
using System;
using System.Linq;
using System.Reflection;

namespace Playground.Core.Utility
{
    public static class AddAllModuleLayersExtension
    {
        private static string interfaceMethodName = typeof(IStartupSetup).GetMethods().FirstOrDefault().Name;

        public static void AddAllModuleLayers(this IServiceCollection services, IConfiguration configuration)
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (Assembly assembly in assemblies)
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (IsValidModuleTypeForDependencyInjection(type))
                    {
                        MethodInfo methodInfo = type.GetMethods().FirstOrDefault(m => m.Name == interfaceMethodName);
                        object concreteInstance = Activator.CreateInstance(type);
                        object[] parameters = new object[] { services, configuration };
                        methodInfo.Invoke(concreteInstance, parameters);
                    }
                }
            }
        }

        private static bool IsValidModuleTypeForDependencyInjection(Type type)
        {
            return type.IsClass && type.IsPublic && typeof(IStartupSetup).IsAssignableFrom(type);
        }
    }
}