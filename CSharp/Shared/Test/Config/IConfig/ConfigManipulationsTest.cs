using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;


namespace BaroJunk
{
  public class ConfigManipulationsTest : UTestPack
  {
    public class EqualsTest : ConfigManipulationsTest
    {
      public override void CreateTests()
      {
        ExampleConfigs.ConfigA husked = new();
        ExampleConfigs.ConfigA husked2 = new();

        husked.NestedConfigB.IntProp = 123;
        husked.NestedConfigB.NestedConfigC = null;

        Tests.Add(new UTest(!husked.EqualsTo(husked2)));
        husked2.NestedConfigB.IntProp = 123;
        husked2.NestedConfigB.NestedConfigC = null;
        Tests.Add(new UTest(husked.EqualsTo(husked2)));
      }
    }

    public class RestoreTest : ConfigManipulationsTest
    {
      public override void CreateTests()
      {
        ExampleConfigs.ConfigA husked = new();

        husked.NestedConfigB = null;

        Tests.Add(new UTest(husked.NestedConfigB is null));
        husked.Self().Restore();
        Tests.Add(new UTest(husked.NestedConfigB is not null));
        Tests.Add(new UTest(husked.NestedConfigB.NestedConfigC is not null));
      }

    }

    public class ClearTest : ConfigManipulationsTest
    {
      public override void CreateTests()
      {
        ExampleConfigs.ConfigA husked = new();

        husked.NestedConfigB.IntProp = 123;
        Tests.Add(new UTest(husked.NestedConfigB.IntProp, 123));
        husked.Self().Clear();
        Tests.Add(new UTest(husked.NestedConfigB.IntProp, 0));
      }
    }

    public class CopyTest : ConfigManipulationsTest
    {
      public override void CreateTests()
      {
        ExampleConfigs.ConfigA husked = new();

        husked.NestedConfigB.IntProp = 123;
        husked.NestedConfigB.NestedConfigC = null;

        ExampleConfigs.ConfigA husked2 = new();

        Tests.Add(new UTest(!husked.EqualsTo(husked2)));
        husked.Self().CopyTo(husked2);
        Tests.Add(new UTest(husked.EqualsTo(husked2)));

        Mod.Logger.Log(husked.ToText());
      }
    }
  }
}