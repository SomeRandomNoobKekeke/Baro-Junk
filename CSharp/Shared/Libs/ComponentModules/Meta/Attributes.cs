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

  public class ModuleCategoryAttribute : Attribute
  {
    public const string None = "None";
    public string Category { get; set; }
    public ModuleCategoryAttribute(string category)
    {
      Category = category;
    }
  }

  public class ModuleDependencyAttribute : Attribute
  {
    public string Name { get; set; }
    public string Category { get; set; }
    public ModuleDependencyAttribute(string name, string category = ModuleCategoryAttribute.None)
    {
      Name = name;
      Category = category;
    }
  }

}
