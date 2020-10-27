//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Playground.Core.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;

//namespace ApiPlayground.Infrastructure
//{
//    public static class AddAllModuleLayersExtension
//    {
//        public static void AddAllModuleLayers(this IServiceCollection services, IConfiguration configuration)
//        {
//            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
//            List<IStartupSetup> moduleInDomains = new List<IStartupSetup>();

//            foreach (Assembly assembly in assemblies)
//            {
//                if (assembly.FullName.Contains("Aws", StringComparison.OrdinalIgnoreCase))
//                {
//                    Console.WriteLine("kk");
//                }

//                foreach (Type type in assembly.GetTypes())
//                {
//                    if (typeof(IStartupSetup).IsAssignableFrom(type))
//                    {
//                        Console.WriteLine("here");
//                    }

//                    if (type.Name.Contains("StartupSetup", StringComparison.OrdinalIgnoreCase))
//                    {
//                        Console.WriteLine("wtf");
//                    }

//                    if (IsValidModuleTypeForDependencyInjection(type))
//                        moduleInDomains.Add(type as IStartupSetup);
//                }
//            }

//            foreach (IStartupSetup module in moduleInDomains)
//            {
//                module.AddDependenciesForLayer(services, configuration);
//            }

//            //IEnumerable<Type> moduleTypesInDomain = assemblies
//            //    .Select(assembly => assembly.GetTypes())
//            //    .SelectMany(typeCollection => typeCollection)
//            //    .Where(IsValidModuleTypeForDependencyInjection);
//        }

//        private static bool IsValidModuleTypeForDependencyInjection(Type type)
//        {
//            return type.IsClass && type.IsPublic && typeof(IStartupSetup).IsAssignableFrom(type);
//        }
//    }
//}
