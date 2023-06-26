# Assembly Helper Utility

The Assembly Helper Utility is a .NET utility class that provides convenient methods for working with assemblies. It simplifies tasks such as retrieving assembly information, finding solution files, and getting types from assemblies.

## Features

- `GetAssembly(Type type)`: Retrieves the assembly that contains the specified type.
- `GetAssembly(string assemblyName)`: Loads the assembly with the given name.
- `GetAllAssembly()`: Returns a list of all assemblies in the solution.
- `GetAssemblyName()`: Retrieves the names of all assemblies in the solution.
- `GetAssemblyName(Type type)`: Gets the name of the assembly that contains the specified type.
- `GetAssemblyNameLength(string assemblyName)`: Returns the length of the assembly name (number of segments separated by dots).
- `GetAssemblyNameLength(Type assemblyType)`: Gets the length of the assembly name that contains the specified type.
- `GeTypeByName(Assembly assembly, string typeName)`: Retrieves types from the specified assembly that contain the given type name.
- `GeTypeByName(string typeName)`: Returns types from all assemblies in the solution that contain the given type name.
- `GetTypes()`: Retrieves all types from all assemblies in the solution.
- `GetType(string typeName)`: Gets the specified type from the assemblies in the solution.
- `GetConstructors()`: Returns a list of constructor parameter types from the types in the assemblies.

## Usage

To use the Assembly Helper Utility in your .NET project, make sure to include the following namespaces at the beginning of your code file:

```csharp
using Microsoft.Build.Construction;
using System.Reflection;
```

## Example:

```csharp
using AssemblyServices;
using Microsoft.Build.Construction;
using System.Reflection;

// Retrieve the assembly containing a specific type
Assembly assembly = AssemblyServices.GetAssembly(typeof(MyType));

// Retrieve all assembly names in the solution
List<string> assemblyNames = AssemblyServices.GetAssemblyName();

// Get types from all assemblies containing a specific type name
IEnumerable<Type> types = AssemblyServices.GeTypeByName("MyType");

// Retrieve all types in the solution
IEnumerable<Type> allTypes = AssemblyServices.GetTypes();

// ...and more

```

## Contributing

Contributions are welcome! If you have any suggestions, bug reports, or feature requests, please feel free to open an issue or submit a pull request.

## License

This utility class is licensed under the MIT License.
