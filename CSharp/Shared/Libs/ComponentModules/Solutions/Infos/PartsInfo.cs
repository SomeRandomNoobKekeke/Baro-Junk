using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;
using BaroJunk;

namespace BaroJunk.ComponentModules
{
  public class PartsInfo
  {
    public Type Component { get; }
    public List<PartInfo> Parts { get; }

    public PartsInfo(Type componentType)
    {
      Component = componentType;
      Parts = CodeAnalyzer.GetParts(componentType).ToList();
    }

  }
}