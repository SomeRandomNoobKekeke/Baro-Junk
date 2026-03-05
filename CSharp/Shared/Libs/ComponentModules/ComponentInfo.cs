using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace BaroJunk.ComponentModules
{
  public class ComponentInfo
  {
    public Type ComponentType { get; }

    public Dictionary<string, PropInfo> Props { get; private set; }


    private void Analyze()
    {
      Props = TypeWalker.WalkProps(ComponentType).ToDictionary(
        prop => prop.StringPath,
        prop => prop
      );
    }

    public ComponentInfo(Type componentType)
    {
      ComponentType = componentType;
      Analyze();
    }
  }
}
