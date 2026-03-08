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

namespace BaroJunk.ComponentModules
{
  public class InAttribute : Attribute { }
  public class OutAttribute : Attribute { }

  public class ThisIsAlsoAttribute : Attribute
  {
    public Type Type { get; }
    public ThisIsAlsoAttribute(Type type) => Type = type;
  }

  public class ThisIsAlsoAttribute<T> : Attribute
  {
    public Type Type => typeof(T);
  }
}
