using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Playground.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ApiPlayground.Infrastructure
{
    public static class AddAllModuleLayersExtension
    {
        public static void AddAllModuleLayers(this IServiceCollection services, IConfiguration configuration)
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            List<Type> moduleTypesInDomain = new List<Type>();

            foreach (Assembly assembly in assemblies)
            {

                foreach (Type type in assembly.GetTypes())
                {
                    if (typeof(IStartupSetup).IsAssignableFrom(type))
                    {
                        Console.WriteLine("here");
                    }

                    if(type.Name.Contains("StartupSetup", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("wtf");
                    }

                    if (IsValidModuleTypeForDependencyInjection(type))
                        moduleTypesInDomain.Add(type);
                }
            }

            //IEnumerable<Type> moduleTypesInDomain = assemblies
            //    .Select(assembly => assembly.GetTypes())
            //    .SelectMany(typeCollection => typeCollection)
            //    .Where(IsValidModuleTypeForDependencyInjection);

            Console.WriteLine("test");
        }

        private static bool IsValidModuleTypeForDependencyInjection(Type type)
        {
            return type.IsClass && type.IsPublic && typeof(IStartupSetup).IsAssignableFrom(type);
        }
    }
}
