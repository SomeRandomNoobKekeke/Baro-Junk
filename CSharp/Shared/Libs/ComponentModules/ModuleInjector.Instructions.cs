using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

using Barotrauma;
using Microsoft.Xna.Framework;
using System.IO;
using System.Text;

namespace BaroJunk
{

  public partial class ModuleInjector
  {
    public class CreateModuleInstruction
    {
      public PropertyInfo[] FullPath { get; }
      public Type ModuleType { get; }

      public CreateModuleInstruction(PropertyInfo[] fullPath, Type moduleType)
      {
        FullPath = fullPath;
        ModuleType = moduleType;
      }
    }

    public class InjectHostInstruction
    {
      public PropertyInfo[] FullPath { get; }

      public InjectHostInstruction(PropertyInfo[] fullPath)
      {
        FullPath = fullPath;
      }
    }

    public class InjectDependencyInstruction
    {
      public PropertyInfo[] TargetPath { get; }
      public PropertyInfo[] DependencyPath { get; }
      public PropertyInfo Property { get; }

      public InjectDependencyInstruction(PropertyInfo[] targetPath, PropertyInfo[] dependencyPath, PropertyInfo property)
      {
        TargetPath = targetPath;
        DependencyPath = dependencyPath;
        Property = property;
      }
    }
  }


}
