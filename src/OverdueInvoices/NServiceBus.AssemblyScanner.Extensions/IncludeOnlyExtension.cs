using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace NServiceBus
{
    public static class IncludeOnlyExtension
    {
        public static AssemblyScannerConfiguration IncludeOnly(this AssemblyScannerConfiguration configuration,
            params string[] assembliesToInclude)
        {
            var excluded = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
                .Select(Path.GetFileNameWithoutExtension)
                .Where(existingAssembly => !assembliesToInclude.Contains(existingAssembly))
                .ToArray();

            configuration.ExcludeAssemblies(excluded);

            return configuration;
        }

        public static AssemblyScannerConfiguration IncludeOnlyThisAssemblyAndReferences(this AssemblyScannerConfiguration configuration)
        {
            var callingAssembly = Assembly.GetCallingAssembly();
            var referencedAssembliesNames = callingAssembly.GetReferencedAssemblies().Select(an => an.Name).ToList();
            referencedAssembliesNames.Add(callingAssembly.GetName().Name);

            return IncludeOnly(configuration, referencedAssembliesNames.ToArray());
        }
    }
}