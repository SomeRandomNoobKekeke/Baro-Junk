using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;


namespace BaroJunk
{

  public class PropAccessTest : UTestPack
  {

    public override void CreateTests()
    {
      ExampleConfigs.ConfigA config = new();

      Tests.Add(new UTest(PropAccess.GetEntry(config, "IntProp"), new ConfigEntry(config, "IntProp")));
      Tests.Add(new UTest(PropAccess.GetEntry(config, "NestedConfigB.IntProp"), new ConfigEntry(config.NestedConfigB, "IntProp")));

      Tests.Add(new UTest(PropAccess.GetProp(config, "IntProp"), 2));
      Tests.Add(new UTest(PropAccess.GetProp(config, "NestedConfigB.IntProp"), 4));
      Tests.Add(new UTest(PropAccess.GetProp(PropAccess.GetProp(config, "NestedConfigB"), "IntProp"), 4));

      Tests.Add(new UTest(PropAccess.GetProp(config, "bebebe"), null));
      Tests.Add(new UTest(PropAccess.GetProp(config, ""), null));
      Tests.Add(new UTest(PropAccess.GetProp(config, null), null));
      Tests.Add(new UTest(PropAccess.GetProp(config, ".."), null));
      Tests.Add(new UTest(PropAccess.GetProp(config, "NestedConfigB.qweqe"), null));
      Tests.Add(new UTest(PropAccess.GetProp(config, ".NestedConfigB.IntProp"), null));

      PropAccess.SetProp(config, "IntProp", 3);
      Tests.Add(new UTest(PropAccess.GetProp(config, "IntProp"), 3));

      PropAccess.SetProp(config, "NestedConfigB.IntProp", 5);
      Tests.Add(new UTest(PropAccess.GetProp(config, "NestedConfigB.IntProp"), 5));
    }

  }
}