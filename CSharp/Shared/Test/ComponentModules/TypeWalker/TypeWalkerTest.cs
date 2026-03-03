using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;
using BaroJunk.ComponentModules;

namespace BaroJunk
{
  public partial class TypeWalkerTest : ComponentModulesTest
  {
    public class A
    {
      public string Prop { get; set; }
    }

    public UTest SingleProp()
    {
      List<List<PropertyInfo>> props = new();

      TypeWalker.WalkProps(typeof(A), (path, prop) =>
      {
        props.Add(path.Append(prop).ToList());
      });

      return new UTest(
        String.Join('.', props[0].Select(pi => pi.Name)),
        "Prop"
      );

    }
  }
}