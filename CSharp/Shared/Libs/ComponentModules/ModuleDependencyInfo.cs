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
  public record ModuleDependencyInfo(PropertyInfo Property, Type Type, string Name)
  {
    public override string ToString() => $"{Type.Name} {Name}";
  }
}
