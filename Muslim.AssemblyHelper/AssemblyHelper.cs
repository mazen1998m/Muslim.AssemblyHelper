using Microsoft.Build.Construction;
using System.Reflection;

namespace Muslim.AssemblyHelper;

public static class AssemblyHelper
{
    public static Assembly GetAssembly(Type type) => Assembly.GetAssembly(type)!;

    public static Assembly GetAssembly(string assemblyName) => Assembly.Load(assemblyName);

    public static List<Assembly> GetAllAssembly()
    {

        var projectName = Assembly.GetExecutingAssembly().FullName!.Split(".").First();

        var solutionFiles = SolutionFile
                .Parse(FindSolutionFile()).ProjectsInOrder
                .Select(x => x.ProjectName)
                .Where(project => project.StartsWith(projectName))
                .ToList();

        var solutionAssemblys = solutionFiles.Select(GetAssembly).ToList();
        return solutionAssemblys;

    }

    private static string FindSolutionFile()
    {
        var currentDirectory = Directory.GetCurrentDirectory();

        // Search for the solution file in the current directory and its parent directories
        string? solutionFilePath = null;
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        while (currentDirectory != null)
        {
            solutionFilePath = Directory.EnumerateFiles(currentDirectory, "*.sln").FirstOrDefault()!;

            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            if (solutionFilePath != null)
                break;

            currentDirectory = Directory.GetParent(currentDirectory)!.FullName;
        }

#pragma warning disable CS8603
        return solutionFilePath;
#pragma warning restore CS8603
    }

    public static List<string> GetAssemblyName()
     => GetAllAssembly().Select(assembly => assembly.GetName().Name!).ToList();

    public static string GetAssemblyName(Type type) => GetAssembly(type).GetName().Name!;

    public static int GetAssemblyNameLength(string assemblyName) => assemblyName.Split(".").Length;

    public static int GetAssemblyNameLength(Type assemblyType) => GetAssemblyName(assemblyType).Split(".").Length;

    public static IEnumerable<Type> GeTypeByName(Assembly assembly, string typeName) => assembly.GetTypes().Where(v => v.FullName!.Contains(typeName));

    public static IEnumerable<Type> GeTypeByName(string typeName)
    {
        List<Type> allTypes = new();
        var allAssembly = GetAllAssembly();
        foreach (
            IEnumerable<Type>? types in allAssembly
                     .Select(assembly => assembly
                         .GetTypes()
                         .Where(v => v.FullName!.Contains(typeName))))
        {
            allTypes.AddRange(types);
        }

        return allTypes;
    }

    public static IEnumerable<Type> GetTypes()
    {
        List<Type> allTypes = new();
        var allAssembly = GetAllAssembly();
        foreach (Type[]? types in allAssembly.Select(assembly => assembly.GetTypes()))
        {
            allTypes.AddRange(types);
        }

        return allTypes;
    }

    public static Type GetType(string typeName)
    {
        Type? type = null;
        foreach (Type? assemblyType
                 in GetAllAssembly().Select(assembly => assembly.GetType(typeName))
                                    .Where(assemblyType => assemblyType != null))
        {
            type = assemblyType;
        }

        return type!;
    }

    public static List<Type> GetConstructors() => (from type in GetTypes().Where(t => t.FullName!.StartsWith(GetAssemblyName(t)))
                                                   from constructor in type.GetConstructors()
                                                   from parameter in constructor.GetParameters()
                                                   select parameter.ParameterType).ToList();

}