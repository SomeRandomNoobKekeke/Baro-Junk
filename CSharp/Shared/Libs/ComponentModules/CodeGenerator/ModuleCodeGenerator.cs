using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

using Barotrauma;
using Microsoft.Xna.Framework;
using System.IO;
using CsCodeGenerator;
using CsCodeGenerator.Enums;

namespace BaroJunk
{

  /// <summary>
  /// First dirty working implementation
  /// </summary>
  public static class ModuleCodeGenerator
  {
    static ModuleCodeGenerator()
    {
      PluginCommands.Add("deletegeneratedfiles", (args) => DeleteGeneratedFiles());
      PluginCommands.Add("printgeneratedfiles", (args) => PrintGeneratedFilePaths());
    }

    public static List<string> GeneratedFilePaths = new();
    public static void DeleteGeneratedFiles()
    {
      foreach (string path in GeneratedFilePaths)
      {
        File.Delete(path);
      }
    }

    public static void PrintGeneratedFilePaths()
    {
      foreach (string path in GeneratedFilePaths)
      {
        Logger.Default.Log($"-> {path}");
      }
    }

    public static List<string> DefaultUsings = new()
    {
      "System;",
      "System.Collections.Generic;",
      "System.Linq;",
    };

    public static string CSExtension = "cs";

    /// <summary>
    /// Super slow
    /// </summary>
    public static void GenerateForAll()
    {
      foreach (Type T in Assembly.GetExecutingAssembly().GetTypes())
      {
        if (T.IsAssignableTo(typeof(IComponent)))
        {
          if (T.GetCustomAttribute<GeneratedComponentAttribute>() != null)
          {
            GenerateFor(T);
          }
        }
      }
    }

    public static void GenerateFor<T>() => GenerateFor(typeof(T));
    public static void GenerateFor(Type T)
    {
      if (!T.IsAssignableTo(typeof(IComponent))) throw new ArgumentException("It's not a component");
      GeneratedComponentAttribute attribute = T.GetCustomAttribute<GeneratedComponentAttribute>();

      if (attribute is null) throw new Exception("Add GeneratedComponent attribute pls");

      string componentDir = Path.GetDirectoryName(attribute.FilePath);
      string outputDir = Path.Combine(componentDir, attribute.OutputDirName);

      Generate(T, outputDir);
    }


    private static void Generate(Type T, string outputDir)
    {
      if (!Directory.Exists(outputDir))
      {
        Directory.CreateDirectory(outputDir);
      }

      ComponentInfo info = ComponentInfo.GetFor(T);

      List<Property> forwardedProps = Generate_ForwardedProps(info).ToList();
      List<Method> forwardedMethods = Generate_ForwardedMethods(info).ToList();

      CsGenerator csGenerator = new CsGenerator()
      {
        OutputDirectory = outputDir,
        Files = new List<FileModel>(){
          new FileModel($"{T.Name}.InjectModules")
          {
            UsingDirectives = DefaultUsings,
            Namespace = T.Namespace,
            Extension = CSExtension,
            Classes = new List<ClassModel>()
            {
              NestIfNeeded(T,
                new ClassModel(T.Name){
                  SingleKeyWord  = KeyWord.Partial,
                  Methods = new List<Method>(){
                    new Method(BuiltInDataType.Void, "InjectModules"){
                      BodyLines = new List<string>(){
                        "InjectModuleHost();",
                        "InjectModuleDependencies();",
                      },
                      AccessModifier = AccessModifier.Private,
                      // SingleKeyWord = KeyWord.Partial,
                    },
                    new Method(BuiltInDataType.Void, "InjectModuleHost"){
                      BodyLines = Generate_InjectModuleHost(info).ToList(),
                      AccessModifier = AccessModifier.Private,
                      // SingleKeyWord = KeyWord.Partial,
                    },
                    new Method(BuiltInDataType.Void, "InjectModuleDependencies"){
                      BodyLines = Generate_InjectModuleDependencies(info).ToList(),
                      AccessModifier = AccessModifier.Private,
                      // SingleKeyWord = KeyWord.Partial,
                    }
                  }
                }
              )
            }
          },
        }
      };

      if (forwardedProps.Count > 0 || forwardedMethods.Count > 0)
      {
        csGenerator.Files.Add(
          new FileModel($"{T.Name}.Forwarded")
          {
            UsingDirectives = DefaultUsings,
            Namespace = T.Namespace,
            Extension = CSExtension,
            Classes = new List<ClassModel>()
            {
              NestIfNeeded(T,
                new ClassModel(T.Name){
                  SingleKeyWord  = KeyWord.Partial,
                  Properties = forwardedProps,
                  Methods = forwardedMethods,
                }
              )
            }
          }
        );
      }

      GeneratedFilePaths.Add(Path.Combine(outputDir, $"{T.Name}.Forwarded.{CSExtension}"));
      GeneratedFilePaths.Add(Path.Combine(outputDir, $"{T.Name}.InjectModules.{CSExtension}"));

      csGenerator.CreateFiles();
    }

    private static string GetClearTypeName(Type T)
    {
      if (T == typeof(void)) return "void";
      return T.Name;
    }
    private static IEnumerable<Method> Generate_ForwardedMethods(ComponentInfo componentInfo)
    {
      foreach (ModuleInfo module in componentInfo.AllModules)
      {
        foreach (ForwardedMethodInfo info in module.ForwardedMethods)
        {
          Method method = new Method(GetClearTypeName(info.Method.ReturnType), info.Method.Name)
          {
            Parameters = info.Method.GetParameters().Select(pi => new Parameter(
              GetClearTypeName(pi.ParameterType),
              pi.Name,
              pi.ParameterType == typeof(string) ? $"\"{pi.DefaultValue}\"" : pi.DefaultValue.ToString()
            )).ToList(),
            BodyLines = new List<string>(){
              $"{module.StringPath}.{info.Method.Name}({String.Join(", ",info.Method.GetParameters().Select(pi=>pi.Name))});"
            }

          };

          yield return method;
        }

      }
    }

    private static IEnumerable<Property> Generate_ForwardedProps(ComponentInfo componentInfo)
    {
      foreach (ModuleInfo module in componentInfo.AllModules)
      {
        foreach (ForwardedPropInfo info in module.ForwardedProps)
        {
          Property prop = new Property(info.Type.Name, info.Name)
          {
            IsAutoImplemented = false,
            // IsGetOnly = info.Access == PropAccess.CanRead, // It's cursed
          };

          if ((info.Access & PropAccess.CanRead) != 0)
          {
            prop.GetterBody = $"{module.StringPath}.{info.Property.Name}";
          }

          if ((info.Access & PropAccess.CanWrite) != 0)
          {
            prop.SetterBody = $"{module.StringPath}.{info.Property.Name} = value";
          }

          yield return prop;
        }

      }
    }

    //TODO what if nested class is private? mb i should track DeclaringTypes only to component level
    private static ClassModel NestIfNeeded(Type T, ClassModel classModel)
    {
      if (T.DeclaringType != null)
      {
        ClassModel declaring = new ClassModel(T.DeclaringType.Name)
        {
          SingleKeyWord = KeyWord.Partial,
          NestedClasses = new List<ClassModel>(){
            classModel
          }
        };

        return NestIfNeeded(T.DeclaringType, declaring);
      }

      return classModel;
    }

    private static IEnumerable<string> Generate_InjectModuleHost(ComponentInfo componentInfo)
    {
      foreach (ModuleInfo module in componentInfo.AllModules)
      {
        if (module.HostProp is not null)
        {
          yield return $"{module.StringPath}.{module.HostProp.Name} = this;";
        }
      }
    }

    private static IEnumerable<string> Generate_InjectModuleDependencies(ComponentInfo componentInfo)
    {
      string MissingDepWarning(ModuleInfo module, ModuleDependencyInfo dependency)
      {
        return $"// Missing dependency: {module} => {dependency}";
      }

      foreach (ModuleInfo module in componentInfo.AllModules)
      {
        foreach (ModuleDependencyInfo dependency in module.Dependencies)
        {
          if (!componentInfo.Modules.ContainsKey(dependency.Type))
          {
            yield return MissingDepWarning(module, dependency);
            continue;
          }

          if (!componentInfo.Modules[dependency.Type].ContainsKey(dependency.Name))
          {
            yield return MissingDepWarning(module, dependency);
            continue;
          }

          yield return $"{module.StringPath}.{dependency.Property.Name} = {componentInfo.Modules[dependency.Type][dependency.Name].StringPath};";
        }
      }
    }




  }


}
