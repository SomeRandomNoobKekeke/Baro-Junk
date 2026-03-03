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
    public class A : IComponent
    {
      public string PropA1 { get; set; }
      public string PropA2 { get; set; }

      public B B { get; set; }
      public C C { get; set; }
    }

    public class B : IModuleContainer
    {
      public string PropB { get; set; }
      public C C { get; set; }
    }

    public class C : IModule
    {
      public string PropC { get; set; }
      public D D { get; set; }
    }

    public class D : IModule
    {
      public string PropD { get; set; }
    }

    public UListTest WalkTest()
    {
      IEnumerable<PropInfo> props = TypeWalker.WalkProps<A>();

      return new UListTest(
        props.Select(p => p.StringPath),
        new List<string>(){
          "PropA1",
          "PropA2",
          "B.PropB",
          "B.C.PropC",
          "B.C.D",
          "C.PropC",
          "C.D",
        }
      );

    }
  }
}