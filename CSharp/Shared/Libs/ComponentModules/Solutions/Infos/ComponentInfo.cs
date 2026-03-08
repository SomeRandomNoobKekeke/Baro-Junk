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
  public class ComponentInfo
  {
    public Type Type { get; }
    public PartsInfo Parts { get; }

    public ComponentInfo(Type T)
    {
      Type = T;
      Parts = new PartsInfo(T);
    }

  }
}