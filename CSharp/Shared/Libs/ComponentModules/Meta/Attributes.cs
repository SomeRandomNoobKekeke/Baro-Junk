using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace BaroJunk
{

  public class GeneratedComponent : Attribute
  {
    public string FilePath { get; set; }

    public GeneratedComponent([CallerFilePathAttribute] string filePath = "")
    {
      FilePath = filePath;
    }
  }

  public class ModuleAttribute : Attribute
  {
    public Type Type { get; set; }
    public string Name { get; set; }
    public ModuleAttribute(Type type = null, string name = null)
    {
      Type = type;
      Name = name;
    }
  }

  public class ModuleContainerAttribute : Attribute
  {
    /// <summary>
    /// Suggested type for contained modules
    /// </summary>
    public Type Type { get; set; }

    public ModuleContainerAttribute(Type type = null)
    {
      Type = type;
    }
  }

  public class ModuleDependencyAttribute : Attribute
  {
    public string Name { get; set; }
    public Type Type { get; set; }

    public ModuleDependencyAttribute(Type type = null, string name = null)
    {
      Type = type;
      Name = name;
    }
  }

  public class ModuleAccessorDependencyAttribute : Attribute
  {
    public string Name { get; set; }
    public Type Type { get; set; }

    public ModuleAccessorDependencyAttribute(Type type, string name = null)
    {
      Type = type;
      Name = name;
    }
  }

  public class ComponentDependencyAttribute : Attribute { }



  public class ForwardedPropAttribute : Attribute
  {
    public PropAccess Access { get; set; }
    public string Name { get; set; }
    public Type Type { get; set; }

    public ForwardedPropAttribute(PropAccess access = PropAccess.None, string name = null, Type type = null)
    {
      Access = access;
      Type = type;
      Name = name;
    }
  }

  public class ForwardedMethodAttribute : Attribute
  {
    public string Name { get; set; }

    public ForwardedMethodAttribute(string name = null)
    {
      Name = name;
    }
  }

}
